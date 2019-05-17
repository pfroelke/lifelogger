import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import * as crypto from 'crypto-js';

import { AlertService } from '../../services/alert.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  invalidLogin = false;
  submitted = false;
  response: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }
        console.log(this.loginForm.value);

        const { userName, password } = this.loginForm.value;

        this.userService.login(userName, crypto.SHA256(password).toString())
            .pipe(first())
            .subscribe(
                data => {
                    // this.alertService.success('Registration successful', true);
                    console.log('login successful');
                    this.invalidLogin = false;
                    this.router.navigate(['/dashboard']);
                },
                error => {
                    // this.alertService.error(error.error);
                    console.log('login failed');
                    console.log(error.error);
                    this.invalidLogin = true;
                });
    }

}
