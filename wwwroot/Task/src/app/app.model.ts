export class Customer{
    Firstname:string = "";
    Lastname:string = "";
    Purchasedate:string = null;
    orders:Array<Order> = new Array<Order>();
    constructor(){
        this.Purchasedate = new  Date().toLocaleDateString();
    }
}
export class Order{
    Product:string = "";
    private _Quantity:number = 0;
    _Price:number = 0;
    TotalPrice:number;
    get Quantity(): number {
        return this._Quantity;
    }
    set Quantity(value: number) {
        this._Quantity = value;
        this.CalculatePrice();
    }
    get Price(): number {
        return this._Price;
    }
    set Price(value: number) {
        this._Price = value;
        this.CalculatePrice();
    }
    private CalculatePrice() : void{
        this.TotalPrice = this._Quantity * this._Price
    }
}