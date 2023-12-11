// Copyright (c) 2023 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal class Remessa : Tarefa
    {
        internal Remessa()
            : base("Remessa") { }

        internal override async Task Executar(CancellationToken cancellationToken = default)
        {
            foreach (var arquivo in Directory.GetFiles(Configuracoes.Paths.Remessas, "*.zip"))
            {
                this.Log($"Envio do Arquivo: {arquivo}");

                var resposta = await proxy.Lotes.Upload(File.ReadAllBytes(arquivo), cancellationToken);

                this.Log($"Id: {resposta.Id}");
                this.Log($"Url: {resposta.Url}");

                var arquivoEnviado = Utilitarios.GerarNomeDeArquivo(Configuracoes.Paths.RemessasEnviadas, arquivo);
                File.Move(arquivo, arquivoEnviado);
                this.Log($"Arquivo movido para: {arquivoEnviado}");
            }
        }
    }
}