

using CashFlow.Domain.Enuns;

namespace CashFlow.Domain.Entities
{
    public class Expenses
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentTypeEnum PaymentType { get; set; } 
    }
}
