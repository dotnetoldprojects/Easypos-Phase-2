namespace Domain.Dtos
{
    public class ProductLine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string UnitCode { get; set; } = "PCE";
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; } = 0;
        public decimal TaxPercent { get; set; } = 15;
    }
}