// Copyright (c) 2023 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal static class Setup
    {
        internal static bool Configurar()
        {
            if (Directory.Exists(Configuracoes.Paths.Dados))
            {
                CriarDiretorio(Configuracoes.Paths.Remessas);
                CriarDiretorio(Configuracoes.Paths.RemessasEnviadas);
                CriarDiretorio(Configuracoes.Paths.Retornos);
                CriarDiretorio(Configuracoes.Paths.Logs);
                return true;
            }

            return false;
        }

        private static void CriarDiretorio(string diretorio)
        {
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
        }
    }
}