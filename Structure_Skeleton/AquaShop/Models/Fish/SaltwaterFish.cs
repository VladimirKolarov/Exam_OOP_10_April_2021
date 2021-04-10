using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        // Can only live in SaltwaterAquarium!

        private const int defaultSize = 5;
        public SaltwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = defaultSize;
        }

        public override void Eat()
        {
            Size += 2;
        }
    }
}
