namespace CashFlow.Application.UseCases.ToExpenses.Report.Pdf
{
    public interface IGenereteExpensesReportPdfUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
