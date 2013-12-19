using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace NFSE.Net
{
    public class WebServiceProxy
    {
        #region Propriedades
        /// <summary>
        /// Descrição do serviço (WSDL)
        /// </summary>
        private ServiceDescription serviceDescription { get; set; }
        /// <summary>
        /// Código assembly do serviço
        /// </summary>
        private Assembly serviceAssemby { get; set; }
        /// <summary>
        /// Certificado digital a ser utilizado no consumo dos serviços
        /// </summary>
        private X509Certificate2 oCertificado { get; set; }

        #region Proxy
        /// <summary>
        /// Utiliza servidor proxy?
        /// </summary>
        public bool UtilizaServidorProxy { get; set; }
        /// <summary>
        /// Endereço do servidor de proxy
        /// </summary>
        public string ProxyServidor { get; set; }
        /// <summary>
        /// Usuário para autenticação no servidor de proxy
        /// </summary>
        public string ProxyUsuario { get; set; }
        /// <summary>
        /// Senha do usuário para autenticação no servidor de proxy
        /// </summary>
        public string ProxySenha { get; set; }
        /// <summary>
        /// Porta de comunicação do servidor proxy
        /// </summary>
        public int ProxyPorta { get; set; }
        /// <summary>
        /// Arquivo WSDL
        /// </summary>
        private string ArquivoWSDL { get; set; }
        private PadroesNFSe PadraoNFSe { get; set; }
        #endregion

        /// <summary>
        /// Lista utilizada para armazenar os webservices
        /// </summary>
        public static List<webServices> webServicesList { get; private set; }

        #endregion

        #region Construtores

        public WebServiceProxy(Uri requestUri, X509Certificate2 Certificado)
        {
            //Definir o certificado digital que será utilizado na conexão com os serviços
            this.oCertificado = Certificado;

            //Confirmar a solicitação SSL automaticamente
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidation);
            //Obeter a descrção do serviço (WSDL)
            this.DescricaoServico(requestUri, this.oCertificado);

            //Gerar e compilar a classe
            this.GerarClasse();
        }

        public WebServiceProxy(string arquivoWSDL, X509Certificate2 Certificado, PadroesNFSe padraoNFSe)
        {
            ArquivoWSDL = arquivoWSDL;
            PadraoNFSe = padraoNFSe;

            //Definir o certificado digital que será utilizado na conexão com os serviços
            this.oCertificado = Certificado;

            //Confirmar a solicitação SSL automaticamente
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidation);

            //Obeter a descrção do serviço (WSDL)
            this.DescricaoServico(arquivoWSDL);

            //Gerar e compilar a classe
            this.GerarClasse();
        }

        public WebServiceProxy(X509Certificate2 Certificado)
        {
            this.oCertificado = Certificado;
        }

        #endregion

        #region Métodos públicos

        #region ReturnArray()
        /// <summary>
        /// Método que verifica se o tipo de retornjo de uma operação/método é array ou não
        /// </summary>
        /// <param name="Instance">Instancia do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <returns>true se o tipo de retorno do método passado por parâmetro for um array</returns>
        public bool ReturnArray(object Instance, string methodName)
        {
            Type tipoInstance = Instance.GetType();

            return tipoInstance.GetMethod(methodName).ReturnType.IsSubclassOf(typeof(Array));
        }
        #endregion

        #region Invoke()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Objeto - Um objeto somente, podendo ser primário ou complexo</returns>
        public object Invoke(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region InvokeXML()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Um objeto do tipo XML</returns>
        public XmlNode InvokeXML(object Instance, string methodName, object[] parameters)
        {
            XmlNode xmlNode = null;

            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            xmlNode = (XmlNode)tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);

            return xmlNode;
        }
        #endregion

        #region InvokeXML()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Um objeto do tipo string</returns>
        public string InvokeStr(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return (string)tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region SetProp()
        /// <summary>
        /// Alterar valor das propriedades da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="propertyName">Nome da propriedade</param>
        /// <param name="novoValor">Novo valor para ser gravado na propriedade</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 09/02/2010
        /// </remarks>
        public void SetProp(object instance, string propertyName, object novoValor)
        {
            Type tipoInstance = instance.GetType();
            PropertyInfo property = tipoInstance.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            property.SetValue(instance, novoValor, null);
        }
        #endregion

        #region GetProp()
        /// <summary>
        /// Alterar valor das propriedades da classe
        /// </summary>
        /// <param name="instance">Instância do objeto</param>
        /// <param name="propertyName">Nome da propriedade</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 09/02/2010
        /// </remarks>
        public object GetProp(object instance, string propertyName)
        {
            Type tipoInstance = instance.GetType();
            PropertyInfo property = tipoInstance.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            return property.GetValue(instance, null);
        }
        #endregion

        #region InvokeArray()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Vetor de objetos - uma lista de objetos primários ou complexos</returns>
        public object[] InvokeArray(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates;
            tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return (object[])tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region CertificateValidation
        /// <summary>
        /// Responsável por retornar uma confirmação verdadeira para a proriedade ServerCertificateValidationCallback 
        /// da classe ServicePointManager para confirmar a solicitação SSL automaticamente.
        /// </summary>
        /// <returns>Retornará sempre true</returns>
        public bool CertificateValidation(object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErros)
        {
            return true;
        }
        #endregion

        #region CriarObjeto()
        /// <summary>
        /// Criar objeto das classes do serviço
        /// </summary>
        /// <param name="NomeClasse">Nome da classe que é para ser instanciado um novo objeto</param>
        /// <returns>Retorna o objeto</returns>
        public object CriarObjeto(string NomeClasse)
        {
            return Activator.CreateInstance(this.serviceAssemby.GetType(NomeClasse));
        }
        #endregion

        #endregion

        #region Métodos privados

        #region DescricaoServico()
        /// <summary>
        /// Obter a descrição completa do serviço, ou seja, o WSDL do webservice a partir de uma URL
        /// </summary>
        /// <param name="requestUri">Uri (endereço https) para obter o WSDL</param>
        /// <param name="Certificado">Certificado digital</param>
        private void DescricaoServico(Uri requestUri, X509Certificate2 Certificado)
        {
            //Forçar utilizar o protocolo SSL 3.0 que está de acordo com o manual de integração do SEFAZ
            //Wandrey 31/03/2010
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            //Definir o endereço para a requisição do wsdl
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

            //Definir dados para conexão com Proxy. Wandrey 22/11/2010
            if (UtilizaServidorProxy)
            {
                request.Proxy = Proxy.DefinirProxy(ProxyServidor, ProxyUsuario, ProxySenha, ProxyPorta);
            }

            //Definir o certificado digital que deve ser utilizado na requisição do wsdl
            request.ClientCertificates.Add(Certificado);

            //Requisitar o WSDL e gravar em um stream                
            Stream stream = request.GetResponse().GetResponseStream();

            //Definir a descrição completa do servido (WSDL)
            this.serviceDescription = ServiceDescription.Read(stream);
        }
        #endregion

        #region DescricaoServico()
        /// <summary>
        /// Obter a descrição completa do serviço, ou seja, o WSDL do webservice de um arquivo local
        /// </summary>
        /// <param name="arquivoWSDL">Local e nome do arquivo WDDL</param>
        /// <param name="Certificado">Certificado digital</param>
        private void DescricaoServico(string arquivoWSDL)
        {
            //Forçar utilizar o protocolo SSL 3.0 que está de acordo com o manual de integração do SEFAZ
            //Wandrey 31/03/2010
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            //Definir a descrição completa do servido (WSDL)
            //this.serviceDescription = ServiceDescription.Read(stream);
            this.serviceDescription = ServiceDescription.Read(arquivoWSDL);
        }
        #endregion

        #region GerarClasse()
        /// <summary>
        /// Gerar o source code do serviço
        /// </summary>
        private void GerarClasse()
        {
            #region Gerar o código da classe
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromNamespace(GerarGrafo(), writer, null);
            #endregion

            string codigoClasse = writer.ToString();

            #region Compilar o código da classe
            CompilerResults results = provider.CompileAssemblyFromSource(ParametroCompilacao(), codigoClasse);
            serviceAssemby = results.CompiledAssembly;
            #endregion
        }
        #endregion

        #region ParametroCompilacao
        /// <summary>
        /// Montar os parâmetros para a compilação da classe
        /// </summary>
        /// <returns>Retorna os parâmetros</returns>
        private CompilerParameters ParametroCompilacao()
        {
            CompilerParameters parameters = new CompilerParameters(new string[] { "System.dll", "System.Xml.dll", "System.Web.Services.dll", "System.Data.dll" });
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.TreatWarningsAsErrors = false;
            parameters.WarningLevel = 4;

            return parameters;
        }
        #endregion

        #region GerarGrafo()
        /// <summary>
        /// Gerar a estrutura e o grafo da classe
        /// </summary>
        private CodeNamespace GerarGrafo()
        {
            #region Gerar a estrutura da classe do serviço
            //Gerar a estrutura da classe do serviço
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.AddServiceDescription(this.serviceDescription, string.Empty, string.Empty);

            //Definir o nome do protocolo a ser utilizado
            //Não posso definir, tenho que deixar por conta do WSDL definir, ou pode dar erro em alguns estados
            //importer.ProtocolName = "Soap12";
            //importer.ProtocolName = "Soap";

            //Tipos deste serviço devem ser gerados como propriedades e não como simples campos
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
            #endregion

            #region Se a NFSe for padrão DUETO/WEBISS/SALVADOR_BA/PRONIN preciso importar os schemas do WSDL
            if (Propriedade.TipoAplicativo == TipoAplicativo.Nfse && (PadraoNFSe == PadroesNFSe.DUETO || PadraoNFSe == PadroesNFSe.WEBISS || PadraoNFSe == PadroesNFSe.SALVADOR_BA || PadraoNFSe == PadroesNFSe.GIF || PadraoNFSe == PadroesNFSe.PRONIN))
            {
                //Tive que utilizar a WebClient para que a OpenRead funcionasse, não foi possível fazer funcionar com a SecureWebClient. Tem que analisar melhor. Wandrey e Renan 10/09/2013
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(ArquivoWSDL);

                //Esta sim tem que ser com a SecureWebClient pq tem que ter o certificado. Wandrey 10/09/2013
                SecureWebClient client2 = new SecureWebClient(oCertificado);

                // Add any imported files
                foreach (System.Xml.Schema.XmlSchema wsdlSchema in serviceDescription.Types.Schemas)
                {
                    foreach (System.Xml.Schema.XmlSchemaObject externalSchema in wsdlSchema.Includes)
                    {
                        if (externalSchema is System.Xml.Schema.XmlSchemaImport)
                        {
                            Uri baseUri = new Uri(ArquivoWSDL);
                            Uri schemaUri = new Uri(baseUri, ((System.Xml.Schema.XmlSchemaExternal)externalSchema).SchemaLocation);
                            stream = client2.OpenRead(schemaUri);
                            System.Xml.Schema.XmlSchema schema = System.Xml.Schema.XmlSchema.Read(stream, null);
                            importer.Schemas.Add(schema);
                        }
                    }
                }
            }
            #endregion

            #region Gerar o o grafo da classe para depois gerar o código
            CodeNamespace @namespace = new CodeNamespace();
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(@namespace);
            ServiceDescriptionImportWarnings warmings = importer.Import(@namespace, unit);
            #endregion

            return @namespace;
        }
        #endregion

        #region RelacCertificado
        /// <summary>
        /// Relacionar o certificado digital com o serviço que será consumido do webservice
        /// </summary>
        /// <param name="instance">Objeto do serviço que será consumido</param>
        private void RelacCertificado(object instance)
        {
            Type tipoInstance = instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, instance, new Object[] { });
            Type tipoClientCertificates;
            tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });
        }
        #endregion

        #endregion

        #region Objeto da BETHA Sistemas para acessar os WebServices da NFSe
        public IBetha Betha;
        #endregion

        #region CarregaWebServicesList()
        /// <summary>
        /// Carrega a lista de webservices definidos no arquivo WebService.XML
        /// </summary>
        public static bool CarregaWebServicesList()
        {
            bool atualizaWSDL = false;
            if (webServicesList == null)
            {
                webServicesList = new List<webServices>();

                XmlDocument doc = new XmlDocument();
                /// danasa 1-2012
                if (Propriedade.TipoAplicativo == TipoAplicativo.Nfse)
                {
                    Propriedade.Municipios = null;
                    Propriedade.Municipios = new List<Municipio>();

                    if (File.Exists(Propriedade.NomeArqXMLMunicipios))
                    {
                        doc.Load(Propriedade.NomeArqXMLMunicipios);
                        XmlNodeList estadoList = doc.GetElementsByTagName("Registro");
                        foreach (XmlNode registroNode in estadoList)
                        {
                            XmlElement registroElemento = (XmlElement)registroNode;
                            if (registroElemento.Attributes.Count > 0)
                            {
                                int IDmunicipio = Convert.ToInt32(registroElemento.Attributes[0].Value);
                                string Nome = registroElemento.Attributes[1].Value;
                                string Padrao = registroElemento.Attributes[2].Value;
                                string UF = Functions.CodigoParaUF(Convert.ToInt32(IDmunicipio.ToString().Substring(0, 2))).Substring(0, 2);

                                ///
                                /// danasa 9-2013
                                /// verifica se o 'novo' padrao existe, nao existindo retorna para atualizar os wsdl's dele
                                string dirSchemas = Path.Combine(Propriedade.PastaExecutavel, "schemas\\NFSe\\" + Padrao);
                                if (!Directory.Exists(dirSchemas))
                                {
                                    atualizaWSDL = true;
                                }
                                PadroesNFSe pdr = WebServiceNFSe.GetPadraoFromString(Padrao);

                                ///
                                /// adiciona na lista que será usada na manutencao
                                Propriedade.Municipios.Add(new Municipio(IDmunicipio, UF, Nome, pdr));

                                webServices wsItem = new webServices(IDmunicipio, Nome, UF);

                                //PreencheURLw(wsItem.URLHomologacao, "URLHomologacao", WebServiceNFSe.URLHomologacao(pdr), "");
                                //PreencheURLw(wsItem.URLProducao, "URLProducao", WebServiceNFSe.URLProducao(pdr), "");
                                PreencheURLw(wsItem.LocalHomologacao, "LocalHomologacao", WebServiceNFSe.WebServicesHomologacao(pdr, IDmunicipio), "");
                                PreencheURLw(wsItem.LocalProducao, "LocalProducao", WebServiceNFSe.WebServicesProducao(pdr, IDmunicipio), "");

                                webServicesList.Add(wsItem);
                            }
                        }
                        if (webServicesList.Count > 0)
                        {
                            ///
                            /// nao vou sair daqui pq pode ser que no "Webservice.xml" tenha algum municipio
                            /// que o usuário nao tenha incluido
                            /// 
                            //return;
                        }
                    }
                }
                /// danasa 1-2012

                if (File.Exists(Propriedade.NomeArqXMLWebService))
                {
                    doc.Load(Propriedade.NomeArqXMLWebService);
                    XmlNodeList estadoList = doc.GetElementsByTagName("Estado");
                    foreach (XmlNode estadoNode in estadoList)
                    {
                        XmlElement estadoElemento = (XmlElement)estadoNode;
                        if (estadoElemento.Attributes.Count > 0)
                        {
                            if (estadoElemento.Attributes[2].Value != "XX")
                            {
                                int ID = Convert.ToInt32(estadoElemento.Attributes[0].Value);
                                string Nome = estadoElemento.Attributes[1].Value;
                                string UF = estadoElemento.Attributes[2].Value;

                                /// danasa 1-2012
                                ///
                                /// verifica se o ID já está na lista
                                /// isto previne que no xml de configuracao tenha duplicidade e evita derrubar o programa
                                ///
                                bool jahExiste = false;
                                foreach (webServices temp in webServicesList)
                                    if (temp.ID == ID)
                                    {
                                        jahExiste = true;
                                        break;
                                    }
                                if (jahExiste) continue;

                                webServices wsItem = new webServices(ID, Nome, UF);
                                XmlNodeList urlList;

                                #region URL´s de Homologação
                                //urlList = estadoElemento.GetElementsByTagName("URLHomologacao");
                                //if (urlList.Count > 0)
                                //    PreencheURLw(wsItem.URLHomologacao, "URLHomologacao", urlList.Item(0).OuterXml, UF);
                                #endregion

                                #region URL´s de produção
                                //urlList = estadoElemento.GetElementsByTagName("URLProducao");
                                //if (urlList.Count > 0)
                                //    PreencheURLw(wsItem.URLProducao, "URLProducao", urlList.Item(0).OuterXml, UF);
                                #endregion

                                #region WSDL´s locais de Homologação
                                urlList = estadoElemento.GetElementsByTagName("LocalHomologacao");
                                if (urlList.Count > 0)
                                    PreencheURLw(wsItem.LocalHomologacao, "LocalHomologacao", urlList.Item(0).OuterXml, UF);
                                #endregion

                                #region WSDL´s locais de Produção
                                urlList = estadoElemento.GetElementsByTagName("LocalProducao");
                                if (urlList.Count > 0)
                                    PreencheURLw(wsItem.LocalProducao, "LocalProducao", urlList.Item(0).OuterXml, UF);
                                #endregion

                                webServicesList.Add(wsItem);

                                // danasa 1-2012
                                if (Propriedade.TipoAplicativo == TipoAplicativo.Nfse)
                                {
                                    ///
                                    /// adiciona na lista que será usada na manutencao
                                    foreach (string p0 in WebServiceNFSe.PadroesNFSeList)
                                        if (p0 != PadroesNFSe.NaoIdentificado.ToString())
                                            if (wsItem.LocalHomologacao.RecepcionarLoteRps.ToLower().IndexOf(p0.ToLower()) > 0 ||
                                                wsItem.LocalProducao.RecepcionarLoteRps.ToLower().IndexOf(p0.ToLower()) > 0)
                                            {
                                                Propriedade.Municipios.Add(new Municipio(ID, UF, Nome, WebServiceNFSe.GetPadraoFromString(p0)));
                                                break;
                                            }
                                }
                                // danasa 1-2012
                            }
                        }
                    }
                }
            }
            return atualizaWSDL;
        }
        #endregion

        #region reloadWebServicesList()
        /// <summary>
        /// Recarrega a lista de webservices
        /// usado pelo projeto da NFes quando da manutencao
        /// </summary>
        public static bool reloadWebServicesList()
        {
            webServicesList = null;
            return CarregaWebServicesList();
        }
        #endregion

        #region PreencheURLw
        private static void PreencheURLw(URLws wsItem, string tagName, string urls, string uf)
        {
            if (urls == "")
                return;

            string AppPath = Propriedade.PastaExecutavel + "\\";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(urls);
            XmlNodeList urlList = doc.ChildNodes;
            if (urlList == null)
                return;

            for (int i = 0; i < urlList.Count; ++i)
            {
                for (int j = 0; j < urlList[i].ChildNodes.Count; ++j)
                {
                    System.Reflection.PropertyInfo ClassProperty = wsItem.GetType().GetProperty(urlList[i].ChildNodes[j].Name);
                    if (ClassProperty != null)
                    {
                        string appPath = AppPath + urlList[i].ChildNodes[j].InnerText;

                        if (!string.IsNullOrEmpty(urlList[i].ChildNodes[j].InnerText))
                        {
                            if (urlList[i].ChildNodes[j].InnerText.ToLower().EndsWith("asmx?wsdl"))
                            {
                                appPath = urlList[i].ChildNodes[j].InnerText;
                            }
                            else
                            {
                                if (!File.Exists(appPath))
                                {
                                    appPath = "";
                                }
                            }
                        }
                        else
                            appPath = "";

                        if (appPath == "")
                            Console.WriteLine(urlList[i].ChildNodes[j].InnerText + "==>" + appPath);

                        ClassProperty.SetValue(wsItem, appPath, null);
                    }
                    else
                    {
                        Console.WriteLine("wsItem <" + urlList[i].ChildNodes[j].Name + "> nao encontrada na classe URLws");
                    }
                }
            }
        }
        #endregion
    }

    public class webServices
    {
        public int ID { get; private set; }
        public string Nome { get; private set; }
        public string UF { get; private set; }
        public URLws LocalHomologacao { get; private set; }
        public URLws LocalProducao { get; private set; }

        public webServices(int id, string nome, string uf)
        {
            LocalHomologacao = new URLws();
            LocalProducao = new URLws();
            ID = id;
            Nome = nome;
            UF = uf;
        }
    }

    class SecureWebClient : WebClient
    {
        private readonly X509Certificate2 Certificado;

        public SecureWebClient(X509Certificate2 certificado)
        {
            Certificado = certificado;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.ClientCertificates.Add(Certificado);
            return request;
        }
    }

    public class URLws
    {
        public URLws()
        {
            CancelarNfse =
            ConsultarLoteRps =
            ConsultarNfse =
            ConsultarNfsePorRps =
            ConsultarSituacaoLoteRps =
            ConsultarURLNfse =
            RecepcionarLoteRps = string.Empty;
        }

        #region Propriedades referente as tags do webservice.xml
        // ******** ATENÇÃO *******
        // os nomes das propriedades tem que ser iguais as tags no WebService.xml
        // ******** ATENÇÃO *******

        #region NFS-e
        /// <summary>
        /// Enviar Lote RPS NFS-e 
        /// </summary>
        public string RecepcionarLoteRps { get; set; }
        /// <summary>
        /// Consultar Situação do lote RPS NFS-e
        /// </summary>
        public string ConsultarSituacaoLoteRps { get; set; }
        /// <summary>
        /// Consultar NFS-e por RPS
        /// </summary>
        public string ConsultarNfsePorRps { get; set; }
        /// <summary>
        /// Consultar NFS-e por NFS-e
        /// </summary>
        public string ConsultarNfse { get; set; }
        /// <summary>
        /// Consultar lote RPS
        /// </summary>
        public string ConsultarLoteRps { get; set; }
        /// <summary>
        /// Cancelar NFS-e
        /// </summary>
        public string CancelarNfse { get; set; }
        /// <summary>
        /// Consulta URL de Visualização da NFSe
        /// </summary>
        public string ConsultarURLNfse { get; set; }
        #endregion

        #endregion
    }

}
