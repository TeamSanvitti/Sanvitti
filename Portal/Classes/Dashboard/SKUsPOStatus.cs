
namespace avii.Classes
{
    public class SKUsPOStatus
    {
        public string CompanyName { get; set; }
        public string SKU { get; set; }
        public int Pending { get; set; }
        public int Processed { get; set; }
        public int Shipped { get; set; }
        public int Closed { get; set; }
        public int Cancel { get; set; }
        public int Return { get; set; }
    }
}