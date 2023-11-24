using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Adapters
{
    public interface IEntityToModelAdapter<E,M>
    {
        E ToEntity(M model);
        M ToModel(E entity);
    }
}
