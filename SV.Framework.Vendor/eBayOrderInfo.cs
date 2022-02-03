using System;
using System.Collections.Generic;
using System.Text;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.Web.Script.Serialization;

namespace SV.Framework.Vendor
{
    public class eBayOrderRequest
    {
        public string dateFrom { get; set; }
        public string dateTo{ get; set; }
        public string orderfulfillmentstatus { get; set; }        
        public int limit { get; set; }
        public int offset { get; set; }        
        public string authCode { get; set; }
    }
    public class eBayOrderInfo
    {
        public int total { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public List<eBayOrders> orders { get; set; }
        public string href { get; set; }
    }
    public class eBayOrders
    {
        public string orderFulfillmentStatus { get; set; }
        public List<FulfillmentStartInstructions> fulfillmentStartInstructions { get; set; }
        public string orderId { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public TotalMarketplaceFee totalMarketplaceFee { get; set; }
       // [JsonIgnore]
       // public CancelStatus cancelStatus { get; set; }
        public int salesRecordReference { get; set; }
        public string buyerCheckoutNotes { get; set; }
        public TotalMarketplaceFee totalFeeBasisAmount { get; set; }
        public string creationDate { get; set; }
        public PaymentSummary paymentSummary { get; set; }
        public Buyer buyer { get; set; }
        public string[] fulfillmentHrefs { get; set; }
        public List<LineItems> lineItems { get; set; }
        public string orderPaymentStatus { get; set; }
        public string sellerId { get; set; }
        public bool ebayCollectAndRemitTax { get; set; }
        public string legacyOrderId { get; set; }

    }
    public class FulfillmentStartInstructions
    {
        public string fulfillmentInstructionsType { get; set; }
        public DateTime minEstimatedDeliveryDate { get; set; }
        public DateTime maxEstimatedDeliveryDate { get; set; }
        public bool ebaySupportedFulfillment { get; set; }
        public ShippingStep shippingStep { get; set; }
    }
    public class ShippingStep
    {
        public string shippingCarrierCode { get; set; }
        public string shippingServiceCode { get; set; }
        public ShipTo shipTo { get; set; }
    }
    public class ShipTo
    {
        public PrimaryPhone primaryPhone { get; set; }
        public string fullName { get; set; }
        public ContactAddress contactAddress { get; set; }
        public string email { get; set; }
    }
    public class PrimaryPhone
    {
        public string phoneNumber { get; set; }
       // public string shippingServiceCode { get; set; }
    }
    public class ContactAddress
    {
        public string stateOrProvince { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string postalCode { get; set; }
        public string addressLine1 { get; set; }
    }

    public class TotalMarketplaceFee
    {
        public string currency { get; set; }
        public decimal value { get; set; }
    }
    public class CancelStatus
    {
        
        public string[] cancelRequests { get; set; }
        public string cancelState { get; set; }
    }
    public class Payment
    {
        public TotalMarketplaceFee amount { get; set; }
        public string paymentMethod { get; set; }
        public string paymentReferenceId { get; set; }
        public string paymentStatus { get; set; }
    }
    public class PaymentSummary
    {
        public List<Payment> payments { get; set; }
        public TotalMarketplaceFee totalDueSeller { get; set; }
        //[JsonIgnore]
        //public string[] refunds { get; set; }
        //public Refunds refunds { get; set; }
        
    }
    public class Refunds
    {
        public DateTime refundDate { get; set; }
        public TotalMarketplaceFee amount { get; set; }
        public string refundStatus { get; set; }
        public string refundReferenceId { get; set; }
    }
    public class PricingSummary
    {
       
        public TotalMarketplaceFee total { get; set; }
        public TotalMarketplaceFee priceSubtotal { get; set; }
        public TotalMarketplaceFee deliveryCost { get; set; }
       

    }
    public class Buyer
    {
        public TaxAddress taxAddress { get; set; }
        public string username { get; set; }        
    }
    public class TaxAddress
    {
        public string stateOrProvince { get; set; }
        public string countryCode { get; set; }
        public string postalCode { get; set; }
    }

    public class LineItems
    {
        public int quantity { get; set; }
        public Int64 lineItemId { get; set; }
        public Int64 legacyItemId { get; set; }
        //[JsonIgnore]
       // public string[] taxes { get; set; }
        public string lineItemFulfillmentStatus { get; set; }
        public string title { get; set; }
        public ItemLocation itemLocation { get; set; }
        public LineItemFulfillmentInstructions lineItemFulfillmentInstructions { get; set; }
        public TotalMarketplaceFee total { get; set; }
        
        public Int64 legacyVariationId { get; set; }
        public string listingMarketplaceId { get; set; }
        public List<AppliedPromotions> appliedPromotions { get; set; }
        public string soldFormat { get; set; }
        public List<EbayCollectAndRemitTax> ebayCollectAndRemitTaxes { get; set; }
        public TotalMarketplaceFee lineItemCost { get; set; }
        public string sku { get; set; }
        public Properties properties { get; set; }
        public string purchaseMarketplaceId { get; set; }
        public DeliveryCost  deliveryCost { get; set; }


    }
    public class ItemLocation
    {
        public string countryCode { get; set; }
        public string postalCode { get; set; }
        public string location { get; set; }
    }
    public class LineItemFulfillmentInstructions
    {
        public DateTime minEstimatedDeliveryDate { get; set; }
        public bool guaranteedDelivery { get; set; }
        public DateTime maxEstimatedDeliveryDate { get; set; }
        public DateTime shipByDate { get; set; }
    }

    public class EbayCollectAndRemitTax
    {
        public string collectionMethod { get; set; }
        public TotalMarketplaceFee amount { get; set; }
        public string taxType { get; set; }
    }
    public class AppliedPromotions
    {
        public TotalMarketplaceFee discountAmount { get; set; }
        public string description { get; set; }
        
        public Int64 promotionId { get; set; }
    }
    
    public class Properties
    {
        public bool buyerProtection { get; set; }
    }
    public class DeliveryCost
    {
        public TotalMarketplaceFee shippingCost { get; set; }
    }
    
}
