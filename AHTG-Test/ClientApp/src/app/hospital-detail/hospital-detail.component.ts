import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../hospital.model';

@Component({
  selector: 'app-hospital-detail',
  templateUrl: './hospital-detail.component.html',
  styleUrls: ['./hospital-detail.component.css']
})
export class HospitalDetailComponent implements OnInit {

  hospital: Hospital | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private hospitalService: HospitalService
  ) { }

  ngOnInit(): void {
    this.getHospital();
  }

  private getHospital(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.hospitalService.getHospital(id)
      .subscribe(hospital => this.hospital = hospital);
  }

}
