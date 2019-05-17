import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  appTitle = 'lifelogger';
  currentUser: User;
  isAuthenticated = false;

  constructor(private userService: UserService) {
    userService.currentUser.subscribe(user => this.updateUser(user));
    userService.isAuthenticated.subscribe(b => this.isAuthenticated = b);
    // this.isAuthenticated = userService.isAuthenticated;
  }

  ngOnInit() {
  }

  updateUser(user: User) {
    this.currentUser = user;
    console.log(this.currentUser);
  }

}
