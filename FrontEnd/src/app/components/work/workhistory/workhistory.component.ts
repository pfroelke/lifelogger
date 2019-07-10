import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { WorkService } from 'src/app/services/work.service';
import { Workday } from 'src/app/models/workday';

@Component({
  selector: 'app-workhistory',
  templateUrl: './workhistory.component.html',
  styleUrls: ['./workhistory.component.scss']
})
export class WorkhistoryComponent implements OnInit {
  workdays: Workday[] = [];

  constructor( private workService: WorkService ) {
    workService.workdays.subscribe(workdays => this.updateWorkdays(workdays));
   }

  ngOnInit() {
    this.loadAllValues();
  }

  private loadAllValues() {
    this.workService
      .getWorkdays()
      .pipe(first())
      .subscribe(workdays => {
        this.workdays = workdays;
      });
  }

  private updateWorkdays(workdays: Workday[]) {
    this.workdays = workdays;
  }

  private remove(workdayId: string) {
    this.workService.removeWorkday(workdayId);
  }
}
