using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.dal;

namespace Demo.Controllers
{
    public class CustomerController : Controller
    {
               
        
 public IActionResult Submit([FromBody]List<Customer> customers)
        {
            // inserting in to db
           CustomerDbContext dal = new CustomerDbContext();
             dal.Database.EnsureCreated();
            foreach (var cust in customers)
            {
                dal.Customers.Add(cust);    // in memort
            }
            
            dal.SaveChanges(); // physical comm
    
            return View("CustomerSubmit");
            
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
