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
        private static readonly Regex _exceptionName = new Regex(@"(System\.(\w|\.)+Exception:?)", RegexOptions.Compiled);
        private static readonly Regex _stackTraceMethodLine = new Regex(@"\s+(at\s+" + MethodCharacters + @")\s+", RegexOptions.Compiled);
        private static readonly Regex _stackTraceFileLine = new Regex(@"\s+(in\s+" + PathCharacters + @"\.cs:line\s+\d+)", RegexOptions.Compiled);
        private static readonly Regex _exceptionMessage = new Regex(@"Exception:", RegexOptions.Compiled);

        const string PathCharacters = @"[\w|\.|\\|:|\s|_]+";
        const string MethodCharacters = @"[\w|\.|_|<|>]+\([\w|\.|_|<|>|,|\s]*\)";

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
            text = _exceptionMessage.Replace(text, match => match.Groups[0].Value + Rtf.NewLine + Rtf.Red);
            text = _exceptionName.Replace(text, match => Rtf.NewLine + Rtf.NewLine + _rtf.Bold(match.Groups[0].Value) + Rtf.NewLine);
            text = _stackTraceMethodLine.Replace(text, match => Rtf.NewLine + Rtf.Gray + match.Groups[0].Value);
            text = _stackTraceFileLine.Replace(text, match => Rtf.NewLine + Rtf.Gray + match.Groups[0].Value);
            return text;
        }

        private bool HasCsharpStacktrace(string text)
        {
            return text.Contains("Exception") && text.Contains(" at ");
        }
    }
}
