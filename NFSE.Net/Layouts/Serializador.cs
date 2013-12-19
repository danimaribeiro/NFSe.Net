using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFSE.Net.Layouts
{
    public class Serializador
    {
        public void SalvarXml<T>(T objeto, string caminhoSalvar)
        {
            using (var stream = new System.IO.StreamWriter(System.IO.File.Open(caminhoSalvar, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                XmlSerializer infoSerializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("e", "http://www.betha.com.br/e-nota-contribuinte-ws");
                infoSerializer.Serialize(stream, objeto, namespaces);
                stream.Close();
            }
        }

        public T LerXml<T>(string caminhoXml)
        {
            using (var stream = new System.IO.StreamReader(System.IO.File.Open(caminhoXml, FileMode.Open, FileAccess.Read)))
            {
                XmlSerializer infoSerializer = new XmlSerializer(typeof(T));
                var objeto = infoSerializer.Deserialize(stream);
                stream.Close();
                return (T)objeto;
            }
        }

    }
}
