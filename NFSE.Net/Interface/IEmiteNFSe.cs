using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    public interface IEmiteNFSe
    {
        void EmiteNF(string file);
        void CancelarNfse(string file);
        void ConsultarLoteRps(string file);
        void ConsultarSituacaoLoteRps(string file);
        void ConsultarNfse(string file);
        void ConsultarNfsePorRps(string file);
        void GerarRetorno(string file, string result, string extEnvio, string extRetorno);

        object WSGeracao { get; }
        object WSConsultas { get; }
    }
}
