using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents; 

namespace QuickLogViewer.UI
{
    public class RichTextBoxHelper : DependencyObject
    {
        public static string GetDocumentRtf(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentRtfProperty);
        }

        public static void SetDocumentRtf(DependencyObject obj, string value)
        {
            obj.SetValue(DocumentRtfProperty, value);
        }

        public static readonly DependencyProperty DocumentRtfProperty =
            DependencyProperty.RegisterAttached("DocumentRtf", typeof(string), typeof(RichTextBoxHelper), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = false,
                PropertyChangedCallback = (sender, e) =>
                    {
                        var richTextBox = (RichTextBox)sender;

                        var rtf = RichText(TryFormatCsharpStacktrace(GetDocumentRtf(richTextBox)));
                        var doc = new FlowDocument();
                        var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                        range.Load(new MemoryStream(Encoding.UTF8.GetBytes(rtf)), DataFormats.Rtf);
                        richTextBox.Document = doc;
                    }
            });

        private static string TryFormatCsharpStacktrace(string text)
        {
            if (!HasCsharpStacktrace(text))
            {
                return text;
            }

            return InjectCsharpStacktraceNewlines(text);
        }

        private static string InjectCsharpStacktraceNewlines(string text)
        {
            return text
                .Replace("Exception:", @"Exception:\line\cf2 ")
                .Replace(" at ", @"\line\cf3 -> ");
        }

        private static bool HasCsharpStacktrace(string text)
        {
            return text.Contains("Exception") && text.Contains(" at ") && text.Contains(".cs:line");
        }

        private static string RichText(string text)
        {
            return @"{\rtf1\ansi\deff0
{\colortbl;\red0\green0\blue0;\red255\green0\blue0;\red100\green100\blue100;}
" + text + @"
}";
        }

        private static string Red(string text)
        {
            return @"\cf2
" + text + @"
\cf1
";
        }
        private static string Gray(string text)
        {
            return @"\cf3
" + text + @"
\cf1
";
        }
    }
}
