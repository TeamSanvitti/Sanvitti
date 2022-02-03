using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;

namespace SV.Framework.Common.LabelGenerator
{
    [JsonConverter(typeof(StringEnumConverter))]
    public interface iPrintLabel
    {
        string LabelType { get; set; }
        string ApiURL { get; set; }
        string AccountID { get; set; }
        string PassPharase { get; set; }
        string RequesterID { get; set; }
        string AuthString { get; set; }
        decimal FinalPostage { get; set; }
        string FulfillmentNumber { get; set; }
        string PackageContent { get; set; }
        Weight PackageWeight { get; set; }

        ShipMethods ShippingMethod { get; set; }
        ShipType ShippingType { get; set; }
        ShipInfo ShipTo { get; set; }
        ShipInfo ShipFrom { get; set; }
        CustomsInfo CustomsInfo { get; set; }

        string TrackingNumber { get; set; }

        string ShippingLabelImage { get; set; }
        DateTime LabelPrintDateTime { get; set; }
        ShipPackageShape PackageShape { get; set; }

       // string ErrorMessage { get; set; }
    }
    public interface iEndiciaLabelCredentials
    {
        string ApiURL { get; set; }
        string AccountID { get; set; }
        string PassPharase { get; set; }
        string RequesterID { get; set; }

        string AuthString { get; set; }
        string ConnectionString { get; set; }

    }

    public enum USStates
    {
        AL,
        AK,
        AZ,
        AR,
        CA,
        CO,
        CT,
        DE,
        DC,
        FL,
        GA,
        HI,
        ID,
        IL,
        IN,
        IA,
        KS,
        KY,
        LA,
        ME,
        MD,
        MA,
        MI,
        MN,
        MS,
        MO,
        MT,
        NE,
        NV,
        NH,
        NJ,
        NM,
        NY,
        NC,
        ND,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VT,
        VA,
        WA,
        WV,
        WI,
        WY

    }
    public enum CanadaStates
    {
        AB,
        BC,
        MB,
        NB,
        NL,
        NS,
        NT,
        NU,
        ON,
        PE,
        QC,
        SK,
        YT
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShipMethods
    {
        PriorityExpress,
        First,
        LibraryMail,
        MediaMail,
        ParcelSelect,
        RetailGround,
        Priority,
        PriorityMailExpressInternational,
        FirstClassMailInternational,
        FirstClassPackageInternationalService,
        PriorityMailInternational,
        fedex_ground,
        fedex_home_delivery,
        fedex_2day,
        fedex_2day_am,
        fedex_express_saver,
        fedex_standard_overnight,
        fedex_priority_overnight,
        fedex_first_overnight,
        fedex_401_day_freight,
        fedex_2_day_freight,
        fedex_3_day_freight,
        fedex_first_overnight_freight,
        TruckLoad
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternationalShipMethods
    {
        PriorityMailExpressInternational,
        FirstClassMailInternational,
        FirstClassPackageInternationalService,
        PriorityMailInternational         
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EndiciaShipMethods
    {
        PriorityExpress,
        First,
        LibraryMail,
        MediaMail,
        ParcelSelect,
        RetailGround,
        Priority,
        PriorityMailExpressInternational,
        FirstClassMailInternational,
        FirstClassPackageInternationalService,
        PriorityMailInternational
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShipPackageShape
    {
        Card,
        Letter,
        Flat,
        Parcel,
        IrregularParcel,
        LargeParcel,
        FlatRateEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateGiftCardEnvelope,
        FlatRateWindowEnvelope,
        FlatRateCardboardEnvelope,
        SmallFlatRateEnvelope,
        SmallFlatRateBox,
        MediumFlatRateBox,
        LargeFlatRateBox,
        cubic,
        dvd_flat_rate_box,
        flat_rate_envelope,
        flat_rate_legal_envelope,
        flat_rate_padded_envelope,
        large_envelope_or_flat,
        large_flat_rate_box,
        large_package,
        large_video_flat_rate_box,
        letter,
        medium_flat_rate_box,
        package,
        regional_rate_box_a,
        regional_rate_box_b,
        regional_rate_box_c,
        small_flat_rate_box,
        thick_envelope
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InternationalShipPackageShape
    {
        Card,
        Letter,
        Flat,
        Parcel,
        IrregularParcel,
        LargeParcel,
        FlatRateEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateGiftCardEnvelope,
        FlatRateWindowEnvelope,
        FlatRateCardboardEnvelope,
        SmallFlatRateEnvelope,
        SmallFlatRateBox,
        MediumFlatRateBox,
        LargeFlatRateBox,
        DVDFlatRateBox,
        LargeVideoFlatRateBox,
        RegionalRateBoxA,
        RegionalRateBoxB,
        LargeFlatRateBoardGameBox,
        HalfTrayBox,
        FullTrayBox,
        EMMTrayBox,
        FlatTubTrayBox
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EndiciaShipPackageShape
    {
        Card,
        Letter,
        Flat,
        Parcel,
        IrregularParcel,
        LargeParcel,
        FlatRateEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateGiftCardEnvelope,

        FlatRateWindowEnvelope,
        FlatRateCardboardEnvelope,
        SmallFlatRateEnvelope,
        SmallFlatRateBox,
        MediumFlatRateBox,
        LargeFlatRateBox
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShipStationPackageShape
    {
        cubic,
        dvd_flat_rate_box,
        flat_rate_envelope,
        flat_rate_legal_envelope,
        flat_rate_padded_envelope,
        large_envelope_or_flat,
        large_flat_rate_box,
        large_package,
        large_video_flat_rate_box,
        letter,
        medium_flat_rate_box,
        package,
        regional_rate_box_a,
        regional_rate_box_b,
        regional_rate_box_c,
        small_flat_rate_box,
        thick_envelope
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShipType
    {
        [Description("S")]
        Ship,
        [Description("R")]
        Return
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShipStationsShipMethods
    {
       // [field: Description("FedEx Ground®")]
        fedex_ground,


        //[field: Description("FedEx Home Delivery®")]
        fedex_home_delivery,

       // [field: Description("FedEx 2Day®")]
        fedex_2day,


        //[field: Description("FedEx 2Day® A.M.")]
        fedex_2day_am,

        //[field: Description("FedEx Express Saver®")]
        fedex_express_saver,

        //[field: Description("FedEx Standard Overnight®")]
        fedex_standard_overnight,
    

        //[field: Description("FedEx Priority Overnight®")]
        fedex_priority_overnight,
    

        //[field: Description("FedEx First Overnight®")]
        fedex_first_overnight,
    

        //[field: Description("FedEx 1 Day® Freight")]
        fedex_401_day_freight,
    

        //[field: Description("FedEx 2 Day® Freight")]
        fedex_2_day_freight,
    

        //[field: Description("FedEx 3 Day® Freight")]
        fedex_3_day_freight,
    

        //[field: Description("FedEx First Overnight® Freight")]
        fedex_first_overnight_freight
    }

    public class ShipStation
    {
        public string carrierCode { get; set; } = "fedex";
        public ShipStationsShipMethods serviceCode { get; set; }
        public ShipStationPackageShape packageCode { get; set; }
        public string confimation { get; set; }
        public string shipDate { get; set; }
        public Weight weight { get; set; }
        public ShipStationAddress shipFrom { get; set; }
        public ShipStationAddress shipTo { get; set; }
        public bool testLabel { get; set; } = true;
    }

    public class Weight
    {
        public double value { get; set; } = 1;
        public string units { get; set; } = "ounces";
    }
    public class ShipStationAddress
    {
        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string street3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public bool residential { get; set; }
        public string addressVerified { get; set; }
        public string phone { get; set; }
    }

    public class ShipStationResponse
    {
        public string shipmentId { get; set; }
        public string shipDate { get; set; }
        public double shipmentCost { get; set; }
        public DateTime createDate { get; set; }
        public string labelData { get; set; }
        public string trackingNumber { get; set; }
        public string exceptionMessage { get; set; }
    }

   public class EndiciaPrintLabel :  iPrintLabel
    {
        public string LabelType { get; set; }
        public string ApiURL { get; set; }
        public string AccountID { get; set; }
        public string PassPharase { get; set; }
        public string RequesterID { get; set; }
        public string AuthString { get; set; }
        public decimal FinalPostage { get; set; }
        public string FulfillmentNumber { get; set; }
        public string PackageContent { get; set; }
        public Weight PackageWeight { get; set; }
        public ShipType ShippingType { get; set; }
        public ShipInfo ShipTo { get; set; }
        public ShipInfo ShipFrom { get; set; }
        public CustomsInfo CustomsInfo { get; set; }

        public string TrackingNumber { get; set; }

        public string ShippingLabelImage { get; set; }
        public DateTime LabelPrintDateTime { get; set; }

        private ShipPackageShape _shipPackageShape = ShipPackageShape.Letter;

        public ShipPackageShape PackageShape
        {
            get { return _shipPackageShape; }
            set
            {
                if (System.Enum.IsDefined(typeof(ShipPackageShape), value.ToString()))
                {
                    _shipPackageShape = value;
                }
                else
                    throw new ApplicationException("Package Shape Method is not correct");
            }
        }

        private ShipMethods _shippingMethod = ShipMethods.Priority;
        public ShipMethods ShippingMethod { get { return _shippingMethod; }
            set
            {
                if (System.Enum.IsDefined(typeof(ShipMethods), value.ToString()))
                {
                    _shippingMethod = value;
                }
                else
                    throw new ApplicationException("Shipping Method is not correct");
            }
        }
    }

    public class ShipStationPrintLabel : iPrintLabel
    {
        public string LabelType { get; set; }
        public string ApiURL { get; set; }
        public string AccountID { get; set; }
        public string PassPharase { get; set; }
        public string RequesterID { get; set; }
        public string AuthString { get; set; }
        public decimal FinalPostage { get; set; }
        public string FulfillmentNumber { get; set; }
        public string PackageContent { get; set; }
        public Weight PackageWeight { get; set; }
        public ShipType ShippingType { get; set; }
        public ShipInfo ShipTo { get; set; }
        public ShipInfo ShipFrom { get; set; }
        public CustomsInfo CustomsInfo { get; set; }
        public string TrackingNumber { get; set; }

        public string ShippingLabelImage { get; set; }
        public DateTime LabelPrintDateTime { get; set; }
        private ShipMethods shippingMethod { get; set; }
        public ShipMethods ShippingMethod
        {
            get { return shippingMethod; }
            set
            {
                if (System.Enum.IsDefined(typeof(ShipStationsShipMethods), value.ToString()))
                {
                    shippingMethod = value;
                }
                else
                    throw new ApplicationException("Shipping Method is not correct");
            }
        }

        private ShipPackageShape _shipPackageShape = ShipPackageShape.Letter;

        public ShipPackageShape PackageShape
        {
            get { return _shipPackageShape; }
            set
            {
                if (System.Enum.IsDefined(typeof(ShipPackageShape), value.ToString()))
                {
                    _shipPackageShape = value;
                }
                else
                    throw new ApplicationException("Package Shape Method is not correct");
            }
        }

    }
    public class EndiciaCredentials : iEndiciaLabelCredentials
    {
        public string ApiURL { get; set; }
        public string AccountID { get; set; }
        public string PassPharase { get; set; }
        public string RequesterID { get; set; }
        public string AuthString { get; set; }
        public string ConnectionString { get; set; }

    }
    //public class ShipStationCredentials
    //{
    //    public string ApiURL { get; set; }
    //    public string AccountID { get; set; }
    //    public string PassPharase { get; set; }
    //    public string RequesterID { get; set; }
    //    public string AuthString { get; set; }
    //}
    public class PrintLabel : iPrintLabel
    {
        public string ApiURL { get; set; }
        public string AccountID { get; set; }
        public string PassPharase { get; set; }
        public string RequesterID { get; set; }
        public string AuthString { get; set; }
        public decimal FinalPostage { get; set; }
        private ShipMethods shippingMethod { get; set; }
        private ShipPackageShape _shipPackageShape = ShipPackageShape.Letter;
        public string FulfillmentNumber { get; set; }
        public string PackageContent { get; set; }
        public Weight PackageWeight { get; set; }
        public ShipType ShippingType { get; set; }
        public ShipInfo ShipTo { get; set; }
        public ShipInfo ShipFrom { get; set; }
        public CustomsInfo CustomsInfo { get; set; }
        public string TrackingNumber { get; set; }
        public string LabelType { get; set; }

        public string ShippingLabelImage { get; set; }
        public DateTime LabelPrintDateTime { get; set; }
        public ShipMethods ShippingMethod
        {
            get { return shippingMethod; }
            set
            {
                shippingMethod = value;;
            }
        }
        public ShipPackageShape PackageShape
        {
            get { return _shipPackageShape; }
            set
            {
                _shipPackageShape = value;
            }
        }
    }

    public interface iShippingLabelGenerator
    {
        iPrintLabel PrintShippingLabel(iPrintLabel printLabel);
        iEndiciaLabelCredentials GetIEndiciaLabelCredentials(iEndiciaLabelCredentials endiciaLabelCredentials);
    }

    public class ShippingLabelOperation : iShippingLabelGenerator
    {
        public ShippingLabelOperation()
        {
            GetConfigs();
        }

        private string shipStationURL = "https://ssapi.shipstation.com/";
        private string authString = "Basic M2NhOWRhM2ViZGIyNDM3MTgyNmJiY2FjMGY3YjMzY2Q6MjAzNDYyZTI0Y2EzNDAyYmFmNGE4ZmFlYmM1YTZkMTE=";

        public async Task<ShipStationResponse> ShipStationLabel(iPrintLabel shipStationLabel)
        {
           // var baseAddress = new Uri(shipStationURL);
            var baseAddress = new Uri(shipStationLabel.ApiURL);
            string responseData = null;
            ShipStationResponse shipStationResponse;
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", authString);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", shipStationLabel.AuthString);
                
                var ship = populateShipStationRequest(shipStationLabel);

                var jsonstring = JsonConvert.SerializeObject(ship);

                using (var content = new StringContent(jsonstring, Encoding.UTF8, "application/json"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    using (HttpResponseMessage response = await httpClient.PostAsync("shipments/createlabel", content))
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                    shipStationResponse = JsonConvert.DeserializeObject<ShipStationResponse>(responseData);

                }
            }

            return shipStationResponse;
        }

        //private readonly EwsLabelServiceSoap service;
        public  iPrintLabel PrintShippingLabel(iPrintLabel printLabel)
        {
            if (!string.IsNullOrWhiteSpace(printLabel.FulfillmentNumber) && ValidateShipInfo(printLabel.ShipTo))
            {
                ServiceReference1.LabelRequestResponse response = null;
                ServiceReference1.LabelRequest labelRequest = null;
                if (System.Enum.IsDefined(typeof(EndiciaShipMethods), printLabel.ShippingMethod.ToString()))
                {
                    ServiceReference1.EwsLabelServiceSoapClient.EndpointConfiguration endpointConfiguration = new EwsLabelServiceSoapClient.EndpointConfiguration();
                    //service.EndpointConfiguration endpointConfiguration
                    ServiceReference1.EwsLabelServiceSoapClient service = new ServiceReference1.EwsLabelServiceSoapClient(endpointConfiguration);
                    labelRequest = populateEndiciaRequest(printLabel);

                    string requestXML = SV.Framework.LabelGenerator.AddressValidationOperation.SerializeObject(labelRequest);

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    response =  service.GetPostageLabelAsync(labelRequest).GetAwaiter().GetResult();
                    if (response != null)
                    {

                        string image = "";
                        if (!string.IsNullOrWhiteSpace(response.Base64LabelImage))
                            image = response.Base64LabelImage;
                        else if (response.Label != null && response.Label.Image != null && response.Label.Image[0].Value != null)
                            image = response.Label.Image[0].Value;

                        string responseXML = SV.Framework.LabelGenerator.AddressValidationOperation.SerializeObject(response);
                        printLabel.FinalPostage = response.FinalPostage;
                        printLabel.TrackingNumber = response.TrackingNumber;

                        printLabel.ShippingLabelImage = image;
                        printLabel.LabelPrintDateTime = DateTime.Today;
                        printLabel.PackageContent = response.ErrorMessage;
                    }
                }
                else if (System.Enum.IsDefined(typeof(ShipStationsShipMethods), printLabel.ShippingMethod.ToString()))
                {
                    ShipStationResponse shipStationResponse = ShipStationLabel(printLabel).GetAwaiter().GetResult();
                    if (shipStationResponse != null)
                    {
                        printLabel.FinalPostage =  Convert.ToDecimal(shipStationResponse.shipmentCost);
                        printLabel.TrackingNumber = shipStationResponse.trackingNumber;
                        printLabel.ShippingLabelImage = shipStationResponse.labelData;
                        printLabel.LabelPrintDateTime = Convert.ToDateTime(shipStationResponse.shipDate);
                        printLabel.PackageContent = shipStationResponse.exceptionMessage;
                    }
                    else
                    {
                        printLabel.PackageContent = string.IsNullOrWhiteSpace(shipStationResponse.trackingNumber)?"Error!":"";
                    }
                }

                //if (labelRequest != null && response != null)
                //{
                //    XmlSerializer xreq = new XmlSerializer(typeof(LabelRequest));
                //    TextWriter writerreq = new StreamWriter(@"C:\lan_Test\endiciaLabelRequest.xml");
                //    xreq.Serialize(writerreq, labelRequest);
                //    XmlSerializer x = new XmlSerializer(typeof(LabelRequestResponse));
                //    TextWriter writer = new StreamWriter(@"C:\lan_Test\endiciaLabelResponse.xml");
                //    x.Serialize(writer, response);
                //}
                return printLabel;
            }
            else
            {
                throw new ApplicationException("PrintShippingLabel: Missing required information");
            }
        }
        public iEndiciaLabelCredentials GetIEndiciaLabelCredentials(iEndiciaLabelCredentials endiciaLabelCredentials)
        {
            //iEndiciaLabelCredentials endiciaLabelCredentials = new iEndiciaLabelCredentials();
            string AccountID = "accountid", RequesterID = "requesterid", PassPhrase = "passphrase";
            string AccountIDValue = "", RequesterIDValue = "", PassPhraseValue = "";
            string connectionString = endiciaLabelCredentials.ConnectionString;
            
            string[] array = connectionString.Split(',');
            if (array.Length == 3)
            {
                
                    
                if (array[0].ToLower().Contains(AccountID))
                {
                    string[] array1 = array[0].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        AccountIDValue = array1[1];
                    }
                            
                }
                if (array[0].ToLower().Contains(RequesterID))
                {
                    string[] array1 = array[0].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        RequesterIDValue = array1[1];
                    }
                            
                }
                if (array[0].ToLower().Contains(PassPhrase))
                {
                    string[] array1 = array[0].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        PassPhraseValue = array1[1];
                    }
                            
                }   
                

                if (array[1].ToLower().Contains(AccountID))
                {
                    string[] array1 = array[1].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        AccountIDValue = array1[1];
                    }
                    
                }
                if (array[1].ToLower().Contains(RequesterID))
                {
                    string[] array1 = array[1].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        RequesterIDValue = array1[1];
                    }                   

                }
                if (array[1].ToLower().Contains(PassPhrase))
                {
                    string[] array1 = array[1].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        PassPhraseValue = array1[1];
                    }                    
                }
                if (array[2].ToLower().Contains(AccountID))
                {
                    string[] array1 = array[2].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        AccountIDValue = array1[1];
                    }
                }
                if (array[2].ToLower().Contains(RequesterID))
                {
                    string[] array1 = array[2].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        RequesterIDValue = array1[1];
                    }                    
                }
                if (array[2].ToLower().Contains(PassPhrase))
                {
                    string[] array1 = array[2].Split('=');
                    if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                    {
                        PassPhraseValue = array1[1];
                    }                    
                }
            }
            endiciaLabelCredentials.AccountID = AccountIDValue;
            endiciaLabelCredentials.RequesterID = RequesterIDValue;
            endiciaLabelCredentials.PassPharase = PassPhraseValue;
            return endiciaLabelCredentials;
        }
        private void GetConfigs()
        {
            shipStationURL = ReadSetting("ShipStation_URL");
            authString = ReadSetting("ShipStation_AUTH");

            if (string.IsNullOrWhiteSpace(shipStationURL) || string.IsNullOrWhiteSpace(authString))
            {
                throw new Exception("Missing configuraiton setting for Ship Station");
            }
        }

        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
               return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
        }


        private bool ValidateShipInfo(ShipInfo shipToInfo)
        {
            if (!string.IsNullOrWhiteSpace(shipToInfo.ContactName)
                && !string.IsNullOrWhiteSpace(shipToInfo.ShipToAddress)
                && !string.IsNullOrWhiteSpace(shipToInfo.ShipToCity)
                && !string.IsNullOrWhiteSpace(shipToInfo.ShipToZip)
                && !string.IsNullOrWhiteSpace(shipToInfo.ShipToState)
                )
                return true;
            else
                return false;
        }



        private ServiceReference1.LabelRequest populateEndiciaRequest(iPrintLabel printLabel)
        {
            CustomsInfo customsInfo = printLabel.CustomsInfo;
            ShipInfo shipToInfo = printLabel.ShipTo;
            ServiceReference1.LabelRequest labelRequest = new ServiceReference1.LabelRequest();
            labelRequest.ImageFormat = "JPEGMONOCHROME";
            //labelRequest.Test = "False";
            labelRequest.ValidateAddress = "TRUE";

            labelRequest.LabelType = printLabel.LabelType; //"Default";//DestinationConfirm

            if (printLabel.PackageShape.ToString().ToLower() == "letter" || printLabel.PackageShape.ToString().ToLower() == "card" || printLabel.PackageShape.ToString().ToLower() == "flat")
            {
                labelRequest.LabelType = "DestinationConfirm";//DestinationConfirm
                labelRequest.LabelSize = "6X4";
            }
            else
                labelRequest.LabelSize = "4x6";

            labelRequest.ImageResolution = "300";

            labelRequest.MailClass = printLabel.ShippingMethod.ToString(); // "Priority";
            labelRequest.MailpieceShape = printLabel.PackageShape.ToString(); //"FlatRateEnvelope";


            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), shipToInfo.ShipToState))
            {
                //labelRequest.SignatureWaiver = "TRUE";
                labelRequest.LabelType = "International"; //"Default";//DestinationConfirm
                labelRequest.LabelSubtype = "Integrated"; //"Default";//DestinationConfirm
                labelRequest.DateAdvance = 1; //"Default";//DestinationConfirm
                                              // labelRequest.MailClass = "FirstClassMailInternational"; // "Priority";
                                              //labelRequest.MailpieceShape = "Flat"; //"FlatRateEnvelope";             

                // labelRequest.ContentsType = labelRequest.ReferenceID; //"FlatRateEnvelope";
               // labelRequest.CustomsInfo = customsInfo;
                labelRequest.ToCountryCode = "CA";
            }

            labelRequest.AccountID = printLabel.AccountID;//"2553271"; // printLabel.AccountID;// "1353742"; //"2553271";// GetConfigurations.ACCOUNT_ID;
            labelRequest.RequesterID = printLabel.RequesterID;//"lxxx";// printLabel.RequesterID;// "Lang";// lxxx";// GetConfigurations.REQUESTER_ID;
            labelRequest.PassPhrase = printLabel.PassPharase;// langlobal@2020"; //printLabel.PassPharase;// "12031Lan1!";// "theskyisred";// GetConfigurations.PASS_PHRASE;
            labelRequest.WeightOz = printLabel.PackageWeight.value;
            labelRequest.PartnerCustomerID = shipToInfo.ContactName;
            labelRequest.PartnerTransactionID = printLabel.FulfillmentNumber;
            labelRequest.FromName = printLabel.ShipFrom.ContactName;
            labelRequest.FromPhone = printLabel.ShipFrom.ContactPhone;
            //labelRequest.FromCompany = printLabel.ShipFrom.ContactName;
            labelRequest.FromCity = printLabel.ShipFrom.ShipToCity;// GetConfigurati;ons.HOST_CITY;
            labelRequest.FromPostalCode = printLabel.ShipFrom.ShipToZip;// GetConfigurations.HOST_POSTAL;
            labelRequest.FromState = printLabel.ShipFrom.ShipToState;// GetConfigurations.HOST_STATE;
            //labelRequest.FromEMail = printLabel.ShipFrom.co;// GetConfigurations.HOST_EMAIL;
            labelRequest.ReturnAddress1 = printLabel.ShipFrom.ShipToAddress; //GetConfigurations.HOST_ADDRESS_NAME;

            labelRequest.ShowReturnAddress = "True";// GetConfigurations.SHOW_RETURN_ADDRESS;
            //labelRequest.ContentsType = "Merchandise"; //GetConfigurations.CONTENTS_TYPE;
            labelRequest.Description = printLabel.PackageContent;

            labelRequest.ToAddress1 = shipToInfo.ShipToAddress;
            labelRequest.ToAddress2 = shipToInfo.ShipToAddress2;

            labelRequest.ToCity = shipToInfo.ShipToCity;
            labelRequest.ToState = shipToInfo.ShipToState;
            // labelRequest.ToCompany = shipToInfo.ContactName;
            labelRequest.ToCountry = shipToInfo.ShipToCountry;
            labelRequest.ToName = shipToInfo.ContactName;
            labelRequest.ToPostalCode = shipToInfo.ShipToZip;


            labelRequest.ReferenceID = printLabel.FulfillmentNumber;
            labelRequest.RubberStamp1 = printLabel.FulfillmentNumber;
            if (printLabel.ShippingMethod.ToString() == "PriorityExpress")
            { }
            else
                labelRequest.ShipDate = printLabel.LabelPrintDateTime.ToString("yyyy-MM-dd");

            return labelRequest;
        }

        private LabelRequest populateEndiciaRequestOld(iPrintLabel printLabel)
        {
            CustomsInfo customsInfo = printLabel.CustomsInfo;
            ShipInfo shipToInfo = printLabel.ShipTo;
            LabelRequest labelRequest = new LabelRequest();
            labelRequest.ImageFormat = "JPEGMONOCHROME";
            //labelRequest.Test = "False";
            labelRequest.ValidateAddress = "TRUE";

            labelRequest.LabelType = printLabel.LabelType; //"Default";//DestinationConfirm

            if (printLabel.PackageShape.ToString().ToLower() == "letter" || printLabel.PackageShape.ToString().ToLower() == "card" || printLabel.PackageShape.ToString().ToLower() == "flat")
            {
                labelRequest.LabelType = "DestinationConfirm";//DestinationConfirm
                labelRequest.LabelSize = "6X4"; }
            else
                labelRequest.LabelSize = "4x6";

            labelRequest.ImageResolution = "300";

            labelRequest.MailClass = printLabel.ShippingMethod.ToString(); // "Priority";
            labelRequest.MailpieceShape = printLabel.PackageShape.ToString(); //"FlatRateEnvelope";


            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), shipToInfo.ShipToState))
            {
                //labelRequest.SignatureWaiver = "TRUE";
                labelRequest.LabelType = "International"; //"Default";//DestinationConfirm
                labelRequest.LabelSubtype = "Integrated"; //"Default";//DestinationConfirm
                labelRequest.DateAdvance = 1; //"Default";//DestinationConfirm
               // labelRequest.MailClass = "FirstClassMailInternational"; // "Priority";
                //labelRequest.MailpieceShape = "Flat"; //"FlatRateEnvelope";             
                
                // labelRequest.ContentsType = labelRequest.ReferenceID; //"FlatRateEnvelope";
                labelRequest.CustomsInfo = customsInfo;
                labelRequest.ToCountryCode = "CA";
            }

            labelRequest.AccountID = printLabel.AccountID;//"2553271"; // printLabel.AccountID;// "1353742"; //"2553271";// GetConfigurations.ACCOUNT_ID;
            labelRequest.RequesterID = printLabel.RequesterID;//"lxxx";// printLabel.RequesterID;// "Lang";// lxxx";// GetConfigurations.REQUESTER_ID;
            labelRequest.PassPhrase = printLabel.PassPharase;// langlobal@2020"; //printLabel.PassPharase;// "12031Lan1!";// "theskyisred";// GetConfigurations.PASS_PHRASE;
            labelRequest.WeightOz = printLabel.PackageWeight.value;
            labelRequest.PartnerCustomerID = shipToInfo.ContactName;
            labelRequest.PartnerTransactionID = printLabel.FulfillmentNumber;
            labelRequest.FromName = printLabel.ShipFrom.ContactName;
            labelRequest.FromPhone = printLabel.ShipFrom.ContactPhone;
            //labelRequest.FromCompany = printLabel.ShipFrom.ContactName;
            labelRequest.FromCity = printLabel.ShipFrom.ShipToCity;// GetConfigurati;ons.HOST_CITY;
            labelRequest.FromPostalCode = printLabel.ShipFrom.ShipToZip;// GetConfigurations.HOST_POSTAL;
            labelRequest.FromState = printLabel.ShipFrom.ShipToState;// GetConfigurations.HOST_STATE;
            //labelRequest.FromEMail = printLabel.ShipFrom.co;// GetConfigurations.HOST_EMAIL;
            labelRequest.ReturnAddress1 = printLabel.ShipFrom.ShipToAddress; //GetConfigurations.HOST_ADDRESS_NAME;

            labelRequest.ShowReturnAddress = "True";// GetConfigurations.SHOW_RETURN_ADDRESS;
            //labelRequest.ContentsType = "Merchandise"; //GetConfigurations.CONTENTS_TYPE;
            labelRequest.Description = printLabel.PackageContent;

            labelRequest.ToAddress1 = shipToInfo.ShipToAddress;
            labelRequest.ToAddress2 = shipToInfo.ShipToAddress2;

            labelRequest.ToCity = shipToInfo.ShipToCity;
            labelRequest.ToState = shipToInfo.ShipToState;
           // labelRequest.ToCompany = shipToInfo.ContactName;
            labelRequest.ToCountry = shipToInfo.ShipToCountry;
            labelRequest.ToName = shipToInfo.ContactName;
            labelRequest.ToPostalCode = shipToInfo.ShipToZip;

            
            labelRequest.ReferenceID = printLabel.FulfillmentNumber;
            labelRequest.RubberStamp1 = printLabel.FulfillmentNumber;
            if (printLabel.ShippingMethod.ToString() == "PriorityExpress")
            { }
             else
                labelRequest.ShipDate = printLabel.LabelPrintDateTime.ToString("yyyy-MM-dd");

            return labelRequest;
        }

        private ShipStation populateShipStationRequest(iPrintLabel printLabel)
        {
            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.USStates), printLabel.ShipTo.ShipToState))
            {
                printLabel.ShipTo.ShipToCountry = "US";
            }
            else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), printLabel.ShipTo.ShipToState))
            {
                printLabel.ShipTo.ShipToCountry = "Canada";
            }

            ShipStation labelRequest = new ShipStation()
            {

                carrierCode = "fedex",
                confimation = "delivery",

            //    packageCode = ShipStationPackageShape.package,
             //   serviceCode = ShipStationsShipMethods.fedex_ground,
                packageCode = (ShipStationPackageShape)Enum.Parse(typeof(ShipStationPackageShape), printLabel.PackageShape.ToString()),
                serviceCode = (ShipStationsShipMethods)Enum.Parse(typeof(ShipStationsShipMethods), printLabel.ShippingMethod.ToString()),
                shipDate = printLabel.LabelPrintDateTime.ToString("yyyy-MM-dd"),//DateTime.Today.ToString("yyyy-MM-dd"),
                shipFrom = new ShipStationAddress()
                {
                    addressVerified = "true",
                    city = printLabel.ShipFrom.ShipToCity,
                    company = printLabel.ShipFrom.ContactName,
                    country = "US",
                    name = printLabel.ShipFrom.ContactName,
                    postalCode = printLabel.ShipFrom.ShipToZip,
                    street1 = printLabel.ShipFrom.ShipToAddress,
                    state = printLabel.ShipFrom.ShipToState,
                    phone = printLabel.ShipFrom.ContactPhone

                },
                shipTo = new ShipStationAddress()
                {

                    addressVerified = "true",
                    city = printLabel.ShipTo.ShipToCity,
                    country = printLabel.ShipTo.ShipToCountry,
                    name = printLabel.ShipTo.ContactName,
                    postalCode = printLabel.ShipTo.ShipToZip,
                    street1 = printLabel.ShipTo.ShipToAddress,
                    street2 = printLabel.ShipTo.ShipToAddress2,
                    state = printLabel.ShipTo.ShipToState,
                    //company = "abc",
                    phone = printLabel.ShipTo.ContactPhone
                },
                

                testLabel = false,
                weight = new Weight()
                {
                    units = printLabel.PackageWeight.units,
                    value = printLabel.PackageWeight.value
                }
                };

            return labelRequest;
        }
    }

}
