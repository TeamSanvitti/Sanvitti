		function fnValueValidate(evt,chkType)
		{
		    var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
           
			var regNum = /^[\d]+/;
			var regInt = /^[\d\.]+/;
			var regDt = /^[\d\/]+/;
			var regStr = /[\w\.\%\-\@\,\#\&\*\ ]+(\w+)*/;
			var regnStr = /[\w\.\-\_\@\*]+(\w+)*/;
			var regPhone = /^[\d\-]+/;///^\([1-9]\d{2}\)\s?\d{3}\-\d{4}$/;
			var regSSN = /^[\d\-]+/; ///^\d{3}\-?\d{2}\-?\d{4}$/
			var regMeasure = /[\w\.\%\-\@\,\#\\'\"&\ ]+(\w+)*/;
			
			if (chkType.toLowerCase() == 's' )
				var regExp = new RegExp(regStr);
			else if (chkType.toLowerCase() == 'n')
				var regExp = new RegExp(regNum);
			else if (chkType.toLowerCase() == 'i')
				var regExp = new RegExp(regInt);				
			else if (chkType.toLowerCase() == 'd')
				var regExp = new RegExp(regDt);
			else if (chkType.toLowerCase() == 'p')
				var regExp = new RegExp(regPhone);				
			else if (chkType.toLowerCase() == 'sn')
				var regExp = new RegExp(regSSN);	
			else if (chkType.toLowerCase() == 'ns') //No Spaces
				var regExp = new RegExp(regnStr);	
			else if (chkType.toLowerCase() == 'm') //No Spaces
				var regExp = new RegExp(regMeasure);											
				
								//alert(charCodes);
			if (charCodes==8 || charCodes == 33 || charCodes==9 || charCodes > 35 && charCodes < 40 || String.fromCharCode(charCodes).match(regExp))
			{
			   //alert(charCodes);
				return true;
			}
			else
				return false;
		}
		
		function fnTxtCount(obj,txt,ilength)
		{
			if (obj != null)
			{ 
				if(obj.value.length > ilength)
				{
					alert(txt + ' has ' +  obj.value.length + ' characters, but lenght cannot be more than ' + ilength + ' characters');
					return false;
				}
			}
			else
			{
				alert(txt + ' does not exist in form');
				return false;
			}
		}	
		
		function fnSearch(txt)
		{
			
			var str = 'list.aspx?verb=' + txt.value;
			//alert(str);
			location.search = str ;
			//window.location.reload;
			window.navigate(str);
		}
		
		function validate()
		{
			var agree=confirm("Do you want to delete the record?"); if (agree) return true; else return false; 
		}

		function confirmuser()
		{
			var agree=confirm("Are you sure?"); if (agree) return true; else return false; 
		}
				
function isPhoneNumber(s) 
{
 
     // Check for correct phone number
     rePhoneNumber = new RegExp(/^[1-9]\d{2}\-\s?\d{3}\-\d{4}$/);
 
     if (!rePhoneNumber.test(s)) {
          //alert("Phone Number Must Be Entered As: (562) 555-1234");
          return false;
     }
 
return true;
}
function isEmail(str) {
  // are regular expressions supported?
  var supported = 0;
  if (window.RegExp) {
    var tempStr = "a";
    var tempReg = new RegExp(tempStr);
    if (tempReg.test(tempStr)) supported = 1;
  }
  if (!supported) 
    return (str.indexOf(".") > 2) && (str.indexOf("@") > 0);
  var r1 = new RegExp("(@.*@)|(\\.\\.)|(@\\.)|(^\\.)");
  var r2 = new RegExp("^.+\\@(\\[?)[a-zA-Z0-9\\-\\.]+\\.([a-zA-Z]{2,3}|[0-9]{1,3})(\\]?)$");
  return (!r1.test(str) && r2.test(str));
}
 
		function ErrorLogValidate()
		{
			var agree=confirm("Clear Log will clear the Error Log. \n\nDo you want to clear the Error Log?"); if (agree) return true; else return false; 
		}

	