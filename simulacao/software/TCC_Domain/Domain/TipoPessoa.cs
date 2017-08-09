using TCC_Infrastructure.Attributes;

namespace TCC_Domain.Domain
{
    public enum TipoPessoa
    {
        [KVAttribute("1", "Pessoa Física")]
        PessoaFisica =1,

        [KVAttribute("2", "Pessoa Jurídica")]
        PessoaJuridica
    }
}
