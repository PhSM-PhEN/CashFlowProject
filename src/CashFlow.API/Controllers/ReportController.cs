using CashFlow.Application.UseCases.ToExpenses.Report.Excel;
using CashFlow.Application.UseCases.ToExpenses.Report.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExecel(
            [FromHeader] DateOnly month,
            [FromServices] IGenereteExpenseReportExcelUseCase useCase)
        {


            byte[] file = await useCase.Execute(month);
            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");
            }

            return NoContent();
        }
        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf(
            [FromHeader] DateOnly month,
            [FromServices] IGenereteExpensesReportPdfUseCase useCase)
        {
            byte[] file = await useCase.Execute(month);
            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "Report.pdf");
            }
            return NoContent();
        }
    }
}