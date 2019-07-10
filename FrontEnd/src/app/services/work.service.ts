import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { AppConfig } from '../config/config';
import { Observable, BehaviorSubject } from 'rxjs';
import { Workday } from '../models/workday';
import { distinctUntilChanged, first, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WorkService {
  private pathAPI = this.config.get('PathAPI');

  private workdaysSubject = new BehaviorSubject<Workday[]>({} as Workday[]);
  public workdays = this.workdaysSubject.asObservable().pipe(distinctUntilChanged());
  private privateWorkdays: Workday[];

  constructor(private http: HttpClient, private config: AppConfig) { }

  createWorkday(startDate: Date, endDate: Date, comment: string) {
    return this.http.post<Workday>(`${this.pathAPI}/Workday/Create`, { startDate, endDate, comment })
    .subscribe(
        workday => {
        this.privateWorkdays.unshift(workday);
        this.workdaysSubject.next(this.privateWorkdays);
      }
    );
  }

  removeWorkday(workdayId: string) {
    return this.http.delete(`${this.pathAPI}/Workday/Remove/${workdayId}`)
    .subscribe(
      data => {
        this.privateWorkdays = this.privateWorkdays.filter(obj => obj.id !== workdayId);
        this.workdaysSubject.next(this.privateWorkdays);
      }
    );
  }

  getWorkdays(): Observable<Workday[]> {
    return this.http.get<Workday[]>(`${this.pathAPI}/Workday/Workdays`)
    .pipe(tap(
      data => {
        this.privateWorkdays = data;
        this.workdaysSubject.next(data);
      }
    ));
  }
}
