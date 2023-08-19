List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "cloak",
        Price = 99.99,
        Available = true,
        ProductTypeId = 1
    },
    new Product()
    {
        Name = "magical hat",
        Price = 49.99,
        Available = true,
        ProductTypeId = 1
    },
    new Product()
    {
        Name = "eye of newt",
        Price = 9.99,
        Available = true,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "deadly poison",
        Price = 4.99,
        Available = false,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "broomstick",
        Price = 199.99,
        Available = true,
        ProductTypeId = 3
    },
    new Product()
    {
        Name = "code which never has bugs",
        Price = 1000000,
        Available = false,
        ProductTypeId = 3
    },
    new Product()
    {
        Name = "14-inch black walnut",
        Price = 34.99,
        Available = false,
        ProductTypeId = 4
    },
    new Product()
    {
        Name = "9-inch white maple",
        Price = 34.99,
        Available = true,
        ProductTypeId = 4
    },
};

List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Id = 1,
        Category = "apparel"
    },
    new ProductType()
    {
        Id = 2,
        Category = "potions"
    },
    new ProductType()
    {
        Id = 3,
        Category = "enchanted objects"
    },
    new ProductType()
    {
        Id = 4,
        Category = "wands"
    }
};

// helper function: returns string with product details
// accepts object w/ type "Product" as parameter

string GenerateProductDetails(Product product)
{
    string availableString;
    if (product.Available)
    {
        availableString = "available for";
    }
    else
    {
        availableString = "not available,";
    }

    string typeString = "";
    foreach (ProductType productType in productTypes)
    {
        if (product.ProductTypeId == productType.Id)
        {
            typeString = $"{productType.Category}";
            break;
        }
    }

    string productString = $"{product.Name} ({typeString}) - {availableString} ${product.Price}";
    return productString;
}

// helper function: returns string with individual product type details
// accepts object w/ type "ProductType" as parameter

string GenerateProductTypeDetails(ProductType productType)
{
    string productTypeString = $"{productType.Category}";
    return productTypeString;
}

// helper function: returns string with all product types

string GenerateAllProductTypes()
{
    string allProductTypes = null;
    
    for (int i = 0; i < productTypes.Count(); i++)
    {
        allProductTypes += @$"{i + 1}. {GenerateProductTypeDetails(productTypes[i])}
";
    }

    return allProductTypes;
}

// menu option 1 - view all products

void ViewAllProducts()
{
    Console.Clear();
    Console.WriteLine(@"All products in inventory:
    ");
    
    foreach (Product product in products)
    {
        string productString = GenerateProductDetails(product);
        Console.WriteLine(productString);
    }

    Console.WriteLine(@"
Type any key to return to main menu...");
    Console.ReadKey();
    Console.Clear();
}

// menu option 2 - view products by selected type

void ViewProductsByType()
{
    Console.Clear();
    Console.WriteLine(@"Select a product type to view matching products:
    ");

    Console.WriteLine($"{GenerateAllProductTypes()}");

    string typeSelection = Console.ReadLine().Trim();

    List<Product> filteredProducts = new List<Product>();

    foreach (Product product in products)
    {
        if (int.Parse(typeSelection) == product.ProductTypeId)
        {
            filteredProducts.Add(product);
        }
    }

    Console.Clear();
    Console.WriteLine(@"Filtered products:
    ");
    
    foreach (Product product in filteredProducts)
    {
        Console.WriteLine($"{GenerateProductDetails(product)}");
    }
    
    Console.WriteLine(@"
Type any key to return to main menu...");
    Console.ReadKey();
    Console.Clear();
}

// menu option 3 - add product to inventory

void AddProduct()
{
    Console.Clear();
    Product newProduct = new Product();
    string confirmation = null;
        
    Console.WriteLine(@"Add new product
    ");

    Console.WriteLine("New product's name: ");
    newProduct.Name = null;
    // error handling necessary?
    newProduct.Name = Console.ReadLine().Trim();
    
    Console.WriteLine("New product's price: ");
    string priceString = null;
    // error handling (double.TryParse)
    priceString = Console.ReadLine().Trim();
    newProduct.Price = double.Parse(priceString);

    newProduct.Available = true;

    Console.WriteLine("New product's product type (select from below): ");
    Console.WriteLine($"{GenerateAllProductTypes()}");

    string typeSelection = null;
    // error handling here (int.TryParse)
    typeSelection = Console.ReadLine().Trim();
    newProduct.ProductTypeId = int.Parse(typeSelection);

    Console.WriteLine($@"{GenerateProductDetails(newProduct)}
    ");

    while (confirmation == null)
    {
        Console.WriteLine(@"Is this correct? (Y / N)
    "); 
        confirmation = Console.ReadLine().Trim();
        if (confirmation == "Y")
        {
            Console.Clear();
            products.Add(newProduct);
            Console.WriteLine(@"New product added!
            ");
        }
        else if (confirmation == "N")
        {
            Console.Clear();
            Console.WriteLine(@"returning to main menu...
            ");
            
        }
        else
        {
            Console.WriteLine($"Please enter a valid response.");
            confirmation = null;
        }
    }

}

// menu option 4 - delete product from inventory

// menu option 5 - update product's details


void Main ()
{
    
    string menuSelection = null;
    while (menuSelection != "0")
    {
        Console.WriteLine(@"~~Reductio & Absurdum~~

mAgIcAl MaIn MeNu

Select an option:
0. Exit
1. View all products
2. View products by type
3. Add product to inventory
4. Delete product from inventory
5. Update product's details
");
        
        menuSelection = Console.ReadLine().Trim();
        
        try 
        {
            switch (menuSelection)
            {
                case "0":
                    Console.WriteLine("AVADA KADAVRA");
                    break;
                case "1":
                    ViewAllProducts();
                    break;
                    // throw new NotImplementedException();
                case "2":
                    ViewProductsByType();
                    break;
                    // throw new NotImplementedException();
                case "3":
                    AddProduct();
                    break;
                    // throw new NotImplementedException();
                case "4":
                    throw new NotImplementedException();
                case "5":
                    throw new NotImplementedException();
                
            
            }
        }
        catch (NotImplementedException)
        {
            Console.Clear();
            
            Console.WriteLine("feature to be built");
        }
        catch (Exception ex)
        {
            Console.Clear();
            
            Console.WriteLine($"{ex}");
            
        }
    }
}

Console.Clear();
Main();