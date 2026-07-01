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

  createTour(tourDto: any): Observable<Tour> {
      return this.http.post<Tour>(`${this.apiUrl}/api/tours`, tourDto);
  }

  uploadTourImage(tourId: number, image: Blob): Observable<Tour> {
    const formData = new FormData();
    console.log(`Uploading image for tour ${tourId}`);
    formData.append('file', image, `tour_${tourId}.png`);
    return this.http.post<Tour>(`${this.apiUrl}/api/tours/${tourId}/image`, formData);
  }

  deleteTour(id: number): Observable<void> {
      console.log(`${this.apiUrl}/api/tours/${id}`)
      return this.http.delete<void>(`${this.apiUrl}/api/tours/${id}`)
  }

  updateTour(id: number, tourDto: any): Observable<Tour>{
    console.log(`Update Tour: ${this.apiUrl}/api/tours/${id}`);
    return this.http.put<Tour>(`${this.apiUrl}/api/tours/${id}`, tourDto);
  }
}
