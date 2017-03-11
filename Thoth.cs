using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using Eto.Drawing;

namespace ThothGui
{

    class ThothDownload
    {
        public static void Download()
        {
                        
        }
    }

    class Thoth : Form
    {

        private Clipboard _clipboard;
        private PasteBox _pastebox;
        public Thoth()
        {
            ClientSize = new Size(300, 200);
            _clipboard = new Clipboard();
            _pastebox = new PasteBox();
            this.KeyDown += PasteEvent;
            var input = new MetadataInput();
            var layout = new TableLayout { Rows = { _pastebox, input } };
            Content = layout;
        }

        private void PasteEvent(object sender, KeyEventArgs e)
        {
            if(e.Key == Keys.V && e.Modifiers == Keys.Control)
            {

                if (_clipboard.Types.Contains<string>("HTML Format"))
                {
                    _pastebox.ExtractPasteData(_clipboard.Html);
                }
            }
        }
    }
}
