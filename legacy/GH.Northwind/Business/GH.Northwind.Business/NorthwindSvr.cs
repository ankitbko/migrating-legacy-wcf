/*
 * Copyright © 2012
 * This code is for the codeproject.com article "N-Tier Architecture and Tips" at  
 * http://www.codeproject.com/Articles/434282/A-N-Tier-Architecture-Sample-with-ASP.NET-MVC3-WCF-and-Entity-Framework. 
 * Permission to use, copy or modify this software freely is hereby granted, 
 * provided that this copyright notice appears in all orginal or modified copies 
 * 
 * This code isn't guaranteed to work correctly; it is your own responsibility for 
 * any result from using this code. 
 *                           
 * @description
 * 
 * @author  
 * @version July 18, 2012
 * @see
 * @since
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Services.Client;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using GH.Common.Framework.Persistence;
using GH.Common.ServiceLocator;
using GH.Northwind.Business.Entities;
using GH.Northwind.Business.Interfaces;
using GH.Northwind.Business.Interfaces.DTO;
using GH.Northwind.EntityFramework;
using AutoMapper;
using GH.Common.Framework.Persistence.DbCxt;

namespace GH.Northwind.Business
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class NorthwindSvr : INorthwindSvr
    {
        static NorthwindSvr()
        {
            DataCxt.Cxt = new NorthwindEntities(ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString);
            ServiceLocator<IPersistence<Customer>>.RegisterService<Persistence.DbCxt.CustomerPrst>();
            ServiceLocator<IPersistence<Product>>.RegisterService<Persistence.DbCxt.ProductPrst>();
            ServiceLocator<IPersistence<Order>>.RegisterService<Persistence.DbCxt.OrderPrst>();
            ServiceLocator<IPersistence<Order_Detail>>.RegisterService<Persistence.DbCxt.Order_DetailPrst>();
            ServiceLocator<IPersistence<Category>>.RegisterService<Persistence.DbCxt.CategoryPrst>();
            ServiceLocator<IPersistence<Supplier>>.RegisterService<Persistence.DbCxt.SupplierPrst>();

            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Order, OrderDto>();
            Mapper.CreateMap<Order_Detail, OrderDetailDto>();
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<Supplier, SupplierDto>();

        }

        public List<CustomerDto> GetCustomers()
        {
            return PersistSvr<Customer>.GetAll().ToList().Select(c => Mapper.Map<CustomerDto>(c)).ToList();
        }

        public void InsertCustomer(Customer customer, bool commit)
        {
            PersistSvr<Customer>.Insert(customer, commit);
        }

        public void UpdateCustomer(Customer currentCustomer, bool commit)
        {
            PersistSvr<Customer>.Update(currentCustomer, commit);
        }

        public void DeleteCustomer(String customerId, bool commit)
        {
            IQueryable<Customer> qCustomer = PersistSvr<Customer>.GetAll();
            List<Customer> cus = (from c in qCustomer where c.CustomerID == customerId select c).ToList();
            if (cus.Count <= 0)
                throw new ApplicationException("NorthwindSvr::DeleteCustomer: customer with Id " + customerId +
                                               " doesn't exist.");
            PersistSvr<Customer>.Delete(cus[0], commit);
        }

        public List<OrderDto> GetOrders()
        {
            return PersistSvr<Order>.GetAll().ToList().Select(c => Mapper.Map<OrderDto>(c)).ToList();
        }

        public List<OrderDetailDto> GetOrderDetailForAnOrder(int orderId)
        {
            IQueryable<Order_Detail> qOrder_Detail = PersistSvr<Order_Detail>.GetAll();
            List<Order_Detail> ods = (from od in qOrder_Detail where od.OrderID == orderId select od).ToList();
            return ods.Select(c => Mapper.Map<OrderDetailDto>(c)).ToList();
        }

        public List<OrderDto> GetOrderForACustomer(String customerId)
        {
            IQueryable<Order> qOrder = PersistSvr<Order>.GetAll();
            List<Order> orders = (from o in qOrder where o.CustomerID == customerId select o).ToList();
            return orders.Select(c => Mapper.Map<OrderDto>(c)).ToList();
        }

        public void CreateOrder(Order order, Order_Detail[] details)
        {
            PersistSvr<Order>.Insert(order, true);
            foreach (var od in details)
            {
                od.OrderID = order.OrderID;
                PersistSvr<Order_Detail>.Insert(od, true);
            }
        }

        public void UpdateOrder(Order currentOrder, Order_Detail[] details, bool commit)
        {
            PersistSvr<Order>.Update(currentOrder, commit);
            foreach (var od in details)
            {
                PersistSvr<Order_Detail>.Update(od, commit);
            }
        }

        public void DeleteAnOrderDetailFromAnOrder(int orderId, int productId, bool commit)
        {
            IQueryable<Order_Detail> qOrder_Detail = PersistSvr<Order_Detail>.GetAll();
            List<Order_Detail> ods =
                (from od in qOrder_Detail where od.ProductID == productId && od.OrderID == orderId select od).ToList();
            if (ods.Count <= 0)
                throw new ApplicationException(
                    "NorthwindSvr::DeleteAnOrderDetailFromAnOrder: order detail with order id " + orderId.ToString() +
                    " and product id: " + productId.ToString() +
                    " doesn't exist.");
            PersistSvr<Order_Detail>.Delete(ods[0], commit);
        }

        public void DeleteOrder(int orderId, bool commit)
        {
            IQueryable<Order> qOrder = PersistSvr<Order>.GetAll();
            List<Order> orders = (from o in qOrder where o.OrderID == orderId select o).ToList();
            if (orders.Count <= 0)
                throw new ApplicationException("NorthwindSvr::DeleteOrder: product with Id " + orderId.ToString() +
                                               " doesn't exist.");
            // Delete each order detail
            var orderDetails = GetOrderDetailForAnOrder(orders[0].OrderID);
            foreach (var od in orderDetails)
            {
                DeleteAnOrderDetailFromAnOrder(orders[0].OrderID, od.ProductID, commit);
            }
            PersistSvr<Order>.Delete(orders[0], commit);
        }

        public List<ProductDto> GetProducts()
        {
            return PersistSvr<Product>.GetAll().ToList().Select(c => Mapper.Map<ProductDto>(c)).ToList();
        }

        public ProductDto GetProductById(int id)
        {
            var pds = PersistSvr<Product>.SearchBy(p => p.ProductID == id).ToList().Select(c => Mapper.Map<ProductDto>(c));
            return pds.Count() == 0 ? null : pds.First();
        }

        public void InsertProduct(Product product, bool commit)
        {
            PersistSvr<Product>.Insert(product, commit);
        }

        public void UpdateProduct(Product currentProduct, bool commit)
        {
            PersistSvr<Product>.Update(currentProduct, commit);
        }

        public void DeleteProduct(int productId, bool commit)
        {
            IQueryable<Product> qProduct = PersistSvr<Product>.GetAll();
            List<Product> pros = (from p in qProduct where p.ProductID == productId select p).ToList();
            if (pros.Count <= 0)
                throw new ApplicationException("NorthwindSvr::DeleteProduct: product with Id " + productId.ToString() +
                                               " doesn't exist.");
            PersistSvr<Product>.Delete(pros[0], commit);
        }


        public List<CategoryDto> GetProductCategories()
        {
            return PersistSvr<Category>.GetAll().ToList().Select(c => Mapper.Map<CategoryDto>(c)).ToList();
        }


        public List<SupplierDto> GetSuppliers()
        {
            return PersistSvr<Supplier>.GetAll().ToList().Select(c => Mapper.Map<SupplierDto>(c)).ToList();
        }

        public void Commit()
        {
            //In our implementation, any entity's commit will do the same: commit all changes to the database
            // so PersistSvr<Order>.Commit() and PersistSvr<Product>.Commit() will do the same.
            PersistSvr<Order>.Commit();
        }

        public TestResult Test()
        {
            return new TestResult() { Result = "Hello" };
        }
    }
}