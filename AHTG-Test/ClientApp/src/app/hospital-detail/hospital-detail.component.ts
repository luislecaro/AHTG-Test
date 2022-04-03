import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../hospital.model';
import { Observable } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-hospital-detail',
  templateUrl: './hospital-detail.component.html',
  styleUrls: ['./hospital-detail.component.css']
})
export class HospitalDetailComponent implements OnInit {

  hospitalObservable: Observable<Hospital> | undefined;
  form: FormGroup;
  id: number = 0;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private hospitalService: HospitalService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group({
      name: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      numberOfEmployees: [null, [Validators.required]],
      addressLine1: [null, [Validators.required]],
      addressLine2: [null, [Validators.required]],
      addressCity: [null, [Validators.required]],
      addressState: [null, [Validators.required]],
      addressZip: [null, [Validators.required]]
    });
  }

  ngOnInit(): void {
    
    this.getHospital();
  }

  private getHospital(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    /*this.hospitalObservable = this.hospitalService.getHospital(id);*/
    this.hospitalService.getHospital(this.id).subscribe(hospital => {
      this.form.get('name')?.setValue(hospital.name);
      this.form.get('phoneNumber')?.setValue(hospital.phoneNumber);
      this.form.get('numberOfEmployees')?.setValue(hospital.numberOfEmployees);
      this.form.get('addressLine1')?.setValue(hospital.addressLine1);
      this.form.get('addressLine2')?.setValue(hospital.addressLine2);
      this.form.get('addressCity')?.setValue(hospital.addressCity);
      this.form.get('addressState')?.setValue(hospital.addressState);
      this.form.get('addressZip')?.setValue(hospital.addressZip);
    });
  }

  updateHospital() {    

    let hospital: Hospital = {
      id: this.id,
      name: this.form.get('name')?.value,
      phoneNumber: this.form.get('phoneNumber')?.value,
      numberOfEmployees: this.form.get('numberOfEmployees')?.value,
      addressLine1: this.form.get('addressLine1')?.value,
      addressLine2: this.form.get('addressLine2')?.value,
      addressCity: this.form.get('addressCity')?.value,
      addressState: this.form.get('addressState')?.value,
      addressZip: this.form.get('addressZip')?.value,
    };

    this.hospitalService.updateHospital(this.id, hospital).subscribe(
      () => { },
      (error) => { }
    );
  }

}
