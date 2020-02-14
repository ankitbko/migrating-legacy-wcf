using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modern.NW.Persistence.Data;
using Modern.NW.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern.NW.Persistence
{
    public class CustomerPrst : PersistenceBase<Customer>
    {
        public CustomerPrst(NorthwindContext dataContext, ILogger<PersistenceBase<Customer>> logger)
            :base(dataContext, logger)
        { }

        protected override IQueryable<Customer> EntitySet
        {
            get { return DataContext.Customers; }
        }

        protected override async Task<Customer> FindMatchedOneAsync(Customer toBeMatched) { return await EntitySet.DefaultIfEmpty(null).FirstAsync(o => o.CustomerId == toBeMatched.CustomerId); }
    }
}
