using System.Collections.Generic;

public class Customer{
    public string Firstname{ get; set;}
    public string Lastname{ get; set;}
    public string Purchasedate{ get; set;}
 //  public string Purchasedate{ get; set}
    public List<Order> orders{get; set; }

   //var Purchasedate = DateTime.Now;
//var date = Purchasedate.Date;

   /*  constructor(){
        this.Purchasedate = new  Date().toLocaleDateString();
    }*/
}
 public class Order{
    public string Product{ get; set; }
    public string Quantity{ get; set; }
    public string Price{ get; set; }
    public string TotalPrice{ get; set; }
    
}