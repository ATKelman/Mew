using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Modules
{
    public interface IReminderData
    {
        Task<List<Reminder>> GetReminders();
        Task<List<Reminder>> GetReminders(int status);
        Task InsertReminder(Reminder reminder);
    }
}