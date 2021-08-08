import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CarReservation } from '../app.models';

@Component({
  selector: 'next-returns-component',
  templateUrl: './next-returns.component.html'
})
export class NextReturnsComponent {
  public nextReturns: CarReservation[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CarReservation[]>(baseUrl + 'cars/notreturned').subscribe(result => {
      this.nextReturns = result;
    }, error => console.error(error));
  }
}

