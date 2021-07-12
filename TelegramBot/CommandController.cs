using ISSystem.DbContext;
using Microsoft.EntityFrameworkCore;
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
    public class CommandController
    {
        RepositoriesUnitOfWork _repositories;
        Message _message;
        ITelegramBotClient _botClient;

        public CommandController(RepositoriesUnitOfWork repositories, Message message, ITelegramBotClient botClient)
        {
            _repositories = repositories;
            _message = message;
            _botClient = botClient;
        }

        //Сделать запись подтвержденной
        public Task app()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("app: Введите id записи", _message.Chat.Id, _botClient);
        }

        //Сделать запись заблокированной
        public Task loc()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("loc: Введите id записи", _message.Chat.Id, _botClient);
        }

        //Сделать запись обычной
        public Task can()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("can: Введите id записи", _message.Chat.Id, _botClient);
        }

        //вывести все подтвержденные записи
        public Task allapp()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListRecording(_repositories
                .Recording.GetApprovedRecordings()), _message.Chat.Id, _botClient);
        }

        //Вывести все заблокированные записи
        public Task allloc()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListRecording(_repositories
                .Recording.GetLockedRecordings()), _message.Chat.Id, _botClient);
        }

        //Вывести все обычные записи
        public Task allcan()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListRecording(_repositories
                .Recording.GetNormalRecordings()), _message.Chat.Id, _botClient);
        }

        //Вывести комментарий записи
        public Task getcom()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("getcom: Введите id записи", _message.Chat.Id, _botClient);
        }

        //Вывести все записи на другое время
        public Task allother()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListOtherRecording(_repositories
                .Recording.GetOtherTimeRecordings()), _message.Chat.Id, _botClient);
        }

        //Посмотреть информацию о записи
        public Task getrec()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("getrec: Введите id записи", _message.Chat.Id, _botClient);
        }

        //Посмотреть все вакансии
        public Task allvac()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListVacancy(_repositories
                .Vacancy.GetActiveVacancies()
                .Include(p => p.Calendar).ToList()), _message.Chat.Id, _botClient);
        }

        //Посмотреть все календари
        public Task allcal()
        {
            return CommandsAsinc.SendTextMessageAsync(CommandHelper
                .CreateStringByListCalendar(_repositories
                .Calendar.GetActiveItemList()), _message.Chat.Id, _botClient);
        }

        //Создать вакансию
        public Task addvac()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("addvac: Введите название вакансии и id календаря через запятую", _message.Chat.Id, _botClient);
        }

        //Привязать календярь к вакансии
        public Task bindvac()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("bindvac: Введите id вакансии и id календаря через запятую", _message.Chat.Id, _botClient);
        }

        ////Авторизоваться как админ
        //public Task login()
        //{
        //    return CommandsAsinc.SendTextMessageAsyncAndReturn("login: Введите логин и пароль через зяпятую", _message.Chat.Id, _botClient);
        //}

        //Вывести сводку по календарям
        //public Task infocal()
        //{

        //}

        //остановить вакансию
        public Task stopvac()
        {
            return CommandsAsinc.SendTextMessageAsyncAndReturn("stopvac: Введите id вакансии", _message.Chat.Id, _botClient);
        }
        //Выйти
        //public Task logout()
        //{

        //}
    }
}
