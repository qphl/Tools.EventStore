// <copyright file="HttpWorkingDaySource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.HttpSource
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using StringSource;

    /// <inheritdoc cref="StringWorkingDaySource{T}" />
    /// <inheritdoc cref="IDisposable"/>
    /// <summary>
    /// An implementation of IWorkingDaySource which uses the returned content of a HTTP Endpoint to determine whether a given DateTime is on a Working Day or a Non-Working Day.
    /// </summary>
    public sealed class HttpWorkingDaySource<T> : StringWorkingDaySource<T>, IDisposable
    {
        private readonly HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWorkingDaySource{T}"/> class which builds a state based on the content returned by the provided URL &amp; parsed via the provided ParseAction, then determines whether any given DateTime is on a Working Day or Non-Working Day based on the internal state.
        /// </summary>
        /// <param name="request">The HTTP Request to make for the state's content.</param>
        /// <param name="parseAction">The Action to pass the response from the HTTP endpoint to, in order to build the HTTP Working Day Source's internal state.</param>
        /// <param name="checkAction">The Action to check the internal state to determine whether a given DateTime is a Working Day or Non-Working Day.</param>
        /// <param name="refreshTimer">The interval at which to update the state by making the request again (on failure to update, the state will not change).</param>
        public HttpWorkingDaySource(HttpRequestMessage request, Func<string, T> parseAction, Func<DateTime, T, bool> checkAction, TimeSpan refreshTimer)
            : base(parseAction, checkAction)
        {
            _timer = new Timer(_ => UpdateState(), null, (int)refreshTimer.TotalMilliseconds, Timeout.Infinite);
            _httpClient = new HttpClient();
            _request = request;
            UpdateState();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _httpClient.Dispose();
            _timer.Dispose();
        }

        private void UpdateState()
        {
            try
            {
                State = ParseAction(_httpClient.SendAsync(_request).Result.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                if (State == null)
                {
                    throw;
                }
            }
        }
    }
}
