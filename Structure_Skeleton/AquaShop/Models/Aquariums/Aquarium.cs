using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private List<IDecoration> decorations;
        private Dictionary<string, IFish> fish;

        public Aquarium(string name, int capacity)
        {
            decorations = new List<IDecoration>();
            fish = new Dictionary<string, IFish>();

            Name = name;
            Capacity = capacity;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort => Decorations.Select(d=>d.Comfort).Sum();

        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => this.fish.Values.ToList();

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (Fish.Count>= Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish.Name, fish);
        }

        public void Feed()
        {
            foreach (IFish fish in Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Name} ({this.GetType().Name}):");
            sb.Append("Fish: ");
            if (!Fish.Any())
            {
                sb.AppendLine("none");
            }
            else
            {
                sb.AppendLine(string.Join(", ", Fish.Select(f=>f.Name)));
            }
            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");

            return sb.ToString().Trim();
        }

        public bool RemoveFish(IFish fish)
        {
            if (this.fish.ContainsKey(fish.Name))
            {
                this.fish.Remove(fish.Name);
                return true;
            }
            return false;
        }
    }
}
