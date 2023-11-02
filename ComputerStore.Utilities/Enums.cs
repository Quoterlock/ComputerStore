using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.Utilities
{
    public enum SortMode
    {
        CostUp, 
        CostDown, 
        ItemId, 
        Name
    }

    public enum OrderStatus
    { 
        Pending, 
        Approved, 
        In_progress, 
        Shipped, 
        Cancelled,
        Refunded,
        Unknown
    }

}
