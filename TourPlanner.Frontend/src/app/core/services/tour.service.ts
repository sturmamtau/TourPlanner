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

  deleteTour(id: number): Observable<void> {
      console.log(`${this.apiUrl}/api/tours/${id}`)
      return this.http.delete<void>(`${this.apiUrl}/api/tours/${id}`)
  }

  updateTour(tour: Tour): Observable<Tour>{
    console.log(`${this.apiUrl}/api/tours/${tour}`)
    return this.http.put<Tour>(`${this.apiUrl}/api/tours/${tour.id}`, tour)
  }
}
