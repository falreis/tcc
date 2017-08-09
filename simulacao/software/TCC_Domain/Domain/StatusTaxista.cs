using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum StatusTaxista
    {
        [KVAttribute("0", "Pendente")]
        Pendente = 0,

        [KVAttribute("1", "Sem Status")]
        SemStatus,

        [KVAttribute("2", "TaxiLivre")]
        TaxiLivre,

        [KVAttribute("3", "Aguardando Confirmação Requisição")]
        AguardandoConfirmacaoRequisicao,

        [KVAttribute("4", "Em Atendimento")]
        EmAtendimento,

        [KVAttribute("5", "Suspenso")]
        Suspenso,

        [KVAttribute("6", "Fora Circulacao")]
        ForaCirculacao

        
    }
}
