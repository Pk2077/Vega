import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { AuthGuard } from '@auth0/auth0-angular';

const routes: Routes = [
  {
    path: '',
    component: VehicleListComponent,
  },
  {
    path: 'vehicles/new',
    component: VehicleFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'vehicles/edit/:id',
    component: VehicleFormComponent,
    canActivate: [AuthGuard],
  },
  { path: 'vehicles/:id', component: ViewVehicleComponent },
  { path: 'vehicles', component: VehicleListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
