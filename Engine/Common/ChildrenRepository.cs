using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGameEngine.Common
{
    public class ChildrenRepository : ListRepository<GameObject>
    {
        public ChildrenRepository(GameObject parent)
            : base(new List<GameObject>(), parent)
        {
           
        }

        public override void Add(GameObject entity)
        {
            entity.Parent = this.Owner;
            base.Add(entity);
        }
    }
}
