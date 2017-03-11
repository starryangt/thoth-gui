using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;

namespace ThothGui
{
    class MetadataInput : TableLayout
    {

        private TextBox _titlebox;
        private TextBox _authorbox;
        public MetadataInput()
        {
            _titlebox = new TextBox();
            _authorbox = new TextBox();
            Rows.Add(
                new TableRow(
                    new Label { Text = "Title: " },
                    new TableCell(_titlebox, true)
                )
            );
            Rows.Add(
                new TableRow(
                    new Label { Text = "Author: " },
                    new TableCell(_authorbox, true)
                )
            );
        }

    }
}
