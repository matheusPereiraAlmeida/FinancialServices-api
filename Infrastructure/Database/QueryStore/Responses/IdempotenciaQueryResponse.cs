namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class IdempotenciaQueryResponse
    {
        public string ChaveIdempotencia { get; }

        public string Requisicao { get; }

        public string Resultado { get; }

        public IdempotenciaQueryResponse(string chaveIdempotencia, string requisicao, string resultado)
        {
            ChaveIdempotencia = chaveIdempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }
    }
}
