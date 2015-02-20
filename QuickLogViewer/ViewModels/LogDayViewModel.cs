using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogViewer.ViewModels
{
    public class LogDayViewModel
    {
        public DateTime Date { get; set; }
        public List<Models.LogModel> Logs { get; set; }

        public string DateFormatted
        {
            get { return Date.ToShortDateString(); }
        }
        public LogDayViewModel()
        {
            Logs = new List<Models.LogModel>();
        }

        public override string ToString()
        {
            return string.Format("LogDay Date={0}, Logs={1}", Date, Logs.Count);
        }
    }
}
