import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfig } from '../config/config';

import { User } from '../models/user';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // tslint:disable-next-line:variable-name
  private pathAPI = this.config.get('PathAPI');

  constructor(private http: HttpClient, private config: AppConfig) {
    // super(helper);
   }

//   getUsers(): Observable<string[]> {
//         const header = new HttpHeaders({ 'Content-Type': 'application/json' });
//         return this.http.get<string[]>(`${this.pathAPI}/values`, { headers: header});
//     }
    getUsers(): Observable<User[]> {
        const header = new HttpHeaders({ 'Content-Type': 'application/json' });
        return this.http.get<User[]>(`${this.pathAPI}/User`, { headers: header});
    }

    // getById(id: number) {
    //     return this.http.get(`${this.pathAPI}/User/${id}`);
    // }

    register(user: User) {
        return this.http.post(`${this.pathAPI}/User/Register`, user);
    }

    // update(user: User) {
    //     return this.http.put(`${this.pathAPI}/User/${user.id}`, user);
    // }

    // delete(id: number) {
    //     return this.http.delete(`${this.pathAPI}/User/${id}`);
    // }
}
