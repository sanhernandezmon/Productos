namespace Productos.Models
{
    public class Product
    {
        public Product(string productName, DateTime validUntilDate, decimal cost, int avaliability)
        {
            ProductName=productName;
            CreationDate=DateTime.Now();
            ValidUntilDate=validUntilDate;
            Cost=cost;
            Avaliability=avaliability;
        }

        public Guid ProductId => new Guid();
        public string ProductName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ValidUntilDate { get; set; }
        public decimal Cost { get; set; }
        public int Avaliability {  get; set; }
    }
}
