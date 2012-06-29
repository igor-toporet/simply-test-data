// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Xunit;

namespace GettingStarted
{
    public class MockingExamples
    {
        public class Phone
        {
            public string Manufacturer { get; set; }
            public string ModelName { get; set; }
            public decimal Price { get; set; }
        }

        public interface IRepository<T>
        {
            IList<T> GetAll();
        }

        public interface IPhoneAdviserServise
        {
            IList<Phone> GetSmartPhones();
            IList<Phone> GetSuperPhones();
        }

        public class PhoneAdviserServise : IPhoneAdviserServise
        {
            private readonly IRepository<Phone> _repository;

            public PhoneAdviserServise(IRepository<Phone> repository)
            {
                _repository = repository;
            }

            public IList<Phone> GetSmartPhones()
            {
                return _repository.GetAll().Where(p => 100 <= p.Price && p.Price <= 499).ToList();
            }

            public IList<Phone> GetSuperPhones()
            {
                return _repository.GetAll().Where(p => p.Price > 499).ToList();
            }
        }

        [Fact]
        public void Example_01_Naked_Mocks()
        {
            var galaxy = new Phone {Manufacturer = "Samsung", ModelName = "Galaxy S III", Price = 700};
            var iphone = new Phone {Manufacturer = "Apple", ModelName = "iPhone 4S", Price = 600};
            var nokia = new Phone {Manufacturer = "Nokia", ModelName = "X7", Price = 250};
            var allPhones = new List<Phone> {galaxy, iphone, nokia};
            var repository = A.Fake<IRepository<Phone>>();
            A.CallTo(() => repository.GetAll()).Returns(allPhones);
            var adviserServise = new PhoneAdviserServise(repository);

            var smartPhones = adviserServise.GetSuperPhones();

            Assert.Equal(2, smartPhones.Count);
            Assert.False(smartPhones.Contains(nokia));
        }
    }
}

// ReSharper restore InconsistentNaming
