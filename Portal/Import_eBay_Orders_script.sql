GO
USE [landb_10102021]
GO

/****** Object:  Table [dbo].[sveBayOrder]    Script Date: 13-10-2021 10:32:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sveBayOrder](
	[eBayOrderID] [int] IDENTITY(1,1) NOT NULL,
	[orderFulfillmentStatus] [varchar](20) NULL,
	[fulfillmentInstructionsType] [varchar](20) NULL,
	[minEstimatedDeliveryDate] [datetime] NULL,
	[maxEstimatedDeliveryDate] [datetime] NULL,
	[ebaySupportedFulfillment] [bit] NULL,
	[shippingCarrierCode] [varchar](50) NULL,
	[shippingServiceCode] [varchar](50) NULL,
	[shipTophoneNumber] [varchar](15) NULL,
	[shipTofullName] [varchar](100) NULL,
	[shipTostateOrProvince] [varchar](50) NULL,
	[shipTocity] [varchar](50) NULL,
	[shipTocountryCode] [varchar](50) NULL,
	[shipTopostalCode] [varchar](50) NULL,
	[shipToaddressLine1] [varchar](200) NULL,
	[shipToemail] [varchar](100) NULL,
	[orderId] [varchar](20) NULL,
	[lastModifiedDate] [datetime] NULL,
	[creationDate] [datetime] NULL,
	[CreateLogID] [int] NULL,
	[UpdateLogID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[eBayOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

CREATE TABLE [dbo].[sveBayOrderLineItems](
	[eBayOrderLineItemID] [int] IDENTITY(1,1) NOT NULL,
	[eBayOrderID] [int] NULL,
	[Quantity] [int] NULL,
	[lineItemId] [varchar](20) NULL,
	[lineItemFulfillmentStatus] [varchar](20) NULL,
	[sku] [varchar](100) NULL,
	[Title] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[eBayOrderLineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

CREATE TABLE [dbo].[sveBayOrderRefreshLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NULL,
	[LogData] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE PROC [dbo].[sveBayOrderInsertUpdate]
@LogID INT,
@UserID INT,
@sveBayOrderType dbo.sveBayOrderType READONLY,
@sveBayOrderLineItemsType dbo.sveBayOrderLineItemsType READONLY
AS
BEGIN
DECLARE @UpdateCount INT, @InsertCount INT, @CustomerUserID INT
SET @CustomerUserID = 1537
BEGIN TRAN T1	
	UPDATE T1 
	SET orderFulfillmentStatus = T2.orderFulfillmentStatus,
	-- fulfillmentInstructionsType = T2.fulfillmentInstructionsType, 
	--minEstimatedDeliveryDate = T2.minEstimatedDeliveryDate, maxEstimatedDeliveryDate = T2.maxEstimatedDeliveryDate, 
	--ebaySupportedFulfillment = T2.ebaySupportedFulfillment, shippingCarrierCode = T2.shippingCarrierCode, shippingServiceCode = T2.shippingServiceCode, 
	--shipTophoneNumber = T2.shipTophoneNumber, shipTofullName = T2.shipTofullName, shipTostateOrProvince = T2.shipTostateOrProvince, 
	--shipTocity = T2.shipTocity, shipTocountryCode = T2.shipTocountryCode, shipTopostalCode = T2.shipTopostalCode, shipToaddressLine1 = T2.shipToaddressLine1, 
	--shipToemail = T2.shipToemail, 
	lastModifiedDate = T2.lastModifiedDate, UpdateLogID = @LogID
	FROM sveBayOrder T1
	JOIN @sveBayOrderType T2 ON T2.orderId = T1.orderId 
	--SELECT @UpdateCount = @@ROWCOUNT

	INSERT INTO sveBayOrder(orderFulfillmentStatus, fulfillmentInstructionsType, minEstimatedDeliveryDate, maxEstimatedDeliveryDate, ebaySupportedFulfillment,
	shippingCarrierCode, shippingServiceCode, shipTophoneNumber, shipTofullName, shipTostateOrProvince, shipTocity, shipTocountryCode, shipTopostalCode,
	shipToaddressLine1, shipToemail, orderId, lastModifiedDate, creationDate, CreateLogID)
	SELECT T1.orderFulfillmentStatus, T1.fulfillmentInstructionsType, T1.minEstimatedDeliveryDate, T1.maxEstimatedDeliveryDate, T1.ebaySupportedFulfillment,
	T1.shippingCarrierCode, T1.shippingServiceCode, T1.shipTophoneNumber, T1.shipTofullName, T1.shipTostateOrProvince, T1.shipTocity, T1.shipTocountryCode, 
	T1.shipTopostalCode, T1.shipToaddressLine1, T1.shipToemail, T1.orderId, T1.lastModifiedDate, T1.creationDate, @LogID
	FROM @sveBayOrderType T1
	LEFT JOIN sveBayOrder T2 ON T2.orderId = T1.orderId
	WHERE T2.orderId IS NULL
	
	UPDATE T1 
	SET 
	--Quantity = T2.Quantity, 
	lineItemFulfillmentStatus = T2.lineItemFulfillmentStatus
	--, SKU = T2.sku
	FROM sveBayOrderLineItems T1
	JOIN @sveBayOrderLineItemsType T2 ON  T2.lineItemId = T1.lineItemId

	
	INSERT INTO sveBayOrderLineItems(eBayOrderID, Quantity, lineItemId, lineItemFulfillmentStatus, sku, title)
	SELECT T2.eBayOrderID, T1.Quantity, T1.lineItemId, T1.lineItemFulfillmentStatus, T1.sku, T1.title 
	FROM @sveBayOrderLineItemsType T1 
	JOIN sveBayOrder T2 ON T2.orderId = T1.orderId
	LEFT JOIN sveBayOrderLineItems T3 ON T3.lineItemId = T1.lineItemId
	WHERE T3.lineItemId IS NULL

	
	INSERT INTO dbo.PurchaseOrder (PO_Num, Store_ID, PO_Date, Contact_Name, contact_Phone, Ship_Via, StatusID, 
       comments, UserID,ShipTo_Attn,ShipTo_Address, ShipTo_Address2,ShipTo_City ,ShipTo_State,ShipTo_Zip,customernumber, CreateDate, POFlag, 
	   requestedshipdate, CustomerOrderNumber, B2BStoreID, IsShipmentRequired, POType, IsInternational)  
    SELECT T1.orderId, '', T1.creationDate, T1.shipTofullName, T1.shipTophoneNumber, T1.shippingServiceCode, 1,
	'eBay Order', @CustomerUserID, T1.shipTofullName, T1.shipToaddressLine1, null, T1.shipTocity, T1.shipTostateOrProvince, T1.shipTopostalCode, null, GETDATE(), 'E',
	T1.minEstimatedDeliveryDate, T1.orderId, '', 0, 'B2C', CASE WHEN T1.shipTocountryCode = 'US' THEN 0 ELSE 1 END --T3.GeoCountryID
	--T1.orderFulfillmentStatus, T1.fulfillmentInstructionsType, 	T1.minEstimatedDeliveryDate, T1.maxEstimatedDeliveryDate, T1.ebaySupportedFulfillment, 	T1.shippingCarrierCode, T1.shipTostateOrProvince, T1.shipTocountryCode,  	 T1.shipToemail, T1.lastModifiedDate, @LogID
	FROM @sveBayOrderType T1
	LEFT JOIN PurchaseOrder T2 ON T2.CustomerOrderNumber = T1.orderId
	--LEFT JOIN av_GeoState T3 ON T3.statename = T1.shipTostateOrProvince
	WHERE T2.CustomerOrderNumber IS NULL
	SELECT @InsertCount = @@ROWCOUNT

	INSERT INTO dbo.PurchaseOrder_Detail (PO_ID, Line_No, Item_Code, Qty, StatusID, ItemCompanyGUID)  
    SELECT T2.PO_ID, 1, T1.lineItemId, T1.Quantity, 1, T3.ItemCompanyGUID -- T1.title 
	FROM @sveBayOrderLineItemsType T1 
	JOIN PurchaseOrder T2 ON T2.CustomerOrderNumber = T1.orderId
	LEFT JOIN PurchaseOrder_Detail T3 ON T3.PO_ID = T2.PO_ID
	LEFT JOIN av_ItemCompanyAssign T4 ON T4.SKU = T1.lineItemId
	WHERE T3.ItemCompanyGUID IS NULL
  
	--DELETE T1 
	--FROM sveBayOrderLineItems T1
	--LEFT JOIN @sveBayOrderLineItemsType T2 ON  T2.lineItemId = T1.lineItemId
	--WHERE T2.lineItemId IS NULL

	SELECT (ISNULL(@InsertCount,0) + ISNULL(@UpdateCount,0))

COMMIT TRAN T1

END
GO

CREATE PROC [dbo].[sveBayOrderValidate]
@sveBayOrderType dbo.sveBayOrderType READONLY,
@sveBayOrderLineItemsType dbo.sveBayOrderLineItemsType READONLY
AS
BEGIN
DECLARE @UpdateCount INT, @InsertCount INT
	
	--1. Validate SKU
	--2. Valiate Product
	--3. 
	DECLARE @tmp TABLE
	(
		orderId VARCHAR(20),
		lineItemId  VARCHAR(20),
		ErrorMessage VARCHAR(100)
	)

	INSERT INTO @tmp(orderId, lineItemId, ErrorMessage)
	SELECT orderId, t1.lineItemId, 
	CASE WHEN T2.SKU IS NULL AND T3.ItemName IS NULL THEN T1.lineItemId + ' SKU and product not exists in our system' 
	     WHEN T2.SKU IS NULL THEN T1.lineItemId + ' SKU not exists in our system' 
		 WHEN T3.ItemName IS NULL THEN T1.title + ' product not exists in our system' ELSE '' END AS ErrorMessage
	FROM @sveBayOrderLineItemsType T1
	LEFT JOIN av_ItemcompanyAssign T2 ON T2.SKU = T1.lineItemId
	LEFT JOIN av_Item T3 ON T3.ItemName = T1.title
	

	SELECT DISTINCT  T1.*, T2.ErrorMessage
	FROM @sveBayOrderType T1 
	JOIN @tmp T2 ON T2.orderId = T1.orderId 
	--GROUP BY T1.orderId 


	
	SELECT T1.*, T2.ErrorMessage
	FROM @sveBayOrderLineItemsType T1 
	JOIN @tmp T2 ON T2.orderId = T1.orderId  AND T2.lineItemId = T1.lineItemId

	
END

GO

CREATE PROC [dbo].[sveBayOrderRefreshLogInsert]
@CreatedBy INT,
@LogData NVARCHAR(MAX)
AS
BEGIN

	INSERT INTO sveBayOrderRefreshLog(CreatedBy, CreateDate, LogData)
	VALUES(@CreatedBy, GETDATE(), @LogData)
	SELECT SCOPE_IDENTITY()
END