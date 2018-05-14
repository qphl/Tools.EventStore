// <copyright file="WorkingDayServiceAddWorkingDayTests.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Tests for adding working days to a Working Day via the Working Day Service.
    /// </summary>
    [TestFixture]
    public class WorkingDayServiceAddWorkingDayTests
    {
        private static readonly WorkingDayService WorkingDayService = new WorkingDayService(new List<IWorkingDaySource> { new MondayWorkingDayTestSource(), new TuesdayWorkingDayTestSource() });

        /// <summary>
        /// Test to ensure that NextWorkingDay returns the correct DateTime (next working day with the time preserved).
        /// </summary>
        [Test]
        public static void NextWorkingDayReturnsCorrectDay()
        {
            Assert.AreEqual(WorkingDayService.NextWorkingDay(new DateTime(2018, 5, 14, 9, 30, 0)), new DateTime(2018, 5, 15, 9, 30, 0));
            Assert.AreEqual(WorkingDayService.NextWorkingDay(new DateTime(2018, 5, 15, 10, 30, 55)), new DateTime(2018, 5, 21, 10, 30, 55));
        }

        /// <summary>
        /// Test to ensure that AddWorkingDays returns the correct DateTime (next working day with the time preserved) when passed 0.
        /// </summary>
        [Test]
        public static void Add0WorkingDaysReturnsCorrectDay()
        {
            Assert.AreEqual(WorkingDayService.AddWorkingDays(0, new DateTime(2018, 5, 14, 9, 30, 0)), new DateTime(2018, 5, 14, 9, 30, 0));
            Assert.AreEqual(WorkingDayService.AddWorkingDays(0, new DateTime(2018, 5, 15, 10, 30, 55)), new DateTime(2018, 5, 15, 10, 30, 55));
        }

        /// <summary>
        /// Test to ensure that AddWorkingDays returns the correct DateTime (next working day with the time preserved) when passed 1.
        /// </summary>
        [Test]
        public static void Add1WorkingDaysReturnsCorrectDay()
        {
            Assert.AreEqual(WorkingDayService.AddWorkingDays(1, new DateTime(2018, 5, 14, 9, 30, 0)), new DateTime(2018, 5, 15, 9, 30, 0));
            Assert.AreEqual(WorkingDayService.AddWorkingDays(1, new DateTime(2018, 5, 15, 10, 30, 55)), new DateTime(2018, 5, 21, 10, 30, 55));
        }

        /// <summary>
        /// Test to ensure that AddWorkingDays returns the correct DateTime (next working day with the time preserved) when passed 2.
        /// </summary>
        [Test]
        public static void Add2WorkingDaysReturnsCorrectDay()
        {
            Assert.AreEqual(WorkingDayService.AddWorkingDays(2, new DateTime(2018, 5, 14, 9, 30, 0)), new DateTime(2018, 5, 21, 9, 30, 0));
            Assert.AreEqual(WorkingDayService.AddWorkingDays(2, new DateTime(2018, 5, 15, 10, 30, 55)), new DateTime(2018, 5, 22, 10, 30, 55));
        }
    }
}
