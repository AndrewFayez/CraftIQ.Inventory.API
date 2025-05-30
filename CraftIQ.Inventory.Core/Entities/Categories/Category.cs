using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Categories
{
    public class Category : BaseEntity
    {
        public Guid _CategoryId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // relation with products
        public List<Product> Products { get; set; } = new();

        public Category() { } // for EF core

        public Category(string name ,string description)
        {
           
            Name = name;
            Description = description;
            CreatedBy = Guid.Empty;
            ModifiedBy = Guid.Empty;
           

        }

        public void UpdateCategory(string name, string description , Guid modifiedBy)
        {

            Name = name;
            Description = description;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.Now;    


        }
    }
}
