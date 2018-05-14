// <copyright file="StringWorkingDaySource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.StringSource
{
    using System;

    /// <inheritdoc cref="IWorkingDaySource"/>
    /// <summary>
    /// An implementation of IWorkingDaySource which uses of a string to determine whether a given DateTime is on a Working Day or a Non-Working Day.
    /// </summary>
    public class StringWorkingDaySource<T> : IWorkingDaySource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringWorkingDaySource{T}"/> class.
        /// </summary>
        /// <param name="content">The string content of the state.</param>
        /// <param name="parseAction">The action to get the state from the content.</param>
        /// <param name="checkAction">The action to get whether a DateTime is on a Working Day based on the current state.</param>
        public StringWorkingDaySource(string content, Func<string, T> parseAction, Func<DateTime, T, bool> checkAction)
            : this(parseAction, checkAction) => State = parseAction(content);

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWorkingDaySource{T}"/> class.
        /// </summary>
        /// <param name="parseAction">The action to get the state from the content content.</param>
        /// <param name="checkAction">The action to get whether a DateTime is on a Working Day based on the current state.</param>
        protected StringWorkingDaySource(Func<string, T> parseAction, Func<DateTime, T, bool> checkAction)
        {
            CheckAction = checkAction ?? throw new ArgumentNullException(nameof(parseAction));
            ParseAction = parseAction ?? throw new ArgumentException(nameof(checkAction));
        }

        /// <summary>
        /// Gets the action to check whether a DateTime is on a Working Day based on the internal state of the String Working Day Source.
        /// </summary>
        protected Func<DateTime, T, bool> CheckAction { get; }

        /// <summary>
        /// Gets the action to parse a string into the internal state of the String Working Day Source.
        /// </summary>
        protected Func<string, T> ParseAction { get; }

        /// <summary>
        /// Gets or sets the current internal state of the String Working Day Source.
        /// </summary>
        protected T State { get; set; }

        /// <inheritdoc />
        public bool IsWorkingDay(DateTime date)
        {
            lock (State)
            {
                return CheckAction(date, State);
            }
        }
    }
}
