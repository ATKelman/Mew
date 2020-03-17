using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Reminder
    {
        public string Username { get; set; }
        public string Channel { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Message { get; set; }

        public Reminder(string username, string channel, DateTime reminderTime, string message)
            => (Username, Channel, ReminderTime, Message) = (username, channel, reminderTime, message);
    }
}
