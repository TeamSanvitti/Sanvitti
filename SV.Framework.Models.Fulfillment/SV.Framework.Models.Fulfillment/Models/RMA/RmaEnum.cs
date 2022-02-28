using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    [Serializable]
    public enum RMAReason
    {

        None = 0,
        DOA = 1,
        AudioIssues = 2,
        ScreenIssues = 3,
        PowerIssues = 4,
        Others = 5,
        MissingParts = 6,
        ReturnToStock = 7,
        BuyerRemorse = 8,
        PhysicalAbuse = 9,
        LiquidDamage = 10,
        DropCalls = 11,
        Software = 12,
        ActivationIssues = 13,
        CoverageIssues = 14,
        LoanerProgram = 15,
        ShippingError = 16,
        HardwareIssues = 17
    }

    //public class RmaEnum
    //{
    [Serializable]
        public enum Warranty
        {
            None = 0,
            Warranty = 1,
            OutOfWarranty = 2
        }

        [Serializable]
        public enum Disposition
        {
            None = 0,
            Credit = 1,
            Replaced = 2,
            Repair = 3,
            Discarded = 4
        }

    //}
}
