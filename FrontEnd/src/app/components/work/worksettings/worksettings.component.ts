import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-worksettings',
  templateUrl: './worksettings.component.html',
  styleUrls: ['./worksettings.component.scss']
})
export class WorksettingsComponent implements OnInit {
  settingsForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.settingsForm = this.formBuilder.group({
      companyName: ['', Validators.required],
      jobTitle: ['', Validators.required],
      incomePerHour: ['', Validators.required],
    });
    this.userService.currentUser.subscribe(user => this.updateForm(user));
  }

  get f() { return this.settingsForm.controls; }

  onSubmit() {
    if (this.settingsForm.disabled) {
      this.settingsForm.enable();
      return;
    }
    // stop here if form is invalid
    if (this.settingsForm.invalid) {
      return;
    }

    const { companyName, jobTitle, incomePerHour } = this.settingsForm.value;
    this.userService.updateWorkConfig(companyName, jobTitle, incomePerHour);
  }

  updateForm(user: User) {
    this.settingsForm.controls.companyName.setValue(user.companyName);
    this.settingsForm.controls.jobTitle.setValue(user.jobTitle);
    this.settingsForm.controls.incomePerHour.setValue(user.incomePerHour);
    this.settingsForm.disable();
  }
}
