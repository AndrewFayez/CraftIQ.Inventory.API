namespace CraftIQ.Inventory.API.Endpoints.Transactions.Create
{
    public class CreateTransactionsResponse
    {
        public Guid TransactionId { get; set; }

        public CreateTransactionsResponse(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
