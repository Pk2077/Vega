export interface Vehicle {
  id: number;
  model: KeyValuePairs;
  make: KeyValuePairs;
  isRegistered: boolean;
  features: KeyValuePairs[];
  contact: Contact;
}
export interface SaveVehicle {
  id: number;
  modelId: number | null;
  makeId: number;
  isRegistered: boolean;
  features: number[];
  contact: Contact;
}

export interface KeyValuePairs {
  id: number;
  name: string;
}

export interface Contact {
  name: string;
  phone: string;
  email: string;
}
// const sources = [
//   this.vehicleService.getMakes(),
//   this.vehicleService.getFeatures(),
// ];

// if (this.vehicle.id) {
//   sources.push(this.vehicleService.getVehicle(this.vehicle.id));
// }

// forkJoin(sources).subscribe({
//   next: (data: any[]) => {
//     if (this.vehicle.id) {
//       this.setVehicle(data[2]);
//       this.populateModels();
//     }
//     this.makes = data[0];
//     this.features = data[1];
//   },
//   error: (err: any) => {
//     if (err.status == 404) {
//       this.router.navigate(['/vehicle/new']);
//     }
//   },
// });
