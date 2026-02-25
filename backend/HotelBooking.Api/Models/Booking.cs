namespace HotelBooking.Api.Models;

public record Booking(
    string Id,
    string HotelId,
    string RoomId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    int NumberOfGuests,
    decimal TotalPrice,
    BookingStatus Status,
    DateTime CreatedAt
);

public enum BookingStatus
{
    Confirmed,
    Cancelled,
    Completed
}

public record CreateBookingRequest(
    string HotelId,
    string RoomId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    int NumberOfGuests
);

public record BookingConfirmation(
    string BookingId,
    string HotelName,
    string RoomType,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    BookingStatus Status
);
