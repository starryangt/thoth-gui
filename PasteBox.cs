using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;

namespace ThothGui
{

    class URL
    {

        public string Text { get; }
        public string Href { get; }

        public URL(string text, string href)
        {
            Text = text;
            Href = href; 
        }

        public URL(NSoup.Nodes.Element tag)
        {
            Text = tag.OwnText();
            Href = tag.Attr("abs:href");
        }
    }

    class PasteBox : TableLayout
    {

        private List<URL> _urls;
        private ListBox _box;
        private Button _upButton;
        private Button _downButton;
        private Button _deleteButton;
        private TableLayout _buttonLayout;

        public PasteBox()
        {
            _box = new ListBox();
            _upButton = new Button { Text = "Move Up" };
            _downButton = new Button { Text = "Move Down" };
            _deleteButton = new Button { Text = "Delete" };
            _buttonLayout = new TableLayout { Rows = { _upButton, _downButton, _deleteButton} };


            _box.BindDataContext((c) => c.DataStore, (List<URL> m) => m);
            _urls = new List<URL>();
            _box.DataStore = new List<URL> { new URL("Paste URLs here", "") };
            Rows.Add(new TableRow(
                new TableCell(_box, true),
                new TableCell(_buttonLayout, false)));
            Size = new Eto.Drawing.Size(300, 300);
            BindHandlers();
        }

        public IEnumerable<URL> GetUrls()
        {
            return _urls;
        }

        private void BindHandlers()
        {
            _box.KeyDown += DeleteKeyHandler;
            _upButton.Click += (object o, EventArgs e) =>
            {
                var selected = _box.SelectedValue;
                if (selected != null && selected is URL)
                {
                    var selectedURL = (URL)selected;
                    MoveURLUp(selectedURL);
                }
            };

            _downButton.Click += (object o, EventArgs e) =>
            {
                var selected = _box.SelectedValue;
                if (selected != null && selected is URL)
                {
                    var selectedURL = (URL)selected;
                    MoveURLDown(selectedURL);
                }
            };

            _deleteButton.Click += (object o, EventArgs e) =>
            {
                var selected = _box.SelectedValue;
                if (selected != null && selected is URL)
                {
                    var selectedURL = (URL)selected;
                    DeleteURL(selectedURL);
                }
            };
        }

        private void DeleteKeyHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Keys.Delete)
            {
                var selected = _box.SelectedValue;
                if(selected != null && selected is URL)
                {
                    var selectedURL = (URL)selected;
                    DeleteURL(selectedURL);
                }
            }
        }

        private void DeleteURL(URL url)
        {
            _urls.Remove(url);
            updateDisplay();
        }

        private void MoveURLUp(URL url)
        {
            var index = _urls.FindIndex((x => x == url));
            _urls.Remove(url);
            var newIndex = (index - 1 >= 0) ? index - 1 : 0;
            _urls.Insert(newIndex, url);
            updateDisplay();
            _box.SelectedIndex = newIndex;
        }

        private void MoveURLDown(URL url)
        {
            var index = _urls.FindIndex((x => x == url));
            _urls.Remove(url);
            var newIndex = (index + 1 <= _urls.Count) ? index + 1 : _urls.Count;
            _urls.Insert(newIndex, url);
            updateDisplay();
            _box.SelectedIndex = newIndex;
        }

        private void updateDisplay()
        {
            _box.DataStore = _urls;
        }

        public void ExtractPasteData(string data)
        {
            var soup = NSoup.NSoupClient.Parse(data);
            var links = soup.Body.Select("a");
            foreach(var link in links)
            {
                _urls.Add(new URL(link));
                updateDisplay();
            }
        }
    }
}
