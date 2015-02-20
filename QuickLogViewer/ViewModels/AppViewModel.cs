using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogViewer.ViewModels
{
    public class AppViewModel
    {
        public List<LogDayViewModel> LogDays { get; set; }
        public LogDayViewModel SelectedLogDay { get; set; }
        public Models.LogModel SelectedLog { get; set; }

        public AppViewModel()
        {
            LogDays = new List<LogDayViewModel>();
        }
    }
}
