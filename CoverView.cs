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
        private Button _clearImageButton;
        public CoverView()
        {
            _imageView = new ImageView();
            _clearImageButton = new Button { Text = "Clear Cover" };
            Rows.Add(new TableRow(new Label { Text = "Cover Preview" } ));
            Rows.Add(new TableRow(_imageView));
            Rows.Add(new TableRow(_clearImageButton));
            _imageView.Size = new Size(100, 300);
            BindHandlers(); 
        }

        private void BindHandlers()
        {
            _clearImageButton.Click += (object o, EventArgs e) =>
            {
                _imageView.Image = null;
            };
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
