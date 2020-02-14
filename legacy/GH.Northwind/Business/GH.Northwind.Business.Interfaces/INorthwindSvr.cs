

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using GH.Northwind.Business.Entities;
using GH.Northwind.Business.Interfaces.DTO;

namespace GH.Northwind.Business.Interfaces
{
    [ServiceContract]
    public interface INorthwindSvr
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Test", ResponseFormat = WebMessageFormat.Json)]
        TestResult Test();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCustomers")]
        List <CustomerDto> GetCustomers();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/InsertCustomer")]
        void InsertCustomer(Customer customer, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/UpdateCustomer")]
        void UpdateCustomer(Customer currentCustomer, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/DeleteCustomer")]
        void DeleteCustomer(String customerId, bool commit);

        [OperationContract]
        List<OrderDto> GetOrders();

        [OperationContract]
        List<OrderDetailDto> GetOrderDetailForAnOrder(int orderId);

        [OperationContract]
        List<OrderDto> GetOrderForACustomer(String customerId);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/CreateOrder")]
        void CreateOrder(Order order, Order_Detail[] details);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/UpdateOrder")]
        void UpdateOrder(Order currentOrder, Order_Detail[] details, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/DeleteOrder")]
        void DeleteOrder(int orderId, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/DeleteAnOrderDetailFromAnOrder")]
        void DeleteAnOrderDetailFromAnOrder(int orderId, int orderDetailId, bool commit);

        [OperationContract]
        List<ProductDto> GetProducts();

        [OperationContract]
        ProductDto GetProductById(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/InsertProduct")]
        void InsertProduct(Product product, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/UpdateProduct")]
        void UpdateProduct(Product currentProduct, bool commit);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/DeleteProduct")]
        void DeleteProduct(int productId, bool commit);

        [OperationContract]
        List<CategoryDto> GetProductCategories();

        [OperationContract]
        List<SupplierDto> GetSuppliers();

        [OperationContract]
        void Commit();
    }


    [DataContract]
    public class TestResult
    {
        [DataMember]
        public string Result { get; set; }
    }
}