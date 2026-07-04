import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BookingService } from '../../services/booking';
import { Booking } from '../../models/booking.model';

@Component({
  selector: 'app-bookings',
  imports: [CommonModule, FormsModule],
  templateUrl: './bookings.html',
  styleUrl: './bookings.css',
})
export class Bookings implements OnInit {
  bookings: Booking[] = [];

  resourceName: string = '';
  userName: string = '';
  startDateTime: string = '';
  endDateTime: string = '';

  searchResourceName: string = '';

  constructor(
    private bookingService: BookingService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.bookingService.getBookings().subscribe({
      next: (data: Booking[]) => {
        this.bookings = data;
        this.changeDetectorRef.detectChanges();
      },
      error: (err: any) => {
        console.error(err);
      },
    });
  }

  searchBookings(): void {
    if (!this.searchResourceName.trim()) {
      this.loadBookings();
      return;
    }

    this.bookingService.getBookingsByResource(this.searchResourceName).subscribe({
      next: (data: Booking[]) => {
        this.bookings = data;
        this.changeDetectorRef.detectChanges();
      },
      error: (err: any) => {
        console.error(err);
        alert('Failed to search bookings.');
      },
    });
  }

  addBooking(): void {
    if (
      !this.resourceName ||
      !this.userName ||
      !this.startDateTime ||
      !this.endDateTime
    ) {
      alert('Please fill all fields.');
      return;
    }

    const newBooking = {
      resourceName: this.resourceName,
      userName: this.userName,
      startDateTime: this.startDateTime,
      endDateTime: this.endDateTime,
    };

    this.bookingService.addBooking(newBooking).subscribe({
      next: () => {
        this.resourceName = '';
        this.userName = '';
        this.startDateTime = '';
        this.endDateTime = '';
        this.loadBookings();
      },
      error: (err: any) => {
        console.error(err);
        alert(err.error || 'Failed to add booking.');
      },
    });
  }

  cancelBooking(id: number): void {
    if (!confirm('Are you sure you want to cancel this booking?')) {
      return;
    }

    this.bookingService.cancelBooking(id).subscribe({
      next: () => {
        this.loadBookings();
      },
      error: (err: any) => {
        console.error(err);
        alert('Failed to cancel booking.');
      },
    });
  }
}