using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Eto.Forms;
using Eto.Drawing;

namespace ThothGui
{

    class ThothDownloadThread
    {

        public bool Done = false;
        private string _title = "";
        private string _author = "";
        private Bitmap _cover;
        private IEnumerable<string> _urls;

        public ThothDownloadThread(IEnumerable<URL> urls, string title, string author, Bitmap cover)
        {
            _title = title;
            _author = author;
            _cover = cover;
            _urls = urls.Select(x => x.Href);
        }

        public void Down()
        {

            if (_cover != null)
            {
                File.WriteAllBytes("Cover.png", _cover.ToByteArray(ImageFormat.Jpeg));
            }
            var stringUrls = _urls;
           
            Download.CheckAndDeleteDirectory("temp");
            if (_cover == null)
            {
                Process.EbookFromListDefault(false, _title, _author, "", stringUrls);
            }
            else
            {
                Process.EbookFromListCoverIsFile(false, _title, _author, "cover.xhtml", stringUrls);
            }
            
            Done = true;
            Download.CheckAndDelete("Cover.png");
            Console.WriteLine("Done!");
        }
    }

    class Thoth : Form
    {

        private Clipboard _clipboard;
        private PasteBox _pastebox;
        private CoverView _cover;
        private ThothConsole _console;
        private MetadataInput _input;
        private Button _downloadButton;
        private ButtonToolItem _toolbarPaste;
        private Dialog<string> _test;

        private bool _downloadRunning = false;

        public Thoth()
        {
            ClientSize = new Size(500, 400);
            _clipboard = new Clipboard();
            _pastebox = new PasteBox();
            _cover = new CoverView();
            _console = new ThothConsole();
            _downloadButton = new Button { Text = "Download" };
            _input = new MetadataInput();
            _test = new Dialog<string>();
            _toolbarPaste = new ButtonToolItem { Text = "Paste" };

            Console.SetOut(_console.Writer);

            //toolbar
            ToolBar = new ToolBar
            {
                Items = {
                    _toolbarPaste,
                    new SeparatorToolItem()
                }
            };

            var layout = new TableLayout();
            layout.Rows.Add(new TableRow(
                new TableLayout {
                    Rows = {
                        _pastebox,
                        _input,
                        new TableRow(new TableCell(_downloadButton, false)) } },
                new TableLayout
                {
                    Rows =
                    {
                        _cover,
                        _console
                    }
                }
            ));
            layout.Rows.Add(null);
            
            Content = layout;

            BindHandlers();
        }

        private void _test_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _test.Result = "hi";
        }

        private void BindHandlers()
        {

            KeyDown += PasteEvent;
            _toolbarPaste.Click += _toolbarPaste_Click;
            _downloadButton.Click += _downloadButton_Click;
        }

        private void _toolbarPaste_Click(object sender, EventArgs e)
        {
            HandlePaste();
        }

        private void _downloadButton_Click(object sender, EventArgs e)
        {
            if (!_downloadRunning)
            {
                _downloadButton.Enabled = false;
                var download = new ThothDownloadThread(_pastebox.GetUrls(), _input.GetTitle(), _input.GetAuthor(), _cover.GetImage());
                ThreadStart start = download.Down;
                start += () =>
                {
                    _downloadRunning = false;
                    Application.Instance.Invoke(() => _downloadButton.Enabled = true);
                };

                _downloadRunning = true;
                var caller = new Thread(start);
                caller.Start();
            }
        }

        private void HandlePaste()
        {
            if (_clipboard.Types.Contains("HTML Format"))
                {
                    _pastebox.ExtractPasteData(_clipboard.Html);
                }
            if (_clipboard.Types.Contains("Bitmap"))
                {
                    if(_clipboard.Image is Bitmap)
                    {
                        var image = (Bitmap)_clipboard.Image;
                        _cover.ExtractPasteData(image);
                    }
                }

        }

        private void PasteEvent(object sender, KeyEventArgs e)
        {
            if(e.Key == Keys.V && e.Modifiers == Keys.Control)
            {
                HandlePaste();
            }
        }
    }
}
