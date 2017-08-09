using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum StatusRequisicao
    {
        [KVAttribute("1", "Realizada")]
        RequisicaoRealizada =1,

        [KVAttribute("2", "Em Processamento")]
        EmProcessamento,

        [KVAttribute("3", "Aguardando Resposta")]
        AguardandoResposta,

        [KVAttribute("4", "Aguardando Confirmação")]
        AguardandoConfirmacao,

        [KVAttribute("5", "Aguardando Atendimento")]
        AguardandoAtendimento,

        [KVAttribute("6", "Em Atendimento")]
        EmAtendimento,

        [KVAttribute("7", "Concluído")]
        Concluido,

        [KVAttribute("8", "Cancelado")]
        Cancelado
    }
}
