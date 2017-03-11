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

    class PasteBox : ListBox
    {

        private List<URL> _urls;
        public PasteBox()
        {
            this.BindDataContext((c) => c.DataStore, (List<URL> m) => m);
            _urls = new List<URL>();
            DataStore = new List<URL> { new URL("Paste URLs here", "") };
            this.KeyDown += DeleteKeyHandler;
        }

        private void DeleteKeyHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Keys.Delete)
            {
                Console.WriteLine("Delete hit");
                var selected = this.SelectedValue;
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
        }

        private void MoveURLDown(URL url)
        {
            var index = _urls.FindIndex((x => x == url));
            _urls.Remove(url);
            var newIndex = (index + 1 <= _urls.Count) ? index + 1 : _urls.Count;
            _urls.Insert(newIndex, url);
            updateDisplay();
        }

        private void updateDisplay()
        {
            var selected = this.SelectedValue;
            DataStore = _urls;
            if(selected != null)
            {
                //this maintains the previously selected item
                SelectedValue = selected;
            }
        }

        public void ExtractPasteData(string data)
        {
            var soup = NSoup.NSoupClient.Parse(data);
            var links = soup.Body.Select("a");
            Console.WriteLine("hey");
            foreach(var link in links)
            {
                _urls.Add(new URL(link));
                updateDisplay();
            }
        }
    }
}
