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
        public bool Vencido(Empresa empresa)
        {
            bool retorna = false;

            PrepInfCertificado(empresa.X509Certificado);

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
