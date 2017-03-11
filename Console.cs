using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using System.IO;

namespace ThothGui
{

    public class ConsoleWriterEventArgs : EventArgs
    {
        public string Value { get; private set; }
        public ConsoleWriterEventArgs(string value)
        {
            Value = value;
        }
    }

    public class ConsoleWriter : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public override void Write(string value)
        {
            if (WriteEvent != null) WriteEvent(this, new ConsoleWriterEventArgs(value));
            base.Write(value);
        }

        public override void WriteLine(string value)
        {
            if (WriteLineEvent != null) WriteLineEvent(this, new ConsoleWriterEventArgs(value));
            base.WriteLine(value);
        }

        public event EventHandler<ConsoleWriterEventArgs> WriteEvent;
        public event EventHandler<ConsoleWriterEventArgs> WriteLineEvent;
    }
    class ThothConsole : TextArea
    {

        public ConsoleWriter Writer;
        public ThothConsole()
        {
            ReadOnly = true;
            //Size = new Eto.Drawing.Size(100, 100);
            Writer = new ConsoleWriter();
            Writer.WriteEvent += Writer_WriteEvent;
            Writer.WriteLineEvent += Writer_WriteLineEvent;
        }

        private void Writer_WriteLineEvent(object sender, ConsoleWriterEventArgs e)
        {
            Application.Instance.Invoke(() => {
                this.Append("\n", true);
                this.Append(e.Value, true);
             });
        }

        private void Writer_WriteEvent(object sender, ConsoleWriterEventArgs e)
        {
            Application.Instance.Invoke(() => {
                this.Append("\n", true);
                this.Append(e.Value, true);
             });
        }
    }
}
