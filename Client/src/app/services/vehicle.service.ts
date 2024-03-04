import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SaveVehicle } from '../model/vehicle';
import { Observable, map, switchMap } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private readonly url = 'https://localhost:7138/api/vehicles/';
  constructor(private http: HttpClient, private authService: AuthService) {}

  getMakes() {
    return this.http.get('https://localhost:7138/api/makes');
  }
  getFeatures() {
    return this.http.get('https://localhost:7138/api/features');
  }

  getVehicle(id: number) {
    return this.http.get(this.url + id);
  }

  getVehicles(filter: any) {
    return this.http.get(this.url + '?' + this.toQueryString(filter));
  }

  toQueryString(obj: any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(
          encodeURIComponent(property) + '=' + encodeURIComponent(value)
        );
    }
    return parts.join('&');
  }

  create(vehicle: any) {
    return this.requestWithAuthorization('post', this.url, vehicle);
  }

  update(vehicle: any) {
    return this.requestWithAuthorization(
      'put',
      `${this.url}${vehicle.id}`,
      vehicle
    );
  }

  delete(id: number) {
    return this.requestWithAuthorization('delete', `${this.url}${id}`);
  }

  private getHeaders(): Observable<HttpHeaders> {
    return this.authService.getAccessTokenSilently().pipe(
      map((token) => {
        return new HttpHeaders().set('Authorization', `Bearer ${token}`);
      })
    );
  }

  private requestWithAuthorization(
    method: string,
    url: string,
    body?: any
  ): Observable<any> {
    return this.getHeaders().pipe(
      switchMap((headers) => {
        switch (method) {
          case 'post':
            return this.http.post(url, body, { headers });
          case 'put':
            return this.http.put(url, body, { headers });
          case 'delete':
            return this.http.delete(url, { headers });
          default:
            throw new Error('Invalid HTTP method');
        }
      })
    );
  }
}
