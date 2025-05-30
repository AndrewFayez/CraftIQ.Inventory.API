using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Services.InventoriesImplementations;
using CraftIQ.Inventory.Services.ProductsImplementations;
using CraftIQ.Inventory.Services.TransactionsImplementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Services
{
    public static class ServicesRegistrations
    {
        public static void AddServicesRegistrations(this IServiceCollection services)
        {
            services.AddScoped(typeof(InventoryFactory<,>));
            services.AddScoped(typeof(IGenericServices<,>), typeof(ProductsServices<,>));
            services.AddScoped(typeof(IGenericServices<,>), typeof(CategoriesServices<,>));
            services.AddScoped(typeof(IGenericServices<,>), typeof(InventoriesServices<,>));
            services.AddScoped(typeof(IGenericServices<,>), typeof(TransactionsServices<,>));

        }
    }
}
