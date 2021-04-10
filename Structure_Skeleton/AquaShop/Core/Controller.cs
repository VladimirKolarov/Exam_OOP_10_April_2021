using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    class Controller : IController
    {
        private DecorationRepository decorations;
        private IDictionary<string, IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new Dictionary<string, IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType == "FreshwaterAquarium")
            {
                aquariums.Add(aquariumName, new FreshwaterAquarium(aquariumName));
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquariums.Add(aquariumName, new SaltwaterAquarium(aquariumName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType == "Ornament")
            {
                decorations.Add(new Ornament());
            }
            else if (decorationType == "Plant")
            {
                decorations.Add(new Plant());
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = aquariums[aquariumName];

            IFish fish = null;

            if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }

            if (fish == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            if ((fishType == "SaltwaterFish" && aquarium.GetType().Name == "SaltwaterAquarium")
                || (fishType == "FreshwaterFish" && aquarium.GetType().Name == "FreshwaterAquarium"))
            {
                aquarium.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else
            {
                return OutputMessages.UnsuitableWater;
            }
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums[aquariumName];
            decimal sum = 0;
            sum += aquarium.Fish.Select(f => f.Price).Sum();
            sum += aquarium.Decorations.Select(d => d.Price).Sum();

            return string.Format(OutputMessages.AquariumValue, aquariumName, $"{sum:f2}");
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums[aquariumName];

            aquarium.Feed();

            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorations.FindByType(decorationType);
            
            if (decoration == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            aquariums[aquariumName].AddDecoration(decoration);
            decorations.Remove(decoration);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            string result = "";

            foreach (var aquaruim in aquariums)
            {
                result += aquaruim.Value.GetInfo() +"\r\n";
            }

            return result.Trim();
        }
    }
}
