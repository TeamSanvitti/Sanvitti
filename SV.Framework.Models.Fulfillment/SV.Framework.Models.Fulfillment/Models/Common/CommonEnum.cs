using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Common
{
    [Serializable]
    public enum RMAStatus
    {
        None = 0,
        Pending = 1,
        Received = 2,
        PendingForRepair = 3,
        PendingForCredit = 4,
        PendingForReplacement = 5,
        Approved = 6,
        Returned = 7,
        Credited = 8,
        Denied = 9,
        Closed = 10,
        OutWithOEMForRepair = 11,
        BackToStockNDF = 12,
        BackToStockCredited = 13,
        BackToStockReplacedByOEM = 14,
        RepairedByOEM = 15,
        ReplacedBYOEM = 16,
        ReplacedByAV = 17,
        RepairedByAV = 18,
        NDFNoDefectFound = 19,
        PreOwnedAstock = 20,
        PreOwnedBtock = 21,
        PreOwnedCtock = 22,
        Rejected = 23,
        RTSReturnToStock = 24,
        Incomplete = 25,
        Damaged = 26,
        Preowned = 27,
        ReturnToOEM = 28,
        ReturnedToStock = 29,
        Cancel = 30,
        ExternalESN = 31,
        PendingShipToOEM = 32,
        SentToOEM = 33,
        PendingShipToSupplier = 34,
        SentToSupplier = 35,
        ReturnedFromOEM = 36,
        ShippingError = 37,
        ReplacedByOEMNew = 38,
        ReplacedByOEMPreowned = 39
    }

    //[Serializable]
    //public enum ResponseErrorCode
    //{
    //    None,
    //    MissingParameter,
    //    UploadedSuccessfully,
    //    SuccessfullyRetrieved,
    //    InternalError,
    //    InconsistantData,
    //    ErrowWhileLoadingData,
    //    PurchaseOrderNotShipped,
    //    PurchaseOrderShipped,
    //    CannotAuthenticateUser,
    //    PurchaseOrderItemNotAssigned,
    //    PurchaseOrderNotExists,
    //    FulfillmentOrderNotExists,
    //    PurchaseOrderAlreadyExists,
    //    ShipByIsNotCorrect,
    //    NoRecordsFound,
    //    NoItemFound,
    //    RMANotExists,
    //    UpdatedSuccessfully,
    //    QuantityIsNotCorrect,
    //    PurchaseOrderCannotBeCancelled,
    //    RMACannotBeCancelled,
    //    StateCodeIsNotCorrect,
    //    FulfillmentOrderAlreadyProcessed,
    //    AccountNumberNotExists,
    //    DataNotUpdated,
    //    DuplicateItemFound,
    //    StroreIDNotExists,
    //    SubmittedSuccessfully,
    //    CancelledSuccessfully
    //}

}
