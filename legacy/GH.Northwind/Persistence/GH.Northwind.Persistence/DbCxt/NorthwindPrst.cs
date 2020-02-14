﻿/*
 * Copyright © 2012
 * This code is for the codeproject article "A N-Tier Architecture Sample with ASP.NET MVC3, WCF and Entity Framework" at  
 * http://www.codeproject.com/Articles/434282/A-N-Tier-Architecture-Sample-with-ASP.NET-MVC3-WCF-and-Entity-Framework. 
 * Permission to use, copy or modify this software freely is hereby granted, 
 * provided that this copyright notice appears in the orginal or modified copies. 
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
using System.Linq;
using System.Linq.Expressions;
using GH.Common.Framework.Persistence.DbCxt;
using GH.Common.Utils;
using GH.Northwind.Business.Entities;
using GH.Northwind.EntityFramework;

namespace GH.Northwind.Persistence.DbCxt
{
    public class CustomerPrst : PersistenceBase<Customer>
    {
        protected override IQueryable<Customer> EntitySet
        {
            get { return (DataContext as NorthwindEntities).Customers; }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Customers)); }
        }

        protected override Customer FindMatchedOne(Customer toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.CustomerID == toBeMatched.CustomerID); }
    }

    public class ProductPrst : PersistenceBase<Product>
    {
        protected override IQueryable<Product> EntitySet
        {
            get
            {
                predicate = p => p.ProductID == 1; 
                return (DataContext as NorthwindEntities).Products;
            }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Products)); }
        }

        protected override Product FindMatchedOne(Product toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.ProductID == toBeMatched.ProductID); }
    }

    public class OrderPrst : PersistenceBase<Order>
    {
        protected override IQueryable<Order> EntitySet
        {
            get { return (DataContext as NorthwindEntities).Orders; }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Orders)); }
        }

        protected override Order FindMatchedOne(Order toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.OrderID == toBeMatched.OrderID); }
    }

    public class Order_DetailPrst : PersistenceBase<Order_Detail>
    {
        protected override IQueryable<Order_Detail> EntitySet
        {
            get { return (DataContext as NorthwindEntities).Order_Details; }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Order_Details));
            }
        }

        protected override Order_Detail FindMatchedOne(Order_Detail toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.OrderID == toBeMatched.OrderID && o.ProductID == toBeMatched.ProductID); }
    }

    public class SupplierPrst : PersistenceBase<Supplier>
    {
        protected override IQueryable<Supplier> EntitySet
        {
            get { return (DataContext as NorthwindEntities).Suppliers; }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Suppliers));
            }
        }

        protected override Supplier FindMatchedOne(Supplier toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.SupplierID == toBeMatched.SupplierID); }
    }

    public class CategoryPrst : PersistenceBase<Category>
    {
        protected override IQueryable<Category> EntitySet
        {
            get { return (DataContext as NorthwindEntities).Categories; }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Categories));
            }
        }

        protected override Category FindMatchedOne(Category toBeMatched) { return EntitySet.DefaultIfEmpty(null).First(o => o.CategoryID == toBeMatched.CategoryID); }
    }
}