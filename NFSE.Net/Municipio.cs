using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public class Municipio
    {
        public int CodigoMunicipio { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
        public string PadraoStr { get; set; }
        public PadroesNFSe Padrao { get; set; }

        public Municipio(int _cod, string _uf, string _nome, PadroesNFSe _padrao)
        {
            this.Nome = _nome;
            if (!_nome.Trim().EndsWith(" - " + _uf))
                this.Nome = _nome.Trim() + " - " + _uf;
            this.CodigoMunicipio = _cod;
            this.Padrao = _padrao;
            this.PadraoStr = _padrao.ToString();
            this.UF = _uf;
        }
    }
}
