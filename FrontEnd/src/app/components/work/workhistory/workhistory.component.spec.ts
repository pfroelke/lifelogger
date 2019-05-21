import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkhistoryComponent } from './workhistory.component';

describe('WorkhistoryComponent', () => {
  let component: WorkhistoryComponent;
  let fixture: ComponentFixture<WorkhistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkhistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkhistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
