﻿using DataAccess.Models;
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

        public Task InsertReminder(Reminder reminder)
        {
            string sql = @"Insert into dbo.Reminder (Username, Channel, ReminderTime, ;essage)
                            values (@Username, @Channel, @ReminderTime, @Message)";
            return _db.SaveData(sql, reminder);
        }
    }
}
