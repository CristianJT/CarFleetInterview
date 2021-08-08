export interface Car {
  id: number;
  code: string;
  make: string;
  model: string;
  version: string;
  description: string;
  year: number;
  value_hour: number;
}

export interface CarReservation {
  reservation_id: number;
  car_id: number;
  car_description: string;
  reservation_date: string;
  return_date: string;
  reservation_value: number;
  department: string;
}

export interface Department {
  id: number;
  name: string;
}

export interface CarReservationNew {
  department_id: number;
  time_minutes: number;
}
