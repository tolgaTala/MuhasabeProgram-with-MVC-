namespace Entities.Dtos.Products
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EndOfYearStock { get; set; }
        public decimal EndOfYearPrice { get; set; }
        public decimal EndOfYearTotal { get; set; }
    }
}
