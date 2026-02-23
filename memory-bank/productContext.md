# Product Context

## What Problem Are We Solving?

### The Gift-Giving Dilemma
People struggle with gift-giving coordination:
- "What should I get them?" - No visibility into what people actually want
- "Did someone already buy this?" - Risk of duplicate purchases
- "How do we coordinate?" - No private way to discuss gifts within family/friend groups
- "This app is too expensive/public" - Existing solutions are costly or too social-media focused

### Current Alternatives and Their Limitations
1. **Amazon Wishlist**: Public, tied to Amazon, no claiming mechanism
2. **Pinterest Boards**: Not designed for this use case, no privacy controls
3. **Spreadsheets**: Manual, no user-friendly interface, prone to errors
4. **Text/Email Chains**: Messy, hard to track, no centralized view

## Our Solution

### Core User Flows

#### 1. **Wishlist Owner Flow**
1. Create account / log in
2. Create a wishlist (e.g., "Sarah's Birthday 2026")
3. Add items with details (name, description, link, image, approximate price)
4. Invite specific people via email to the wishlist
5. View their wishlist (can't see who claimed what)

#### 2. **Wishlist Member Flow**
1. Receive invitation email
2. Accept invitation / create account
3. View wishlist items
4. "Claim" an item (marks it as claimed for others, but owner can't see who claimed it)
5. Unclaim if plans change

#### 3. **Group Coordination**
- Multiple people can be invited to the same wishlist
- Each member sees which items are still available
- Owner sees total items but not claiming details (maintains surprise)
- Members can see what's claimed but not WHO claimed it (unless they claimed it themselves)

### Key Features

#### Must Have (MVP)
- User authentication and account management
- Create, edit, delete wishlists
- Add items to wishlists (name, description, URL, image, price)
- Invite specific users to view a wishlist
- Claim/unclaim items
- View wishlists you own vs wishlists shared with you

#### Should Have (Phase 2)
- Email notifications for invitations
- Item priority/importance levels
- Categories for items
- Wishlist templates for common occasions
- Basic search and filtering

#### Nice to Have (Future)
- PWA for mobile experience
- Export wishlist to PDF
- Guest access (view-only without account)
- Archive old wishlists

### User Experience Goals

1. **Simplicity**: Creating a wishlist should take < 5 minutes
2. **Clarity**: Clear visual distinction between "available" and "claimed" items
3. **Privacy**: Strong emphasis on private, invite-only lists
4. **Surprise Preservation**: Owner can't see who claimed items
5. **Flexibility**: Easy to add/remove items or members at any time

### Non-Goals

- This is NOT a social network (no public profiles, no following)
- This is NOT a shopping platform (we don't sell anything)
- This is NOT a price tracker (we don't scrape prices)
- This is NOT a recommendation engine (we don't suggest items)

## Business Model

This is a personal learning project designed for self-hosting, not monetization. If it evolves, potential future models:
- Personal use only (current plan)
- Hosted version with freemium model
- One-time purchase for self-hosting package

Cost efficiency and learning are the current priorities.
