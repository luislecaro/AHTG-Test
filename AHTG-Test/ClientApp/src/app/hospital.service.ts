import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hospital } from './hospital.model';

@Injectable({
  providedIn: 'root'
})
export class HospitalService {

  hospitalsUrl = "/api/hospitals";

  constructor(private http: HttpClient) {
    
  }

  getHospitals(): Observable<Hospital[]> {
    return this.http.get<Hospital[]>(this.hospitalsUrl)
  }

  getHospital(id: number): Observable<Hospital> {
    return this.http.get<Hospital>(this.hospitalsUrl + "/" + id);
  }

  addHospital(hospital: Hospital): Observable<Hospital> {
    return this.http.post<Hospital>(this.hospitalsUrl, hospital);
  }

  updateHospital(id: number, hospital: Hospital) {
    return this.http.put<Hospital>(this.hospitalsUrl + "/" + id, hospital);
  }

  deleteHospital(id: number): Observable<unknown> {
    return this.http.delete(this.hospitalsUrl + "/" + id);
  }
}
