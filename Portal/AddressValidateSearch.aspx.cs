using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class AddressValidateSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        private void POLoad()
        {
            int companyID = 0, statusID = 0;
            string fulfillmentNumber = txtPoNum.Text.Trim();

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;

                }
            }
            if(ddlStatus.SelectedIndex > 0)
            {
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            Session["poaddlist"] = null;
            List<SV.Framework.Fulfillment.FulfillmentModel> poList = SV.Framework.Fulfillment.ValidateAddressOperation.GetFulfillment(companyID, statusID, fulfillmentNumber);
            if(poList != null && poList.Count > 0)
            {
                gvPO.DataSource = poList;
                gvPO.DataBind();
                Session["poaddlist"] = poList;
            }
            else
            {
                lblMsg.Text = "No record found";
                gvPO.DataSource = null;
                gvPO.DataBind();

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            POLoad();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["poaddlist"] = null;
            gvPO.DataSource = null;
            gvPO.DataBind();

            lblMsg.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            if(Session["adm"] != null)
                dpCompany.SelectedIndex = 0;
        }

        protected void lnkSuggestion_Command(object sender, CommandEventArgs e)
        {
            int poid = Convert.ToInt32(e.CommandArgument);
            List<SV.Framework.Fulfillment.CandidateAddress> Suggestions = SV.Framework.Fulfillment.ValidateAddressOperation.GetSuggestions(poid);
            if (Suggestions != null && Suggestions.Count > 0)
            {
                rptLog.DataSource = Suggestions;
                rptLog.DataBind();
            }
            else
            {
                rptLog.DataSource = null;
                rptLog.DataBind();
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }

        protected void lnkValidate_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.LabelGenerator.ValidateAddress validateAddress = new SV.Framework.LabelGenerator.ValidateAddress();
            SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials request2 = new SV.Framework.Common.LabelGenerator.EndiciaCredentials();
            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.LabelGenerator.AddressValidationOperation addressAccess = new SV.Framework.LabelGenerator.AddressValidationOperation();
            SV.Framework.LabelGenerator.ValidateAddressResponse response = new SV.Framework.LabelGenerator.ValidateAddressResponse();

            int poid = Convert.ToInt32(e.CommandArgument);
            if(poid > 0)
            {
                if (Session["poaddlist"] != null)
                {
                    List<SV.Framework.Fulfillment.FulfillmentModel> poList = Session["poaddlist"] as List<SV.Framework.Fulfillment.FulfillmentModel>;
                    var lgList = (from item in poList where item.POID.Equals(poid) select item).ToList();


                    if (lgList != null && lgList.Count > 0)
                    {
                        request2.ConnectionString = lgList[0].ConnectionString;
                        SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials iEndiciaLabelCredentials = shipAccess.GetIEndiciaLabelCredentials(request2);

                        validateAddress.AccountID = iEndiciaLabelCredentials.AccountID;
                        validateAddress.RequesterID = iEndiciaLabelCredentials.RequesterID;
                        validateAddress.PassPharase = iEndiciaLabelCredentials.PassPharase;
                        validateAddress.Address1 = lgList[0].Address1;
                        validateAddress.Address2 = lgList[0].Address2;
                        validateAddress.City = lgList[0].City;
                        validateAddress.Company = lgList[0].CompanyName;
                        validateAddress.Name = lgList[0].ContactName;
                        validateAddress.State = lgList[0].State;
                        validateAddress.PostalCode = lgList[0].Zip;
                        validateAddress.CountryCode = "US";

                        response = addressAccess.ValidateAddress(validateAddress);
                        if(response != null && response.Address != null)
                        {
                            SV.Framework.Fulfillment.ValidateAddressModel request = new SV.Framework.Fulfillment.ValidateAddressModel();
                            List<SV.Framework.Common.LabelGenerator.Address> addresses = new List<SV.Framework.Common.LabelGenerator.Address>();
                            List<SV.Framework.Fulfillment.CandidateAddress> CandidateAddresses = new List<SV.Framework.Fulfillment.CandidateAddress>();
                            SV.Framework.Fulfillment.CandidateAddress candidateAddress = null;


                            request.Address1 = response.Address.Address1;
                            request.Address2 = response.Address.Address2;
                            request.City = response.Address.City;
                            request.State = response.Address.State;
                            request.CompanyName = response.Address.Company;
                            request.PostalCode = response.Address.PostalCode;
                            request.Zip4 = response.Address.Zip4;
                            request.CityStateZipOK = response.CityStateZipOK;
                            request.AddressCleansingResult = response.AddressCleansingResult;
                            request.AddressMatch = response.AddressMatch;
                            request.VerificationLevel = response.VerificationLevel;
                            request.Status = response.Status;
                            addresses = response.CandidateAddresses;
                            if (addresses != null && addresses.Count > 0)
                            {
                                foreach (SV.Framework.Common.LabelGenerator.Address item in addresses)
                                {
                                    candidateAddress = new SV.Framework.Fulfillment.CandidateAddress();
                                    candidateAddress.Address1 = item.Address1;
                                    candidateAddress.City = item.City;
                                    candidateAddress.State = item.State;
                                    candidateAddress.PostalCode = item.PostalCode;
                                    candidateAddress.CountryCode = "US";
                                    CandidateAddresses.Add(candidateAddress);
                                }
                                request.CandidateAddresses = CandidateAddresses;



                            }
                            SV.Framework.Fulfillment.ValidateAddressOperation.ValidateAddressInsert(request);

                            lblMsg.Text = "Submitted successfully";

                        }

                    }

                }

            }
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkSuggestion = e.Row.FindControl("lnkSuggestion") as LinkButton;
                if (lnkSuggestion != null)
                {
                    lnkSuggestion.OnClientClick = "openDialogAndBlock('Suggetions', '" + lnkSuggestion.ClientID + "')";

                }
            }
        }
    }
}