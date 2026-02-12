namespace CashFlow.Application.UseCases.ToExpenses.Report.Excel
{
    public interface IGenereteExpenseReportExcelUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
