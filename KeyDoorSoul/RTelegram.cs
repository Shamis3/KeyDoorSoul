using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

public enum TypeCard
{
    Arrow, Pause
}

public class RTelegeram
{
    public static ITelegramBotClient bot = new TelegramBotClient("7070685607:AAFcqL5wTwkCp0FxYjRaTeOatDeyYMCq5cs");
    
    public static void Start()
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { },
        };

        bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var messages = new Messages();
        
        Messages.SetMessages(messages);

        var news = Messages.GetMessages();


        ReplyKeyboardMarkup keyboardStart = new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Продолжить" } }) { ResizeKeyboard = true };

        try
        {

            string name1 = "ysdf,l";
            string action = "sadas";
            string quest = "sdad";
            

            Card card = new Card()
            {
                Name = "Болт Зевса",
                ActionCard = "Отсасать",
                Quest = "Вкусно? пидор"
            };

            Card card2 = new Card()
            {
                Name = "Болт Pudge",
                ActionCard = "Отсасать Pege",
                Quest = "Вкусно? geyS"
            };

            List<Card> listCards = new List<Card>();
            listCards.Add(card2);
            listCards.Add(card);

            var selectCard = listCards.Where(x => x.Name == "Болт Зевса").Select(y => y);

            var cardName = card.Quest;

            var message = update.Message.Text;
            ChatId id = new ChatId(update.Message.Chat.Id);
            int messageId = update.Message.MessageId;

            // if(условие){ код, который работает если условие == true }
            if(message == "/start")
            {
                bot.DeleteMessageAsync(id, messageId);

                InputFile photo = null;
                bot.SendTextMessageAsync(id, "иллюстрация 1"); // Отправка иллюстрации 1
                bot.SendTextMessageAsync(id, "введние", replyMarkup: keyboardStart); // Отправка введения в игру
                
            }

            if (message == "Продолжить")
            {
                bot.DeleteMessageAsync(id, messageId - 1); // Удаление пред-го сообщения
                bot.DeleteMessageAsync(id, messageId);


                bot.SendTextMessageAsync(id, "иллюстрация 2"); // Отправка иллюстрации 2
                bot.SendTextMessageAsync(id, "Вводное приключение"); // Вводное сообщение
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        await Task.Run(() => { });
    }
}



class Card
{
    public string Name { get; set; } = string.Empty;
    public string ActionCard { get; set; }
    public string Quest { get; set; }
    public TypeCard Type { get; set; } = TypeCard.Pause;
}


class Person    
{
    public ChatId Id { get; set;}
    public string Name { get; set; } 
}


class Messages
{
    private string Vvodnoe { get; set; } = "Первое сообщение";
    private string Adventure { get; set; } = "Второе сообщение";


    public static Messages GetMessages()
    {
        var path = Environment.CurrentDirectory + @$"\messages.json";

        var jsonText = File.ReadAllText(path);

        var messages = JsonConvert.DeserializeObject<Messages>(jsonText);

        return messages;
    }

    public static void SetMessages(Messages messages)
    {
        var path = Environment.CurrentDirectory + @$"\messages.json";

        string jsonText = JsonConvert.SerializeObject(messages);

        File.WriteAllText(path, jsonText );
    }
}
