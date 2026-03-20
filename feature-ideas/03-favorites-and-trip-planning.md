# Feature Idea: Favorites & Trip Planning

## Summary

Let guests save hotels to a favorites list and organize them into named trip plans, making it easy to compare options and return to interesting hotels later.

## Motivation

Hotel browsing is often a multi-session activity. Guests research across many hotels before committing. Without a way to save and compare, users lose track of hotels they liked. A trip-planning layer turns the site from a transactional tool into a planning companion—and pairs naturally with the AI chat assistant.

## Proposed Changes

### Backend

- Add a `TripPlan` model with fields: `Id`, `Name`, `GuestEmail`, `HotelIds` (list), `Notes`, `CreatedAt`.
- Add a `TripPlanService` for CRUD operations on trip plans.
- New API endpoints:
  - `POST /api/trip-plans` — Create a new trip plan.
  - `GET /api/trip-plans/by-email/{email}` — List a guest's trip plans.
  - `PUT /api/trip-plans/{id}` — Update plan name, notes, or hotel list.
  - `DELETE /api/trip-plans/{id}` — Delete a trip plan.
  - `GET /api/trip-plans/{id}/compare` — Return side-by-side details for all hotels in the plan (price range, star rating, amenities overlap).
- Add `CreateTripPlan`, `AddHotelToTripPlan`, and `CompareTripPlanHotels` agent tools so the chat assistant can help guests build and refine plans conversationally.

### Frontend

- Add a heart/bookmark icon to `HotelCard` and `HotelDetailPage` to save a hotel to a trip plan.
- New `TripPlansPage` (route: `/trip-plans`) where guests enter their email to view saved plans, add/remove hotels, and see a comparison table.
- Comparison table columns: hotel name, city, star rating, starting price, key amenities, and a "Book" shortcut.
- Update `Navbar` with a link to Trip Plans.

### Data

- No seed data required; trip plans are user-created at runtime.
