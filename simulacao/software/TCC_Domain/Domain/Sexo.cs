using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum Sexo
    {
        [KVAttribute("1", "Feminino")]
        Feminino=1,

        [KVAttribute("2", "Masculino")]
        Masculino
    }
}
