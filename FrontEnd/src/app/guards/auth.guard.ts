import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { UserService } from '../services/user.service';
import { JWTHelperService } from '../helpers/jwthelper.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  isAuthenticated = false;

  constructor(private userService: UserService, private router: Router, private jwthelper: JWTHelperService) {
    userService.isAuthenticated.subscribe(b => this.isAuthenticated = b);
  }

  canActivate() {
    if (!this.isAuthenticated) {
      this.userService.isLoggedIn().then((data) => {
        if (!data) {
          this.router.navigate(['/']);
        }
        return data;
      });
    }
    return true;
  }
}
