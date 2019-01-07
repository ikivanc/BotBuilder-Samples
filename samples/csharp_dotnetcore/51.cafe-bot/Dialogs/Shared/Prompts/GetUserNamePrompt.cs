﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
<<<<<<< HEAD
using Newtonsoft.Json;
=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

namespace Microsoft.BotBuilderSamples
{
    public class GetUserNamePrompt : TextPrompt
    {
<<<<<<< HEAD
        // The name of the bot you deployed.
        public static readonly string MsBotName = "cafe66";

=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
        /// <summary>
        /// Key in the bot config (.bot file) for the LUIS instances.
        /// In the .bot file, multiple instances of LUIS can be configured.
        /// </summary>
<<<<<<< HEAD
        public static readonly string LuisConfiguration = MsBotName + "_" + "getUserProfile";
=======
        public static readonly string LuisConfiguration = $"getUserProfile";
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

        // Dialog name
        private const string InterruptionDispatcher = "interruptionDispatcherDialog";

        // LUIS intent names from ./Resources/getUserProfile.lu
        private const string WhyDoYouAsk = "Why_do_you_ask";
        private const string GetUserNameIntent = "Get_user_name";
        private const string NoNameIntent = "No_Name";
        private const string NoneIntent = "None";
        private const string CancelIntent = "Cancel";

        // User name entity from ./Resources/getUserProfile.lu
        private const string UserName = "userName";
        private const string UserNamePatternAny = "userName_patternAny";
        private const string TurnCounterProperty = "turnCounterProperty";
        private const bool HaveUserProfile = true;
        private const bool NoUserPrompt = false;
        private const string ConfirmCancelPrompt = "confirmCancelPrompt";

        private readonly IStatePropertyAccessor<CounterState> _turnCounterAccessor;
        private readonly BotServices _botServices;
        private readonly IStatePropertyAccessor<UserProfile> _userProfileAccessor;
        private readonly ConversationState _conversationState;
        private readonly IStatePropertyAccessor<OnTurnProperty> _onTurnAccessor;

        public GetUserNamePrompt(
                    string dialogId,
                    BotServices botServices,
                    IStatePropertyAccessor<UserProfile> userProfileAccessor,
                    ConversationState conversationState,
                    IStatePropertyAccessor<OnTurnProperty> onTurnAccessor,
                    IStatePropertyAccessor<CounterState> onTurnCounterAccessor,
                    PromptValidator<string> validator = null)
            : base(dialogId, validator)
        {
            _botServices = botServices ?? throw new ArgumentNullException(nameof(botServices));
            _userProfileAccessor = userProfileAccessor ?? throw new ArgumentNullException(nameof(botServices));
            _conversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
            _onTurnAccessor = onTurnAccessor ?? throw new ArgumentNullException(nameof(onTurnAccessor));
            _turnCounterAccessor = onTurnCounterAccessor ?? throw new ArgumentNullException(nameof(_turnCounterAccessor));
        }

        public async override Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken))
        {
            var context = dc.Context;

            // Get turn counter
            var turnCounter = await _turnCounterAccessor.GetAsync(context, () => new CounterState());
            turnCounter.TurnCount = ++turnCounter.TurnCount;

<<<<<<< HEAD
            // set updated turn counter
=======
            // Set updated turn counter
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            await _turnCounterAccessor.SetAsync(context, turnCounter);

            // See if we have card input. This would come in through onTurnProperty
            var onTurnProperty = await _onTurnAccessor.GetAsync(context);
            if (onTurnProperty != null)
            {
                if (onTurnProperty.Entities.Count > 0)
                {
                    // find user name in on turn property
                    var userNameInOnTurnProperty = onTurnProperty.Entities.FirstOrDefault(item => string.Compare(item.EntityName, UserName, StringComparison.Ordinal) == 0);
                    if (userNameInOnTurnProperty != null)
                    {
                        var userName = userNameInOnTurnProperty.Value as string;
                        if (!string.IsNullOrWhiteSpace(userName))
                        {
                            await UpdateUserProfilePropertyAsync(userName, context);
                        }

                        return await ContinueDialogAsync(dc);
                    }
                }
            }

            if (turnCounter.TurnCount >= 1)
            {
<<<<<<< HEAD
                // We we need to get user's name right. Include a card.
=======
                // We need to get user's name right. Include a card.
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                var activity = dc.Context.Activity.CreateReply();
                activity.Attachments = new List<Attachment> { Helpers.CreateAdaptiveCardAttachment(@".\Dialogs\WhoAreYou\Resources\getNameCard.json"), };
                await context.SendActivityAsync(activity);
            }
            else if (turnCounter.TurnCount >= 3)
            {
                // We are not going to spend more than 3 turns to get user's name.
                return await EndGetUserNamePromptAsync(dc);
            }

<<<<<<< HEAD
            // call LUIS and get results
=======
            // Call LUIS and get results
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            var luisResults = await _botServices.LuisServices[LuisConfiguration].RecognizeAsync(context, cancellationToken);

            var topLuisIntent = luisResults.GetTopScoringIntent();
            var topIntent = topLuisIntent.intent;

            if (string.IsNullOrWhiteSpace(topIntent))
            {
<<<<<<< HEAD
                // go with intent in onTurnProperty
=======
                // Go with intent in onTurnProperty
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                topIntent = string.IsNullOrWhiteSpace(onTurnProperty.Intent) ? "None" : onTurnProperty.Intent;
            }

            // Did user ask for help or said they are not going to give us the name?
            switch (topIntent)
            {
                case NoNameIntent:
<<<<<<< HEAD
                    // set user name in profile to Human
=======
                    // Set user name in profile to Human
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    await _userProfileAccessor.SetAsync(context, new UserProfile("Human"));
                    return await EndGetUserNamePromptAsync(dc);
                case GetUserNameIntent:
                    // Find the user's name from LUIS entities list.
                    if (luisResults.Entities.TryGetValue(UserName, out var entity))
                    {
                        var userName = (string)entity[0];
                        await UpdateUserProfilePropertyAsync(userName, context);
                        return await base.ContinueDialogAsync(dc);
                    }
                    else if (luisResults.Entities.TryGetValue(UserNamePatternAny, out var entity_pattery))
                    {
                        var userName = (string)entity_pattery[0];
                        await UpdateUserProfilePropertyAsync(userName, context);
                        return await base.ContinueDialogAsync(dc);
                    }
                    else
                    {
                        await context.SendActivityAsync("Sorry, I didn't get that. What's your name ?");
                        return await base.ContinueDialogAsync(dc);
                    }

                case WhyDoYouAsk:
                    await context.SendActivityAsync("I need your name to be able to address you correctly!");
                    await context.SendActivityAsync(MessageFactory.SuggestedActions(new List<string> { "I won't give you my name", "What is your name?" }));
                    return await base.ContinueDialogAsync(dc);

                case NoneIntent:
                    await UpdateUserProfilePropertyAsync(context.Activity.Text, context);
                    return await base.ContinueDialogAsync(dc);

                case CancelIntent:
<<<<<<< HEAD
                    // start confirmation prompt
=======
                    // Start confirmation prompt
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    var opts = new PromptOptions
                    {
                        Prompt = new Activity
                        {
                            Type = ActivityTypes.Message,
                            Text = "Are you sure you want to cancel ?",
                        },
                    };
                    return await dc.PromptAsync(ConfirmCancelPrompt, opts);

                default:
                    // Handle interruption.
                    var onTurnPropertyValue = await _onTurnAccessor.GetAsync(dc.Context);
                    return await dc.BeginDialogAsync(InterruptionDispatcher, onTurnPropertyValue);
            }
        }

        public async override Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (result == null)
            {
                // User said yes to cancel prompt.
<<<<<<< HEAD
                await dc.Context.SendActivityAsync("Sure. I've cancelled that!");
=======
                await dc.Context.SendActivityAsync("Sure. I've canceled that!");
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                return await dc.CancelAllDialogsAsync();
            }
            else
            {
                // User said no to cancel.
                return await base.ResumeDialogAsync(dc, reason, result);
            }
        }

        protected async Task<DialogTurnResult> EndGetUserNamePromptAsync(DialogContext innerDc)
        {
            var context = innerDc.Context;
            await context.SendActivityAsync("No worries. Hello Human, nice to meet you!");
            await context.SendActivityAsync("You can always say 'My name is <your name>' to introduce yourself to me.");

            // End this dialog since user does not wish to proceed further.
            return await innerDc.EndDialogAsync(NoUserPrompt);
        }

        protected async Task UpdateUserProfilePropertyAsync(string userName, ITurnContext context)
        {
            var userProfile = await _userProfileAccessor.GetAsync(context);
            if (userProfile == null)
            {
                userProfile = new UserProfile(userName);
            }
            else
            {
                userProfile.UserName = userName;
            }

            await _userProfileAccessor.SetAsync(context, userProfile);
        }
    }
}
