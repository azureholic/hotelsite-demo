# Feature Idea: Booking Modification & Room Upgrades

## Summary

Allow guests to modify existing bookings (change dates, guest count) and offer room upgrade suggestions when a higher-tier room is available at a reasonable price difference.

## Motivation

Currently, the only post-booking action is cancellation. Guests frequently need to adjust dates or party size, and forcing a cancel-and-rebook flow is frustrating. Proactive upgrade offers also present an upsell opportunity common in the hotel industry.

## Proposed Changes

### Backend

- Add a `PUT /api/bookings/{id}` endpoint accepting updated `checkIn`, `checkOut`, and `numberOfGuests` fields. Recalculate `TotalPrice` based on the new stay duration and validate room availability/capacity.
- Add a `GET /api/bookings/{id}/upgrades` endpoint that returns available rooms in the same hotel with higher capacity or tier, along with the price difference per night.
- Extend `BookingService` with `ModifyBooking` and `GetUpgradeOptions` methods.
- Add `ModifyBooking` and `GetUpgradeOptions` agent tools to both the MCP server and AG-UI tool sets so the chat assistant can walk guests through modifications and upgrades conversationally.

### Frontend

- Add an "Edit Booking" button to `BookingCard` on the My Bookings page that opens a modification form pre-filled with current booking details.
- Show an "Upgrade Available" badge on bookings where a better room exists, linking to a comparison view with pricing.
- Display a confirmation summary showing the old vs. new booking details and any price difference before the guest confirms.

### Data

- Ensure seed data includes hotels with multiple room tiers so upgrade paths are demonstrable.
