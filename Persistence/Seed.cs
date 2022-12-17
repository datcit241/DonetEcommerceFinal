using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new User
                {
                    Name = "Amos Blanda",
                    UserName = "amos",
                    Email = "amos@test.com",
                    Address = "1 at fake street",
                },
                new User
                {
                    Name = "Brent Goodwin",
                    UserName = "brent",
                    Email = "brent@test.com",
                    Address = "2 at fake street",
                },
                new User
                {
                    Name = "Carol Koss",
                    UserName = "carol",
                    Email = "carol@test.com",
                    Address = "3 at fake street",
                }
            };

            var carts = new List<Cart>();
            foreach (var user in users)
            {
                carts.Add(new Cart
                {
                    UserId = user.Id
                });
                await userManager.CreateAsync(user, "Password_123");
            }

            await context.AddRangeAsync(carts);
            await context.SaveChangesAsync();
        }

        if (context.Products.Any()) return;
        var products = new List<Product>
        {
            new Product
            {
                Name = "Nike Air Zoom Pegasus 37 A.I.R. Chaz Bear",
                Description = "Leather panels. Laces. Rounded toe. Rubber sole.",
                Label = ProductLabel.New,
                Revenue = 0,
                Quantity = 10,
                Status = ProductStatus.Available,
            }
        };
        var prices = new List<Price>
        {
            new Price
            {
                Product = products[0],
                Amount = 100d,
                DateSet = DateTime.Today
            }
        };

        await context.Products.AddRangeAsync(products);
        await context.Prices.AddRangeAsync(prices);

        await context.SaveChangesAsync();
    }
}