using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLogViewer.Components
{
    public class AppComponent
    {
        public LogComponent LogComponent { get; set; }

        public AppComponent()
        {
            LogComponent = new LogComponent();
        }
    }
}
