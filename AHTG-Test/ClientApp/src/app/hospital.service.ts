import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hospital } from './hospital.model';

@Injectable({
  providedIn: 'root'
})
export class HospitalService {

  hospitalsUrl = "api/hospitals";

  constructor(private http: HttpClient) {
    
  }

  getHospitals(): Observable<Hospital[]> {
    return this.http.get<Hospital[]>(this.hospitalsUrl)
  }

  deleteHospital(id: number): Observable<unknown> {
    return this.http.delete(this.hospitalsUrl + "/" + id);
  }
}
