export interface Booking {
  id: number;
  resourceName: string;
  userName: string;
  startDateTime: string;
  endDateTime: string;
  status: string;
  createdAt: string;
  cancelledAt: string | null;
}
export interface AddBooking {
  resourceName: string;
  userName: string;
  startDateTime: string;
  endDateTime: string;
}