using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketPricingLibrary;
using System;

namespace SupermarketPricingUnitTest
{
    [TestClass]
    public class PricingRulesHelperUnitTest
    {
        [TestMethod]
        public void TestPriceMethodWithNegativePrice()
        {
            const string expected_message = "The unit price can not be a negative number!";
            var ex = Assert.ThrowsException<Exception>(() => PricingRulesHelper.price(-3.0F, 0.0F));
            Assert.AreEqual(expected_message, ex.Message);
        }

        [TestMethod]
        public void TestPriceMethodWithNegativeQuantity()
        {
            const string expected_message = "The quantity or weight can not be a negative number!";
            var ex = Assert.ThrowsException<Exception>(() => PricingRulesHelper.price(0.0F, -5.0F));
            Assert.AreEqual(expected_message, ex.Message);
        }

        [TestMethod]
        public void TestPriceMethodWithZeroPrice()
        {
            float das = PricingRulesHelper.price(0.0F, 0.0F);
            Assert.AreEqual(0.0F, das);
        }

        [TestMethod]
        public void TestPriceMethodWithZeroQuantity()
        {
            float das = PricingRulesHelper.price(0.0F, 0.0F);
            Assert.AreEqual(0.0F, das);
        }

        [TestMethod]
        public void TestPriceMethodWithoutPricingRule()
        {
            float das = PricingRulesHelper.price(0.65F, 2.0F);
            Assert.AreEqual(1.3F, das);
        }

        [TestMethod]
        public void TestPriceMethodWithThreeForADollarRule()
        {
            float das = PricingRulesHelper.price(0.40F, 8.0F, PricingRulesHelper.three_for_a_dollar_rule);
            Assert.AreEqual(2.8F, das);
        }

        [TestMethod]
        public void TestPriceMethodWithWeightInPoundRule()
        {
            //$1.99 / pound
            float das = PricingRulesHelper.price(1.99F, 10.0F, PricingRulesHelper.weight_in_pound_rule);
            Assert.AreEqual(19.9F, das);
        }

        [TestMethod]
        public void TestPriceMethodWithWeightInOunceRule()
        {
            /// $1.99/pound (so what does 4 ounces cost?)
            /// 1 pound (lb) is equal to 16 Ounces (oz).
            float das = PricingRulesHelper.price(1.99F, 4.0F, PricingRulesHelper.weight_in_ounce_rule);
            Assert.AreEqual(0.4975F, das);
        }

        [TestMethod]
        [DataRow(1.0F, 7.0F, 5.0F)]
        [DataRow(1.0F, 8.0F, 6.0F)]
        [DataRow(1.0F, 9.0F, 6.0F)]
        public void TestPriceMethodWithBuyTwoGetOneFree(float unit_price, float quantity, float price_to_pay)
        {
            /// buy two, get one free
            /// the unit price is not defined, so the unit costs $1.00, par example.
            float das = PricingRulesHelper.price(unit_price, quantity, PricingRulesHelper.buy_two_get_one_free);
            Assert.AreEqual(price_to_pay, das);
        }
    }
}
