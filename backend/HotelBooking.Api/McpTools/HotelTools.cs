using System.ComponentModel;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using ModelContextProtocol.Server;

namespace HotelBooking.Api.McpTools;

[McpServerToolType]
public class HotelTools(HotelService hotelService, BookingService bookingService)
{
    [McpServerTool, Description("Search for hotels by city, country, star rating, max price, or number of guests. All parameters are optional.")]
    public List<HotelSummary> SearchHotels(
        [Description("City name to search in")] string? city = null,
        [Description("Country name")] string? country = null,
        [Description("Minimum star rating (1-5)")] int? minStars = null,
        [Description("Maximum price per night")] decimal? maxPrice = null,
        [Description("Number of guests")] int? guests = null)
    {
        return hotelService.Search(new HotelSearchRequest(city, country, null, null, guests, minStars, maxPrice));
    }

    [McpServerTool, Description("Get full details of a specific hotel by its ID, including all rooms and amenities.")]
    public Hotel? GetHotelDetails([Description("The hotel ID (e.g. h1, h2)")] string hotelId)
    {
        return hotelService.GetById(hotelId);
    }

    [McpServerTool, Description("Get available rooms for a hotel, optionally filtered by number of guests.")]
    public List<Room> GetAvailableRooms(
        [Description("The hotel ID")] string hotelId,
        [Description("Number of guests")] int? guests = null)
    {
        return hotelService.GetAvailableRooms(hotelId, guests);
    }

    [McpServerTool, Description("Create a booking for a hotel room. Returns booking confirmation with ID and total price.")]
    public BookingConfirmation? CreateBooking(
        [Description("The hotel ID")] string hotelId,
        [Description("The room ID")] string roomId,
        [Description("Guest full name")] string guestName,
        [Description("Guest email address")] string guestEmail,
        [Description("Check-in date (YYYY-MM-DD)")] DateTime checkIn,
        [Description("Check-out date (YYYY-MM-DD)")] DateTime checkOut,
        [Description("Number of guests")] int numberOfGuests)
    {
        return bookingService.Create(new CreateBookingRequest(
            hotelId, roomId, guestName, guestEmail, checkIn, checkOut, numberOfGuests));
    }

    [McpServerTool, Description("Get details of a specific booking by booking ID.")]
    public Booking? GetBooking([Description("The booking ID (e.g. BK-XXXXXXXX)")] string bookingId)
    {
        return bookingService.GetById(bookingId);
    }

    [McpServerTool, Description("List all bookings for a guest by their email address.")]
    public List<Booking> ListBookings([Description("Guest email address")] string email)
    {
        return bookingService.GetByEmail(email);
    }

    [McpServerTool, Description("Cancel a confirmed booking by its ID.")]
    public string CancelBooking([Description("The booking ID to cancel")] string bookingId)
    {
        return bookingService.Cancel(bookingId)
            ? $"Booking {bookingId} has been cancelled successfully."
            : $"Could not cancel booking {bookingId}. It may not exist or is already cancelled.";
    }
}
