using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    /// <summary>
    /// Classe responsável por definir uma lista dos arquivos de SCHEMAS para validação dos XMLs
    /// </summary>
    public class SchemaXML
    {
        /// <summary>
        /// Informações dos schemas para validação dos XML
        /// </summary>
        public static Dictionary<string, InfSchema> InfSchemas = new Dictionary<string, InfSchema>();
        /// <summary>
        /// O Maior ID que tem na lista
        /// </summary>
        public static int MaxID { get; set; }           
    }

    public class InfSchema
    {
        /// <summary>
        /// TAG do XML que identifica qual XML é
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// Identificador único numérico do XML 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Breve descrição do arquivo XML
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Nome do arquivo de schema para validar o XML
        /// </summary>
        public string ArquivoXSD { get; set; }
        /// <summary>
        /// Nome da tag do XML que será assinada
        /// </summary>
        public string TagAssinatura { get; set; }
        /// <summary>
        /// Nome da tag que tem o atributo ID
        /// </summary>
        public string TagAtributoId { get; set; }
        /// <summary>
        /// Nome da tag de lote do XML que será assinada
        /// </summary>
        public string TagLoteAssinatura { get; set; }
        /// <summary>
        /// Nome da tag de lote que tem o atributo ID
        /// </summary>
        public string TagLoteAtributoId { get; set; }
        /// <summary>
        /// URL do schema de cada XML
        /// </summary>
        public string TargetNameSpace { get; set; }
    }
}
