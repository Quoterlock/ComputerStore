using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Adapters
{
    public class OrderAdapter : IEntityToModelAdapter<Order, OrderModel>
    {
        public Order ToEntity(OrderModel model)
        {
            var entity = new Order()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                LastUpdateTime = model.LastUpdateTime,
                PostOfficeAddress = model.PostOfficeAddress,
                TotalCost = model.TotalCost,
                CreationDate = model.CreationDate,
                Status = model.Status.ToString(),
                ItemsID = new List<string>(),
                CustomerComment = model.CustomerComment
            };
            foreach (var item in model.Items)
                for (int i = 0; i < item.Value; i++)
                    if (item.Key.Id != null)
                        entity.ItemsID.Add(item.Key.Id);
            return entity;
        }

        public OrderModel ToModel(Order entity)
        {
            var model = new OrderModel()
            {
                Id = entity.Id,
                CreationDate = entity.CreationDate,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                LastUpdateTime = entity.LastUpdateTime,
                PostOfficeAddress = entity.PostOfficeAddress,
                TotalCost = entity.TotalCost,
                Items = new Dictionary<ItemModel, int>(),
                Status = OrderStatuses.StringToEnum(entity.Status),
                CustomerComment = entity.CustomerComment
            };

            foreach (var ItemId in entity.ItemsID)
            {
                var key = model.Items.Keys.Where(k => k.Id == ItemId).FirstOrDefault();
                if (key != null)
                {
                    model.Items[key]++;
                }
                else
                    model.Items.Add(new ItemModel { Id = ItemId }, 1);
            }
            return model;
        }
    }
}
