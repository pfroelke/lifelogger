import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workday',
  templateUrl: './workday.component.html',
  styleUrls: ['./workday.component.scss']
})
export class WorkdayComponent implements OnInit {
  workdayForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.workdayForm = this.formBuilder.group({
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
      hours: ['', Validators.required],
      comment: ['', Validators.required]
    });
  }

  get f() { return this.workdayForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.workdayForm.invalid) {
        return;
    }
    console.log(this.workdayForm.value);
  }

}
