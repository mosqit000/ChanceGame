



using ChanceGame.Database;
using ChanceGame.Database.Entities;
using ChanceGame.Security;

namespace ChanceGame.Configurations;

public class DbInitializer
{
    public static void Initialize(UserContext context)
    {
        context.Database.EnsureCreated(); 

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Username = "amer",Password = SecurityUtils.HashPassword("amer",5)},
                new User { Username = "uwe",Password = SecurityUtils.HashPassword("uwe",5)},
                new User { Username = "hannibal",Password = SecurityUtils.HashPassword("hannibal",5)},
                new User { Username = "murdock",Password = SecurityUtils.HashPassword("murdock",5)},
                new User { Username = "baracus",Password = SecurityUtils.HashPassword("baracus",5)},
                new User { Username = "peck",Password = SecurityUtils.HashPassword("peck",5)}
            );
            context.SaveChanges();
        }
    }
}