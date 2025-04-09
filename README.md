# ChanceGame

The solution contains 2 projects:
1. Main project: ChanceGame
   1. Project uses sqlite for a persistent DB.
   2. It contains 2 APIs to handle reqeusts
         1. /login
         this API is used as Authentication for users, which are initially seeded in the DB.
         users are amer, uwe, and members of the A-Team. (passwords match usernames)
        2.   /placeBet
         main API to place bet using a stake and a point that the user guesses.
   3. Authorization on the APIs is done using JWTs.
   4. Logic validation is also applied based on requested criteria and common business logic (edge and extreme cases).
   5. Error handeling and identification of players are also implemented.
   6. Project uses ASP .NET core 6.
   7. Postman collection uploaded.
2. Unit tests: TestChanceGame
   1. Contains unit tests for the main project.
3. To access the place bet API, you will need to call the /login API first, with the suitable username adn password to have a JWT, which you will use as a Bearer token in the next API (/placeBet API), or you will get an unautorized error.
