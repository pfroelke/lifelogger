import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { tap, distinctUntilChanged, first } from 'rxjs/operators';

import { AppConfig } from '../config/config';
import { User } from '../models/user';
import { longStackSupport } from 'q';
import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { Jwt } from '../models/jwt';
import { identifierName } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private pathAPI = this.config.get('PathAPI');

  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());

  private isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient, private config: AppConfig) {
    this.isLoggedIn();
   }

  getUsers(): Observable<User[]> {
    const header = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<User[]>(`${this.pathAPI}/User`, { headers: header });
  }

  getById(id: string): Observable<User> {
    const header = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<User>(`${this.pathAPI}/User/${id}`, { headers: header});
  }

  register(user: User) {
    return this.http.post(`${this.pathAPI}/User/Register`, user);
  }

  login(userName: string, password: string) {
    return this.http.post<{jwt: Jwt, user: User}>(`${this.pathAPI}/User/Login`, { userName, password })
    .pipe(tap(res => {
      this.isAuthenticatedSubject.next(true);
      localStorage.setItem('id', res.user.id);
      localStorage.setItem('auth_token', res.jwt.token);
      localStorage.setItem('exp', res.jwt.expires as unknown as string);
      this.currentUserSubject.next(res.user);
      console.log(res.user);
    }));
  }

  logOut() {
    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);

    localStorage.removeItem('id');
    localStorage.removeItem('auth_token');
    localStorage.removeItem('exp');
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }

  isLoggedIn() {
    return new Promise(resolve => {
      this.http.get(`${this.pathAPI}/User/isLoggedIn`)
      .pipe(first())
      .subscribe(
          data => {
            const id = localStorage.getItem('id');
            this.getById(id).pipe(first())
            .subscribe(user => {
              this.currentUserSubject.next(user);
              this.isAuthenticatedSubject.next(true);
            });
            resolve(true);
          },
          error => {
            this.logOut();
            resolve(false);
          });
      }
    );
  }

  // update(user: User) {
  //     return this.http.put(`${this.pathAPI}/User/${user.id}`, user);
  // }

  // delete(id: number) {
  //     return this.http.delete(`${this.pathAPI}/User/${id}`);
  // }
}
