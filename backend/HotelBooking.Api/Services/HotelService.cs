using HotelBooking.Api.Data;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Services;

public class HotelService
{
    private readonly List<Hotel> _hotels = SeedData.GetHotels();

    public List<HotelSummary> Search(HotelSearchRequest request)
    {
        var query = _hotels.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(h => h.City.Contains(request.City, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(request.Country))
            query = query.Where(h => h.Country.Contains(request.Country, StringComparison.OrdinalIgnoreCase));

        if (request.MinStars.HasValue)
            query = query.Where(h => h.StarRating >= request.MinStars.Value);

        if (request.Guests.HasValue)
            query = query.Where(h => h.Rooms.Any(r => r.MaxGuests >= request.Guests.Value && r.IsAvailable));

        if (request.MaxPrice.HasValue)
            query = query.Where(h => h.Rooms.Any(r => r.PricePerNight <= request.MaxPrice.Value && r.IsAvailable));

        return query.Select(h => new HotelSummary(
            h.Id, h.Name, h.City, h.Country, h.StarRating, h.ImageUrl,
            h.Rooms.Where(r => r.IsAvailable).Min(r => r.PricePerNight),
            h.Amenities
        )).ToList();
    }

    public Hotel? GetById(string id) => _hotels.FirstOrDefault(h => h.Id == id);

    public List<HotelSummary> GetAll() => Search(new HotelSearchRequest());

    public List<Room> GetAvailableRooms(string hotelId, int? guests = null)
    {
        var hotel = GetById(hotelId);
        if (hotel == null) return [];

        var rooms = hotel.Rooms.Where(r => r.IsAvailable);
        if (guests.HasValue)
            rooms = rooms.Where(r => r.MaxGuests >= guests.Value);

        return rooms.ToList();
    }
}
