using System.ComponentModel;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using Microsoft.Extensions.AI;

namespace HotelBooking.Api.AgentTools;

/// <summary>
/// Function tools for the AG-UI agent. These wrap the same services used by the MCP tools.
/// </summary>
public static class HotelAgentTools
{
    public static IList<AITool> Create(HotelService hotelService, BookingService bookingService)
    {
        return
        [
            AIFunctionFactory.Create(
                [Description("Search for hotels by city, country, star rating, max price, or number of guests")]
                (string? city, string? country, int? minStars, decimal? maxPrice, int? guests) =>
                    hotelService.Search(new HotelSearchRequest(city, country, null, null, guests, minStars, maxPrice)),
                "SearchHotels"),

            AIFunctionFactory.Create(
                [Description("Get full details of a hotel including rooms and amenities")]
                (string hotelId) => hotelService.GetById(hotelId),
                "GetHotelDetails"),

            AIFunctionFactory.Create(
                [Description("Get available rooms for a hotel, optionally filtered by guest count")]
                (string hotelId, int? guests) => hotelService.GetAvailableRooms(hotelId, guests),
                "GetAvailableRooms"),

            AIFunctionFactory.Create(
                [Description("Create a booking for a hotel room")]
                (string hotelId, string roomId, string guestName, string guestEmail,
                 DateTime checkIn, DateTime checkOut, int numberOfGuests) =>
                    bookingService.Create(new CreateBookingRequest(
                        hotelId, roomId, guestName, guestEmail, checkIn, checkOut, numberOfGuests)),
                "CreateBooking"),

            AIFunctionFactory.Create(
                [Description("Get booking details by booking ID")]
                (string bookingId) => bookingService.GetById(bookingId),
                "GetBooking"),

            AIFunctionFactory.Create(
                [Description("List all bookings for a guest by email address")]
                (string email) => bookingService.GetByEmail(email),
                "ListBookings"),

            AIFunctionFactory.Create(
                [Description("Cancel a confirmed booking")]
                (string bookingId) => bookingService.Cancel(bookingId)
                    ? $"Booking {bookingId} cancelled successfully."
                    : $"Could not cancel {bookingId}.",
                "CancelBooking"),
        ];
    }
}
