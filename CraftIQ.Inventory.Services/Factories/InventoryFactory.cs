using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Core.Entities.Orders;
using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using CraftIQ.Inventory.Services.InventoriesImplementations;
using CraftIQ.Inventory.Services.OrderDetailsImplementations;
using CraftIQ.Inventory.Services.OrdersImplementations;
using CraftIQ.Inventory.Services.ProductsImplementations;
using CraftIQ.Inventory.Services.TransactionsImplementations;
using huzcodes.Persistence.Interfaces.Repositories; 

namespace CraftIQ.Inventory.Services.Factories
{
    public class InventoryFactory<TRequest, TRespons>
                                 (IRepository<Category> categoryRepository,
                                  IRepository<Product> productRepository,
                                  IRepository<Core.Entities.Inventories.Inventory> inventoryRepository,
                                  IRepository<Transaction> transactionRepository,
                                  IRepository<Order> orderRepository,
                                  IRepository<OrderDetail> orderDetailRepository
                                  )
    {
        private readonly IRepository<Category> _categoryRepository = categoryRepository;
        private readonly IRepository<Product> _productRepository = productRepository;
        private readonly IRepository<Core.Entities.Inventories.Inventory> _inventoryRepository = inventoryRepository;
        private readonly IRepository<Transaction> _transactionRepository = transactionRepository;
        private readonly IRepository<Order> _orderRepository = orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository = orderDetailRepository;

        public IGenericServices<TRequest, TRespons> Build(string key)
        {
            switch (key)
            {
                case nameof(Category):
                    return new CategoriesServices<TRequest, TRespons>(_categoryRepository);
                case nameof(Product):
                    return new ProductsServices<TRequest, TRespons>(_productRepository, _categoryRepository,_inventoryRepository,_transactionRepository);
                case nameof(Core.Entities.Inventories.Inventory):
                    return new InventoriesServices<TRequest, TRespons>(_inventoryRepository);
                case nameof(Transaction):
                    return new TransactionsServices<TRequest, TRespons>(_transactionRepository);
                case nameof(Order):
                    return new OrdersServices<TRequest, TRespons>(_orderRepository);
                case nameof(OrderDetail):
                    return new OrderDetailsServices<TRequest, TRespons>(_orderDetailRepository , _productRepository , _orderRepository);
                default:
                    return null!;
            }

        }


    }
}
