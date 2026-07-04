import { Component } from '@angular/core';
import { Bookings } from './components/bookings/bookings';

@Component({
  selector: 'app-root',
  imports: [Bookings],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {}
