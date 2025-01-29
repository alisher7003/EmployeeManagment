using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using EmployeeManagment.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace EmployeeManagment.BackgroundServices
{
    public class TelegramBotService : BackgroundService
    {
        private readonly ILogger<TelegramBotService> _logger;
        private readonly TelegramBotClient _botClient;
        private readonly ReceiverOptions _receiverOptions;
        private readonly EMDbContext dbContext;

        public TelegramBotService(ILogger<TelegramBotService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _botClient = new TelegramBotClient("BOT_TOKEN");

            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery, UpdateType.InlineQuery } // Only listen to messages,
            };
            // we can use DbContextFactory instead of this code.
            using var scope = serviceProvider.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<EMDbContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramBotService is starting.");

            // Start receiving updates
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                receiverOptions: _receiverOptions,
                cancellationToken: stoppingToken
            );

            // Keep the service alive
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery != null)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery, cancellationToken);
                return;
            }

            if (update.Message is not { } message) return;
            if (message.Text is not { } messageText) return;

            _logger.LogInformation($"Received message: {messageText} from {message.Chat.Id}");

            await botClient.SendMessage(
                chatId: message.Chat.Id,
                text: $"You said: {messageText}",
                cancellationToken: cancellationToken
            );

            if (update.Message.Text?.ToLower()?.Contains("poll") is true)
                await botClient.SendPoll(
                    chatId: message.Chat.Id,
                    question: "How are you?",
                    options: new[]
                    {
                        new InputPollOption("Good"),
                        new InputPollOption("Bad"),
                        new InputPollOption("Ok")
                    },
                    isAnonymous: false,
                    cancellationToken: cancellationToken
                );
            if (update.Message.Text?.ToLower()?.Contains("gif") is true)
                await botClient.SendAnimation(
                    chatId: message.Chat.Id, // User ID (Chat ID)
                    animation: "https://media.giphy.com/media/3o7TKz9bX9v9KzCnXK/giphy.gif", // GIF URL
                    caption: "Here is a fun GIF for you! 🎉",
                    cancellationToken: cancellationToken
                );

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("👍 Like", "like"),
                    InlineKeyboardButton.WithCallbackData("👎 Dislike", "dislike")
                }
            });

            await botClient.SendMessage(
                chatId: message.Chat.Id,
                text: "How do you feel about this bot?",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );

        }

        private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            string responseMessage;

            if (callbackQuery.Data == "like")
            {
                responseMessage = "Thank you for liking the bot! 👍";
            }
            else if (callbackQuery.Data == "dislike")
            {
                responseMessage = "Sorry to hear that! 👎 We'll try to improve.";
            }
            else
            {
                responseMessage = "Unknown response!";
            }

            await botClient.SendMessage(
                chatId: callbackQuery.Message!.Chat.Id,
                text: responseMessage,
                cancellationToken: cancellationToken
            );
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"Telegram Bot Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
