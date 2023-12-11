// Copyright (c) 2023 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

using System.Text;

namespace BITSIGN.Integrador
{
    internal class Logger : IDisposable
    {
        private const string TemplateDeArquivo = "{0:yyMMdd}.txt";
        internal const string Informativo = "Informativo";
        internal const string Alerta = "Alerta";
        internal const string Falha = "Falha";

        internal record Registro(DateTime Data, string Tarefa, string Severidade, string Mensagem);
        private readonly IList<Registro> registros = new List<Registro>();

        internal void Log(Registro log) =>
            this.registros.Add(log);

        internal void Log(string tarefa, string mensagem, string severidade = Informativo) =>
            this.registros.Add(new(DateTime.Now, tarefa, severidade, mensagem));

        internal void Log(string tarefa, Exception erro) =>
            this.registros.Add(new(DateTime.Now, tarefa, "Falha", erro.ToString().SemQuebraDeLinha()));

        public void Dispose()
        {
            if (this.registros.Count > 0)
            {
                var conteudo = new StringBuilder();

                foreach (var registro in this.registros.OrderBy(static r => r.Data))
                    conteudo.AppendLine($"{registro.Data:HH:mm:ss} - {registro.Tarefa.ToUpper()} - {registro.Severidade} - {registro.Mensagem}");

                File.AppendAllText(Path.Combine(Configuracoes.Paths.Logs, string.Format(TemplateDeArquivo, DateTime.Now)), conteudo.ToString());
                this.registros.Clear();
            }
        }
    }
}