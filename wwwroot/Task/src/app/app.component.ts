import { Component } from '@angular/core';
import{ Customer, Order } from  './app.model';
import {HttpClient} from '@angular/common/http';
//import{ order } from  './app.model';
  import { from } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Task';
 // binds with the customer UI
  customer:Customer =new Customer();
  // binds with the order UI
  orderobj:Order= new Order();
  // List of Customers
  customers:Array<Customer> = new Array<Customer>();

Errors:any=[];
constructor(public  http:HttpClient){
}

SelectCustomer(val){
  this.customer = this.customers[val];
 }

 SelectOrder(val){
  this.orderobj = this.customer.orders[val];
 }
  AddOrders(){

    this.customer.orders.push(this.orderobj);
  
    this.orderobj= new Order();

  }
  AddCustomer(){
    this.customers.push(this.customer);
    this.customer = new Customer();
  }
  
  Submit(){
    var observable = this.http.post("/Customer/Submit",this.customers);
    observable.subscribe(res=>this.Success(res),
                        res=>this.Errors(res));
    
      //      this.http.post<any>("/api/Student",this.customerobj.orders).subscribe(res=>{
        //    this.customerobj.orders=res.Results;
        
        }
       
        Error(res) {         
          this.Errors = res.error;
          ;   
        } 
        Success(res){
          this.customer.orders = res;
          this.customer = new Customer();
          this.Errors = [];
        }
    
    
}

