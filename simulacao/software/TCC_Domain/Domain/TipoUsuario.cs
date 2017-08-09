using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum TipoUsuario
    {
        [KVAttribute("1", "Taxista")]
        Taxista = 1,

        [KVAttribute("2", "Cliente")]
        Cliente,

        [KVAttribute("3", "Administrador")]
        Administrador
    }
}
