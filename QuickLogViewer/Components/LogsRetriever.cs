using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuickLogViewer.Components
{
    public class LogsRetriever : IDisposable
    {
        private Lazy<EventLog> _eventLog = new Lazy<EventLog>(() => new EventLog("Application"));

        public List<ViewModels.LogDayViewModel> GetLogs()
        {
            return _eventLog.Value.Entries.Cast<EventLogEntry>()
                .Where(entry => entry.EntryType != EventLogEntryType.Information)
                .GroupBy(entry => entry.TimeGenerated.Date)
                .Select(group => new ViewModels.LogDayViewModel
                {
                    Date = group.Key,
                    Logs = group.Select(entry => new Models.LogModel
                    {
                        Category = entry.Category,
                        Occured = entry.TimeGenerated,
                        Text = entry.Message,
                        Source = entry.Source,
                        Type = entry.EntryType
                    }).OrderByDescending(log => log.Occured).ToList()
                }).OrderByDescending(day => day.Date).Take(5).ToList();
        }

        public void Dispose()
        {
            if (_eventLog.IsValueCreated)
            {
                _eventLog.Value.Dispose();
                _eventLog = null;
            }
        }
    }
}
