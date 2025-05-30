using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Infrastructure.Data;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Infrastructure
{
    public static class InfrastructureRegistrations
    {
        public static void AddInventoryDbContext(this IServiceCollection services , string connectionString) =>
        
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));

        public static void AddInfrastructureRegistrations(this IServiceCollection services)
        {
            services.AddScoped (typeof(IRepository<>), typeof(InventoryRepository<>));
            services.AddScoped (typeof(IReadRepository<>), typeof(InventoryRepository<>));
        }
    }
}
