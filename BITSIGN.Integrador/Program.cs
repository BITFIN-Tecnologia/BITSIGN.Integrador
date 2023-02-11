// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

using System.Globalization;
using System.Text;

namespace BITSIGN.Integrador
{
    internal class Program
    {
        const string Executavel = "BITSIGN.Integrador.exe";
        static readonly CultureInfo cultura = new("pt-BR");

        static async Task Main(string[] args)
        {
            Setup.Configurar();

            if (args?.Length > 0 && args[0].ToLower() is "?" or "ajuda")
            {
                ExibirAjuda();
            }
            else
            {
                using (var log = new Logger())
                {
                    foreach (var tarefa in Criar(args))
                    {
                        if (tarefa != null)
                        {
                            var sucesso = true;
                            log.Log(tarefa.Nome, $"Início da Tarefa");

                            try
                            {
                                await tarefa.Executar();
                            }
                            catch (Exception ex)
                            {
                                log.Log(tarefa.Nome, ex);
                                sucesso = false;
                            }
                            finally
                            {
                                foreach (var l in tarefa.Logs)
                                    log.Log(l);

                                log.Log(tarefa.Nome, sucesso ? "Executada com Sucesso" : "Falha na Execução", sucesso ? Logger.Informativo : Logger.Alerta);
                                log.Log(tarefa.Nome, $"Fim da Tarefa");
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<Tarefa?> Criar(string[]? args)
        {
            var parametros = ConfigurarParametros(args);

            if (args?.Length == 1)
            {
                yield return Tarefa.Criar(args[0], parametros);
            }
            else
            {
                yield return Tarefa.Criar("Remessa");
                yield return Tarefa.Criar("Retorno", parametros);
            }
        }

        private static Parametros? ConfigurarParametros(string[]? args)
        {
            if (args?.Length > 0)
            {
                if (args[0] == "Retorno")
                {
                    if (
                        DateTime.TryParseExact(args[1], "dd/MM/yyyy", cultura, DateTimeStyles.None, out var dataInicial) &&
                        DateTime.TryParseExact(args[2], "dd/MM/yyyy", cultura, DateTimeStyles.None, out var dataFinal))
                        return new(dataInicial, dataFinal);
                    else if (Guid.TryParse(args[1], out var loteId))
                        return new(loteId);
                }
            }

            return new(new(2021, 6, 14), new(2021, 6, 14));
        }

        private static void ExibirAjuda() =>
            Console.WriteLine(
                new StringBuilder()
                    .AppendLine("BITSIGN.Integrador")
                    .AppendLine("Envio e Recebimentos de documentos para assinatura.")
                    .AppendLine($"Copyright (C) {DateTime.Now.Year} - BITSIGN(R) - Assinaturas Digitais")
                    .AppendLine()
                    .AppendLine("Uso:")
                    .AppendLine("    Sincronizar (Remessa e Retorno):")
                    .AppendLine($"        {Executavel}")
                    .AppendLine("    Enviar Lotes:")
                    .AppendLine($"        {Executavel} Remessa")
                    .AppendLine("    Recepcionar Lotes:")
                    .AppendLine($"        {Executavel} Retorno")
                    .AppendLine("    Recepcionar Lotes de um Período:")
                    .AppendLine($"        {Executavel} Retorno 00/00/0000 00/00/0000")
                    .AppendLine("    Recepcionar Lote Específico:")
                    .AppendLine($"        {Executavel} Retorno {Guid.Empty}")
                    .ToString());
    }
}