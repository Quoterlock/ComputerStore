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
