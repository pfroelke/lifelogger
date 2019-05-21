import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-workhistory',
  templateUrl: './workhistory.component.html',
  styleUrls: ['./workhistory.component.scss']
})
export class WorkhistoryComponent implements OnInit {
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
