using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSE.Net
{
    #region Classe interna para comparar as pastas informadas nas configurações do UniNFe
    /// <summary>
    /// danasa 8-2009
    /// classe interna para comparar as pastas informadas
    /// </summary>
    public class FolderCompare
    {
        public Int32 id { get; set; }
        public string folder { get; set; }

        public FolderCompare(Int32 _id, string _folder)
        {
            this.id = _id;
            this.folder = _folder;
        }
    }
    #endregion
}
