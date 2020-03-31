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
        public int Status { get; set; }

        public Reminder(string username, string channel, DateTime reminderTime, string message, int status)
            => (Username, Channel, ReminderTime, Message, Status) = (username, channel, reminderTime, message, status);
    }
}
