Financial = {
    Url: {
        GetFinancialData: "/Report/GetFinancialReport",        
    },    
    ID: {
        btnSearch: "btnSearch",
        fromDate: "fromDate",
        endDate: "endDate",
        financialContainer: "financialContainer",
        toastContainer:"toast-container"
    },
    Class: {
        multiselectcontainer:"multiselect-container",
    },   
    Attr: {}, 
    Message: {
        SelectKitchens: "Please select kitchen!",
    }, 
    Init: function () {
        var $this = this;
        $this.ClickEvent();
       
    },
    ClickEvent: function () {
        var $this = this;
        $("#" + $this.ID.btnSearch).click(function () {
            CommonMethods.ShowLoad();
            var profileIds;
            $("." + $this.Class.multiselectcontainer).find("input[type=checkbox]:checked").each(function () {
                if (profileIds) {
                    profileIds = profileIds + "," + $(this).attr("value");
                }
                else {
                    profileIds = $(this).attr("value");
                }
            });
            if (profileIds == undefined) {
                CommonMethods.ToastrError($this.Message.SelectKitchens);
                CommonMethods.HideLoad();
                return
            }
            var FinancialSearchDTO = {
                ProfileIds: profileIds,
                StartDate: $("#" + $this.ID.fromDate).val(),
                EndDate: $("#" + $this.ID.endDate).val()
            }
            CommonJsFunction.AjaxContent.ContentType = "application/json; charset=utf-8";
            CommonJsFunction.AjaxContent.DataType = "html";
            CommonJsFunction.Ajax($this.Url.GetFinancialData, CommonJsFunction.MethodType.POST, JSON.stringify(FinancialSearchDTO), function (result) {
                $("#" + $this.ID.financialContainer).html("").html(result);
                setTimeout(function () {
                    CommonMethods.HideLoad();
                }, 300);
            });

        });
    }
}

//Common Method for use in Discover Above JS Class
CommonMethods = {
    HideTimeOunt: function (counterId,timeOut) {
        setTimeout(function () {
            $("#" + counterId).hide();
        }, timeOut);
    },

    AjaxCall: function (Url,callType,data) {
        var postData = JSON.stringify(data);         
        CommonJsFunction.AjaxContent.ContentType = "application/json; charset=utf-8";
        CommonJsFunction.AjaxContent.DataType = "html";
        CommonJsFunction.Ajax(
            Url
            //, CommonJsFunction.MethodType.POST
            ,callType
            , postData
            , function (result) {
                return result;
            });
    },

    ToastrError: function (message) {
        if ($("#" + Financial.ID.toastContainer).length == 0)
               toastr.error(message);
    },

    ShowLoad: function () {
        $(".loading_container_overlay,.loader_container").addClass("show").removeClass("hide");
    },

    HideLoad: function () {
        $(".loading_container_overlay,.loader_container").addClass("hide").removeClass("show");
    }
}