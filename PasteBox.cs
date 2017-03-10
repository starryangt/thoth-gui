using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;

namespace ThothGui
{
    class PasteBox : ListBox
    {
        public PasteBox()
        {
            var strings = new List<string>();
            strings.Add("Heil");
            strings.Add("Heil");
            strings.Add("Heil");
            strings.Add("Heil");
            this.DataStore = strings;
        }
    }
}
