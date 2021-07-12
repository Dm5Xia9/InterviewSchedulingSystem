using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class AnswerController
    {
        readonly RepositoriesUnitOfWork _repositories;
        readonly Message _message;
        readonly ITelegramBotClient _botClient;

        public AnswerController(RepositoriesUnitOfWork repositories, Message message, ITelegramBotClient botClient)
        {
            _repositories = repositories;
            _message = message;
            _botClient = botClient;
        }

        public Task app()
        {
            var record = _repositories.Recording.GetItemById(Convert.ToInt32(_message.Text));

            if (record == null)
                return CommandsAsinc.SendTextMessageAsync("Такой записи не существует", _message.Chat.Id, _botClient);

            record.ChangeIsApproved();
            _repositories.Recording.Update(record);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Стутус записи изменен", _message.Chat.Id, _botClient);
        }

        public Task loc()
        {
            var record = _repositories.Recording.GetItemById(Convert.ToInt32(_message.Text));

            if (record == null)
                return CommandsAsinc.SendTextMessageAsync("Такой записи не существует", _message.Chat.Id, _botClient);

            record.ChangeIsLocked();
            _repositories.Recording.Update(record);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Стутус записи изменен", _message.Chat.Id, _botClient);
        }

        public Task can()
        {
            var record = _repositories.Recording.GetItemById(Convert.ToInt32(_message.Text));

            if (record == null)
                return CommandsAsinc.SendTextMessageAsync("Такой записи не существует", _message.Chat.Id, _botClient);

            record.IsApproved = false;
            record.IsLocked = false;
            _repositories.Recording.Update(record);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Стутус записи изменен", _message.Chat.Id, _botClient);
        }

        public Task getcom()
        {
            var record = _repositories.Recording.GetItemById(Convert.ToInt32(_message.Text));
            if (record.Comment != null)
                return CommandsAsinc.SendTextMessageAsync(record.Comment, _message.Chat.Id, _botClient);
            else
                return CommandsAsinc.SendTextMessageAsync("Комментария нету :(", _message.Chat.Id, _botClient);
        }

        //Посмотреть информацию о записи
        public Task getrec()
        {
            var record = _repositories.Recording.GetItemById(Convert.ToInt32(_message.Text));
            if(record == null)
                return CommandsAsinc.SendTextMessageAsync("Такой записи нету", _message.Chat.Id, _botClient);

            record.Candidate = _repositories.Candidate.GetItemById(record.CandidateId);
            record.Vacancy = _repositories.Vacancy.GetItemById(record.VacancyId);

            return CommandsAsinc.SendTextMessageAsync(CommandHelper.CreateStringByRecording(record), _message.Chat.Id, _botClient);
        }

        //Создать вакансию
        public Task addvac()
        {
            var mess = CommandHelper.SplitString(_message.Text);

            if(_repositories.Calendar.GetItemById(Convert.ToInt32(mess.Item2)) == null)
                return CommandsAsinc.SendTextMessageAsync("Такого календаря не существует", _message.Chat.Id, _botClient);

            var vac = new Vacancy
            {
                Name = mess.Item1,
                CalendarId = Convert.ToInt32(mess.Item2)
            };

            _repositories.Vacancy.Add(vac);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Вакансия создана", _message.Chat.Id, _botClient);
        }

        //Привязать календярь к вакансии
        public Task bindvac()
        {
            var mess = CommandHelper.SplitString(_message.Text);

            if (_repositories.Calendar.GetItemById(Convert.ToInt32(mess.Item2)) == null)
                return CommandsAsinc.SendTextMessageAsync("Такого календаря не существует", _message.Chat.Id, _botClient);

            var vac =  _repositories.Vacancy.GetItemById(Convert.ToInt32(mess.Item1));

            if (vac == null)
                return CommandsAsinc.SendTextMessageAsync("Такой вакансии не существует", _message.Chat.Id, _botClient);

            vac.CalendarId = Convert.ToInt32(mess.Item2);

            _repositories.Vacancy.Update(vac);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Вакансия изменена", _message.Chat.Id, _botClient);
        }

        //Авторизоваться как админ
        public Task login()
        {
            return CommandsAsinc.SendTextMessageAsync("Вакансия изменена", _message.Chat.Id, _botClient);
        }

        //остановить вакансию
        public Task stopvac()
        {
            var vac = _repositories.Vacancy.GetItemById(Convert.ToInt32(_message.Text));

            if (vac == null)
                return CommandsAsinc.SendTextMessageAsync("Такой вакансии не существует", _message.Chat.Id, _botClient);

            vac.IsActive = false;

            _repositories.Vacancy.Update(vac);

            _repositories.SaveChanges();

            return CommandsAsinc.SendTextMessageAsync("Вакансия остановлена", _message.Chat.Id, _botClient);
        }

    }
}
