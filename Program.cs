List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "cloak",
        Price = 99.99,
        Available = true,
        ProductTypeId = 1,
        DateStocked = new DateTime(2023, 1, 1)
    },
    new Product()
    {
        Name = "magical hat",
        Price = 49.99,
        Available = true,
        ProductTypeId = 1,
        DateStocked = new DateTime(2023, 2, 1)
    },
    new Product()
    {
        Name = "eye of newt",
        Price = 9.99,
        Available = true,
        ProductTypeId = 2,
        DateStocked = new DateTime(2023, 3, 1)
    },
    new Product()
    {
        Name = "deadly poison",
        Price = 4.99,
        Available = false,
        ProductTypeId = 2,
        DateStocked = new DateTime(2023, 4, 1)
    },
    new Product()
    {
        Name = "broomstick",
        Price = 199.99,
        Available = true,
        ProductTypeId = 3,
        DateStocked = new DateTime(2023, 5, 1)
    },
    new Product()
    {
        Name = "code which never has bugs",
        Price = 1000000,
        Available = false,
        ProductTypeId = 3,
        DateStocked = new DateTime(2023, 6, 1)
    },
    new Product()
    {
        Name = "14-inch black walnut",
        Price = 34.99,
        Available = false,
        ProductTypeId = 4,
        DateStocked = new DateTime(2023, 7, 1)
    },
    new Product()
    {
        Name = "9-inch white maple",
        Price = 34.99,
        Available = true,
        ProductTypeId = 4,
        DateStocked = new DateTime(2023, 8, 1)
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
    string availabilityString;
    if (product.Available)
    {
        availabilityString = $"available for {product.DaysOnShelf} days, for";
    }
    else
    {
        availabilityString = "not available,";
    }

    // string typeString = "";
    // foreach (ProductType productType in productTypes)
    // {
    //     if (product.ProductTypeId == productType.Id)
    //     {
    //         typeString = $"{productType.Category}";
    //         break;
    //     }
    // }

    // refactoredd above loop to use FirstOrDefault method
    string typeString = productTypes.FirstOrDefault(type => type.Id == product.ProductTypeId).Category;

    string productString = $"{product.Name} ({typeString}) - {availabilityString} ${product.Price}";
    return productString;
}

// helper function: returns string with individual product TYPE details
// accepts object w/ type "ProductType" as parameter

string GenerateProductTypeDetails(ProductType productType)
{
    string productTypeString = $"{productType.Category}";
    return productTypeString;
}

// helper function: returns string with individual product TYPE details
// accepts INTEGER, productTypeId, as parameter

string GenerateProductTypeDetailsById(int typeId)
{
    ProductType specificType = productTypes[typeId - 1];
    string productTypeString = $"{specificType.Category}";
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

    // List<Product> filteredProducts = new List<Product>();

    // foreach (Product product in products)
    // {
    //     if (int.Parse(typeSelection) == product.ProductTypeId)
    //     {
    //         filteredProducts.Add(product);
    //     }
    // }

    // refactored above code to utilize .Where
    List<Product> filteredProducts = products.Where(product => product.ProductTypeId == int.Parse(typeSelection)).ToList();

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

    newProduct.DateStocked = DateTime.Now;

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

void DeleteProduct()
{
    Console.Clear();
    Console.WriteLine(@"Please select a product to delete from inventory, or type '0' to return to the main menu.
    ");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {GenerateProductDetails(products[i])}");
    }
    
    string deletionSelectionStr = null;
    while (deletionSelectionStr == null)
    {
        try
        {
            deletionSelectionStr = Console.ReadLine().Trim();
            if (int.TryParse(deletionSelectionStr, out int deletionSelectionInt))
            {
                if (deletionSelectionInt > 0 && deletionSelectionInt <= products.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"{products[deletionSelectionInt - 1].Name} has been deleted.");
                    products.RemoveAt(deletionSelectionInt - 1);
                    break;
                }
                else if (deletionSelectionInt == 0)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine($"Valid menu numbers only please.");
                    deletionSelectionStr = null;
                }
            }
            else
            {
                Console.WriteLine($"Integers only please.");
                deletionSelectionStr = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }
    
}

// menu option 5 - update product's details

void UpdateProduct()
{
    Console.Clear();
    Console.WriteLine(@"Please select a product to update.
    ");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {GenerateProductDetails(products[i])}");
    }
    
    string updateSelectionStr = null;
    int updateSelectionInt;
    Product selectedProduct = null;
    while (updateSelectionStr == null)
    {
        try
        {
            updateSelectionStr = Console.ReadLine().Trim();
            if (int.TryParse(updateSelectionStr, out updateSelectionInt))
            {
                if (updateSelectionInt > 0 && updateSelectionInt <= products.Count)
                {
                    Console.WriteLine($"{products[updateSelectionInt - 1].Name} selected.");
                    selectedProduct = products[updateSelectionInt - 1];
                    break;
                }
                // else if (updateSelectionInt == 0)
                // {
                //     Console.Clear();
                //     break;
                // }
                else
                {
                    Console.WriteLine($"Valid menu numbers only please.");
                    updateSelectionStr = null;
                }
            }
            else
            {
                Console.WriteLine($"Integers only please.");
                updateSelectionStr = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }

    string productTypeString = GenerateProductTypeDetailsById(selectedProduct.ProductTypeId);

    Console.WriteLine($@"Please select a property to update.

    1. Name: {selectedProduct.Name}
    2. Price: {selectedProduct.Price}
    3. Currently available: {selectedProduct.Available}
    4. Product Type: {productTypeString}
    5. Date Stocked: {selectedProduct.DateStocked.ToString("yyyy-MM-dd")}
    ");

    string propertySelectionStr = null;
    while (propertySelectionStr == null)
    {
        try
        {
            propertySelectionStr = Console.ReadLine().Trim();
            if (int.TryParse(propertySelectionStr, out int propertySelectionInt))
            {
                if (propertySelectionInt > 0 && propertySelectionInt <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Valid numbers only please.");
                    updateSelectionStr = null;
                }
            }
            else
            {
                Console.WriteLine($"Integers only please.");
                updateSelectionStr = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }

    // below needs exception handling. Wrap in while loop, then try/catch.
    // default to current values if user causes exception to be thrown.

    if (propertySelectionStr == "1")
    {
        Console.WriteLine($"Please type updated name:");
        string updatedName = Console.ReadLine().Trim();
        products[int.Parse(updateSelectionStr) - 1].Name = updatedName;
        Console.Clear();
        Console.WriteLine($"{products[int.Parse(updateSelectionStr) - 1].Name} has been updated.");
    }
    else if (propertySelectionStr == "2")
    {
        Console.WriteLine($"Please type updated price:");
        double updatedPrice = double.Parse(Console.ReadLine().Trim());
        products[int.Parse(updateSelectionStr) - 1].Price = updatedPrice;
        Console.Clear();
        Console.WriteLine($"{products[int.Parse(updateSelectionStr) - 1].Name} has been updated.");
    }
    else if (propertySelectionStr == "3")
    {
        Console.WriteLine($"Please type 'T' if available, 'F' if unavailable:");
        bool updatedAvailability = selectedProduct.Available;
        string trueOrFalse = null;
        while (trueOrFalse == null)
        {
            trueOrFalse = Console.ReadLine().Trim();
            if (trueOrFalse == "T")
            {
                updatedAvailability = true;
                break;
            }
            else if (trueOrFalse == "F")
            {
                updatedAvailability = false;
                break;
            }
            else
            {
                Console.WriteLine($"Please type 'T' or 'F'");
                trueOrFalse = null;
            }
        }
        products[int.Parse(updateSelectionStr) - 1].Available = updatedAvailability;
        Console.Clear();
        Console.WriteLine($"{products[int.Parse(updateSelectionStr) - 1].Name} has been updated.");
    }
    else if (propertySelectionStr == "4")
    {
        Console.WriteLine($"Please select updated product type:");
        Console.WriteLine($"{GenerateAllProductTypes()}");
        
        string updatedProductTypeId = null;
        // error handling here (int.TryParse)
        updatedProductTypeId = Console.ReadLine().Trim();

        products[int.Parse(updateSelectionStr) - 1].ProductTypeId = int.Parse(updatedProductTypeId);
        Console.Clear();
        Console.WriteLine($"{products[int.Parse(updateSelectionStr) - 1].Name} has been updated.");
    }
    else if (propertySelectionStr == "5")
    {
        while (true)
        {
            Console.WriteLine("When was the product initially stocked?");
            Console.WriteLine("Year (e.g., 2023): ");
            int newProductYear = int.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Month (e.g., 7): ");
            int newProductMonth = int.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Day (e.g., 31): ");
            int newProductDay = int.Parse(Console.ReadLine().Trim());

            try
            {
                products[int.Parse(updateSelectionStr) - 1].DateStocked = new DateTime(newProductYear, newProductMonth, newProductDay);
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine($"Date does not exist. Please enter a valid date.");
            }
        }

        Console.Clear();
        Console.WriteLine($"{products[int.Parse(updateSelectionStr) - 1].Name} has been updated.");
    }
    

}

// menu option 6 - view all products with property Available == true

void ViewAvailableProducts()
{
    Console.Clear();
    Console.WriteLine(@"All available products in inventory:
    ");
    
    List<Product> availableProducts = products.Where(product => product.Available).ToList();

    foreach (Product product in availableProducts)
    {
        string productString = GenerateProductDetails(product);
        Console.WriteLine(productString);
    }

    Console.WriteLine(@"
Type any key to return to main menu...");
    Console.ReadKey();
    Console.Clear();
}

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
6. View all available products
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
                    DeleteProduct();
                    break;
                    // throw new NotImplementedException();
                case "5":
                    UpdateProduct();
                    break;
                    // throw new NotImplementedException();
                case "6":
                    ViewAvailableProducts();
                    break;
                
            
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