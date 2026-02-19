using CashFlow.Domain.Enuns;
using CashFlow.Domain.Report;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.ToExpenses.Report.Excel
{
    public class GenereteExpenseReportExcelUseCase(IExpensesReadOnlyRepository repository,
        ILoggedUser loggedUser) : IGenereteExpenseReportExcelUseCase

    {
        private const string CURRENT_SYMBOL = "R$";
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        public async Task<byte[]> Execute(DateOnly month)
        {
            var loggedUser = await _loggedUser.Get();
            var expenses = await _repository.FilterByMonth(loggedUser, month);
            if (expenses.Count == 0)
            {
                return [];
            }

            using var workbook = new XLWorkbook
            {
                Author = loggedUser.Name,
            };

            workbook.Style.Font.FontName = "Arial";
            workbook.Style.Font.FontSize = 12;
            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

            CreateHeader(worksheet);

            var row = 2;

            foreach (var expense in expenses)
            {
                worksheet.Cell($"A{row}").Value = expense.Title;
                worksheet.Cell($"B{row}").Value = expense.Description;
                worksheet.Cell($"C{row}").Value = ConvertPaymentTypeToString(expense.PaymentType);
                worksheet.Cell($"D{row}").Value = expense.Date.ToString("dd/MM/yyyy");
                worksheet.Cell($"E{row}").Value = expense.Amount;
                worksheet.Cell($"E{row}").Style.NumberFormat.Format = $"{CURRENT_SYMBOL} #,##0.00";
                worksheet.Cell($"E{row}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);


                row++;
            }
            worksheet.Columns().AdjustToContents();
            

            var file = new MemoryStream();

            workbook.SaveAs(file);

            return file.ToArray();
        }


        private static void CreateHeader(IXLWorksheet worksheet)
        {

            worksheet.Cell("A1").Value = ResourceReportGeneretionMessage.TITLE;
            worksheet.Cell("B1").Value = ResourceReportGeneretionMessage.DESCRIPTION;
            worksheet.Cell("C1").Value = ResourceReportGeneretionMessage.PAYMENT_TYPE;
            worksheet.Cell("D1").Value = ResourceReportGeneretionMessage.DATE;
            worksheet.Cell("E1").Value = ResourceReportGeneretionMessage.AMOUNT;

            worksheet.Cells("A1:E1").Style.Font.Bold = true;
            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6"); // htmlcolorcodes.com
            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

        }

        private static string ConvertPaymentTypeToString(PaymentTypeEnum paymentType)
        {
            return paymentType switch
            {
                PaymentTypeEnum.CreditCard => ResourceReportGeneretionMessage.CREDIT_CARD,
                PaymentTypeEnum.DebitCard => ResourceReportGeneretionMessage.DEBIT_CARD,
                PaymentTypeEnum.BankTransfer => ResourceReportGeneretionMessage.BANK_TRASFER,
                PaymentTypeEnum.Pix => ResourceReportGeneretionMessage.PIX,
                PaymentTypeEnum.Cash => ResourceReportGeneretionMessage.CASH,
                _ => string.Empty
            };
        }
    }

}
