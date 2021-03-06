﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.BotBuilderSamples.Translation;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
<<<<<<< HEAD
    /// This is a Transient lifetime service.  Transient lifetime services are created
=======
    /// This is a Transient lifetime service. Transient lifetime services are created
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class MultiLingualBot : IBot
    {
<<<<<<< HEAD
        private const string English = "en";
        private const string Spanish = "es";
=======
        private const string EnglishEnglish = "en";
        private const string EnglishSpanish = "es";
        private const string SpanishEnglish = "in";
        private const string SpanishSpanish = "it";
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

        private readonly MultiLingualBotAccessors _accessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLingualBot"/> class.
        /// </summary>
        /// <param name="accessors">Bot State Accessors.</param>
        public MultiLingualBot(MultiLingualBotAccessors accessors)
        {
            _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));
        }

        private DialogSet Dialogs { get; set; }

        /// <summary>
        /// Run every turn of the conversation. Handles orchestration of messages.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                string userLanguage = await _accessors.LanguagePreference.GetAsync(turnContext, () => TranslationSettings.DefaultLanguage) ?? TranslationSettings.DefaultLanguage;

                bool translate = userLanguage != TranslationSettings.DefaultLanguage;

                if (IsLanguageChangeRequested(turnContext.Activity.Text))
                {
<<<<<<< HEAD
=======
                    var curentLang = turnContext.Activity.Text.ToLower();
                    var lang = curentLang == EnglishEnglish || curentLang == SpanishEnglish ? EnglishEnglish : EnglishSpanish;

>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    // If the user requested a language change through the suggested actions with values "es" or "en",
                    // simply change the user's language preference in the user state.
                    // The translation middleware will catch this setting and translate both ways to the user's
                    // selected language.
                    // If Spanish was selected by the user, the reply below will actually be shown in spanish to the user.
<<<<<<< HEAD
                    await _accessors.LanguagePreference.SetAsync(turnContext, turnContext.Activity.Text);
                    var reply = turnContext.Activity.CreateReply($"Your current language code is: {turnContext.Activity.Text}");
=======
                    await _accessors.LanguagePreference.SetAsync(turnContext, lang);
                    var reply = turnContext.Activity.CreateReply($"Your current language code is: {lang}");
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

                    await turnContext.SendActivityAsync(reply, cancellationToken);

                    // Save the user profile updates into the user state.
                    await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
                }
                else
                {
                    // Show the user the possible options for language. If the user chooses a different language
                    // than the default, then the translation middleware will pick it up from the user state and
                    // translate messages both ways, i.e. user to bot and bot to user.
                    var reply = turnContext.Activity.CreateReply("Choose your language:");
                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = new List<CardAction>()
                        {
<<<<<<< HEAD
                            new CardAction() { Title = "Español", Type = ActionTypes.PostBack, Value = Spanish },
                            new CardAction() { Title = "English", Type = ActionTypes.PostBack, Value = English },
=======
                            new CardAction() { Title = "Español", Type = ActionTypes.PostBack, Value = EnglishSpanish },
                            new CardAction() { Title = "English", Type = ActionTypes.PostBack, Value = EnglishEnglish },
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                        },
                    };

                    await turnContext.SendActivityAsync(reply);
                }
            }
        }

        private static bool IsLanguageChangeRequested(string utterance)
        {
            if (string.IsNullOrEmpty(utterance))
            {
                return false;
            }

            utterance = utterance.ToLower().Trim();
<<<<<<< HEAD
            return utterance == Spanish || utterance == English;
        }
    }
}
=======
            return utterance == EnglishSpanish || utterance == EnglishEnglish
                || utterance == SpanishSpanish || utterance == SpanishEnglish;
        }
    }
}
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
