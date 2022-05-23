# Ethical brands guide

Website for browsing ethically, ecologically and socially responsible food brand ratings

Find the rating of a brand, explore rated brands and give a rating with your own judgement!

## Features

**Available to all**

- Brand list and detail preview
- Brand search by a name (or a partial name)
- Brand list sort by ecological, social, animal welfare or combined rating, highest first
- Brand list filtering by a category (vegetables, milk products...)
- Brand rating and comment submission
- Brand rating expert request submission
- Expert, guest and combined rating and comment previews

**Available to internal users only**

- Login at /login
- Brand, category and company with rating creation
- Brand modification and deletion
- Brand rating request list preview
- Brand rating request fulfillment and deletion
- All companies and categories list preview

**Available to administrators only**

- Previewing all users and their privileges
- Adding new users

## Technologies

- .NET 5 and Entity Framework for WEB api
- MS SQL database for data storage
- React for Front end

## Practices used in API

- Password hashing with SHA512
- Design patterns
  - Decorator pattern
  - Strategy pattern
  - Options pattern
- Logging
- Dependency Injection
- SOLID principles
- Layered architecture (presentation, business and data)
- Data transfer objects (DTO) and mapping
- Swagger documentation (loads on browser when API is started)
- Database building with migrations
- Authorization with JwtBearer tokens
- User input validation both in Client app and API
- Many to many relationship between Brands and Categories

## Further development

- Move DTO mapping to DTO methods or use an auto mapping technology
- Add unit tests
- Add more error handling
- Add more Swagger documentation
- Try method injection
- Add public comment moderation in Client app
- Add more detailed error messages from the API
- Use cache for authorization data storage
- Add Front end screenshots to the Readme file
- Add installation and usage instructions t the Readme file