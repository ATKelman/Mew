using Discord.Commands;
using DiscordClient.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordClient.Modules
{
    public class ReminderCommands : ModuleBase
    {
        // ToDo Add IDataAccess 
        private IDataAccess _data;

        public ReminderCommands(IServiceProvider services)
        {
            _data = services.GetRequiredService<IDataAccess>();
        }

        [Command("RemindMe")]
        [Summary("Stores a message to relayed to the user after/at a specified time.")]
        public async Task RemindMe([Remainder] string message)
        {
            try
            {
                var input = message.Split(' ');
                DateTime reminderTime = DateTime.Now;
                string reminderMessage = "";

                // ToDo Make Regex to extract time 
                // Make Extract time and message a function 

                // Handle exact time
                if (input[0].Contains(':'))
                {
                    // !Remindme HH:mm {message} 
                    if (!DateTime.TryParse(input[0], out reminderTime))
                        throw new Exception("Error - Unable to Parse DateTime, correct command usage: !Remindme HH:mm {message}");
                    foreach (var msg in input.Skip(1))
                    {
                        reminderMessage += msg + " ";
                    }
                }
                // Handle commands
                else
                {
                    // !Remindme -d {int : days} -h {int : hours} -m {int : minutes} {string : message}
                    // Not all commands must be included. 
                    for (int i = 0; i < input.Count(); i++)
                    {
                        switch (input[i])
                        {
                            // Days
                            case "-d":
                                if (!int.TryParse(input[i + 1], out int days))
                                    throw new Exception("Error - Unable to Parse Days. Command -d");
                                reminderTime.AddDays(days);
                                i++;
                                break;
                            // Hours
                            case "-h":
                                if (!int.TryParse(input[i + 1], out int hours))
                                    throw new Exception("Error - Unable to Parse Hours. Command -h");
                                reminderTime.AddHours(hours);
                                i++;
                                break;
                            // Minutes
                            case "-m":
                                if (!int.TryParse(input[i + 1], out int minutes))
                                    throw new Exception("Error - Unable to Parse Minutes. Command -m");
                                reminderTime.AddMinutes(minutes);
                                i++;
                                break;
                            default:
                                reminderMessage += input[i] + " ";
                                break;
                        }
                    }
                }

                var reminder = new Reminder(Context.User.Mention, Context.Channel.Id.ToString(), reminderTime, reminderMessage);

                var isSuccess = await _data.Insert(reminder);

                if (isSuccess)
                {
                    var emote = new Discord.Emoji("thumbsup");
                    await Context.Message.AddReactionAsync(emote);
                }
                else
                {
                    await ReplyAsync("Unable to Set Reminder.");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }
    }
}
