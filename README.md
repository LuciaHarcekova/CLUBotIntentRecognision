# Conversational language understanding (CLU) Bot Intent Recognision
During this POC, I have been testing the Conversational language understanding (descendant of LUIS) to recognize intents from user inputs using the Microsoft bot SDK framework.

## Conversational language understanding (CLU)
CLU is a cloud-based API service that applies machine-learning intelligence to enable you to build natural language understanding component to be used in an end-to-end conversational application.

CLU enables users to build custom natural language understanding models to predict the overall intention of an incoming utterance and extract important information from it. CLU only provides the intelligence to understand the input text for the client application and doesn't perform any actions.

## Intent recognition
Intent recognition, is a natural language processing technique used to identify the meaning or intent behind a user's input or request in a written or spoken format. The objective of intent recognition is to comprehend the user's intention behind their input and classify it into one or more pre-defined categories or intents. Typically used in chatbots, virtual assistants, and automated customer service systems, intent recognition enables the computer system to accurately understand and respond to user requests or queries in real-time.

## Quickstart

### Create Azure resource
1. Go to [Azure portal](https://ms.portal.azure.com/#home) and create "Languge" resource.

### Create and train your model
1. Go to the Language Studio and sign in with your Azure account.

#### Create project
1. In the Choose a [language resource](https://language.cognitive.azure.com/) window that appears, find your Azure subscription, and choose your Language resource. If you don't have a resource, you can create a new one.
2. Create a conversational language understanding project.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/Create_clu_project.png" alt="create_clu_project"/>

## Build schema
1. Go to the "Schema defnition" and click the "+Add" button to create a new schema.
2. Add the new intents. 
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/Add_intent.png" alt="Add_intent"/>

## Label utterances
1. Navigate to the "Data labeling".
2. And add the new Entities.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/add_entity.png" alt="add_entity"/>
3. After that step by step add Intent+Utterance couples, in each Utterance tag entities.

<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/Data_labeling.png" alt="Data_labeling"/>


<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/data_labeled.png" alt="data_labeled"/>

### Train model
1. Select "Training jobs" from the left side menu.
2. Select Train a new model and enter a new model name in the text box. Otherwise to replace an existing model with a model trained on the new data, select Overwrite an existing model and then select an existing model. Overwriting a trained model is irreversible, but it won't affect your deployed models until you deploy the new model.
3. Select training mode. You can choose Standard training for faster training, but it is only available for English. Or you can choose Advanced training which is supported for other languages and multilingual projects, but it involves longer training times. Learn more about training modes.
4. Select a data splitting method. You can choose Automatically splitting the testing set from training data where the system will split your utterances between the training and testing sets, according to the specified percentages. Or you can Use a manual split of training and testing data, this option is only enabled if you have added utterances to your testing set when you labeled your utterances.
5. Click the "Train" button.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/train_model.png" alt="train_model"/>

### Deploy model
1. Select "Deploying a model" from the left side menu.
2. Select Add deployment to start the Add deployment wizard.
3. Select Create a new deployment name to create a new deployment and assign a trained model from the dropdown below. You can otherwise select Overwrite an existing deployment name to effectively replace the model that's used by an existing deployment.
4. Select a trained model from previous part.
5. Select Deploy to start the deployment job.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/deploy_model.png" alt="deploy_model"/>


## Run the code
After we created the model, let's connect it with our VS Bot Framework project. I have used prebuild solution and then created a connection to call the model, I have created.

1. Build the basics Bot Framework project. I have used "Echo Bot" as a starting point.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/echo_bot.png" alt="echo_bot"/>

2. Install the Azure Cognitive Language Services Conversations client library for .NET with NuGet:
  ```
  dotnet add package Azure.AI.Language.Conversations
```
3. Add the connection with the model. 
In order to interact with the Conversations service, you'll need to create an instance of the ConversationAnalysisClient class. 
You will need an endpoint, and an API key to instantiate a client object. You will get them from the "Overview page" of the "Language" resource in the Azure Portal.
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/portal_manage_keys.png" alt="portal_manage_keys"/>

```
  Uri endpoint = new Uri("https://myaccount.cognitive.microsoft.com");
  AzureKeyCredential credential = new AzureKeyCredential("{api-key}");
  ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);
```  
4. Rest of the code changes are in file [RecognizeIntentBot.cs](https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/CLUBotIntentRecognision/Bots/RecognizeIntentBot.cs)

## Enjoy the bot
<img src="https://github.com/LuciaHarcekova/CLUBotIntentRecognision/blob/master/Assets/dialog_example.png" alt="dialog_example"/>

## Resources
- [Quickstart: Conversational language understanding](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/conversational-language-understanding/quickstart?pivots=language-studio)
- [What is conversational language understanding?](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/conversational-language-understanding/overview)
- [Azure Cognitive Language Services Conversations client library for .NET - version 1.0.0](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/ai.language.conversations-readme?view=azure-dotnet)
