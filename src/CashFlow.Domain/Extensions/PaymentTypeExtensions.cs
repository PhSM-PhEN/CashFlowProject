using CashFlow.Domain.Enuns;
using CashFlow.Domain.Report;


namespace CashFlow.Domain.Extensions
{
    public static class PaymentTypeExtensions
    {
        public static string PaymentToString(this PaymentTypeEnum payment)
        {
            return payment switch
            {
                PaymentTypeEnum.CreditCard => ResourceReportGeneretionMessage.CREDIT_CARD,
                PaymentTypeEnum.DebitCard => ResourceReportGeneretionMessage.DEBIT_CARD,
                PaymentTypeEnum.Cash => ResourceReportGeneretionMessage.CASH,
                PaymentTypeEnum.BankTransfer => ResourceReportGeneretionMessage.BANK_TRASFER,
                PaymentTypeEnum.Pix => ResourceReportGeneretionMessage.PIX,
                _ => string.Empty
            };
        }

    }
}
