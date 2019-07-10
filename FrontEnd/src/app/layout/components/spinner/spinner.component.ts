import { Component, OnInit, HostBinding, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {
  @Input()
  size: number;
  @Input()
  value: number;
  @Input()
  width: number;
  @Input()
  title: string;
  wrapperSize: number;
  wrapperMiddle: number;
  spinnerRadius: number;

  @HostBinding('style')
  get valueAsStyle(): any {
    this.spinnerRadius = this.size / 2;
    this.wrapperSize = this.size + this.width;
    this.wrapperMiddle = this.wrapperSize / 2;
    return this.sanitizer.bypassSecurityTrustStyle(`
     --wrapperSize: ${this.wrapperSize + 'px'};
     --wrapperMiddle: ${this.wrapperMiddle + 'px'};
     --size: ${this.size + 'px'};
     --width: ${this.width + 'px'};
     --value: ${this.value};
     --strokeOffset: ${Math.PI * this.size * (1 - this.value) + 'px'};
     --strokeLength: ${Math.PI * this.size + 'px'}
     `);
  }

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
  }
}
