import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-worksettings',
  templateUrl: './worksettings.component.html',
  styleUrls: ['./worksettings.component.scss']
})
export class WorksettingsComponent implements OnInit {
  settingsForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.settingsForm = this.formBuilder.group({
      companyName: ['', Validators.required],
      jobTitle: ['', Validators.required],
      incomePerHour: ['', Validators.required],
    });
  }

  get f() { return this.settingsForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.settingsForm.invalid) {
        return;
    }
    console.log(this.settingsForm.value);
  }

}
