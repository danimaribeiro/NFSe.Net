using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    /// <summary>
    /// Interface para assinaturas de classe que emitem a NFe
    /// </summary>
    public interface IEmiteNFSeIPM : IEmiteNfIPM
    {
        /// <summary>
        /// Código da cidade para emissão da NF de serviço
        /// <para>Em alguns sistemas é utilizado o código da cidade da receita federal, este arquivo pode ser encontrado em ~\uninfe\doc\Codigos_Cidades_Receita_Federal.xls</para>
        /// </summary>
        int Cidade { get; set; }

        /// <summary>
        /// Proxy para ser utilizado em requisições do tipo POST. Pode ser nulo
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        ///Caminho para salvar o xml de retorno
        /// </summary>
        string CaminhoXmlRetorno { get; set; }
    }
}
