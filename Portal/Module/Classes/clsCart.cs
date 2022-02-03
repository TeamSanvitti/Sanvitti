using System;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsCart.
	/// </summary>
	public class clsCart
	{
		public clsCart()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		private static DataTable GetBlankShopCart()
		{
			DataTable dtShop;
			dtShop = dtShop = new DataTable();
			dtShop.Columns.Add("id");
			dtShop.Columns.Add("ItemID");
			dtShop.Columns.Add("ItemName");
			dtShop.Columns.Add("Qty");
			dtShop.Columns.Add("Price");
			dtShop.Columns.Add("Total");
			return dtShop;
		}

		public static DataTable Add(ref DataTable dtCart, string sItemID,string  sName,string  sQty,string  sPrice)
		{
			DataRow dRow;
			if (dtCart == null)
				dtCart = GetBlankShopCart();
			if (dtCart.Select("ItemName = '" + sName + "'").Length ==0)
			{
				dRow = dtCart.NewRow();
				dRow["ItemID"] = sItemID;
				dRow["ItemName"] = sName;
				dRow["Qty"] = sQty;
				dRow["Price"] = sPrice.Replace("$","");
				dRow["Total"] = (sQty !=null?Convert.ToUInt32(sQty):1) * (sPrice !=null?Convert.ToDouble(sPrice.Replace("$","")):1);
				dtCart.Rows.Add (dRow);
				dtCart.AcceptChanges();
				return dtCart;
			}
			else
				throw new Exception("Item already exists in order list, please go to order list to update the quantity of item");
		}




	}
}
