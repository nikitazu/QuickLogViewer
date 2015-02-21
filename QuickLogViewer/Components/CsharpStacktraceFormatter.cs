using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogViewer.Components
{
    public class CsharpStacktraceFormatter
    {
        private static readonly Rtf _rtf = new Rtf();

        public string TryFormatCsharpStacktrace(string text)
        {
            if (!HasCsharpStacktrace(text))
            {
                return text;
            }

            return _rtf.RichText(InjectCsharpStacktraceNewlines(text));
        }

        private string InjectCsharpStacktraceNewlines(string text)
        {
            return text
                .Replace("Exception:", "Exception:" + Rtf.NewLine + Rtf.Red)
                .Replace(" at ", Rtf.NewLine + Rtf.Gray + " -> ");
        }

        private bool HasCsharpStacktrace(string text)
        {
            return text.Contains("Exception") && text.Contains(" at ") && text.Contains(".cs:line");
        }
    }
}
