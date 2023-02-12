// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal static class Utilitarios
    {
        internal static string SemQuebraDeLinha(this string valor) =>
            valor.ToString().Replace(Environment.NewLine, " ");

        internal static string GerarNomeDeArquivo(string diretorioDeDestino, string nomeDoArquivoDeOrigem)
        {
            var prefixo = Path.GetFileNameWithoutExtension(nomeDoArquivoDeOrigem);
            var extensao = Path.GetExtension(nomeDoArquivoDeOrigem);
            var contador = 0;
            string? temp;

            do
            {
                temp = Path.Combine(diretorioDeDestino, $"{prefixo}-{++contador:000}{extensao}");
            } while (File.Exists(temp));

            return temp;
        }
    }
}