using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class CommandsAsinc
    {
        public static async Task SendTextMessageAsyncAndReturn(string returnText, ChatId chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId,
                returnText, ParseMode.Default,
                replyMarkup: new ForceReplyMarkup { Selective = true });
        }

        public static async Task SendTextMessageAsync(string returnText, ChatId chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId, returnText);
        }
    }
}
