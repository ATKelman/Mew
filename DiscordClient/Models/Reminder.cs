using System;

namespace DiscordClient.Models
{
    public class Reminder
    {
        public string Username { get; set; }
        public string Channel { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Message { get; set; }
    }
}
