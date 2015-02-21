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
        private static readonly Components.CsharpStacktraceFormatter _csharpStacktraceFormatter = new Components.CsharpStacktraceFormatter();

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

                        var rtf = _csharpStacktraceFormatter.TryFormatCsharpStacktrace(GetDocumentRtf(richTextBox));
                        var doc = new FlowDocument();
                        var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                        range.Load(new MemoryStream(Encoding.UTF8.GetBytes(rtf)), DataFormats.Rtf);
                        richTextBox.Document = doc;
                    }
            });
    }
}
