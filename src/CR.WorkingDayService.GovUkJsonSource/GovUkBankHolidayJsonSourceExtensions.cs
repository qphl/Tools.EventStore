// <copyright file="GovUkBankHolidayJsonSourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.GovUkJsonSource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using HttpSource;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Extension Methods for the WorkingDayService Builder to use the UK Government Bank Holiday Endpoint.
    /// </summary>
    public static class GovUkBankHolidayJsonSourceExtensions
    {
        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a HTTP Working Day Source based on the UK Government's JSON API Endpoint for Bank Holidays.
        /// </summary>
        /// <param name="builder">The WorkingDayServiceBuilder to configure to use the UK Government's JSON API for Working Day detection.</param>
        /// <param name="refreshTime">The time interval between attepted refreshes of the Bank Holiday list.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseGovUkBankHolidayJsonSource(this WorkingDayServiceBuilder builder, TimeSpan refreshTime)
            => builder.UseSource(GovUkBankHolidayJsonSource(refreshTime));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a HTTP Working Day Source based on the UK Government's JSON API Endpoint for Bank Holidays, as well as any previously registered Sources.
        /// </summary>
        /// <param name="builder">The WorkingDayServiceBuilder to configure to use the UK Government's JSON API for Working Day detection.</param>
        /// <param name="refreshTime">The time interval between attepted refreshes of the Bank Holiday list.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddGovUkBankHolidayJsonSource(this WorkingDayServiceBuilder builder, TimeSpan refreshTime)
            => builder.AddSource(GovUkBankHolidayJsonSource(refreshTime));

        private static IWorkingDaySource GovUkBankHolidayJsonSource(TimeSpan refreshTime) => new HttpWorkingDaySource<List<DateTime>>(
            new HttpRequestMessage(HttpMethod.Get, "https://www.gov.uk/bank-holidays.json"),
            json =>
            {
                var jobject = JObject.Parse(json);
                return jobject["england-and-wales"]["events"]?.Select(e => DateTime.Parse(e["date"].ToObject<string>())).ToList();
            }, (dateTime, list) => !list.Contains(dateTime.Date),
            refreshTime);
    }
}
