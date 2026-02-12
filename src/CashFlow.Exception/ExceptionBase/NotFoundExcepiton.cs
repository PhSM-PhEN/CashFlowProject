using System.Net;

namespace CashFlow.Exception.ExceptionBase
{
    public class NotFoundExcepiton(string message) : CashFlowException(message)
    {
        public override List<string> GetErrors()
        {
            return [Message];
        }

        public override int StatusCode => (int) HttpStatusCode.NotFound;

    }
}
