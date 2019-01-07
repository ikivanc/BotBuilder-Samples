﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Linq;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using Newtonsoft.Json.Linq;

namespace Microsoft.BotBuilderSamples
{
    public class ReservationProperty
    {
<<<<<<< HEAD
        private static readonly string PartySizeEntity = luisEntities[1];
        private static readonly string DateTimeEntity = luisEntities[2];
        private static readonly string LocationEntity = luisEntities[3];
        private static readonly string ConfirmationEntity = "confirmationList";
        private static readonly int FourWeeks = 4;
        private static readonly int MaxPartySize = 10;

        // Possible LUIS entities. You can refer to Dialogs\Dispatcher\Resources\entities.lu for list of entities.
        private static string[] luisEntities = { "confirmationList", "number", "datetime", "cafeLocation", "userName_patternAny", "userName" };
=======
        // Possible LUIS entities. You can refer to Dialogs\Dispatcher\Resources\entities.lu for list of entities.
        private static string[] luisEntities = { "confirmationList", "number", "datetime", "cafeLocation", "userName_patternAny", "userName" };

#pragma warning disable SA1214 // Readonly fields should appear before non-readonly fields
        private static readonly string PartySizeEntity = luisEntities[1];
        private static readonly string DateTimeEntity = luisEntities[2];
        private static readonly string LocationEntity = luisEntities[3];
        private static readonly string ConfirmationEntity = luisEntities[0];
        private static readonly int FourWeeks = 4;
        private static readonly int MaxPartySize = 10;
#pragma warning restore SA1214 // Readonly fields should appear before non-readonly fields
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

        // Date constraints for reservations
        private static string[] reservationDateConstraints =
        {
            TimexCreator.ThisWeek(),
            TimexCreator.NextWeeksFromToday(FourWeeks),
        };

        // Time constraints for reservations
        private static string[] reservationTimeConstraints =
        {
            TimexCreator.Daytime,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationProperty"/> class.
        /// </summary>
        /// <param name="id">Reservation id.</param>
        /// <param name="date">Reservation date.</param>
        /// <param name="time">Reservation time.</param>
<<<<<<< HEAD
        /// <param name="partySize">Numbe of guests in reservation.</param>
=======
        /// <param name="partySize">Number of guests in reservation.</param>
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
        /// <param name="location">Location of reservation.</param>
        /// <param name="reservationConfirmed">True if reservation confirmed; otherwise unconfirmed.</param>
        /// <param name="needsChange">True if requires a modification.</param>
        /// <param name="metaData">Additional data.</param>
        public ReservationProperty(
            string id = null,
            string date = null,
            string time = null,
            int partySize = 0,
            string location = null,
            bool reservationConfirmed = false,
            bool needsChange = false,
            object metaData = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Date = date ?? string.Empty;
            Time = time ?? string.Empty;
            DateLGString = string.Empty;
            TimeLGString = string.Empty;
            DateTimeLGString = string.Empty;
            PartySize = partySize;
            Location = location ?? string.Empty;
            ReservationConfirmed = reservationConfirmed;
            NeedsChange = needsChange;
            MetaData = metaData;
        }

        public string Id { get; }

        public string Date { get; set; }

        public string DateLGString { get; set; }

        public string Time { get; set; }

        public string TimeLGString { get; set; }

        public string DateTimeLGString { get; set; }

        public int PartySize { get; set; }

        public string Location { get; set; }

        public bool ReservationConfirmed { get; set; }

        public bool NeedsChange { get; set; }

        public object MetaData { get; set; }

        public static ReservationResult FromOnTurnProperty(OnTurnProperty onTurnProperty)
        {
            var returnResult = new ReservationResult(new ReservationProperty());
            return Validate(onTurnProperty, returnResult);
        }

        public static ReservationResult Validate(OnTurnProperty onTurnProperty, ReservationResult returnResult)
        {
            if (onTurnProperty == null || onTurnProperty.Entities.Count == 0)
            {
                return returnResult;
            }

            // We only will pull number -> party size, datetimeV2 -> date and time, cafeLocation -> location.
            var numberEntity = onTurnProperty.Entities.Find(item => item.EntityName.Equals(PartySizeEntity));
<<<<<<< HEAD
            var dateTimeEntity = onTurnProperty.Entities.Find(item => item.EntityName.Equals(ReservationProperty.DateTimeEntity));
=======
            var dateTimeEntity = onTurnProperty.Entities.Find(item => item.EntityName.Equals(DateTimeEntity));
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            var locationEntity = onTurnProperty.Entities.Find(item => item.EntityName.Equals(LocationEntity));
            var confirmationEntity = onTurnProperty.Entities.Find(item => item.EntityName.Equals(ConfirmationEntity));

            if (numberEntity != null)
            {
                // We only accept MaxPartySize in a reservation.
<<<<<<< HEAD
                if (int.Parse(numberEntity.Value as string) > MaxPartySize)
                {
                    returnResult.Outcome.Add(new ReservationOutcome("Sorry. " + int.Parse(numberEntity.Value as string) + " does not work. I can only accept up to 10 guests in a reservation.", PartySizeEntity));
=======
                var partySize = int.Parse(numberEntity.Value as string);
                if (partySize > MaxPartySize)
                {
                    returnResult.Outcome.Add(new ReservationOutcome($"Sorry. {int.Parse(numberEntity.Value as string)} does not work. I can only accept up to 10 guests in a reservation.", PartySizeEntity));
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    returnResult.Status = ReservationStatus.Incomplete;
                }
                else
                {
<<<<<<< HEAD
                    returnResult.NewReservation.PartySize = (int)numberEntity.Value;
=======
                    returnResult.NewReservation.PartySize = partySize;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                }
            }

            if (dateTimeEntity != null)
            {
                // Get parsed date time from TIMEX
                // LUIS returns a timex expression and so get and un-wrap that.
                // Take the first date time since book table scenario does not have to deal with multiple date times or date time ranges.
<<<<<<< HEAD
                var timeProp = dateTimeEntity.Value as string;
                if (timeProp != null)
                {
                    var today = DateTime.Now;
                    var parsedTimex = new TimexProperty(timeProp);

                    // See if the date meets our constraints.
                    if (parsedTimex.DayOfMonth != null && parsedTimex.Year != null && parsedTimex.Month != null)
                    {
                        var date = DateTimeOffset.Parse($"{parsedTimex.Year}-${parsedTimex.Month}-${parsedTimex.DayOfMonth}");
                        returnResult.NewReservation.Date = date.UtcDateTime.ToString("o").Split("T")[0];
                        returnResult.NewReservation.DateLGString = new TimexProperty(returnResult.NewReservation.Date).ToNaturalLanguage(today);
                        var validDate = TimexRangeResolver.Evaluate(dateTimeEntity.Value as string[], reservationDateConstraints);
                        if (validDate != null || (validDate.Count == 0))
                        {
                            // Validation failed!
                            returnResult.Outcome.Add(new ReservationOutcome(
                                $"Sorry. {returnResult.NewReservation.DateLGString} does not work.  "
                                + "I can only make reservations for the next 4 weeks.", ReservationProperty.DateTimeEntity));
                            returnResult.NewReservation.Date = string.Empty;
                            returnResult.Status = ReservationStatus.Incomplete;
                        }
=======
                var timexProp = ((JToken)dateTimeEntity.Value)?["timex"]?[0]?.ToString();
                if (timexProp != null)
                {
                    var today = DateTime.Now;
                    var parsedTimex = new TimexProperty(timexProp);

                    // Validate the date (and check constraints (later))
                    if (parsedTimex.DayOfMonth != null && parsedTimex.Year != null && parsedTimex.Month != null)
                    {
                        var date = DateTimeOffset.Parse($"{parsedTimex.Year}-{parsedTimex.Month}-{parsedTimex.DayOfMonth}");
                        returnResult.NewReservation.Date = date.UtcDateTime.ToString("o").Split("T")[0];
                        returnResult.NewReservation.DateLGString = new TimexProperty(returnResult.NewReservation.Date).ToNaturalLanguage(today);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    }

                    // See if the time meets our constraints.
                    if (parsedTimex.Hour != null &&
                        parsedTimex.Minute != null &&
                        parsedTimex.Second != null)
                    {
<<<<<<< HEAD
                        var validtime = TimexRangeResolver.Evaluate(dateTimeEntity.Value as string[], reservationTimeConstraints);
=======
                        var timexOptions = ((JToken)dateTimeEntity.Value)?["timex"]?.ToObject<List<string>>();

                        var validtime = TimexRangeResolver.Evaluate(timexOptions, reservationTimeConstraints);
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

                        returnResult.NewReservation.Time = ((int)parsedTimex.Hour).ToString("D2");
                        returnResult.NewReservation.Time += ":";
                        returnResult.NewReservation.Time += ((int)parsedTimex.Minute).ToString("D2");
                        returnResult.NewReservation.Time += ":";
                        returnResult.NewReservation.Time += ((int)parsedTimex.Second).ToString("D2");

<<<<<<< HEAD
                        if (validtime != null || (validtime.Count == 0))
                        {
                            // Validation failed!
                            returnResult.Outcome.Add(new ReservationOutcome("Sorry, that time does not work. I can only make reservations that are in the daytime (6AM - 6PM)", ReservationProperty.DateTimeEntity));
=======
                        if (validtime == null || (validtime.Count == 0))
                        {
                            // Validation failed!
                            returnResult.Outcome.Add(new ReservationOutcome("Sorry, that time does not work. I can only make reservations that are in the daytime (6AM - 6PM)", DateTimeEntity));
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                            returnResult.NewReservation.Time = string.Empty;
                            returnResult.Status = ReservationStatus.Incomplete;
                        }
                    }

                    // Get date time LG string if we have both date and time.
<<<<<<< HEAD
                    if (string.IsNullOrWhiteSpace(returnResult.NewReservation.Date) && string.IsNullOrWhiteSpace(returnResult.NewReservation.Time))
=======
                    if (!string.IsNullOrWhiteSpace(returnResult.NewReservation.Date) && !string.IsNullOrWhiteSpace(returnResult.NewReservation.Time))
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                    {
                        returnResult.NewReservation.DateTimeLGString = new TimexProperty(returnResult.NewReservation.Date + "T" + returnResult.NewReservation.Time).ToNaturalLanguage(today);
                    }
                }
            }

            // Take the first found value.
            if (locationEntity != null)
            {
<<<<<<< HEAD
                var cafeLocation = ((JObject)locationEntity.Value)[0][0];
=======
                var cafeLocation = locationEntity.Value;
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145

                // Capitalize cafe location.
                returnResult.NewReservation.Location = char.ToUpper(((string)cafeLocation)[0]) + ((string)cafeLocation).Substring(1);
            }

            // Accept confirmation entity if available only if we have a complete reservation
            if (confirmationEntity != null)
            {
<<<<<<< HEAD
                if ((string)((JObject)confirmationEntity.Value)[0][0] == "yes")
=======
                var value = confirmationEntity.Value as string;
                if (value != null && value == "yes")
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
                {
                    returnResult.NewReservation.ReservationConfirmed = true;
                    returnResult.NewReservation.NeedsChange = false;
                }
                else
                {
                    returnResult.NewReservation.NeedsChange = true;
                    returnResult.NewReservation.ReservationConfirmed = false;
                }
            }

            return returnResult;
        }

        public bool HaveCompleteReservation()
        {
            return !string.IsNullOrWhiteSpace(Id) &&
                   !string.IsNullOrWhiteSpace(Date) &&
                   !string.IsNullOrWhiteSpace(Time) &&
                   PartySize > 0 &&
                   !string.IsNullOrWhiteSpace(Location);
        }

        public ReservationResult UpdateProperties(OnTurnProperty onTurnProperty)
        {
            var returnResult = new ReservationResult(this);
            return Validate(onTurnProperty, returnResult);
        }

        public string GetMissingPropertyReadOut()
        {
            if (string.IsNullOrWhiteSpace(Location))
            {
                return "What city?";
            }
            else if (string.IsNullOrWhiteSpace(Date))
            {
                return "When do you want to come in?";
            }
            else if (string.IsNullOrWhiteSpace(Time))
            {
                return "What time?";
            }
            else if (PartySize == 0)
            {
                return "How many guests ?";
            }

            return string.Empty;
        }

        /// <summary>
        /// Helper method for Language Generation read out based on properties that have been captured.
        /// </summary>
        /// <returns>A <see cref="string"/> which provides a sentence.</returns>
        public string GetGroundedPropertiesReadOut()
        {
            var today = DateTime.Now;
            if (HaveCompleteReservation())
            {
                return ConfirmationReadOut();
            }

            var groundedProperties = string.Empty;
            if (PartySize > 0)
            {
                groundedProperties += $" for {PartySize} guests,";
                if (!string.IsNullOrWhiteSpace(Location))
                {
                    groundedProperties += $" in our {Location} store,";
                    if (!string.IsNullOrWhiteSpace(Date) && !string.IsNullOrWhiteSpace(Time))
                    {
                        groundedProperties += " for " + new TimexProperty(Date + "T" + Time).ToNaturalLanguage(today) + ".";
                    }
                    else if (!string.IsNullOrWhiteSpace(Date))
                    {
                        groundedProperties += " for " + new TimexProperty(Date).ToNaturalLanguage(today);
                    }
                    else if (!string.IsNullOrWhiteSpace(TimeLGString))
                    {
                        groundedProperties += $" for {TimeLGString}";
                    }

                    if (string.IsNullOrWhiteSpace(groundedProperties))
                    {
                        return groundedProperties;
                    }

                    return $"Ok. I have a table {groundedProperties}";
                }
            }

            return string.Empty;
        }

        // Helper to generate confirmation read out string.
        public string ConfirmationReadOut()
        {
            var today = DateTime.Now;
<<<<<<< HEAD
            return PartySize + " at our " + Location + " store for " + new TimexProperty(Date + "T" + Time).ToNaturalLanguage(today) + ".";
=======
            return $"{PartySize} at our {Location} store for {new TimexProperty(Date + "T" + Time).ToNaturalLanguage(today)}.";
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
        }

        // Helper to generate help read out string.
        public string HelpReadOut()
        {
            if (string.IsNullOrWhiteSpace(Location))
            {
                return "We have cafe locations in Seattle, Bellevue, Redmond and Renton.";
            }
            else if (string.IsNullOrWhiteSpace(Date))
            {
<<<<<<< HEAD
                return "I can help you reserve a table up to 4 weeks from today..You can say \"tomorrow\", \"next sunday at 3pm\"...";
=======
                return "I can help you reserve a table up to 4 weeks from today... You can say \"tomorrow\", \"next Sunday at 3pm\"...";
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            }
            else if (string.IsNullOrWhiteSpace(Time))
            {
                return "All our cafe locations are open 6AM - 6PM.";
            }
            else if (PartySize == 0)
            {
<<<<<<< HEAD
                return "I can help you book a table for up to 10 guests..";
=======
                return "I can help you book a table for up to 10 guests...";
>>>>>>> 9a1346f23e7379b539e9319c6886e3013dc05145
            }

            return string.Empty;
        }
    }
}
