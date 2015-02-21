using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogViewer.Components
{
    public class Rtf
    {
        public const string Black = @"\cf1 ";
        public const string Red = @"\cf2 ";
        public const string Gray = @"\cf3 ";
        public const string NewLine = @"\line ";

        public string RichText(string text)
        {
            return @"{\rtf1\ansi\deff0
{\colortbl;\red0\green0\blue0;\red255\green0\blue0;\red100\green100\blue100;}
" + text + @"
}";
        }

        public string Escape(string text)
        {
            return text.Replace(@"\", @"\\").Replace("\n", Rtf.NewLine);
        }

        public string Bold(string text)
        {
            return @"\b " + text + @"\b0 ";
        }
    }
}
