using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using Eto.Drawing;

namespace ThothGui
{
    class CoverView : TableLayout
    {
        private ImageView _imageView;
        public CoverView()
        {
            _imageView = new ImageView();
            Rows.Add(new TableRow(new Label { Text = "Cover Preview" } ));
            Rows.Add(new TableRow(_imageView));
            _imageView.Size = new Size(100, 300);
        }

        public void ExtractPasteData(Image image)
        {
            _imageView.Image = image;
        }

        public Bitmap GetImage()
        {
            return (Bitmap)_imageView.Image;
        }
    }
}
