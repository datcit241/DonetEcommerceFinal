using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    private static readonly Random _random = new();
    private static DataContext _context;
    private static UserManager<User> _userManager;

    private static List<Image> _images;
    private static List<Image> _avatars;
    private static List<Image> _productImages;

    private static List<User> _users;

    private static List<Product> _products;
    private static List<Price> _prices;

    private static List<Category> _categories;
    private static List<CategoryValue> _categoryValues;
    private static List<ProductCategory> _productCategories;

    private static List<Variation> _variations;
    private static List<VariationOption> _variationOptions;
    private static List<ProductVariation> _productVariations;

    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        if (userManager.Users.Any()) return;

        _context = context;
        _userManager = userManager;

        _avatars = SeedImages("/assets/images/avatars", "avatar", 24);
        _productImages = SeedImages("/assets/images/products", "product", 24);

        SeedUsers();
        SeedAvatars();
        CreateUsers();


        SeedProducts();
        SeedPrices();
        SeedProductImages();

        SeedCategories();
        SeedCategoryValues();
        SeedProductCategories();

        SeedVariations();
        SeedVariationOptions();
        SeedProductVariations();

        context.AttachRange(_images);

        context.AttachRange(_products);
        context.AttachRange(_prices);

        context.AttachRange(_categories);
        context.AttachRange(_categoryValues);
        context.AttachRange(_productCategories);

        context.AttachRange(_variations);
        context.AttachRange(_variationOptions);
        context.AttachRange(_productVariations);

        // await context.AddRangeAsync(_images);
        //
        // await context.AddRangeAsync(_products);
        // await context.AddRangeAsync(_prices);
        //
        // await context.AddRangeAsync(_categories);
        // await context.AddRangeAsync(_categoryValues);
        // await context.AddRangeAsync(_productCategories);
        //
        //
        // await context.AddRangeAsync(_variations);
        // await context.AddRangeAsync(_variationOptions);
        // await context.AddRangeAsync(_productVariations);

        await context.SaveChangesAsync();
    }

    public static List<Image> SeedImages(string url, string name, int total)
    {
        var list = new List<Image>();
        for (var i = 1; i <= total; i++)
            list.Add(new Image
            {
                Url = url,
                Name = name + "_" + i,
                Extension = "jpg"
            });

        _images ??= new List<Image>();
        _images.AddRange(list);
        return list;
    }

    public static void SeedUsers()
    {
        _users = new List<User>
        {
            new()
            {
                Name = "Amos Blanda",
                UserName = "amos",
                Email = "amos@test.com",
                Address = "1 at fake street"
            },
            new()
            {
                Name = "Brent Goodwin",
                UserName = "brent",
                Email = "brent@test.com",
                Address = "2 at fake street"
            },
            new()
            {
                Name = "Carol Koss",
                UserName = "carol",
                Email = "carol@test.com",
                Address = "3 at fake street"
            }
        };
    }

    public static void SeedAvatars()
    {
        for (int i = 0, len = _users.Count; i < len; i++) _users[i].Avatar = _avatars[i + 1];
    }

    public static async void CreateUsers()
    {
        var carts = new List<Cart>();
        foreach (var user in _users)
        {
            carts.Add(new Cart
            {
                UserId = user.Id
            });
            await _userManager.CreateAsync(user, "Password_123");
        }

        await _context.AddRangeAsync(carts);
        await _context.SaveChangesAsync();
    }

    public static void SeedProducts()
    {
        var names = new List<string>
        {
            "Nike Air Force 1 NDESTRUKT",
            "Nike Space Hippie 04",
            "Nike Air Zoom Pegasus 37 A.I.R. Chaz Bear",
            "Nike Blazer Low 77 Vintage",
            "Nike ZoomX SuperRep Surge",
            "Zoom Freak 2",
            "Nike Air Max Zephyr",
            "Jordan Delta",
            "Air Jordan XXXV PF",
            "Nike Waffle Racer Crater",
            "Kyrie 7 EP Sisterhood",
            "Nike Air Zoom BB NXT",
            "Nike Air Force 1 07 LX",
            "Nike Air Force 1 Shadow SE",
            "Nike Air Zoom Tempo NEXT",
            "Nike DBreak-Type",
            "Nike Air Max Up",
            "Nike Air Max 270 React ENG",
            "NikeCourt Royale",
            "Nike Air Zoom Pegasus 37 Premium",
            "Nike Air Zoom SuperRep",
            "NikeCourt Royale",
            "Nike React Art3mis",
            "Nike React Infinity Run Flyknit"
        };
        var desc =
            "The Air Force 1 NDSTRKT blends unbelievable comfort with head-turning style and street-ready toughness to create an \'indestructible\' feel. In a nod to traditional work boots, the timeless silhouette comes covered in rubber reinforcements in high-wear areas. Lace up for tough conditions with this hardy take on a lifestyle classic.\nIntroduced in 1982, the Air Force 1 redefined basketball footwear from the hardwood to the tarmac. It was the first basketball sneaker to house Nike Air, but its innovative nature has since taken a back seat to its status as a street icon.";

        _products = new List<Product>();

        var start = new DateTime(2020, 1, 1);
        var range = (DateTime.Today - start).Days;

        for (int i = 0, len = names.Count; i < len; i++)
            _products.Add(new Product
            {
                Name = names[i],
                Status = ProductStatus.Available,
                Quantity = _random.Next(0, 20),
                Description = desc,
                Revenue = 0,
                DateAdded = start.AddDays(_random.Next(range))
            });
    }

    public static void SeedPrices()
    {
        _prices = new List<Price>();
        foreach (var product in _products)
            _prices.Add(new Price
            {
                Product = product,
                Amount = _random.Next(10, 100),
                DateSet = DateTime.Now
            });
    }

    public static void SeedProductImages()
    {
        var totalImages = _productImages.Count;

        for (int i = 0, len = _products.Count; i < len; i++)
        {
            var fromImage = i % totalImages;
            var toImage = fromImage + _random.Next(Math.Min(totalImages - fromImage, 7));

            var img = new List<Image>();
            for (; fromImage < toImage; fromImage++) img.Add(_productImages[fromImage]);

            _products[i].Images = img;
        }
    }

    public static void SeedCategories()
    {
        _categories = new List<Category>
        {
            new() { Name = "For" },
            new() { Name = "Category" }
        };
    }

    public static void SeedCategoryValues()
    {
        _categoryValues = new List<CategoryValue>
        {
            new() { Name = "Men", Category = _categories[0] },
            new() { Name = "Women", Category = _categories[0] },
            new() { Name = "Kids", Category = _categories[0] },
            new() { Name = "Sneakers", Category = _categories[1] },
            new() { Name = "Running Shoes", Category = _categories[1] },
            new() { Name = "Chunk Taylor", Category = _categories[1] },
            new() { Name = "Old Skool", Category = _categories[1] },
            new() { Name = "Hiking Shoes", Category = _categories[1] }
        };
    }

    public static void SeedProductCategories()
    {
        _productCategories = new List<ProductCategory>();

        var len = _categoryValues.Count;
        foreach (var product in _products)
        {
            var value = _categoryValues[_random.Next(len)];
            _productCategories.Add(new ProductCategory
            {
                Product = product,
                CategoryValue = value,
                Category = value.Category
            });
        }
    }

    public static void SeedVariations()
    {
        _variations = new List<Variation>
        {
            new()
            {
                Name = "Size"
            },
            new()
            {
                Name = "Color"
            }
        };
    }

    public static void SeedVariationOptions()
    {
        _variationOptions = new List<VariationOption>
        {
            new()
            {
                Name = "Small",
                Variation = _variations[0],
                Charge = 15
            },

            new()
            {
                Name = "Medium",
                Variation = _variations[0],
                Charge = 0
            },
            new()
            {
                Name = "Large",
                Variation = _variations[0],
                Charge = 20
            },
            new()
            {
                Name = "White",
                Variation = _variations[1],
                Charge = 0
            },
            new()
            {
                Name = "Black",
                Variation = _variations[1],
                Charge = 5
            }
        };
    }

    public static void SeedProductVariations()
    {
        _productVariations = new List<ProductVariation>();
        foreach (var product in _products)
        {
            if (_random.Next(2) == 1)
                _productVariations.Add(new ProductVariation
                {
                    Product = product,
                    Variation = _variations[0]
                });

            if (_random.Next(2) == 1)
                _productVariations.Add(new ProductVariation
                {
                    Product = product,
                    Variation = _variations[1]
                });
        }
    }
}