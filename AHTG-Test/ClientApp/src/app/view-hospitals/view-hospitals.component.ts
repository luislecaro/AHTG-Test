import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Hospital } from '../hospital.model';
import { HospitalService } from '../hospital.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-view-hospitals',
  templateUrl: './view-hospitals.component.html',
  styleUrls: ['./view-hospitals.component.css']
})
export class ViewHospitalsComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;  

  constructor(
    private hospitalService: HospitalService,
    public dialog: MatDialog) {
  }

  isBusy = true;
  dataSource: MatTableDataSource<Hospital> = new MatTableDataSource();
  hospitals: Hospital[] = [];

  displayedColumns: string[] = ['name', 'number-of-employees', 'state', 'actions'];

  ngOnInit(): void {

    this.getHospitals();

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator as MatPaginator;
  }

  onDeleteClick(hospital: Hospital) {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {},
    });

    dialogRef.afterClosed().subscribe(result => {
      var shouldDelete = (result === "true");

      if (shouldDelete) {
        this.delete(hospital);
      }
    });
  }

  private getHospitals(): void {
    this.isBusy = true;

    this.hospitalService.getHospitals().subscribe(hospitalsList => {
      this.hospitals = hospitalsList;
      this.dataSource.data = this.hospitals;

      this.isBusy = false;
    });
  }

  private delete(hospital: Hospital): void {
    this.isBusy = true;

    this.hospitalService.deleteHospital(hospital.id).subscribe(
      () => {
        this.isBusy = false;
        this.getHospitals();
      },
      (error) => {
        this.isBusy = false;
      }
    );
  }

}
