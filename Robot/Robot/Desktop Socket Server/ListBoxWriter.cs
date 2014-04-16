using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Desktop_Socket_Server
{
        public class ListBoxWriter : TextWriter
        {
            private ListBox list;
            private StringBuilder content = new StringBuilder();

            public ListBoxWriter(ListBox list)
            {
                this.list = list;
            }

            public override void Write(char value)
            {
                try
                {
                    base.Write(value);
                    content.Append(value);
                    if (value == '\n')
                    {
                        list.Items.Add(content.ToString());
                        content = new StringBuilder();
                    }
                }
                catch (Exception ex)
                {
                    ;;
                }
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
        }
}
