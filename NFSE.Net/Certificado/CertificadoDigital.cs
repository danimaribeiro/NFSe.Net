using NFSE.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net.Certificado
{
    /// <summary>
    /// Classe para trabalhar com certificados digitais
    /// </summary>
    public class CertificadoDigital
    {
        #region Propriedades da classe

        /// <summary>
        /// Certificado selecionado pelo método SelecionarCertificado()
        /// </summary>
        public X509Certificate2 oCertificado { get; private set; }
        /// <summary>
        /// True significa que o certificado informado para o método "PrepInfCertificado()" 
        /// foi localizado e os dados foram preparados, false significa que o certificado 
        /// não foi localizado.
        /// </summary>
        public bool lLocalizouCertificado { get; private set; }
        /// <summary>
        /// Data inicial da validade do certificado
        /// </summary>
        public DateTime dValidadeInicial { get; private set; }
        /// <summary>
        /// Data final da validade do certificado
        /// </summary>
        public DateTime dValidadeFinal { get; private set; }
        /// <summary>
        /// Subject do Certificado, Razão Social da Empresa Certificada, CNPJ, etc...
        /// </summary>
        public string sSubject { get; private set; }

        #endregion

        /// <summary>
        /// Método responsável por abrir um browse para selecionar o 
        /// certificado digital que será utilizado para autenticação
        /// dos WebServices e gravar ele no atributo oCertificado
        /// </summary>
        /// <returns>
        /// Retorna se o certificado foi selecionado corretamente ou não.
        /// true  = foi selecionado corretamente.
        /// false = não foi selecionado, algum problema ocorreu ou foi cancelado o selecionamento pelo usuário.
        /// </returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public bool SelecionarCertificado()
        {
            bool vRetorna;

            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";               
                vRetorna = false;
            }
            else
            {
                oX509Cert = scollection[0];
                oCertificado = oX509Cert;
                vRetorna = true;
            }

            return vRetorna;
        }

        /// <summary>
        /// Exibi uma tela com o certificado digital selecionado para ser
        /// utilizado na integração com os WEBServices da NFe
        /// </summary>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public void ExibirCertSel()
        {
            if (this.oCertificado == null)
            {
                throw new Exception("Nenhum certificado foi selecionado.");
            }
            else
            {
                X509Certificate2UI.DisplayCertificate(oCertificado);
            }
        }

        /// <summary>
        /// Pega algumas informações do certificado digital informado por parâmetro para o método
        /// e disponibiliza em propriedades para utilização
        /// </summary>
        /// <param name="pCertificado">Certificado de onde é para extrair as informações</param>
        /// <example>
        /// CertificadoDigitalClass oCertDig = new CertificadoDigitalClass();
        /// if (oCertDig.SelecionarCertificado() == true)
        /// {
        ///    oCertDig.SelecionarCertificado(); //Selecionar o certificado atualizando a propriedade "oCertificado"
        ///    oCertDig.PrepInfCertificado(oCertDig.oCertificado); //Preparar as informações do certificado
        ///    MessageBox.Show(oCertDig.sSubject); //Demonstra o subject do certificado
        /// }
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>24/01/2009</date>
        public void PrepInfCertificado(X509Certificate2 certificado)
        {
            try
            {
                sSubject = certificado.Subject;
                dValidadeInicial = certificado.NotBefore;
                dValidadeFinal = certificado.NotAfter;
                lLocalizouCertificado = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Certificado digita está vencido ou não
        /// </summary>
        /// <param name="emp">Empresa que é para ser verificado o certificado</param>
        /// <returns>true = Certificado Vencido</returns>
        public bool Vencido(int emp)
        {
            bool retorna = false;

            PrepInfCertificado(Empresa.Configuracoes[emp].X509Certificado);

            if (lLocalizouCertificado == true)
            {
                if (DateTime.Compare(DateTime.Now, dValidadeFinal) > 0)
                {
                    retorna = true;
                }
            }

            return retorna;
        }
    }
}
