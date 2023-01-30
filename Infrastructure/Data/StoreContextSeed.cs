using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAysnc(StoreContext context, ILoggerFactory loggerFactory){

            try 
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands) 
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

                if(!context.ProductTypes.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var brands = JsonSerializer.Deserialize<List<ProductType>>(brandsData);

                    foreach (var item in brands) 
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

                if(!context.Products.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var brands = JsonSerializer.Deserialize<List<Product>>(brandsData);

                    foreach (var item in brands) 
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

            } catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "There was an issue seeding the database");
            }

        }
    }
}