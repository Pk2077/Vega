import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVehicleComponent } from './view-vehicle.component';

describe('ViewVehicleComponent', () => {
  let component: ViewVehicleComponent;
  let fixture: ComponentFixture<ViewVehicleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewVehicleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
