using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Media;

namespace QuickLogViewer.Models
{
    public class LogModel
    {
        public string Category { get; set; }
        public string Source { get; set; }
        public DateTime Occured { get; set; }
        public string Text { get; set; }
        public EventLogEntryType Type { get; set; }

        public string TypeName
        {
            get { return Type.ToString(); }
        }

        public Brush TypeBrush
        {
            get { return Type == EventLogEntryType.Error || Type == EventLogEntryType.FailureAudit ? Brushes.Red : Brushes.Black; }
        }

        public string OccuredTimeFormatted
        {
            get { return Occured.ToLongTimeString(); }
        }

        public override string ToString()
        {
            return string.Format("Category={0} Source={1}, Occured={2}, Text={3}", Category, Source, Occured, Text);
        }
    }
}
