using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System;
using Azure.Core;
using System.Text.Json;
using Azure;
using Azure.AI.Language.Conversations;

namespace CLUBotIntentRecognision.Bots
{
    public class RecognizeIntentBot : ActivityHandler
    {
        private readonly object _cluProjectName = "projectName";
        private readonly object _cluDeploymentName = "deploymentName";
        private readonly ConversationAnalysisClient _conversationsClient;

        public RecognizeIntentBot(IConfiguration configuration)
        {
            Uri endpoint = new Uri("https://lharcekova-clu.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential(configuration["CluAPIKey"]);
            _conversationsClient = new ConversationAnalysisClient(
                endpoint,
                credential);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var data = new
            {
                analysisInput = new
                {
                    conversationItem = new
                    {
                        text = turnContext.Activity.Text,
                        id = "1",
                        participantId = "1",
                    }
                },
                parameters = new
                {
                    projectName = _cluProjectName,
                    deploymentName = _cluDeploymentName,
                    stringIndexType = "Utf16CodeUnit",
                },
                kind = "Conversation",
            };

            Response response = _conversationsClient.AnalyzeConversation(RequestContent.Create(data));

            JsonDocument result = JsonDocument.Parse(response.ContentStream);
            JsonElement conversationalTaskResult = result.RootElement;
            JsonElement orchestrationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");

            await turnContext.SendActivityAsync(MessageFactory.Text(orchestrationPrediction.ToString()), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(
            IList<ChannelAccount> membersAdded, 
            ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
