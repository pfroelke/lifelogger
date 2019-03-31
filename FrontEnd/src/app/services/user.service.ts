import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/operators';

import { AppConfig } from '../config/config';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // tslint:disable-next-line:variable-name
  private pathAPI = this.config.get('PathAPI');
  loggedIn: boolean;

  constructor(private http: HttpClient, private config: AppConfig) {
    // super(helper);
   }

    getUsers(): Observable<User[]> {
        const header = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.get<User[]>(`${this.pathAPI}/User`, { headers: header });
    }
    // getById(id: number) {
    //     return this.http.get(`${this.pathAPI}/User/${id}`);
    // }

    register(user: User) {
        return this.http.post(`${this.pathAPI}/User/Register`, user);
    }

    login(user: User) {
        return this.http.post<{auth_token: string}>(`${this.pathAPI}/User/Login`, user)
        .pipe(tap(res => {
          localStorage.setItem('auth_token', res.auth_token);
        }));
    }

    isLoggedIn(): boolean {
      return !!localStorage.getItem('auth_token');
    }
    // update(user: User) {
    //     return this.http.put(`${this.pathAPI}/User/${user.id}`, user);
    // }

    // delete(id: number) {
    //     return this.http.delete(`${this.pathAPI}/User/${id}`);
    // }
}
