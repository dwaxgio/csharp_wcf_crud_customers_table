using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
// SE AGREGAN LAS SIGUIENTES REFERENCIAS
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract] // SE EJECUTAN LAS OPERACIONES CRUD
        CustomerData Get();
        [OperationContract]
        void Insert(string name, string country);
        [OperationContract]
        void Update(int customerId, string name, string country);
        [OperationContract]
        void Delete(int customerId);
    }

    [DataContract] // CustomerData contiene la propiedad DataTable de CustomersTable, se usará para enviar la información desde el sericio WCF a la APP
    public class CustomerData
    {
        public CustomerData() // Constructor
        {
            this.CustomersTable = new DataTable("CustomersData");
        }
        [DataMember]
        public DataTable CustomersTable { get; set; }
    }
}
