// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal static class Setup
    {
        internal static void Configurar()
        {
            CriarDiretorio(Configuracoes.Paths.Remessas);
            CriarDiretorio(Configuracoes.Paths.RemessasEnviadas);
            CriarDiretorio(Configuracoes.Paths.Retornos);
            CriarDiretorio(Configuracoes.Paths.Logs);
        }

        private static void CriarDiretorio(string diretorio)
        {
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
        }
    }
}