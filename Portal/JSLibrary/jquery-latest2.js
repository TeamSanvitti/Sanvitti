/*!
* jQuery JavaScript Library v1.5.1
* http://jquery.com/
*
* Copyright 2011, John Resig
* Dual licensed under the MIT or GPL Version 2 licenses.
* http://jquery.org/license
*
* Includes Sizzle.js
* http://sizzlejs.com/
* Copyright 2011, The Dojo Foundation
* Released under the MIT, BSD, and GPL Licenses.
*
* Date: Wed Feb 23 13:55:29 2011 -0500
*/
(function (window, undefined) {


    jQuery.fn.extend({
        AjaxReady: function (fn) {
            if (fn) {
                return jQuery.event.add(this[0], "AjaxReady", fn, null);
            } else {
                var ret = jQuery.event.trigger("AjaxReady", null, this[0], false, null);
                // if there was no return value then the even validated correctly
                if (ret === undefined)
                    ret = true;
                return ret;
            }
        }
    });


    window.jQuery = window.$ = jQuery;
})(window);