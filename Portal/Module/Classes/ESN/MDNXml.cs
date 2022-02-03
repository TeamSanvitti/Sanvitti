using System.Xml;
using System.Xml.Serialization;


namespace avii.Classes
{
    
    [XmlRoot(ElementName = "MDNroot", IsNullable = true)]
    public class MDNXml
    {
        private int? _podid = 0;
        private bool? _provisioning = false;

        [XmlElement(ElementName = "PodID", IsNullable = true)]
        public int? PodID
        {
            get { return _podid; }
            set
            {
                _podid = value;
            }
        }
        [XmlElement(ElementName = "Provisioning", IsNullable = true)]
        public bool? Provisioning
        {
            get { return _provisioning; }
            set
            {
                _provisioning = value;
            }
        }
    }
}