using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum StatusCliente
    {
        [KVAttribute("0", "Pendente")]
        Pendente = 0,

        [KVAttribute("1", "Sem Status")]
        SemStatus,

        [KVAttribute("2", "Aguardando Atendimento")]
        AguardandoAtendimento,

        [KVAttribute("3", "Em Atendimento")]
        EmAtendimento
    }
}
