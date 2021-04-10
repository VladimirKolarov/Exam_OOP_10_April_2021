using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        // Can only live in FreshwaterAquarium!

        private const int defaultSize = 3;
        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = defaultSize;
        }

        public override void Eat()
        {
            Size += 3;
        }
    }
}
