namespace QueriesSQLSERVER{
    public class DatabaseQUERIES
    {
        public string InsertBrands (string brandName){
            return $"INSERT INTO Brands VALUES({brandName})";
        }
        public const string GetCustomers = "SELECT * FROM Customers";
        public const string GetProductOfId = "SELECT * FROM PRODUCTS WHERE ProductId = 1";
        public const string UpdateProducts = "";
        public const string DeleteProducts = "DELETE FROM Customers WHERE CustomerId = {}";
    }
}
