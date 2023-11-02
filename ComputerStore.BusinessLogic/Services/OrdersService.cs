using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using ComputerStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class OrdersService : IOrdersService
    {
        private IUnitOfWork _unitOfWork;
        public OrdersService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Delete(string orderId)
        {
            if(!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    await _unitOfWork.Orders.DeleteAsync(orderId);
                    await _unitOfWork.Commit();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<OrderModel>> GetAll()
        {
            var entities = await _unitOfWork.Orders.GetAsync();
            return ConvertEntitiesToModels(entities);
        }

        public async Task<OrderModel> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var entity = await _unitOfWork.Orders.GetAsync(id);
                    if (entity != null)
                    {
                        return Convertor.ConvertEntityToModel(entity);
                    }
                    else throw new Exception("Order doesn't exist id: " + id);
                } 
                catch (Exception ex) 
                { 
                    throw new Exception(ex.Message); 
                }
            }
            else throw new ArgumentNullException("order id");
        }

        public async Task Add(OrderModel order)
        {
            if (order != null)
            {
                await _unitOfWork.Orders.AddAsync(Convertor.ConvertModelToEntity(order));
                await _unitOfWork.Commit();
            }
            else throw new ArgumentNullException("order model");
        }

        public async Task SetStatus(string id, OrderStatus status)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var order = await _unitOfWork.Orders.GetAsync(id);
                order.Status = status.ToString();
                await _unitOfWork.Commit();
            }
        }

        public async Task Update(OrderModel order)
        {
            if (order != null)
            {
                try
                {
                    await _unitOfWork.Orders.UpdateAsync(Convertor.ConvertModelToEntity(order));
                    await _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("order model");
        }

        private List<OrderModel> ConvertEntitiesToModels(List<Order> entities)
        {
            var models = new List<OrderModel>();
            foreach (var entity in entities)
            {
                models.Add(Convertor.ConvertEntityToModel(entity));
            }
            return models;
        }
    }
}
