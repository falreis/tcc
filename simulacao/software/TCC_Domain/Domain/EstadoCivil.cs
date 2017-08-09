using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum EstadoCivil 
    {
        [KVAttribute("1", "Solteiro")]
        Solteiro = 1,

        [KVAttribute("2", "Casado")]
        Casado,

        [KVAttribute("3", "Separado")]
        Separado,

        [KVAttribute("4", "Divorciado")]
        Divorciado,

        [KVAttribute("5", "Viúvo")]
        Viuvo
    }
}
