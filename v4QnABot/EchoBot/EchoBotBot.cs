// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using EchoBot.Models;
using EchoBot;

namespace EchoBot
{
    public class EchoBotBot : IBot
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;

        public EchoBotBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoBotBot>();
            _logger.LogTrace("Turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

        public async Task OnTurnAsync(ITurnContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (context.Activity.Type == ActivityTypes.Message)
            {
                var response = await CustomQnAMaker.GetResults(context.Activity.Text);
                var qnaResponse = JsonConvert.DeserializeObject<QnaResponse>(response);

                if (response != "failure")
                {
                    //if (!response.Any())
                    if(qnaResponse.answers[0].score < 10.0)
                    {
                        await context.SendActivityAsync("回答が見つかりませんでした。");
                    }
                    else
                    {
                        await context.SendActivityAsync($"{qnaResponse.answers[0].answer}");

                        /*
                        if (qnaResponse.answers.Count() >= 2)
                        {
                            await context.SendActivityAsync($"自信がないので他の回答もお伝えします\n\n{qnaResponse.answers[1].answer}");
                        }
                        */
                    }
                }
            }
            else
            {
                //await context.SendActivityAsync($"{context.Activity.Type} event detected");
                await context.SendActivityAsync($"ようこそ！");
            }
           
        }

    }
}
