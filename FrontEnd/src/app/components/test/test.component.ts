import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { first } from 'rxjs/operators';

import { User } from 'src/app/models/user';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent implements OnInit {

  users: User[] = [];

  constructor( private userService: UserService ) { }

  ngOnInit() {
    this.loadAllValues();
  }

  private loadAllValues() {
    this.userService
      .getUsers()
      .pipe(first())
      .subscribe(users => {
        this.users = users;
      });
  }

}
