import { Component, OnInit, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {
  @Input() opened: boolean;


  constructor() {
   }

  ngOnInit() {
  }

  open() {
    this.opened = true;
  }

  close() {
    this.opened = false;
  }

  toggle() {
    this.opened ? this.close() : this.open();
  }

}
