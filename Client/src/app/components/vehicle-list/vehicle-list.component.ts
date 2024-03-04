import { Component } from '@angular/core';
import { KeyValuePairs, Vehicle } from '../../model/vehicle';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css',
})
export class VehicleListComponent {
  queryResult: any = {};
  makes!: KeyValuePairs[];
  query: any = {
    pageSize: 3,
  };
  columns = [
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    {},
  ];

  constructor(
    private vehicleService: VehicleService,
    public auth: AuthService
  ) {}

  ngOnInit() {
    this.populateVehicles();

    this.vehicleService.getMakes().subscribe((m: any) => (this.makes = m));
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: 3,
    };
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query).subscribe((result: any) => {
      this.queryResult = result;
    });
  }

  sortBy(columnName: any) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = false;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }
  onPageChange(page: any) {
    this.query.page = page;
    this.populateVehicles();
  }
}
