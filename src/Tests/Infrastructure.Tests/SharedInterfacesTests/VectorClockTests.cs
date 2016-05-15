﻿namespace BudgetFirst.Infrastructure.Tests.SharedInterfacesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SharedInterfaces.Messaging;

    /// <summary>
    /// Contains tests for vector clocks
    /// </summary>
    [TestFixture]
    public class VectorClockTests
    {
        /// <summary>
        /// New vector clocks start at 1
        /// </summary>
        [Test]
        public void StartAtOne()
        {
            VectorClock clock = new VectorClock();
            Assert.That(!clock.Vector.ContainsKey("key1"));

            clock = clock.Increment("key1");
            Assert.That(clock.Vector["key1"] == 1);
        }

        /// <summary>
        /// Incrementing vector clocks increments by 1
        /// </summary>
        [Test]
        public void IncrementClock()
        {
            Dictionary<string, int> vector = new Dictionary<string, int>();
            vector["key1"] = 2;
            vector["key2"] = 3;
            VectorClock clock = new VectorClock(vector);

            clock = clock.Increment("key1");
            clock = clock.Increment("key2");

            Assert.That(clock.Vector["key1"] == 3 && clock.Vector["key2"] == 4);
        }

        /// <summary>
        /// Tests merging of vector clocks
        /// </summary>
        [Test]
        public void MergeClock()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = new VectorClock();
            clock2 = clock2.Increment("key2");
            clock2 = clock2.Increment("key2");

            VectorClock clock3 = new VectorClock();
            clock3 = clock3.Increment("key3");

            VectorClock mergedClock = new VectorClock();
            mergedClock = mergedClock.Merge(clock1);
            mergedClock = mergedClock.Increment("key1");
            mergedClock = mergedClock.Merge(clock2);
            mergedClock = mergedClock.Merge(clock3);

            Assert.That(mergedClock.Vector["key1"] == 4 && mergedClock.Vector["key2"] == 2 && mergedClock.Vector["key3"] == 1);

            // Merge should not affect the original VectorClock
            Assert.That(clock1.Vector["key1"] == 3);
        }

        /// <summary>
        /// Tests copying of vector clocks
        /// </summary>
        [Test]
        public void CopyClock()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = clock1.Copy();
            clock2 = clock2.Increment("key1");

            Assert.That(clock1 != clock2);
            Assert.That(clock1.Vector != clock2.Vector);
            Assert.That(clock1.Vector["key1"] == 3);
            Assert.That(clock2.Vector["key1"] == 4);
        }

        /// <summary>
        /// Tests comparing of vector clocks
        /// </summary>
        [Test]
        public void CompareVectors()
        {
            VectorClock clock1 = new VectorClock();
            clock1 = clock1.Increment("key1");

            VectorClock clock2 = clock1.Copy();
            clock2 = clock2.Increment("key2");

            VectorClock clock3 = new VectorClock();
            clock3 = clock3.Increment("key1");
            clock3 = clock3.Increment("key2");

            Assert.That(clock1.CompareVectors(clock2) == VectorClock.ComparisonResult.Smaller);
            Assert.That(clock2.CompareVectors(clock1) == VectorClock.ComparisonResult.Greater);
            Assert.That(clock3.CompareVectors(clock2) == VectorClock.ComparisonResult.Equal);
        }

        /// <summary>
        /// Simultaneous operations are detected
        /// </summary>
        [Test]
        public void DetectSimultaneousOperations()
        {
            VectorClock baseClock = new VectorClock();
            baseClock = baseClock.Increment("key1");
            baseClock = baseClock.Increment("key1");

            VectorClock clock1 = baseClock.Copy();
            VectorClock clock2 = baseClock.Copy();
            VectorClock clock3 = baseClock.Copy();
            clock1 = clock1.Increment("key1");
            clock2 = clock2.Increment("key2");
            clock3 = clock3.Increment("key3");

            Assert.That(clock1.CompareVectors(clock2) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(clock2.CompareVectors(clock1) == VectorClock.ComparisonResult.Simultaneous);

            Assert.That(clock1.CompareVectors(clock3) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(clock3.CompareVectors(clock1) == VectorClock.ComparisonResult.Simultaneous);

            Assert.That(clock3.CompareVectors(clock2) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(clock2.CompareVectors(clock3) == VectorClock.ComparisonResult.Simultaneous);

            VectorClock clock4 = clock1.Merge(clock2);
            clock4 = clock4.Merge(clock3);

            Assert.That(clock4.CompareVectors(clock1) == VectorClock.ComparisonResult.Greater);
            Assert.That(clock4.CompareVectors(clock2) == VectorClock.ComparisonResult.Greater);
            Assert.That(clock4.CompareVectors(clock3) == VectorClock.ComparisonResult.Greater);
            Assert.That(clock1.CompareVectors(clock4) == VectorClock.ComparisonResult.Smaller);
            Assert.That(clock2.CompareVectors(clock4) == VectorClock.ComparisonResult.Smaller);
            Assert.That(clock3.CompareVectors(clock4) == VectorClock.ComparisonResult.Smaller);

            // All previous clocks  descend from baseClock which has the vector ["key1" : 2]
            VectorClock otherClock = new VectorClock();
            otherClock = otherClock.Increment("key4");

            // otherClock should be simultaneous with all of them since it doesnt recognize ["key1" : 2]
            Assert.That(otherClock.CompareVectors(baseClock) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock1) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock2) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock3) == VectorClock.ComparisonResult.Simultaneous);
            Assert.That(otherClock.CompareVectors(clock4) == VectorClock.ComparisonResult.Simultaneous);
        }
    }
}
