// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import { ActivityTypes, ConversationState, MemoryStorage } from 'botbuilder-core';
import 'botframework-webchat/botchat.css';
import { App } from 'botframework-webchat/built/App';
import './css/app.css';
import { WebChatAdapter } from './webChatAdapter';

// Create the custom WebChatAdapter.
const webChatAdapter = new WebChatAdapter();

<<<<<<< HEAD
// Connect our BotFramework-WebChat App instance with the DOM.
App({
    user: { id: "Me!" },
    bot: { id: "bot" },
    botConnection: webChatAdapter.botConnection,
=======
// Create user and bot profiles.
// These profiles fill out additional information on the incoming and outgoing Activities.
export const USER_PROFILE = { id: 'Me!', name: 'Me!', role: 'user' };
export const BOT_PROFILE = { id: 'bot', name: 'bot', role: 'bot' };

// Connect our BotFramework-WebChat App instance with the DOM.
App({
    user: USER_PROFILE,
    bot: BOT_PROFILE,
    botConnection: webChatAdapter.botConnection
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
}, document.getElementById('bot'));

// Instantiate MemoryStorage for use with the ConversationState class.
const memory = new MemoryStorage();

// Add the instantiated storage into ConversationState.
const conversationState = new ConversationState(memory);

// Create a property to keep track of how many messages are received from the user.
const countProperty = conversationState.createProperty('turnCounter');

// Register the business logic of the bot through the WebChatAdapter's processActivity implementation.
webChatAdapter.processActivity(async (turnContext) => {
    // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
    if (turnContext.activity.type === ActivityTypes.Message) {
        // Read from state.
        let count = await countProperty.get(turnContext);
        count = count === undefined ? 1 : count;
<<<<<<< HEAD
        await turnContext.sendActivity(`${count}: You said "${turnContext.activity.text}"`);
        // Increment and set turn counter.
        await countProperty.set(turnContext, ++count);
    } else {
        await turnContext.sendActivity(`[${turnContext.activity.type} event detected]`);
=======
        await turnContext.sendActivity(`${ count }: You said "${ turnContext.activity.text }"`);
        // Increment and set turn counter.
        await countProperty.set(turnContext, ++count);
    } else {
        await turnContext.sendActivity(`[${ turnContext.activity.type } event detected]`);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
    }
    await conversationState.saveChanges(turnContext);
});

// Prevent Flash of Unstyled Content (FOUC): https://en.wikipedia.org/wiki/Flash_of_unstyled_content
<<<<<<< HEAD
document.addEventListener('DOMContentLoaded', function () {
    requestAnimationFrame(() => document.body.style.visibility = 'visible');
=======
document.addEventListener('DOMContentLoaded', () => {
    window.requestAnimationFrame(() => {
        document.body.style.visibility = 'visible';
        // After the content has finished loading, send the bot a "conversationUpdate" Activity with the user's information.
        // When the bot receives a "conversationUpdate" Activity, the developer can opt to send a welcome message to the user.
        webChatAdapter.botConnection.postActivity({
            recipient: BOT_PROFILE,
            membersAdded: [ USER_PROFILE ],
            type: ActivityTypes.ConversationUpdate
        });
    });
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
});
