using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Modules
{
    public class ReminderData : IReminderData
    {
        private readonly ISqlDataAccess _db;

        public ReminderData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Reminder>> GetReminders()
        {
            string sql = "select * from dbo.Reminder";
            return _db.LoadData<Reminder, dynamic>(sql, new { });
        }

        // ToDo Make function dynamic so query is updated based on parameters provided
        public Task<List<Reminder>> GetReminders(int status)
        {
            var parameters = new { Status = status };
            string sql = "select * from dbo.Reminder where status = @Status";
            return _db.LoadData<Reminder, dynamic>(sql, parameters);
        }

        public Task InsertReminder(Reminder reminder)
        {
            string sql = @"Insert into dbo.Reminder (Username, Channel, ReminderTime, Message, Status)
                            values (@Username, @Channel, @ReminderTime, @Message, 10)";
            return _db.SaveData(sql, reminder);
        }
    }
}
