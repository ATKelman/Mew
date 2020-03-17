using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Modules
{
    public interface IReminderData
    {
        Task<List<Reminder>> GetReminders();
        Task InsertReminder(Reminder reminder);
    }
}