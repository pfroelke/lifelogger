import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkService } from 'src/app/services/work.service';
import Utils from 'src/app/helpers/utils';

@Component({
  selector: 'app-workday',
  templateUrl: './workday.component.html',
  styleUrls: ['./workday.component.scss']
})
export class WorkdayComponent implements OnInit {
  workdayForm: FormGroup;
  start = new Date();
  end = Utils.addHoursToDate(this.start, 8);
  hours = new Date(this.end.getTime() - this.start.getTime());

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.workdayForm = this.formBuilder.group({
      startTime: [Utils.dateToString(this.start), Validators.required],
      endTime: [Utils.dateToString(this.end), Validators.required],
      hours: [`${this.hours.getHours() - 1}`, Validators.required],
      comment: ['', Validators.required]
    });
    this.onChanges();
  }

  onChanges() {
    const f = this.workdayForm.controls;

    f.startTime.valueChanges.subscribe(startString => {
      this.start.setTime(Utils.stringToTime(startString));
    });

    f.endTime.valueChanges.subscribe(endString => {
      this.end.setTime(Utils.stringToTime(endString));
      this.hours.setTime(this.end.getTime() - this.start.getTime());
      f.hours.setValue(Utils.timeToHours(this.hours.getTime()), {emitEvent: false});
    });

    f.hours.valueChanges.subscribe(hours => {
      this.hours.setTime(0);
      this.hours.setHours(Number(hours) + 1);
      this.end.setTime(this.start.getTime() + this.hours.getTime());
      f.endTime.setValue(Utils.dateToString(this.end), {emitEvent: false});
    });
  }

  get f() { return this.workdayForm.controls; }

  onSubmit() {
    // stop here if form is invalid
    if (this.workdayForm.invalid) {
        return;
    }

    const { comment } = this.workdayForm.value;
    this.workService.createWorkday(this.start, this.end, comment);
  }

}
