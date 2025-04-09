# ChanceGame

The solution contains 2 projects:
1. Main project: ChanceGame
   1.1. Project uses sqlite for a persistent DB.
   1.2. It contains 2 APIs to handle reqeusts
     1.2.1.  /login
         this API is used as Authentication for users, which are initially seeded in the DB.
         users are amer, uwe, and members of the A-Team. (passwords match usernames)
     1.2.2.  /placeBet
         main API to place bet using a stake and a point that the user guesses.
   1.3. Authorization on the APIs is done using JWTs.
   1.4. Logic validation is also applied based on requested criteria and common business logic (edge and extreme cases).
   1.5. Error handeling and identification of players are also implemented.
   1.6. Project uses ASP .NET core 6.
   1.7. Postman collection uploaded.
3. Unit tests: TestChanceGame
   3.1. Contains unit tests for the main project.
