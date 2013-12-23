using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class Empresas
    {
        public Empresas()
        {
            this.ListaEmpresas = new List<InfoEmpresa>();
        }

        public List<InfoEmpresa> ListaEmpresas { get; set; }

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
                bool erro;
                var empresas = serializar.TryLerXml<Empresas>(Propriedade.NomeArqEmpresa, out erro);
                if (erro)
                    empresas = new Empresas();
                if (!empresas.ListaEmpresas.Exists(x => x.Cnpj == cnpj))
                    empresas.ListaEmpresas.Add(new InfoEmpresa() { Cnpj = cnpj, Nome = nome });

                serializar.SalvarXml<Empresas>(empresas, Propriedade.NomeArqEmpresa);
                serializar.SalvarXml<Core.Empresa>(empresa, caminhoConfiguracaoEmpresa);
            }
            else
            {
                var serializar = new Layouts.Serializador();
                var dicionarioEmpresas = new List<InfoEmpresa>();
                dicionarioEmpresas.Add(new InfoEmpresa() { Cnpj = cnpj, Nome = nome });
                serializar.SalvarXml<Empresas>(new Empresas() { ListaEmpresas = dicionarioEmpresas }, Propriedade.NomeArqEmpresa);
                serializar.SalvarXml<Core.Empresa>(empresa, caminhoConfiguracaoEmpresa);
            }
        }
    }

    public class InfoEmpresa
    {
        public string Cnpj { get; set; }
        public string Nome { get; set; }
    }
}
