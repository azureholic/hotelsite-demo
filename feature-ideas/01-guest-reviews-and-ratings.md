# Feature Idea: Guest Reviews & Ratings

## Summary

Allow guests to leave reviews and star ratings after their stay, and display aggregate ratings and recent reviews on hotel detail pages.

## Motivation

The application currently shows hotel star ratings but has no guest feedback mechanism. User-generated reviews build trust, help future guests make informed decisions, and increase engagement with the platform.

## Proposed Changes

### Backend

- Add a `Review` model with fields: `Id`, `HotelId`, `BookingId`, `GuestName`, `Rating` (1-5), `Comment`, `CreatedAt`.
- Add a `ReviewService` to manage CRUD operations and compute aggregate scores.
- New API endpoints:
  - `POST /api/hotels/{id}/reviews` — Submit a review (linked to a completed booking).
  - `GET /api/hotels/{id}/reviews` — List reviews for a hotel (paginated, sorted by date).
  - `GET /api/hotels/{id}/reviews/summary` — Get average rating and review count.
- Add `SearchHotelReviews` and `SubmitReview` tools to the MCP and AG-UI agent tool sets so the chat assistant can help guests read or leave reviews.

### Frontend

- Add a reviews section to `HotelDetailPage` showing the average guest rating, review count, and a scrollable list of recent reviews.
- After a booking is completed, prompt the guest to leave a review via a simple form (rating selector + text area).
- Update `HotelCard` to show the average guest rating badge alongside the hotel star rating.

### Data

- Seed a handful of sample reviews per hotel for demo purposes.
