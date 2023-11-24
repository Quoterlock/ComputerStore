namespace ComputerStore.Utilities
{
    public static class OrderStatuses
    {
        public const string PENDING = "Pending";
        public const string APPRUVED = "Approved";
        public const string IN_PROGRESS = "In_progress";
        public const string SHIPPED = "Shipped";
        public const string CANCELLED = "Cancelled";
        public const string REFUNDED = "Refunded";
        public const string NONE = "None";

        public static OrderStatus StringToEnum(string status)
        {
            if (status.Equals(OrderStatuses.PENDING)) return OrderStatus.Pending;
            if (status.Equals(OrderStatuses.SHIPPED)) return OrderStatus.Shipped;
            if (status.Equals(OrderStatuses.REFUNDED)) return OrderStatus.Refunded;
            if (status.Equals(OrderStatuses.APPRUVED)) return OrderStatus.Approved;
            if (status.Equals(OrderStatuses.CANCELLED)) return OrderStatus.Cancelled;
            if (status.Equals(OrderStatuses.IN_PROGRESS)) return OrderStatus.In_progress;
            else return OrderStatus.Unknown;
        }
    }
}
