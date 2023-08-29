// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

using Microsoft.Extensions.Configuration;

namespace BITSIGN.Integrador
{
    internal class Configuracoes
    {
        private static readonly IConfiguration config;

        static Configuracoes()
        {
            config =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        internal class Paths
        {
            internal static string Dados => config["Paths:Dados"];

            internal static string Remessas => Path.Combine(Dados, "Remessas");

            internal static string RemessasEnviadas => Path.Combine(Dados, "Remessas", "Enviadas");

            internal static string Retornos => Path.Combine(Dados, "Retornos");

            internal static string Logs => Path.Combine(Dados, "Logs");
        }
    }
}