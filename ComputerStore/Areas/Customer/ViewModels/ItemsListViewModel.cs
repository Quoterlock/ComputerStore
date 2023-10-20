﻿using ComputerStore.BusinessLogic.Domains;
using ComputerStore.Utilities;
using NuGet.Protocol.Core.Types;

namespace ComputerStore.Areas.Customer.ViewModels
{
    public class ItemsListViewModel
    {
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
        public int Count { get; set; }
        public string Tag { get; set; } = "none";
        public SortMode SortBy { get; set; } = SortMode.ItemId;
    }
}
