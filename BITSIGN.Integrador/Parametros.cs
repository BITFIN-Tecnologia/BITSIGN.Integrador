// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da BITSIGN.

namespace BITSIGN.Integrador
{
    internal class Parametros
    {
        public Parametros(DateTime dataInicial, DateTime dataFinal)
        {
            this.DataInicial = dataInicial;
            this.DataFinal = dataFinal;
        }

        public Parametros(Guid loteId)
        {
            this.LoteId = loteId;
        }

        internal DateTime? DataInicial { get; set; }

        internal DateTime? DataFinal { get; set; }

        internal Guid? LoteId { get; set; }
    }
}