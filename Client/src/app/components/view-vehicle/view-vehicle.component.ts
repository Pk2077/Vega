import { Component, ElementRef, NgZone, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { PhotoService } from '../../services/photo.service';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css'],
})
export class ViewVehicleComponent {
  vehicle!: any;
  vehicleId!: number;
  photos!: any[];

  currentFile!: any;
  progress = 0;
  message = '';
  selectedFiles: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public auth: AuthService,
    private vehicleService: VehicleService,
    private photoService: PhotoService
  ) {
    route.params.subscribe((p) => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId).subscribe(
      (v) => (this.vehicle = v),
      (err) => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return;
        }
      }
    );

    this.photoService
      .getPhotos(this.vehicleId)
      .subscribe((photos: any) => (this.photos = photos));
  }

  uploadPhoto() {
    this.progress = 0;
    this.currentFile = this.selectedFiles.item(0);
    this.photoService.upload(this.currentFile, this.vehicleId).subscribe(
      (event) => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round((100 * event.loaded) / event.total);
        else if (event instanceof HttpResponse) {
          this.message = event.body.message;
        }
      },

      (err) => {
        this.progress = 0;
        this.message = 'Could not upload';
        this.currentFile = undefined;
      }
    );
  }

  selectFile(event: any): void {
    this.selectedFiles = event.target.files;
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.delete(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['/vehicles']);
      });
    }
  }
}
