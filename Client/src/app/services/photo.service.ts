import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PhotoService {
  constructor(private http: HttpClient) {}

  upload(file: any, vehicleId: any): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    const req = new HttpRequest(
      'POST',
      `https://localhost:7138/api/vehicles/${vehicleId}/photos`,
      formData,
      {
        reportProgress: true,
        responseType: 'json',
      }
    );
    return this.http.request(req);
  }

  getPhotos(vehicleId: any) {
    return this.http.get(
      `https://localhost:7138/api/vehicles/${vehicleId}/photos`
    );
  }
}
