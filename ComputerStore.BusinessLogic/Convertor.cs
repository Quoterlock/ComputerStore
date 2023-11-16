﻿using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Utilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic
{
    internal static class Convertor
    {
        internal static Image ConvertModelToEntity(ImageModel? model)
        {
            return new Image
            {
                Id = model.Id,
                Alt = model.Alt,
                Bytes = model.Bytes
            };
        }

        internal static ImageModel ConvertEntityToModel(Image entity)
        {
            return new ImageModel()
            {
                Alt = entity.Alt,
                Bytes = entity.Bytes,
                Id = entity.Id
            };
        }

        public static CategoryModel ConvertEntityToModel(Category entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Thumbnail = ConvertEntityToModel(entity.Image??new Image())
            };
        }

        public static Category ConvertModelToEntity(CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Image = new Image() { 
                    Bytes = model.Thumbnail.Bytes, 
                    Alt =  model.Thumbnail.Alt },
            };
        }

        internal static ItemModel EntityToModel(Item entity)
        {
            return new ItemModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = ConvertEntityToModel(entity.Image),
                Price = entity.Price,
                Category = new CategoryModel
                {
                    Id = entity.Category.Id,
                    Name = entity.Category.Name
                },
            };
        }

        internal static Item ConvertModelToEntity(ItemModel model)
        {
            return new Item()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Image = ConvertModelToEntity(model.Image),
                Category = new Category() { Id = model.Category.Id }
            };
        }

        internal static OrderModel ConvertEntityToModel(Order entity)
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
                Status = OrderStatusStringToEnum(entity.Status),
                CustomerComment = entity.CustomerComment
            };

            foreach(var ItemId in entity.ItemsID)
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

        internal static Order ConvertModelToEntity(OrderModel model)
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
            foreach(var item in model.Items)
                for (int i = 0; i < item.Value; i++)
                    if (item.Key.Id != null)
                            entity.ItemsID.Add(item.Key.Id);
            return entity;
        }

        internal static OrderStatus OrderStatusStringToEnum(string status)
        {
            if (status.Equals(OrderStatuses.PENDING)) return OrderStatus.Pending;
            if (status.Equals(OrderStatuses.SHIPPED)) return OrderStatus.Shipped;
            if (status.Equals(OrderStatuses.REFUNDED)) return OrderStatus.Refunded;
            if (status.Equals(OrderStatuses.APPRUVED)) return OrderStatus.Approved;
            if(status.Equals(OrderStatuses.CANCELLED)) return OrderStatus.Cancelled;
            if (status.Equals(OrderStatuses.IN_PROGRESS)) return OrderStatus.In_progress;
            else return OrderStatus.Unknown;
        }
    }
}
