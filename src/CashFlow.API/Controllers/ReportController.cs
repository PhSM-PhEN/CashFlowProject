using CashFlow.Application.UseCases.ToExpenses.Report.Excel;
using CashFlow.Application.UseCases.ToExpenses.Report.Pdf;
using CashFlow.Domain.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExecel(
            [FromQuery] DateOnly month,
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
            [FromQuery] DateOnly month,
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