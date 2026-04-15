import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TourLog } from '../models/tourLog.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class TourLogService {
  private apiUrl = `${environment.apiUrl}/api/tourlogs`; // Pfad an dein Backend anpassen

  constructor(private http: HttpClient) {}

  createLog(log: TourLog): Observable<TourLog> {
    return this.http.post<TourLog>(this.apiUrl, log);
  }

  updateLog(log: TourLog): Observable<TourLog> {
    return this.http.put<TourLog>(`${this.apiUrl}/${log.id}`, log);
  }

  deleteLog(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}