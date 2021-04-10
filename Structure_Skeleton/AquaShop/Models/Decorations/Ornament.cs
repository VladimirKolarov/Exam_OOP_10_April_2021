using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int defaultComfort = 1;
        private const decimal defaultPrice = 5m;
        public Ornament() : base(defaultComfort, defaultPrice)
        {

        }
    }
}
