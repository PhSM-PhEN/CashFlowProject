
using CashFlow.Application.UseCases.ToExpenses.Report.Pdf.Colors;
using CashFlow.Application.UseCases.ToExpenses.Report.Pdf.Fonts;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Report;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using Document = MigraDoc.DocumentObjectModel.Document;
using Font = MigraDoc.DocumentObjectModel.Font;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace CashFlow.Application.UseCases.ToExpenses.Report.Pdf
{
    public class GenereteExpensesReportPdfUseCase : IGenereteExpensesReportPdfUseCase
    {

        private const string CURRENT_SIMBOL = "R$";
        private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
        private readonly ILoggedUser _loggedUser;
        private readonly IExpensesReadOnlyRepository _repository;

        public GenereteExpensesReportPdfUseCase(IExpensesReadOnlyRepository respoitory,
            ILoggedUser loggedUser)
        {

            _repository = respoitory;
            _loggedUser = loggedUser;
            GlobalFontSettings.FontResolver = new ExpenseReportFontsResolver();
        }
        public async Task<byte[]> Execute(DateOnly month)
        {
            var loggedUser = await _loggedUser.Get();
            var expenses = await _repository.FilterByMonth(loggedUser, month);

            if (expenses.Count == 0)
                return [];

            var document = CreatDocument(loggedUser.Name, month);
            var page = CreatePage(document);


            var totalExpense = expenses.Sum(expense => expense.Amount);

            CreateTotalSpentSection(page, month, totalExpense);

            foreach (var ex in expenses)
            {
                var table = CreateExpenseTable(page);
                var row = table.AddRow();
                row.Height = HEIGHT_ROW_EXPENSE_TABLE;

                AddExpenseTitle(row.Cells[0], ex.Title);
                AddHeaderForAmount(row.Cells[3]);

                row = table.AddRow();
                row.Height = HEIGHT_ROW_EXPENSE_TABLE;
                row.Cells[0].AddParagraph(ex.Date.ToString("D"));
                SetStyleBaseForExpenseInformation(row.Cells[0]);
                row.Cells[0].Format.LeftIndent = 10;

                row.Cells[1].AddParagraph(ex.Date.ToString("t"));
                SetStyleBaseForExpenseInformation(row.Cells[1]);
                row.Cells[1].Format.LeftIndent = 10;

                row.Cells[2].AddParagraph(ex.PaymentType.PaymentToString());
                SetStyleBaseForExpenseInformation(row.Cells[2]);
                row.Cells[2].Format.LeftIndent = 10;

                AddAmountForExpense(row.Cells[3], ex.Amount);
                if (string.IsNullOrWhiteSpace(ex.Description) == false)
                {
                    var descriptionRow = table.AddRow();
                    descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

                    descriptionRow.Cells[0].AddParagraph(ex.Description);
                    descriptionRow.Cells[0].Format.Font = new Font()
                    {
                        Name = FontHelper.RALEWAY_REGULAR,
                        Size = 14,
                        Color = ColorHelper.BLACK
                    };
                    descriptionRow.Cells[0].Shading.Color = ColorHelper.GREEN_LIGHT;
                    descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    descriptionRow.Cells[0].MergeRight = 2;
                    descriptionRow.Cells[0].Format.LeftIndent = 10;
                    row.Cells[3].MergeDown = 1;
                }

                AddWhitSpace(table);


            }


            return RenderDocument(document);
        }
        private static Document CreatDocument(string author ,DateOnly month)
        {
            var document = new Document();
            
            document.Info.Title = ($"{ResourceReportGeneretionMessage.EXPENSE_FOR} {month:Y}");
            document.Info.Author = author;

            var style = document.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;
            style!.Font.Color = ColorHelper.BLACK;

            return document;
        }
        private static Section CreatePage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;



            return section;
        }
        private static void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpense)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";

            var title = string.Format($"{ResourceReportGeneretionMessage.TOTAL_SPENT_IN}, {month:Y}");
            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15, Color = ColorHelper.BLACK });
            paragraph.AddLineBreak();

            paragraph.AddFormattedText($"{totalExpense:f2} {CURRENT_SIMBOL}", new Font { Name = FontHelper.ROBOTO_BLACK, Size = 50 });
        }
        private static Table CreateExpenseTable(Section page)
        {
            var table = page.AddTable();

            table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
            return table;
        }
        private static void AddExpenseTitle(Cell cell, string expenseTitle)
        {
            cell.AddParagraph(expenseTitle);
            cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };

            cell.Shading.Color = ColorHelper.RED_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
            cell.Format.LeftIndent = 10;

        }
        private static void AddHeaderForAmount(Cell cell)
        {
            cell.AddParagraph(ResourceReportGeneretionMessage.AMOUNT);
            cell.Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_BLACK,
                Size = 14,
                Color = ColorHelper.WHITE
            };
            cell.Shading.Color = ColorHelper.RED_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;

        }
        private static void SetStyleBaseForExpenseInformation(Cell cell)
        {
            cell.Format.Font = new Font
            {
                Name = FontHelper.ROBOTO_REGULAR,
                Size = 16,
                Color = ColorHelper.BLACK
            };
            cell.Shading.Color = ColorHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;

        }
        private static void AddAmountForExpense(Cell cell, decimal amount)
        {
            cell.AddParagraph($"- {amount:f2} {CURRENT_SIMBOL} ");

            cell.Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_REGULAR,
                Size = 14,
                Color = ColorHelper.BLACK
            };
            cell.Shading.Color = ColorHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;


        }
        private static void AddWhitSpace(Table table)
        {

            var row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;

        }
        private static byte[] RenderDocument(Document document)
        {
            var render = new PdfDocumentRenderer
            {

                Document = document
            };

            render.RenderDocument();

            using var file = new MemoryStream();
            render.PdfDocument.Save(file);

            return file.ToArray();
        }
    }
}
