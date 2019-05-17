import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import * as crypto from 'crypto-js';

import { AlertService } from '../../services/alert.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    invalidRegister = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserService,
        private alertService: AlertService
    ) {
        // redirect to home if already logged in
        // if (this.authenticationService.currentUserValue) {
        //     this.router.navigate(['/']);
        // }
    }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
          userName: ['', Validators.required],
          firstName: ['', Validators.required],
          lastName: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          password: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        console.log(this.registerForm.value);

        const { userName, firstName, lastName, email, password } = this.registerForm.value;
        const user = {
          userName,
          firstName,
          lastName,
          email,
          password : crypto.SHA256(password).toString()
        };

        this.userService.register(user as any)
            .pipe(first())
            .subscribe(
                data => {
                    // this.alertService.success('Registration successful', true);
                    this.invalidRegister = false;
                    this.router.navigate(['/']);
                },
                error => {
                    // this.alertService.error(error.error);
                    console.log(error.error);
                    this.invalidRegister = true;
                });
    }
}
