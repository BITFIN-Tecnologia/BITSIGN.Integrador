// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal static class Utilitarios
    {
        internal static string SemQuebraDeLinha(this string valor) =>
            valor.ToString().Replace(" ", Environment.NewLine);

        internal static string GerarNomeDeArquivo(string diretorioDeDestino, string nomeDoArquivoDeOrigem)
        {
            var temp = "";

            return Path.Combine(diretorioDeDestino, "");
        }
    }
}