import { ChartModule } from 'angular2-chartjs';
import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { AppErrorHandler } from './app.error-handler';
import * as Raven from 'raven-js';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { PhotoService } from './services/photo.service';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AuthModule } from '@auth0/auth0-angular';
import { AuthButtonComponent } from './components/auth-button/auth-button.component';

Raven.config(
  'https://b7dc3edc402a0aee594420b4812b2ab0@o4506761753657344.ingest.sentry.io/4506761757786112'
).install();

@NgModule({
  declarations: [
    AppComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent,
    NavMenuComponent,
    AuthButtonComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AuthModule.forRoot({
      domain: 'dev-58m8zk08sc8wh047.us.auth0.com',
      clientId: 'VavHEYQXIr2B9cqOq4kBSWGUuoOX3Zmv',
      authorizationParams: {
        redirect_uri: window.location.origin,
      },
    }),
    ToastrModule.forRoot(),
  ],
  providers: [
    VehicleService,
    PhotoService,
    ToastrService,
    { provide: ErrorHandler, useClass: AppErrorHandler },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
