namespace CashFlow.Communication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; set; } = [];

        public ResponseErrorJson(string errorMessage)
        {
            // Inicializa a lista com uma única mensagem de erro
            ErrorMessages = [errorMessage];
        }
        public ResponseErrorJson(List<string> errorMessage)
        {
            // Inicializa a lista com múltiplas mensagens de erro
            ErrorMessages = errorMessage;
        }
    }
}
