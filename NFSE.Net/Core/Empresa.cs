using NFSE.Net.Certificado;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NFSE.Net.Core
{

    /// <summary>
    /// Classe contém os dados da empresa e suas configurações
    /// </summary>
    /// <remarks>
    /// Autor: Wandrey Mundin Ferreira
    /// Data: 28/07/2010
    /// </remarks>
    public class Empresa
    {       
        #region Propriedades diversas
        /// <summary>
        /// CNPJ da Empresa
        /// </summary>
        public string CNPJ { get; set; }
        /// <summary>
        /// Nome da Empresa
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Código da unidade Federativa da Empresa
        /// </summary>
        public int CodigoMunicipio { get; set; }
        /// <summary>
        /// Inscrição municipal da empresa
        /// </summary>
        public string InscricaoMunicipal { get; set; }
        /// <summary>
        /// Ambiente a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpAmb { get; set; }
        /// <summary>
        /// Tipo de emissão a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpEmis { get; set; }
        /// <summary>
        /// Define a utilização do certficado instalado no windows ou através de arquivo
        /// </summary>
        public bool CertificadoInstalado { get; set; }
        /// <summary>
        /// Quando utilizar o certificado através de arquivo será necessário informar o local de armazenamento do certificado digital
        /// </summary>
        public string CertificadoArquivo { get; set; }
        /// <summary>
        /// Quando utilizar o certificado através de arquivo será necessário informar a senha do certificado
        /// </summary>
        public string CertificadoSenha { get; set; }
        /// <summary>
        /// Certificado digital - Subject
        /// </summary>
        public string Certificado { get; set; }
        /// <summary>
        /// Certificado digital - ThumbPrint
        /// </summary>
        public string CertificadoThumbPrint { get; set; }
        /// <summary>
        /// Certificado digital
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public X509Certificate2 X509Certificado { get; set; }
        /// <summary>
        /// Usuário de acesso ao webservice (Utilizado pelo UniNFS-e para algumas prefeituras)
        /// </summary>
        public string UsuarioWS { get; set; }
        /// <summary>
        /// Senha de acesso ao webservice (Utilizado pelo UniNFS-e para algumas prefeituras)
        /// </summary>
        public string SenhaWS { get; set; }
        #endregion

        #region Propriedades da parte das configurações por empresa


        /// <summary>
        /// Nome da pasta onde é gravado as configurações e informações da Empresa
        /// </summary>
        public string PastaEmpresa { get; set; }
        /// <summary>
        /// Nome do arquivo XML das configurações da empresa
        /// </summary>
        public string NomeArquivoConfig { get; set; }

        #endregion

        #region Propriedade para controle do nome da pasta a serem salvos os XML´s enviados
        private DiretorioSalvarComo mDiretorioSalvarComo = "";
        /// <summary>
        /// Define como devem ser salvos os diretórios dentro do Uninfe.
        /// <para>por enqto apenas usa a data e os valores possíveis para definir são:</para>
        /// <para>    A para ANO</para>
        /// <para>    M para MES</para>
        /// <para>    D para DIA</para>
        /// <para>    pode se passar como desejar</para>
        /// <para>    Ex:</para>
        /// <para>        AMD   para criar a pasta como ..\Enviados\Autorizados\ANOMESDIA\nfe.xml</para>
        /// <para>        AM    para criar a pasta como ..\Enviados\Autorizados\ANOMES\nfe.xml</para>
        /// <para>        AD    para criar a pasta como ..\Enviados\Autorizados\ANODIA\nfe.xml</para>
        /// <para>        A\M\D para criar a pasta como ..\Enviados\Autorizados\ANO\MES\DIA\nfe.xml</para>
        /// <para>        podem ser criadas outras combinações, ficando a critério do usuário</para>
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        [XmlIgnore]
        public DiretorioSalvarComo DiretorioSalvarComo
        {
            get
            {
                if (string.IsNullOrEmpty(mDiretorioSalvarComo))
                    mDiretorioSalvarComo = "AM";//padrão

                return mDiretorioSalvarComo;
            }

            set { mDiretorioSalvarComo = value; }
        }
        #endregion


        #region Coleções
        /// <summary>
        /// Configurações por empresa
        /// </summary>
        [XmlIgnore]
        public static List<Empresa> Configuracoes = new List<Empresa>();
        /// <summary>
        /// Objetos dos serviços da NFe
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, WebServiceProxy> WSProxy = new Dictionary<string, WebServiceProxy>();

        [XmlIgnore]
        public List<string> ErrosValidacao { get; set; }
        #endregion

        /// <summary>
        /// Empresa
        /// danasa 20-9-2010
        /// </summary>
        public Empresa()
        {              
            this.ErrosValidacao = new List<string>();
        }
     
        #region "Carrega Configurações de empresa"

        /// <summary>
        /// Busca as configurações da empresa dentro de sua pasta gravadas em um XML chamado UniNfeConfig.Xml
        /// </summary>
        public static void CarregarEmpresasConfiguradas()
        {
            Empresa.Configuracoes.Clear();
            var empresas = Empresas.CarregarEmpresasCadastradas();
            foreach (var item in empresas.ListaEmpresas)
            {
                string caminhoConfiguracaoEmpresa = System.IO.Path.Combine(Propriedade.PastaExecutavel, item.Cnpj, "nfse", Propriedade.NomeArqConfig);
                if (System.IO.File.Exists(caminhoConfiguracaoEmpresa))
                {
                    var serializador = new Layouts.Serializador();
                    var empresa = serializador.LerXml<Empresa>(caminhoConfiguracaoEmpresa);
                    if (!string.IsNullOrWhiteSpace(empresa.CertificadoSenha))
                        empresa.CertificadoSenha = Criptografia.descriptografaSenha(empresa.CertificadoSenha);
                    else
                        empresa.ErrosValidacao.Add("A senha do certificado é inválida.");
                    empresa.X509Certificado = BuscaConfiguracaoCertificado(empresa);

                    Empresa.Configuracoes.Add(empresa);
                }
                else
                    throw new Exception(string.Format("O arquivo de configuração da empresa: {0} - {1} não existe ", item.Nome, item.Cnpj));
            }
        }

        #endregion
        
        #region Reset certificado
        /// <summary>
        /// Reseta o certificado da empresa e recria o mesmo
        /// </summary>
        /// <param name="index">identificador da empresa</param>
        /// <returns></returns>
        public static X509Certificate2 ResetCertificado(Core.Empresa empresa)
        {
            empresa.X509Certificado.Reset();


            empresa.X509Certificado = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Ajustar o certificado digital de String para o tipo X509Certificate2
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = null;
            if (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint))
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByThumbprint, empresa.CertificadoThumbPrint, false);
            else
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, empresa.Certificado, false);

            for (int i = 0; i < collection1.Count; i++)
            {
                //Verificar a validade do certificado
                if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                {
                    empresa.X509Certificado = collection1[i];
                    break;
                }
            }

            //Se não encontrou nenhum certificado com validade correta, vou pegar o primeiro certificado, porem vai travar na hora de tentar enviar a nota fiscal, por conta da validade. Wandrey 06/04/2011
            if (empresa.X509Certificado == null && collection1.Count > 0)
                empresa.X509Certificado = collection1[0];

            return empresa.X509Certificado;

        }
        #endregion

        #region BuscaConfiguracaoCertificado
        public static X509Certificate2 BuscaConfiguracaoCertificado(Empresa empresa)
        {
            //Ajustar o certificado digital de String para o tipo X509Certificate2
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = new X509Certificate2Collection();
            if (!string.IsNullOrEmpty(empresa.CertificadoThumbPrint))
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByThumbprint, empresa.CertificadoThumbPrint, false);
            else if (!string.IsNullOrWhiteSpace(empresa.Certificado))
                collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, empresa.Certificado, false);

            for (int i = 0; i < collection1.Count; i++)
            {
                //Verificar a validade do certificado
                if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                {
                    empresa.X509Certificado = collection1[i];
                    break;
                }
            }

            if (empresa.X509Certificado == null && !string.IsNullOrWhiteSpace(empresa.CertificadoArquivo))
            {
                try
                {
                    using (FileStream fs = new FileStream(empresa.CertificadoArquivo, FileMode.Open))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        empresa.X509Certificado = new X509Certificate2(buffer, empresa.CertificadoSenha);
                    }
                }
                catch (System.Security.Cryptography.CryptographicException e)
                {
                    empresa.ErrosValidacao.Add(e.Message);
                }
                catch (System.IO.DirectoryNotFoundException d)
                {
                    empresa.ErrosValidacao.Add(d.Message);
                }
                catch (System.IO.FileNotFoundException f)
                {
                    empresa.ErrosValidacao.Add(f.Message);
                }
                catch (Exception)
                {
                    empresa.ErrosValidacao.Add("Configurações do certificado são inválidas.");
                }
            }
            return empresa.X509Certificado;
        }

        #endregion

    }
}
