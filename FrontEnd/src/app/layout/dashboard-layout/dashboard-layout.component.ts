import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: ['./dashboard-layout.component.scss']
})
export class DashboardLayoutComponent implements OnInit {
  sidenavWidth = 50;

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  logout() {
    this.userService.logOut();
  }

  sidenavOpen() {
    this.sidenavWidth = 300;
  }

  sidenavClose() {
    this.sidenavWidth = 50;
  }

}
