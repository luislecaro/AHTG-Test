import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Hospital } from '../hospital.model';
import { HospitalService } from '../hospital.service';

@Component({
  selector: 'app-view-hospitals',
  templateUrl: './view-hospitals.component.html',
  styleUrls: ['./view-hospitals.component.css']
})
export class ViewHospitalsComponent implements OnInit {

  constructor(private hospitalService: HospitalService) { }

  dataSource: MatTableDataSource<Hospital> = new MatTableDataSource();
  hospitals: Hospital[] = [];

  displayedColumns: string[] = ['name', 'number-of-employees', 'state', 'actions'];

  ngOnInit(): void {
    this.getHospitals();
  }

  delete(hospital: Hospital) {
    this.hospitalService.deleteHospital(hospital.id).subscribe(
      () => {
        this.getHospitals();
      },
      (error) => {        
      }
    );    
  }

  getHospitals(): void {
    this.hospitalService.getHospitals().subscribe(hospitalsList => {
      this.hospitals = hospitalsList;
      this.dataSource.data = this.hospitals;
    });
  }

}
