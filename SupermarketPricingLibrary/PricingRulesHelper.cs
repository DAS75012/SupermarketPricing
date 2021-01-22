using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketPricingLibrary
{
    public class PricingRulesHelper
    {
        /// <summary>
        /// How much will the customer pay
        /// </summary>
        /// <param name="_unit_price">unit price</param>
        /// <param name="_quantity_or_weight">quantity or weight to price</param>
        /// <param name="_pricing_rule">how to price</param>
        /// <returns>price to pay</returns>
        public static float price(float _unit_price, float _quantity_or_weight, Func<float, float, float> _pricing_rule = null)
        {
            if (_unit_price < 0)
                throw new Exception("The unit price can not be a negative number!");

            if (_quantity_or_weight < 0)
                throw new Exception("The quantity or weight can not be a negative number!");

            if (_unit_price == 0 || _quantity_or_weight == 0)
                return 0.0F;

            if (_pricing_rule == null)
                return _unit_price * _quantity_or_weight;

            return _pricing_rule(_unit_price, _quantity_or_weight);
        }

        /// <summary>
        /// three for a dollar
        /// the unit price is not defined, so the unit costs $0.40, par example.
        /// </summary>
        /// <param name="_unit_price">unit price</param>
        /// <param name="_quantity">quantity to price</param>
        /// <returns>price to pay</returns>
        public static float three_for_a_dollar_rule(float _unit_price, float _quantity) => ((int)_quantity / 3) + (((int)_quantity % 3) * _unit_price);

        /// <summary>
        /// $1.99/pound (so what does 4 ounces cost?)
        /// an once = 14.828125858124 pound.
        /// </summary>
        /// <param name="_unit_price">unit price</param>
        /// <param name="_weight">weight (in pound) to price</param>
        /// <returns>price to pay</returns>
        public static float weight_in_pound_rule(float _unit_price, float _weight_in_pound) => _weight_in_pound * _unit_price;

        /// <summary>
        /// $1.99/pound (so what does 4 ounces cost?)
        /// 1 pound (lb) is equal to 16 Ounces (oz).
        /// </summary>
        /// <param name="_unit_price">unit price</param>
        /// <param name="_weight">weight (in ounce) to price</param>
        /// <returns>price to pay</returns>
        public static float weight_in_ounce_rule(float _unit_price, float _weight_in_ounce) => weight_in_pound_rule(_unit_price, _weight_in_ounce / 16);

        /// <summary>
        /// buy two, get one free
        /// the unit price is not defined, so the unit costs $1.0, par example.
        /// </summary>
        /// <param name="_unit_price">unit price</param>
        /// <param name="_quantity">quantity to price</param>
        /// <returns>price to pay</returns>
        public static float buy_two_get_one_free(float _unit_price, float _quantity) 
        {
            var result = (int)_quantity % 3;
            if (result == 2)
            {
                Console.WriteLine("Please ask to the customer to get one more.");
                _quantity += 1;
                result = 0;
            }
                
            return (((int)(_quantity / 3) * 2) + result) * _unit_price;
        }
    }
}
