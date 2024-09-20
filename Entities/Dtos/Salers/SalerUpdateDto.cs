namespace Entities.Dtos.Salers
{
    public class SalerUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNo { get; set; }
    }
}
