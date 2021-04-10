namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    public class AquariumsTests
    {
        private Aquarium aquarium;
        private Aquarium setupAquarium;
        private Fish setupFish;


        [SetUp]
        public void Setup()
        {
            setupAquarium = new Aquarium("SetUpAquarium", 5);
            setupFish = new Fish("Nemo");
        }

        [Test]
        public void NameProperty_Throws_When_NameIsNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => aquarium = new Aquarium(null, 10));
            Assert.Throws<ArgumentNullException>(() => aquarium = new Aquarium("", 4));
        }

        [Test]
        public void CapacityProperty_Throws_When_CapacityIsLessThanZero()
        { 
            Assert.Throws<ArgumentException>(() => aquarium = new Aquarium("WaterWorld", -10));
        }

        [Test]
        public void Add_Throws_When_AquariumIsFull()
        {
            for (int i = 0; i < setupAquarium.Capacity; i++)
            {
                setupAquarium.Add(new Fish($"Fish{i}"));
            }

            Assert.Throws<InvalidOperationException>(() => setupAquarium.Add(new Fish("LastFish")));
        }

        [Test]
        public void CoutnIsZeroByDefault_And_CountIncrements_When_FishIsAdded()
        {
            Assert.That(setupAquarium.Count, Is.EqualTo(0));

            setupAquarium.Add(setupFish);
            Assert.That(setupAquarium.Count, Is.EqualTo(1));
        }


        [Test]
        public void RemoveFish_Throws_When_FishWithGivenNameIsNull()
        {
            string invalidFishName = "Stamat";
            for (int i = 0; i < setupAquarium.Capacity; i++)
            {
                setupAquarium.Add(new Fish($"Fish{i}"));
            }

            Fish fish = new Fish(invalidFishName);

            Assert.Throws<InvalidOperationException>(()=> setupAquarium.RemoveFish(invalidFishName));
        }

        [Test]
        public void RemoveFish_RemovesFishFromCollection()
        {
            int num = 4;
            int expectedFreeSpace = setupAquarium.Capacity - num;

            for (int i = 0; i < setupAquarium.Capacity; i++)
            {
                setupAquarium.Add(new Fish($"Fish{i}"));
            }

            for (int i = 0; i < num; i++)
            {
                setupAquarium.RemoveFish($"Fish{i}");
            }

            Assert.That(setupAquarium.Count, Is.EqualTo(expectedFreeSpace));
        }

        [Test]
        public void SellFish_Throws_When_FishWithGivenNameIsNull()
        {
            string invalidFishName = "Stavri";
            for (int i = 0; i < setupAquarium.Capacity; i++)
            {
                setupAquarium.Add(new Fish($"Fish{i}"));
            }

            Fish fish = new Fish(invalidFishName);

            Assert.Throws<InvalidOperationException>(() => setupAquarium.RemoveFish(invalidFishName));
        }

        [Test]
        public void SellFish_ReturnsExpectedFish()
        {
            string fishName = "Laluger";
            setupAquarium.Add(new Fish(fishName));

            var actual = setupAquarium.SellFish(fishName);

            Assert.That(actual.Name, Is.EqualTo(fishName));
            Assert.That(actual.Available, Is.EqualTo(false));
        }

        [Test]
        public void Report_ReturnsExpected()
        {
            setupAquarium.Add(setupFish);
            setupAquarium.Add(new Fish("Dori"));
            setupAquarium.Add(new Fish("Shark"));

            string expected = $"Fish available at {setupAquarium.Name}: Nemo, Dori, Shark";
            var actual = setupAquarium.Report();

            Assert.That(actual, Is.EqualTo(expected));
        }



    }
}
