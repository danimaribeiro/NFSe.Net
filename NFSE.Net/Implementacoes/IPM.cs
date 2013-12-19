using NFSE.Net.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Implementacoes
{
    namespace Exceptions
    {
        /// <summary>
        /// Serviço não disponível para o padrão IPM
        /// </summary>
        public class ServicoInexistenteIPMException : ServicoInexistenteException
        {
            public override string Message
            {
                get
                {
                    return "Serviço não disponível para padrão IPM";
                }
            }
        }
    }

    /// <summary>
    /// Emite notas fiscais de serviço no padrão IPM
    /// </summary>
    public class IPM : IEmiteNFSeIPM
    {
        #region Propriedades
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public int Cidade { get; set; }
        public IWebProxy Proxy { get; set; }
        public string PastaRetorno { get; set; }
        #endregion

        #region Construtores
        public IPM(string usuario, string senha, int cidade, string caminhoRetorno)
        {
            Usuario = usuario;
            Senha = senha;
            Cidade = cidade;
            PastaRetorno = caminhoRetorno;
        }
        #endregion

        public string EmitirNF(string file, TpAmb tpAmb, bool cancelamento = false)
        {
            string result = "";

            using (POSTRequest post = new POSTRequest { Proxy = Proxy })
            {
                //                                                                                                    informe 1 para retorno em xml
                result = post.PostForm("http://www.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php?eletron=1", new Dictionary<string, string> {
                     {"login", Usuario  },  //CPF/CNPJ, sem separadores}
                     {"senha", Senha},      //Senha de acesso ao sistema: www.nfse.
                     {"cidade", Cidade.ToString()},   //Código da cidade na receita federal (TOM), pesquisei o código em http://www.ekwbrasil.com.br/municipio.php3.
                     {"f1", file}           //Endereço físico do arquivo
                });
            }

            if (!cancelamento)
                GerarRetorno(file, result, Propriedade.ExtEnvio.EnvLoteRps, Propriedade.ExtRetorno.RetLoteRps);
            else
                GerarRetorno(file, result, Propriedade.ExtEnvio.EnvCancelamento_XML, Propriedade.ExtRetorno.CanNfse);

            return result;

        }

        public void GerarRetorno(string file, string result, string extEnvio, string extRetorno)
        {
            FileInfo fi = new FileInfo(file);
            string nomearq = PastaRetorno + "\\" + fi.Name.Replace(extEnvio, extRetorno);

            StreamWriter write = new StreamWriter(nomearq);
            write.Write(result);
            write.Flush();
            write.Close();
            write.Dispose();
        }
    }
}
