﻿using FeistelCipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services
{
    class TextCommand : ITextCommand
    {
        private readonly IFeistelSipher _sipher;
        public TextCommand(IFeistelSipher sipher) => _sipher = sipher;

        public string GetText(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return "Не корректная команда";
            }
            string cmd = message;
            var welcomeCommands = GetWelcomeCommands();
            if(welcomeCommands.Any(c => c.Equals(cmd, StringComparison.OrdinalIgnoreCase)))
            {
                return GetStartMessage();
            }
            else
            {
                /*if(message.Length > 2 && message.Substring(0, 2).ToLower() == "/d")
                {
                    return sipher.CryptText(message[2..].Trim(), true);
                }*/
                string result = _sipher.CryptText(message.Trim());
                return $"Encrypted text: {result}\r\n\r\nDecrypted Text: {_sipher.CryptText(result, true)}";
            }
        }

        private string GetStartMessage() => @$"Здравствуй, пользователь! Я шифровальный бот) Могу зашифровать/расшифровать любое твое сообщение. Для шифрования я использую шифр Фейстеля!
Вот список моих комманд:
{GetCommands()}
Следовательно, для того чтобы зашифровать Ваш текст, просто введите его.";

        private string GetCommands() => @"/start - Начало диалога с ботом, вступительное сообщение;
/test, /connect - эти команды аналогичны команде /start, предназначены для проверки соединения с ботом;
[ваш текст] - текст который необходимо зашифровать";

        private string[] GetWelcomeCommands() => new string[]
        {
            "/start", "-start",
            "/test", "-test",
            "/connect", "-connect"
        };
    }
}
