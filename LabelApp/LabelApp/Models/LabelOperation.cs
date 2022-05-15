using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class LabelOperation
    {
        //public readonly AppDbContext _db;
        //public LabelOperation(AppDbContext db)
        //{
        //    _db = db;
        //}
        //public static 
        public static svGeneralShipmentLabel GenerateLabel(svGeneralShipmentLabel svGeneralShipmentLabel)
        {
            //svGeneralShipmentLabel svGeneralShipmentLabel = new svGeneralShipmentLabel();
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            List<CustomValues> customValues = new List<CustomValues>();
            DateTime LabelPrintDateTime = DateTime.Today;

            labelInfo.LabelType = "Default";
            string shipmentType = "S";
            string shipFromCountry = "USA";


            labelInfo.FulfillmentNumber = svGeneralShipmentLabel.FromName;
            labelInfo.LabelPrintDateTime = LabelPrintDateTime;
            //shipToInfo
            shipToInfo.ShipToAddress = svGeneralShipmentLabel.ToAddress1;
            shipToInfo.ShipToAddress2 = svGeneralShipmentLabel.ToAddress2;
            shipToInfo.ContactName = svGeneralShipmentLabel.ToName;
            shipToInfo.ShipToCity = svGeneralShipmentLabel.ToCity;
            shipToInfo.ShipToState = svGeneralShipmentLabel.ToState;
            shipToInfo.ShipToZip = svGeneralShipmentLabel.ToZip;
            shipToInfo.ShipToAttn = svGeneralShipmentLabel.ToName;
            shipToInfo.ContactPhone = svGeneralShipmentLabel.ToPhone;
            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.USStates), shipToInfo.ShipToState))
            {
                shipToInfo.ShipToCountry = "USA";
            }
            else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), shipToInfo.ShipToState))
            {
                // List<avii.Classes.BasePurchaseOrderItem> purchaseOrderItemList = Session["purchaseOrderItemList"] as List<avii.Classes.BasePurchaseOrderItem>;

                //SV.Framework.Common.LabelGenerator.CustomsInfo customsInfo = new SV.Framework.Common.LabelGenerator.CustomsInfo();
                //List<SV.Framework.Common.LabelGenerator.CustomsItem> customsItems = new List<SV.Framework.Common.LabelGenerator.CustomsItem>();
                //if(purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                {
                  //  customsInfo.ContentsType = "Documents";
                   // labelInfo.LabelType = "International";

                    //foreach (RepeaterItem item in rptCustom.Items)
                    //{
                    //    Label lbProductName = item.FindControl("lbName") as Label;
                    //    HiddenField hdQty = item.FindControl("hdQty") as HiddenField;
                    //    HiddenField hdPODID = item.FindControl("hdPODID") as HiddenField;
                    //    TextBox txtValue = item.FindControl("txtValue") as TextBox;

                    //    if (string.IsNullOrEmpty(txtValue.Text.Trim()))
                    //    {
                    //        lblShipItem.Text = "Custom value is required!";
                    //        return returnResult;
                    //    }
                    //    SV.Framework.Common.LabelGenerator.CustomsItem customsItem = new SV.Framework.Common.LabelGenerator.CustomsItem();
                    //    CustomValues customValue1 = new CustomValues();

                    //    customsItem.Description = lbProductName.Text;
                    //    customsItem.Quantity = Convert.ToInt32(hdQty.Value);
                    //    customsItem.Weight = Convert.ToDecimal(weight);
                    //    customsItem.Value = Convert.ToDecimal(txtValue.Text);

                    //    customsItems.Add(customsItem);

                    //    customValue1.CustomValue = customsItem.Value;
                    //    customValue1.POD_ID = Convert.ToInt32(hdPODID.Value);
                    //    customValues.Add(customValue1);
                    //}
                    
                   // customsInfo.CustomsItems = customsItems;
                }
               // labelInfo.CustomsInfo = customsInfo;
                //shipToInfo.ShipToCountry = "CANADA";
            }
            labelInfo.ShipTo = shipToInfo;

            //ship From Info
            shipFromInfo.ShipToAddress = svGeneralShipmentLabel.FromAddress1;
            shipFromInfo.ShipToAddress2 = svGeneralShipmentLabel.FromAddress2;
            shipFromInfo.ContactName = svGeneralShipmentLabel.FromName;
            shipFromInfo.ShipToCity = svGeneralShipmentLabel.FromCity;
            shipFromInfo.ShipToState = svGeneralShipmentLabel.FromState;
            shipFromInfo.ShipToZip = svGeneralShipmentLabel.FromZip;
            shipFromInfo.ShipToAttn = svGeneralShipmentLabel.FromName;
            shipFromInfo.ShipToCountry = shipFromCountry;
            shipFromInfo.ContactPhone = svGeneralShipmentLabel.FromPhone;

            labelInfo.ShipFrom = shipFromInfo;
            //
            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = Convert.ToDouble(svGeneralShipmentLabel.ShipmentWeight) };
            labelInfo.PackageContent = "Description";

            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
            // if (ddlShipVia.SelectedIndex > 0)
            {
                Enum.TryParse(svGeneralShipmentLabel.ShipVia, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                labelInfo.ShippingMethod = shipMethods;
            }
            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;

            if (!string.IsNullOrEmpty(svGeneralShipmentLabel.ShipPackage))
            {
                Enum.TryParse(svGeneralShipmentLabel.ShipPackage, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                labelInfo.PackageShape = shipPackage;
            }
            else
                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter;
            string Endicia = "AccountID=1353742,RequesterID=Lang,PassPhrase=12031Lan1@"; //12031Lan1@
            string shipStationApiUrl = "", shipStationAuthString = "";
            shipStationApiUrl = "https://ssapi.shipstation.com/";
            shipStationAuthString = "Basic M2NhOWRhM2ViZGIyNDM3MTgyNmJiY2FjMGY3YjMzY2Q6MjAzNDYyZTI0Y2EzNDAyYmFmNGE4ZmFlYmM1YTZkMTE=";

            APIAddressInfo aPIAddressInfo = new APIAddressInfo();
            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), labelInfo.ShippingMethod.ToString()))
            {
                aPIAddressInfo.ConnectionString = Endicia;
                aPIAddressInfo.APIAddress = "";
                SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials request2 = new SV.Framework.Common.LabelGenerator.EndiciaCredentials();
                if (aPIAddressInfo != null)
                {
                    request2.ConnectionString = aPIAddressInfo.ConnectionString;

                    SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials iEndiciaLabelCredentials = shipAccess.GetIEndiciaLabelCredentials(request2);
                    labelInfo.AccountID = iEndiciaLabelCredentials.AccountID;
                    labelInfo.RequesterID = iEndiciaLabelCredentials.RequesterID;
                    labelInfo.PassPharase = iEndiciaLabelCredentials.PassPharase;
                    labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                }
                
            }
            else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.ShipStationsShipMethods), labelInfo.ShippingMethod.ToString()))
            {
                aPIAddressInfo.APIAddress = shipStationApiUrl;
                aPIAddressInfo.ConnectionString = shipStationAuthString;
                if (aPIAddressInfo != null)
                {
                    labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                    labelInfo.AuthString = aPIAddressInfo.ConnectionString;
                }
                
            }
            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
            {
                svGeneralShipmentLabel.ShippingLabel = response.ShippingLabelImage;
                svGeneralShipmentLabel.TrackingNumber = response.TrackingNumber;
                svGeneralShipmentLabel.FinalPostage = response.FinalPostage;

                //response.
                //request.LineItems = listitems;
                //ShippingLabelResponse setResponse = avii.Classes.ShippingLabelOperation.ShippingLabelUpdateNew(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking, poid, customValues);
            }
            else
            {
                svGeneralShipmentLabel.FromAddress2 = response.PackageContent;
                if (!string.IsNullOrWhiteSpace(response.PackageContent))
                {
                    //{ lblShipItem.Text = response.PackageContent;
                //else
                //        lblShipItem.Text = "Technical error please try again!";
                }
            }

            return svGeneralShipmentLabel;
        }

        public static  List<SelectListItem> GetShipMethods(IEnumerable<svShipBy> iList)
        {

            List<SelectListItem> ShipMethods = new List<SelectListItem>();
            foreach(var item in iList)
            {
                var newItem = new SelectListItem { Text = item.ShipByText, Value = item.ShipByCode };
                
                ShipMethods.Add(newItem);
            }
            
            return ShipMethods;
        }


    }
}
