using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using Eto.Drawing;

namespace ThothGui
{
    class Thoth : Form
    {
        public Thoth()
        {
            ClientSize = new Size(300, 200);
            Content = new PasteBox();
        }
    }
}
