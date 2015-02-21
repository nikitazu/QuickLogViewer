using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickLogViewer.Components
{
    public class CsharpStacktraceFormatter
    {
        private static readonly Rtf _rtf = new Rtf();
        private static readonly Regex _exceptionName = new Regex(@"(System\.(\w|\.)+Exception)", RegexOptions.Compiled);

        public string TryFormatCsharpStacktrace(string text)
        {
            string escapedText = _rtf.Escape(text);

            if (!HasCsharpStacktrace(escapedText))
            {
                return escapedText;
            }

            return _rtf.RichText(InjectCsharpStacktraceNewlines(escapedText));
        }

        private string InjectCsharpStacktraceNewlines(string text)
        {
            var temp = text
                .Replace("Exception:", "Exception:" + Rtf.NewLine + Rtf.Red)
                .Replace(" at ", Rtf.NewLine + Rtf.Gray + " -> ");

            return _exceptionName.Replace(temp, (match) => _rtf.Bold(match.Groups[0].Value));
        }

        private bool HasCsharpStacktrace(string text)
        {
            return text.Contains("Exception") && text.Contains(" at ") && text.Contains(".cs:line");
        }
    }
}
