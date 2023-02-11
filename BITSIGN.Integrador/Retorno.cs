// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal class Retorno : Tarefa
    {
        private readonly Parametros? parametros;

        internal Retorno(Parametros? parametros = null)
            : base("Retorno")
        {
            this.parametros = parametros;
        }

        internal override async Task Executar(CancellationToken cancellationToken = default)
        {
            if (parametros?.LoteId == null)
            {
                this.Log($"Busca por Lotes: {parametros?.DataInicial ?? DateTime.Now.Date:dd/MM/yyyy} - {parametros?.DataFinal ?? DateTime.Now:dd/MM/yyyy}");

                var lotes = await proxy.Buscador.Lotes(new()
                {
                    BaseDaData = "Status",
                    DataInicial = parametros?.DataInicial ?? DateTime.Now.Date,
                    DataFinal = parametros?.DataFinal ?? DateTime.Now,
                    Status = "Assinado"
                }, cancellationToken);

                if (lotes.Dados?.Any() ?? false)
                    foreach (var lote in lotes.Dados)
                        await Salvar(lote, cancellationToken);
            }
            else
            {
                this.Log($"Busca pelo Lote: {parametros.LoteId}");

                var lote = await proxy.Lotes.Detalhes(parametros.LoteId.Value, cancellationToken);

                if (lote != null)
                    await Salvar(lote, cancellationToken);
            }
        }

        private async Task Salvar(Proxy.DTOs.Lote lote, CancellationToken cancellationToken)
        {
            var path = Path.Combine(Configuracoes.Paths.Retornos, $"{lote.Id}.zip");

            if (lote.Status == "Assinado" && !File.Exists(path))
            {
                var pacote = await proxy.Lotes.Pacote(lote.Id, cancellationToken);

                if (pacote != null)
                {
                    this.Log($"Armazenando o Pacote: {lote.Id}.zip");
                    File.WriteAllBytes(path, pacote.Conteudo);
                }
            }
            else
            {
                this.Log($"Já existe um arquivo com o nome: {lote.Id}.zip");
            }
        }
    }
}