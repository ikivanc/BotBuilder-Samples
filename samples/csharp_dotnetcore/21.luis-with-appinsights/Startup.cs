<<<<<<< HEAD
﻿// Copyright (c) Microsoft Corporation. All rights reserved.
=======
// Copyright (c) Microsoft Corporation. All rights reserved.
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.IO;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
<<<<<<< HEAD
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.BotBuilderSamples.AppInsights;
=======
using Microsoft.Bot.Builder.Integration.ApplicationInsights.Core;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.BotBuilderSamples
{
    public class Startup
    {
        private ILoggerFactory _loggerFactory;
        private bool _isProduction = false;

        public Startup(IHostingEnvironment env)
        {
            _isProduction = env.IsProduction();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a <see cref="IServiceCollection"/> of service descriptors.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
        public void ConfigureServices(IServiceCollection services)
        {
            var secretKey = Configuration.GetSection("botFileSecret")?.Value;
            var botFilePath = Configuration.GetSection("botFilePath")?.Value;
<<<<<<< HEAD

            // Loads .bot configuration file and adds a singleton that your Bot can access through dependency injection.
            var botConfig = BotConfiguration.Load(botFilePath ?? @".\<YOUR BOT CONFIGURATION>.bot", secretKey);
            services.AddSingleton(sp => botConfig ?? throw new InvalidOperationException($"The .bot config file could not be loaded. ({botConfig})"));

            // Retrieve current endpoint.
            var environment = _isProduction ? "production" : "development";
            var service = botConfig.Services.Where(s => s.Type == "endpoint" && s.Name == environment).FirstOrDefault();
=======
            if (!File.Exists(botFilePath))
            {
                throw new FileNotFoundException($"The .bot configuration file was not found. botFilePath: {botFilePath}");
            }

            // Loads .bot configuration file and adds a singleton that your Bot can access through dependency injection.
            var botConfig = BotConfiguration.Load(botFilePath ?? @".\luis-with-appinsights.bot", secretKey);
            services.AddSingleton(sp => botConfig ?? throw new InvalidOperationException($"The .bot configuration file could not be loaded. botFilePath: {botFilePath}"));

            // Use Application Insights
            services.AddBotApplicationInsights(botConfig);

            // Retrieve current endpoint.
            var environment = _isProduction ? "production" : "development";
            var service = botConfig.Services.FirstOrDefault(s => s.Type == "endpoint" && s.Name == environment);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            if (!(service is EndpointService endpointService))
            {
                throw new InvalidOperationException($"The .bot file does not contain an endpoint with name '{environment}'.");
            }

            var connectedServices = InitBotServices(botConfig);
            services.AddSingleton(sp => connectedServices);

            services.AddBot<LuisBot>(options =>
            {
                options.CredentialProvider = new SimpleCredentialProvider(endpointService.AppId, endpointService.AppPassword);

<<<<<<< HEAD
                // Add MyAppInsightsLoggerMiddleware (logs activity messages into Application Insights)
                var appInsightsLogger = new MyAppInsightsLoggerMiddleware(connectedServices.TelemetryClient.InstrumentationKey, logUserName: true, logOriginalMessage: true);
                options.Middleware.Add(appInsightsLogger);

                // Creates a logger for the application to use.
                ILogger logger = _loggerFactory.CreateLogger<LuisBot>();
=======
                // Creates a telemetryClient for OnTurnError handler.
                var sp = services.BuildServiceProvider();
                var telemetryClient = sp.GetService<IBotTelemetryClient>();
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

                // Catches any errors that occur during a conversation turn and logs them.
                options.OnTurnError = async (context, exception) =>
                {
<<<<<<< HEAD
                    logger.LogError($"Exception caught : {exception}");
                    await context.SendActivityAsync("Sorry, it looks like something went wrong.");
                };

=======
                    telemetryClient.TrackException(exception);
                    await context.SendActivityAsync("Sorry, it looks like something went wrong.");
                };

                // Add MyAppInsightsLoggerMiddleware (logs activity messages into Application Insights)
                var appInsightsLogger = new TelemetryLoggerMiddleware(telemetryClient, logUserName: true, logOriginalMessage: true);
                options.Middleware.Add(appInsightsLogger);

>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                // The Memory Storage used here is for local bot debugging only. When the bot
                // is restarted, everything stored in memory will be gone.
                IStorage dataStore = new MemoryStorage();

                // For production bots use the Azure Blob or
                // Azure CosmosDB storage providers. For the Azure
                // based storage providers, add the Microsoft.Bot.Builder.Azure
                // Nuget package to your solution. That package is found at:
                // https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure/
                // Uncomment the following lines to use Azure Blob Storage
                // //Storage configuration name or ID from the .bot file.
                // const string StorageConfigurationId = "<STORAGE-NAME-OR-ID-FROM-BOT-FILE>";
                // var blobConfig = botConfig.FindServiceByNameOrId(StorageConfigurationId);
                // if (!(blobConfig is BlobStorageService blobStorageConfig))
                // {
                //    throw new InvalidOperationException($"The .bot file does not contain an blob storage with name '{StorageConfigurationId}'.");
                // }
                // // Default container name.
                // const string DefaultBotContainer = "<DEFAULT-CONTAINER>";
                // var storageContainer = string.IsNullOrWhiteSpace(blobStorageConfig.Container) ? DefaultBotContainer : blobStorageConfig.Container;
                // IStorage dataStore = new Microsoft.Bot.Builder.Azure.AzureBlobStorage(blobStorageConfig.ConnectionString, storageContainer);

                // Create Conversation State object.
                // The Conversation State object is where we persist anything at the conversation-scope.
                var conversationState = new ConversationState(dataStore);
<<<<<<< HEAD

                options.State.Add(conversationState);
=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder. This provides the mechanisms to configure the application request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment.</param>
<<<<<<< HEAD
=======
        /// <param name="loggerFactory">Represents a type used to configure the logging system and create instances of ILogger.</param>
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;

<<<<<<< HEAD
            app.UseDefaultFiles()
=======
            // Application Insights will capture ILogger based events.
            _loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);

            app.UseBotApplicationInsights()
                .UseDefaultFiles()
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                .UseStaticFiles()
                .UseBotFramework();
        }

        /// <summary>
        /// Initialize the bot's references to external services.
        ///
        /// For example, Application Insights and LUIS services
        /// are created here. These external services are configured
        /// using the <see cref="BotConfiguration"/> class (based on the contents of your ".bot" file).
        /// </summary>
        /// <param name="config">The <see cref="BotConfiguration"/> object based on your ".bot" file.</param>
        /// <returns>A <see cref="BotServices"/> representing client objects to access external services the bot uses.</returns>
        /// <seealso cref="BotConfiguration"/>
        /// <seealso cref="LuisRecognizer"/>
        private static BotServices InitBotServices(BotConfiguration config)
        {
<<<<<<< HEAD
            TelemetryClient telemetryClient = null;
            var luisServices = new Dictionary<string, LuisRecognizer>();
=======
            var luisServices = new Dictionary<string, TelemetryLuisRecognizer>();
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

            foreach (var service in config.Services)
            {
                switch (service.Type)
                {
                    case ServiceTypes.Luis:
                        {
                            var luis = (LuisService)service;
                            if (luis == null)
                            {
                                throw new InvalidOperationException("The Luis service is not configured correctly in your '.bot' file.");
                            }

                            if (string.IsNullOrWhiteSpace(luis.AppId))
                            {
                                throw new InvalidOperationException("The Luis Model Application Id ('appId') is required to run this sample.  Please update your '.bot' file.");
                            }

                            if (string.IsNullOrWhiteSpace(luis.AuthoringKey))
                            {
                                throw new InvalidOperationException("The Luis Authoring Key ('authoringKey') is required to run this sample.  Please update your '.bot' file.");
                            }

                            if (string.IsNullOrWhiteSpace(luis.SubscriptionKey))
                            {
                                throw new InvalidOperationException("The Subscription Key ('subscriptionKey') is required to run this sample.  Please update your '.bot' file.");
                            }

                            if (string.IsNullOrWhiteSpace(luis.Region))
                            {
                                throw new InvalidOperationException("The Region ('region') is required to run this sample.  Please update your '.bot' file.");
                            }

                            var app = new LuisApplication(luis.AppId, luis.SubscriptionKey, luis.GetEndpoint());
<<<<<<< HEAD
                            var recognizer = new MyAppInsightLuisRecognizer(app);
=======
                            var recognizer = new TelemetryLuisRecognizer(app);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                            luisServices.Add(LuisBot.LuisKey, recognizer);
                            break;
                        }

                    case ServiceTypes.AppInsights:
                        {
                            var appInsights = (AppInsightsService)service;
                            if (appInsights == null)
                            {
                                throw new InvalidOperationException("The Application Insights is not configured correctly in your '.bot' file.");
                            }

                            if (string.IsNullOrWhiteSpace(appInsights.InstrumentationKey))
                            {
                                throw new InvalidOperationException("The Application Insights Instrumentation Key ('instrumentationKey') is required to run this sample.  Please update your '.bot' file.");
                            }

<<<<<<< HEAD
                            var telemetryConfig = new TelemetryConfiguration(appInsights.InstrumentationKey);
                            telemetryClient = new TelemetryClient(telemetryConfig)
                            {
                                InstrumentationKey = appInsights.InstrumentationKey,
                            };
=======
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                            break;
                        }
                }
            }

<<<<<<< HEAD
            var connectedServices = new BotServices(telemetryClient, luisServices);
=======
            var connectedServices = new BotServices(luisServices);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            return connectedServices;
        }
    }
}
