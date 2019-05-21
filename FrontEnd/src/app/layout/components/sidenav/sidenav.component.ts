import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  width = 26;
  opened = false;


  constructor() { }

  ngOnInit() {
  }

  open() {
    this.opened = true;
    this.width = 300;
  }

  close() {
    this.opened = false;
    this.width = 26;
  }

  toggle() {
    this.opened ? this.close() : this.open();
  }

}
