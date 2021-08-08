import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car, CarReservation, CarReservationNew, Department } from '../app.models';

@Component({
  selector: 'car-fleet-component',
  templateUrl: './car-fleet.component.html'
})
export class CarFleetComponent {
  public departments: Department[];
  public cars: Car[];
  public reservations: CarReservation[];

  carSelected: number;
  departmentSelected: number;
  timeSelected: number = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Car[]>(baseUrl + 'cars').subscribe(result => {
      this.cars = result;
    }, error => console.error(error));

    http.get<Department[]>(baseUrl + 'departments').subscribe(result => {
      this.departments = result;
    }, error => console.error(error));
  }

  getReservations(carId: number) {
    this.carSelected = carId;

    this.http.get<CarReservation[]>('/cars/' + carId + '/reservations').subscribe(result => {
      this.reservations = result;
    }, error => console.error(error));
  }

  addReservations() {
    let reservation: CarReservationNew = {
      department_id: this.departmentSelected,
      time_minutes: this.timeSelected
    };

    this.http.post<CarReservation>('/cars/' + this.carSelected + '/reserve', reservation).subscribe(result => {
      this.reservations.push(result);
    }, error => console.error(error));
  }
}


