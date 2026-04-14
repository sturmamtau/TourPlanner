import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Tour } from '../models/tour.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TourService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllTours(): Observable<Tour[]>{
      return this.http.get<Tour[]>(`${this.apiUrl}/api/tours`);
  }

  createTour(tour: Tour): Observable<any> {
      return this.http.post<Tour>(`${this.apiUrl}/api/tours`, tour);
  }
}
