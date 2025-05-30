using CraftIQ.Inventory.Shared.Contracts.Inventories;

namespace CraftIQ.Inventory.Core.Entities.Inventories
{
    public class Inventory : BaseEntity
    {

        public Inventory() { } // Default constructor for EF Core
        public Guid _InventoryId { get; set; }
        public string Name { get; set; } = string.Empty; 
        public int Quantity { get; set; }
        public int Reorderlevel { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTimeOffset LastUpdated { get; set; }
       
        public List<Product> Products { get; set; } = new();

        public Inventory(string name, int quantity, int reorderlevel, string location, DateTimeOffset lastUpdated)
        {
            _InventoryId = Guid.NewGuid();
            Name = name;
            Quantity = quantity;
            Reorderlevel = reorderlevel;
            Location = location;
            LastUpdated = DateTimeOffset.Now;
            CreatedBy = new();
            CreatedOn = DateTimeOffset.Now;
            ModifiedBy = new();
            ModifiedOn = DateTimeOffset.Now;
        }
        public void UpdateInventory(InventoriesOperationsContract inventory) 
        {
            ModifiedOn =  DateTimeOffset.Now;
            LastUpdated = DateTimeOffset.Now;
            Name = string.IsNullOrEmpty(inventory.Name) ? this.Name: inventory.Name;
            Quantity = inventory.Quantity == 0? this.Quantity : inventory.Quantity;
            Reorderlevel = inventory.Reorderlevel == 0 ? this.Reorderlevel : inventory.Reorderlevel;
            Location = string.IsNullOrEmpty(inventory.Location) ? this.Location : inventory.Location ;

        }  
        
        
    }
}
