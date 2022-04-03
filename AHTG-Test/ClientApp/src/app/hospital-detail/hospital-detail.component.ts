import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../hospital.model';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { State, States } from '../states';

@Component({
  selector: 'app-hospital-detail',
  templateUrl: './hospital-detail.component.html',
  styleUrls: ['./hospital-detail.component.css']
})
export class HospitalDetailComponent implements OnInit {

  hospitalObservable: Observable<Hospital> | undefined;
  form: FormGroup;
  id: number = 0;
  isBusy = false;
  states: State[] = [];

  private addMode = false;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private hospitalService: HospitalService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      name: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      numberOfEmployees: [null, [Validators.required]],
      addressLine1: [null, [Validators.required]],
      addressLine2: [null, [Validators.required]],
      addressCity: [null, [Validators.required]],
      addressState: [null, [Validators.required]],
      addressZip: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(5), Validators.pattern('^[0-9]{5}')]]
    });
  }

  ngOnInit(): void {

    let stateList = States.List;
    stateList.unshift({ code: null, description: '-- Select a state --' });
    this.states = stateList;

    this.id = Number(this.route.snapshot.paramMap.get('id'));

    if (this.id === 0) {
      this.addMode = true;
    }
    else {
      this.addMode = false;
      this.getHospital();
    }
  }

  private getHospital(): void {

    this.isBusy = true;

    this.hospitalService.getHospital(this.id).subscribe(hospital => {
      this.form.get('name')?.setValue(hospital.name);
      this.form.get('phoneNumber')?.setValue(hospital.phoneNumber);
      this.form.get('numberOfEmployees')?.setValue(hospital.numberOfEmployees);
      this.form.get('addressLine1')?.setValue(hospital.addressLine1);
      this.form.get('addressLine2')?.setValue(hospital.addressLine2);
      this.form.get('addressCity')?.setValue(hospital.addressCity);
      this.form.get('addressState')?.setValue(hospital.addressState);
      this.form.get('addressZip')?.setValue(hospital.addressZip);

      this.isBusy = false;
    });
  }

  submitForm(): void {

    const hospital: Hospital = {
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

    if (this.addMode) {
      this.addHospital(hospital);
    }
    else {
      this.updateHospital(hospital);
    }
  }

  private addHospital(hospital: Hospital): void {

    this.isBusy = true;

    this.hospitalService.addHospital(hospital).subscribe(
      () => {
        this.isBusy = false;
        this.snackBar.open('Added successfully', 'OK', { duration: 3000, panelClass: ['green-snackbar'] });
      },
      (error) => {
        this.isBusy = false;
        this.snackBar.open('An error occurred', 'OK', { duration: 3000, panelClass: ['red-snackbar'] });
      }
    );
  }

  private updateHospital(hospital: Hospital): void {

    this.isBusy = true;

    this.hospitalService.updateHospital(this.id, hospital).subscribe(
      () => {
        this.isBusy = false;
        this.snackBar.open('Updated successfully', 'OK', { duration: 3000, panelClass: ['green-snackbar'] });
      },
      (error) => {
        this.isBusy = false;
        this.snackBar.open('An error occurred', 'OK', { duration: 3000, panelClass: ['red-snackbar'] });
      }
    );
  }

}
