using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class AppEnums
    {
       // [JsonConverter(typeof(StringEnumConverter))]
        public enum InternationalShipPackageType
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

       // [JsonConverter(typeof(StringEnumConverter))]
        public enum ShipPackageType
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

        public enum InternationalShipMethods
        {
            PriorityMailExpressInternational,
            FirstClassMailInternational,
            FirstClassPackageInternationalService,
            PriorityMailInternational
        }
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
        public static List<SelectListItem> GetShipMethods(string Carrier)
        {
            List<SelectListItem> ShipMethods = new List<SelectListItem>();
            // PackageType shipShapeInfo = null;
            if (Carrier.ToLower() == "fedex")
            {
                string[] itemNames = System.Enum.GetNames(typeof(ShipStationsShipMethods));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    var newItem = new SelectListItem { Text = itemNames[i], Value = itemNames[i] };
                    //list.Add(newItem);

                    ShipMethods.Add(newItem);
                }

            }
            else
            {
                string[] itemNames = System.Enum.GetNames(typeof(EndiciaShipMethods));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    var newItem = new SelectListItem { Text = itemNames[i], Value = itemNames[i] };

                    ShipMethods.Add(newItem);
                }
            }
            return ShipMethods;
        }
        public static List<SelectListItem> GetPackageTypes(bool IsInternational)
        {
            List<SelectListItem> PackageTypes = new List<SelectListItem>();
           // PackageType shipShapeInfo = null;
            if (IsInternational)
            {
                string[] itemNames = System.Enum.GetNames(typeof(InternationalShipPackageType));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    var newItem = new SelectListItem { Text = itemNames[i], Value = itemNames[i] };
                    //list.Add(newItem);

                    PackageTypes.Add(newItem);
                }

            }
            else
            {
                string[] itemNames = System.Enum.GetNames(typeof(ShipPackageType));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    var newItem = new SelectListItem { Text = itemNames[i], Value = itemNames[i] };

                    PackageTypes.Add(newItem);
                }
            }
            return PackageTypes;
        }
    }
}
