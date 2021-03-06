﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License

using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
<<<<<<< HEAD
=======
using Microsoft.Bot.Builder;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
using Microsoft.Bot.Builder.AI.Luis;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Represents references to external services.
    ///
    /// For example, LUIS services are kept here as a singleton.  This external service is configured
    /// using the <see cref="BotConfiguration"/> class.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    /// <seealso cref="https://www.luis.ai/home"/>
    /// <seealso cref="https://azure.microsoft.com/en-us/services/application-insights/"/>
    public class BotServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotServices"/> class.
        /// </summary>
        /// <param name="client">An Application Insights <see cref="TelemetryClient"/> instance.</param>
        /// <param name="luisServices">A dictionary of named <see cref="LuisRecognizer"/> instances for usage within the bot.</param>
<<<<<<< HEAD
        public BotServices(TelemetryClient client, Dictionary<string, LuisRecognizer> luisServices)
        {
            TelemetryClient = client ?? throw new ArgumentNullException(nameof(client));
=======
        public BotServices(Dictionary<string, TelemetryLuisRecognizer> luisServices)
        {
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            LuisServices = luisServices ?? throw new ArgumentNullException(nameof(luisServices));
        }

        /// <summary>
<<<<<<< HEAD
        /// Gets the Application Insights Telemetry client.
        /// Use this to log new custom events/metrics/traces/etc into your
        /// Application Insights service for later analysis.
        /// </summary>
        /// <value>
        /// The Application Insights <see cref="TelemetryClient"/> instance created based on configuration in the .bot file.
        /// </value>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.applicationinsights.telemetryclient?view=azure-dotnet"/>
        public TelemetryClient TelemetryClient { get; }

        /// <summary>
=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
        /// Gets the set of LUIS Services used.
        /// Given there can be multiple <see cref="LuisRecognizer"/> services used in a single bot,
        /// LuisServices is represented as a dictionary.  This is also modeled in the
        /// ".bot" file since the elements are named.
        /// </summary>
        /// <remarks>The LUIS services collection should not be modified while the bot is running.</remarks>
        /// <value>
        /// A <see cref="LuisRecognizer"/> client instance created based on configuration in the .bot file.
        /// </value>
<<<<<<< HEAD
        public Dictionary<string, LuisRecognizer> LuisServices { get; } = new Dictionary<string, LuisRecognizer>();
    }
}
=======
        public Dictionary<string, TelemetryLuisRecognizer> LuisServices { get; } = new Dictionary<string, TelemetryLuisRecognizer>();
    }
}
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
