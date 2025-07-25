# Classes

## Product

### Responsibilities
<ul>
    <li>_name: string</li>
    <li>_id: string</li>
    <li>_price: float</li>
    <li>_quantity: int</li> 
</ul>

### Constructor
<ul>
    <li>Product(string name, string id, float price, int quantity)</li>
</ul>

### Behaviors
<ul>
    <li>TotalCostPerProduct: float</li>
    <li>GetProductId: string</li>
    <li>GetProductName: string</li>
</ul>

## Address

### Responsibilities
<ul>
    <li>_street: string</li>
    <li>_city: string</li>
    <li>_state: string</li>
    <li>_country: string</li>
</ul>

### Constructor
<ul>
    <li>Address(string street, string city, string state, string country)</li>
</ul>

### Behaviors
<ul>
    <li>IsExportation: bool</li>
    <li>FullAddress: string</li>
</ul>

## Customer

### Responsibilities
<ul>
    <li>_name: string</li>
    <li>_userAddress: Address</li>
</ul>

### Constructor
<ul>
    <li>Customer(string name, Address userAddress)</li>
</ul>

### Behaviors
<ul>
    <li>IsFromUS: bool</li>
    <li>CustomerName: string</li>
    <li>CustomerAddress: string</li>
</ul>

## Order

### Responsibilities
<ul>
    <li>_customerInfo: Customer</li>
    <li>_order: List&lt;Product&gt;</li>
</ul>

### Constructor
<ul>
    <li>Order(Customer customerInfo, List&lt;Product&gt; orderList)</li>
</ul>

### Behaviors
<ul>
    <li>ShippingCost: int</li>
    <li>TotalPrice: float</li>
    <li>PackingLabel: string</li>
    <li>ShippingLabel: string</li>
</ul>
