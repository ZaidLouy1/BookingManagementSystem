import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking, AddBooking } from '../models/booking.model';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private apiUrl = 'https://localhost:44340/api/Bookings';

  constructor(private http: HttpClient) {}

  getBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/GetAll`);
  }

  getBookingsByResource(resourceName: string): Observable<Booking[]> {
    return this.http.get<Booking[]>(
      `${this.apiUrl}/GetByCriteria?ResourceName=${encodeURIComponent(resourceName)}`
    );
  }

  addBooking(newBooking: AddBooking): Observable<number> {
    return this.http.post<number>(this.apiUrl, newBooking);
  }

  cancelBooking(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
