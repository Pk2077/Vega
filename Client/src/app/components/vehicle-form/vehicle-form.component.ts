import { IndividualConfig, ToastrService } from 'ngx-toastr';
import { VehicleService } from '../../services/vehicle.service';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { KeyValuePairs, SaveVehicle, Vehicle } from '../../model/vehicle';
import * as _ from 'underscore';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent {
  vehicle: SaveVehicle = {
    features: [],
    contact: {
      name: '',
      phone: '',
      email: '',
    },
    id: 0,
    modelId: 0,
    makeId: 0,
    isRegistered: false,
  };
  makes: any[] = [];
  models!: KeyValuePairs[];
  features: any[] = [];

  constructor(
    private vehicleService: VehicleService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.route.params.subscribe((p) => {
      const id = p['id'];
      if (id) {
        this.vehicle.id = +id;
      } else {
        this.vehicle.id = 0;
      }
    });
  }

  ngOnInit() {
    if (this.vehicle.id !== 0) {
      this.vehicleService.getVehicle(this.vehicle.id).subscribe((v: any) => {
        this.setVehicle(v);
      });
    }

    const makes$ = this.vehicleService.getMakes();
    const features$ = this.vehicleService.getFeatures();

    forkJoin([makes$, features$]).subscribe((data: any) => {
      this.makes = data[0];
      this.features = data[1];

      if (this.vehicle.makeId) {
        this.populateModels();
      }
    });
  }

  private populateModels() {
    const selectedMake = this.makes.find(
      (m: KeyValuePairs) => m.id == this.vehicle.makeId
    );
    this.models = selectedMake.models;
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.contact = v.contact;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.features = v.features.map((feature) => feature.id);
  }

  onMakeChange() {
    this.populateModels();
    this.vehicle.modelId = null;
  }

  onFeaturesToggle(featureId: any, $event: any) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      const index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle).subscribe((x) => {
        const config: Partial<IndividualConfig> = {
          closeButton: true,
          timeOut: 3000, // Custom timeOut
          progressBar: true, // Show progress bar
          progressAnimation: 'increasing', // Animation type
          positionClass: 'toast-top-right',
          titleClass: 'toast-title',
          messageClass: 'toast-message',
          // Additional customizations can be added here
        };
        this.toastr.success('Vehicle updated successfully', 'Success', config);
        this.router.navigate(['/vehicles']);
      });
    } else {
      this.vehicleService.create(this.vehicle).subscribe((success) => {
        const config: Partial<IndividualConfig> = {
          closeButton: true,
          timeOut: 3000, // Custom timeOut
          progressBar: true, // Show progress bar
          progressAnimation: 'increasing', // Animation type
          positionClass: 'toast-top-right',
          titleClass: 'toast-title',
          messageClass: 'toast-message',
          // Additional customizations can be added here
        };
        this.toastr.success('Vehicle created successfully', 'Success', config);
        this.router.navigate(['/vehicles']);
      });
    }
  }

  delete() {
    if (confirm('Are you sure')) {
      this.vehicleService
        .delete(this.vehicle.id)
        .subscribe((x) => this.router.navigate(['vehicle/new']));
    }
  }
}
