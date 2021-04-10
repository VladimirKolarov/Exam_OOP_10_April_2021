using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
    class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> model;

        public DecorationRepository()
        {
            model = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => this.model;

        public void Add(IDecoration model)
        {
            this.model.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            return this.model.FirstOrDefault(m => m.GetType().Name == type);
        }

        public bool Remove(IDecoration model)
        {
            return this.model.Remove(model);
        }
    }
}
