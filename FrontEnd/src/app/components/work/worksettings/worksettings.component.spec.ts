import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorksettingsComponent } from './worksettings.component';

describe('WorksettingsComponent', () => {
  let component: WorksettingsComponent;
  let fixture: ComponentFixture<WorksettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorksettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorksettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
