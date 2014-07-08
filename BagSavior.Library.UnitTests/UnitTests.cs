using System;
using System.Collections.Generic;
using BagSavior.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BagSavior.Library.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        /// This tests insures the basic functionality is working for an even number of bags.
        /// </summary>
        [TestMethod]
        public void BasicTestEven()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            var numberOfBags = bagCalculator.GetNumberOfBags(new List<string>
            {
                "PRODUCE",
                "EGGS",
                "PRODUCE",
                "EGGS"
            }, bagStrength);
            Assert.AreEqual(numberOfBags, 2);
        }

        /// <summary>
        /// This tests insures the basic functionality is working for an odd number of bags.
        /// </summary>
        [TestMethod]
        public void BasicTestOdd()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            var numberOfBags = bagCalculator.GetNumberOfBags(new List<string>
            {
                "PRODUCE",
                "EGGS",
                "PRODUCE",
                "EGGS",
                "FROZEN"
            }, bagStrength);
            Assert.AreEqual(numberOfBags, 3);
        }

        /// <summary>
        /// Tests an empty list being passed in.
        /// </summary>
        [TestMethod]
        public void EmtpyTest()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            var numberOfBags = bagCalculator.GetNumberOfBags(new List<string>(), bagStrength);
            Assert.AreEqual(numberOfBags, 0);
        }

        /// <summary>
        /// Tests a null list being passed in.
        /// </summary>
        [TestMethod]
        public void NullTest()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            var numberOfBags = bagCalculator.GetNumberOfBags(null, bagStrength);
            Assert.AreEqual(numberOfBags, 0);
        }

        /// <summary>
        /// Tests an invalid name being placed into the item type list.
        /// </summary>
        [TestMethod]
        public void InvalidNameTest()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            try
            {
                var numberOfBags = bagCalculator.GetNumberOfBags(new List<string>
                {
                    "PRODUCE1",
                    "EGGS",
                    "PRODUCE",
                    "EGGS"
                }, bagStrength);
                // Should never reach this...
                Assert.AreEqual(numberOfBags, 0);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
                return;
            }

            // Should never reach this.
            Assert.IsTrue(false);
        }

        /// <summary>
        /// Tests an invalid item type name length in the item type list.
        /// </summary>
        [TestMethod]
        public void InvalidNameLengthTest()
        {
            const int bagStrength = 2;
            var bagCalculator = new BagCalculator(new AppSettingsManager());
            try
            {
                var numberOfBags = bagCalculator.GetNumberOfBags(new List<string>
                {
                    "PRODUCEPRODUCEPRODUCEPRODUCEPRODUCEPRODUCEPRODUCEPRODUCE",
                    "EGGS",
                    "PRODUCE",
                    "EGGS"
                }, bagStrength);
                // Should never reach this...
                Assert.AreEqual(numberOfBags, 0);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
                return;
            }

            // Should never reach this.
            Assert.IsTrue(false);
        }
    }
}
