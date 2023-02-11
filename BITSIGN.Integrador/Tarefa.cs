// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Configuracoes;

namespace BITSIGN.Integrador
{
    internal abstract class Tarefa
    {
        protected static readonly ProxyDoServico proxy;
        private readonly IList<Logger.Registro> logs = new List<Logger.Registro>();

        static Tarefa() =>
            proxy = new(new(new AppSettingsJson()));

        protected Tarefa(string nome) =>
            this.Nome = nome;

        internal string Nome { get; }

        internal abstract Task Executar(CancellationToken cancellationToken = default);

        protected void Log(string mensagem, string severidade = Logger.Informativo) =>
            this.logs.Add(new(DateTime.Now, this.Nome, severidade, mensagem));

        protected void Log(Exception erro) =>
            this.logs.Add(new(DateTime.Now, this.Nome, Logger.Falha, erro.ToString().SemQuebraDeLinha()));

        internal static Tarefa? Criar(string? descricao, Parametros? parametros = null) =>
            descricao?.ToLower() switch
            {
                "remessa" => new Remessa(),
                "retorno" => new Retorno(parametros),
                _ => null
            };

        public override string ToString() => this.Nome;

        internal IEnumerable<Logger.Registro> Logs => this.logs;
    }
}