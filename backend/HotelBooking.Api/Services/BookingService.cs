using HotelBooking.Api.Models;

namespace HotelBooking.Api.Services;

public class BookingService
{
    private readonly List<Booking> _bookings = [];
    private readonly HotelService _hotelService;

    public BookingService(HotelService hotelService) => _hotelService = hotelService;

    public BookingConfirmation? Create(CreateBookingRequest request)
    {
        var hotel = _hotelService.GetById(request.HotelId);
        if (hotel == null) return null;

        var room = hotel.Rooms.FirstOrDefault(r => r.Id == request.RoomId && r.IsAvailable);
        if (room == null) return null;

        var nights = (request.CheckOut - request.CheckIn).Days;
        if (nights <= 0) return null;

        var booking = new Booking(
            Id: $"BK-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            HotelId: request.HotelId,
            RoomId: request.RoomId,
            GuestName: request.GuestName,
            GuestEmail: request.GuestEmail,
            CheckIn: request.CheckIn,
            CheckOut: request.CheckOut,
            NumberOfGuests: request.NumberOfGuests,
            TotalPrice: room.PricePerNight * nights,
            Status: BookingStatus.Confirmed,
            CreatedAt: DateTime.UtcNow
        );

        _bookings.Add(booking);

        return new BookingConfirmation(
            booking.Id, hotel.Name, room.Type,
            booking.CheckIn, booking.CheckOut,
            booking.TotalPrice, booking.Status
        );
    }

    public Booking? GetById(string id) => _bookings.FirstOrDefault(b => b.Id == id);

    public List<Booking> GetByEmail(string email) =>
        _bookings.Where(b => b.GuestEmail.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();

    public bool Cancel(string id)
    {
        var index = _bookings.FindIndex(b => b.Id == id && b.Status == BookingStatus.Confirmed);
        if (index < 0) return false;

        _bookings[index] = _bookings[index] with { Status = BookingStatus.Cancelled };
        return true;
    }

    public List<Booking> GetAll() => _bookings.ToList();
}
