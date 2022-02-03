using System;

namespace avii.Classes
{
    [Serializable]
     public class ItemMaker
    {
        public int MakerGUID { get; set; }
        public string MakerName { get; set; }
        public string ShortName { get; set; }
        public string MakerDescription { get; set; }
        public string MakerImage { get; set; }
        public bool Active { get; set; }
        public int AddressID { get; set; }
        public Address MakerAddresses { get; set; }
        public ContactInfo MakerContactInfo { get; set; }
        public int MakerCount { get; set; }
        public bool ShowunderCatalog { get; set; }
    }
}
