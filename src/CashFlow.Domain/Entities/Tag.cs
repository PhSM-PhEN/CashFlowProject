using CashFlow.Domain.Enuns;

namespace CashFlow.Domain.Entities
{
    public class Tag
    {
        public long Id { get; set; }

        public TagEnum Value { get; set; }

        public long ExpensesId { get; set; }
        public Expenses Expenses { get; set; } = default!;
    }
}
