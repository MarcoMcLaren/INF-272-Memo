namespace inf272Semester2SectionAQ1.Models
{
    public class Products
    {
        public int ProductID { get; }
        public string Name { get; set; }
        public double CostPrice { get; set; }
        internal double? SellingPrice { get; set; }
        private double VAT { get; set; } = 15 / 100;

        public void ComputeSellingPrice()
        {
            SellingPrice = CostPrice+(VAT*CostPrice);
        }
    }
}
