using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class Empresas
    {
        public Dictionary<string, string> ListaEmpresas { get; set; }

        public static Empresas CarregarEmpresasCadastradas()
        {
            if (System.IO.File.Exists(Propriedade.NomeArqEmpresa))
            {
                try
                {
                    var serializar = new Layouts.Serializador();
                    return serializar.LerXml<Empresas>(Propriedade.NomeArqEmpresa);
                }
                catch (Exception ex)
                {
                    throw new Exception("O arquivo de configuração é inválido. A lista de empresas não pode ser recuperada!", ex);
                }
            }
            else
                throw new Exception("O arquivo de configurações não existe. Verifique as configurações");
        }

        public static void SalvarNovaEmpresa(Core.Empresa empresa, string cnpj, string nome)
        {
            string caminhoConfiguracaoEmpresa = System.IO.Path.Combine(Propriedade.PastaExecutavel, cnpj, "nfse", Propriedade.NomeArqConfig);
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(caminhoConfiguracaoEmpresa)))
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(caminhoConfiguracaoEmpresa));

            if (System.IO.File.Exists(Propriedade.NomeArqEmpresa))
            {
                var serializar = new Layouts.Serializador();
                var empresas = serializar.LerXml<Empresas>(Propriedade.NomeArqEmpresa);
                if (!empresas.ListaEmpresas.ContainsKey(cnpj))
                    empresas.ListaEmpresas.Add(cnpj, nome);

                serializar.SalvarXml<Empresas>(empresas, Propriedade.NomeArqEmpresa);
                serializar.SalvarXml<Core.Empresa>(empresa, caminhoConfiguracaoEmpresa);
            }
            else
            {
                var serializar = new Layouts.Serializador();
                var dicionarioEmpresas = new Dictionary<string, string>();
                dicionarioEmpresas.Add(cnpj, nome);
                serializar.SalvarXml<Empresas>(new Empresas() { ListaEmpresas = dicionarioEmpresas }, Propriedade.NomeArqEmpresa);
                serializar.SalvarXml<Core.Empresa>(empresa, caminhoConfiguracaoEmpresa);
            }
        }
    }
}
