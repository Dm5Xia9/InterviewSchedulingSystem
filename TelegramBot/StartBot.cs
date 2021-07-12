using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class StartBot
    {
        private static ITelegramBotClient bot;
        private static DbContextOptions<AppDbContext> options;

        public static async Task Run(string connectString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            options = optionsBuilder
                    .UseNpgsql(connectString)
                    .Options;

            bot = new TelegramBotClient("************");
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            await bot.ReceiveAsync(new DefaultUpdateHandler(CommandHandlerAsync, CommandErrorAsync), cancellationToken);
        }

        public static async Task CommandHandlerAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is Message message)
            {

                AppDbContext appDbContext = new(options);
                RepositoriesUnitOfWork repositories = new(appDbContext);

                if(message.Text[0] == '/')
                {
                    CommandController commandController = new(repositories, message, botClient);

                    MethodInfo methodInfo = typeof(CommandController)
                        .GetMethod(message.Text.Remove(0, 1), Array.Empty<Type>());

                    try
                    {
                        var task = (Task)methodInfo.Invoke(commandController, Array.Empty<object>());
                        task.Wait(cancellationToken);
                    }
                    catch
                    {
                        await bot.SendTextMessageAsync(message.Chat.Id, "Команда не найдена", cancellationToken: cancellationToken);
                    }
                }
                else
                {
                    if (message.ReplyToMessage == null)
                        return;

                    AnswerController answerController = new(repositories, message, botClient);

                    var method = message.ReplyToMessage.Text.Split(':').First();

                    MethodInfo methodInfo = typeof(AnswerController)
                        .GetMethod(method, Array.Empty<Type>());

                    try
                    {
                        var task = (Task)methodInfo.Invoke(answerController, Array.Empty<object>());
                        task.Wait(cancellationToken);
                    }
                    catch
                    {
                        await bot.SendTextMessageAsync(message.Chat.Id, "Команда не найдена", cancellationToken: cancellationToken);
                    }
                }

                if (!appDbContext.TelegramChats.Any(p => p.TelegramChatId ==  message.Chat.Id))
                {
                    appDbContext.TelegramChats.Add(new TelegramChat { TelegramChatId = message.Chat.Id });
                    appDbContext.SaveChanges();
                }
            }
        }

        public static async Task CommandErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException apiRequestException)
            {
                await botClient.SendTextMessageAsync(123, apiRequestException.ToString(), cancellationToken: cancellationToken);
            }
        }

        public static void Send(string message)
        {
            AppDbContext appDbContext = new(options);
            var chatIds = appDbContext.TelegramChats.Select(p => p.TelegramChatId);
            foreach (var item in chatIds)
            {
                bot.SendTextMessageAsync(new ChatId(item), message);
            }
        }

        
    }
}
