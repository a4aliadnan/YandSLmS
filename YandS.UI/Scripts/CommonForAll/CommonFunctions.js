/*
|------------------------------------------|
| MZEC Oracle Application .Net  JAVA SCRIPT|
            CommonFunctions.js
|------------------------------------------|
| @author:  Ali Adnan                      |
| @version: 1.0 (10 May 2017)              |
|                                          |
|------------------------------------------|
*/
/**
This Script file handle all Common functionlities
*/

/**
Global Variables
*/
var UNAME, UBRANCH, ULocation, CLIENTIP, CLIENTNAME, EMPNO, LoggedInUser;
/**
END Global Variables
*/
function round2Fixed(value) {
    value = +value;

    if (isNaN(value))
        return NaN;

    // Shift
    value = value.toString().split('e');
    value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + 3) : 3)));

    // Shift back
    value = value.toString().split('e');
    return (+(value[0] + 'e' + (value[1] ? (+value[1] - 3) : -3))).toFixed(3);
}

function ApplyTemplate(Container, Tmpl, msg) {
    $('#' + Container).html('');
    $('#' + Container).setTemplateURL(Tmpl, null, { filter_data: false });
    $('#' + Container).processTemplate(msg);
}

String.prototype.trim = String.prototype.trim || function () {
    return this.replace(/^\s+/, '').replace(/\s+$/, '');
};

// Enable Sum of each Column in Datatable
jQuery.fn.dataTable.Api.register('sum()', function () {
    return this.flatten().reduce(function (a, b) {
        if (typeof a === 'string') {
            a = a.replace(/[^\d.-]/g, '') * 1;
        }
        if (typeof b === 'string') {
            b = b.replace(/[^\d.-]/g, '') * 1;
        }

        return a + b;
    }, 0);
});

$(document).ready(function () {
    try {
        $.ajaxSetup({ cache: false });
        $.blockUI.defaults.css = {
            padding: 0,
            margin: 0,
            width: '0%',
            top: '40%',
            left: '50%',
            textAlign: 'center',
            color: '#fff',
            border: '0px',
            backgroundColor: '#fff',
            opacity: 0.9,
            cursor: 'wait'
        };

        $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

        LoggedInUser = JSON.parse(window.localStorage.getItem('LoggedInUser'));
        EMPNO = window.localStorage.getItem('EMPNO');
        CLIENTIP = window.localStorage.getItem('CLIENTIP');
        CLIENTNAME = window.localStorage.getItem('CLIENTNAME');

        if (EMPNO === null || EMPNO.length === 0) {
            console.log("Error - CommonFunctions.js >>>> Document Ready >>> EMPNO is NULL");
        }

        if (CLIENTIP === null || CLIENTIP.length === 0) {
            console.log("Error - CommonFunctions.js >>>> Document Ready >>> CLIENTIP is NULL");
        }

        if (CLIENTNAME === null || CLIENTNAME.length === 0) {
            console.log("Error - CommonFunctions.js >>>> Document Ready >>> CLIENTNAME is NULL");
        }

        if (LoggedInUser === null || LoggedInUser.length === 0) {
            console.log("Error - CommonFunctions.js >>>> Document Ready >>> LoggedInUser is NULL");
        }
        else {
            //SETTING HEADER INFORMATION

            UNAME = LoggedInUser.FULL_NAME;
            UBRANCH = LoggedInUser.DEPARTMENT;
            ULocation = LoggedInUser.WILAYA_NAME;

            //$('#Span5').html('<h4>' + UNAME + '</h4>');
            //$('#Span8').html(UBRANCH);
            //$('#Span7').html(ULocation);
            //var appendTD = '<object data="../Staff_Images/' + LoggedInUser.EMPLOYEE_NUMBER + '.jpg" style="width:60pt;height:60pt;display:block; text-align:center"><img src="../Staff_Images/clip_image001.png" style="width:60pt;height:60pt; text-align:center" alt="" /></object>';
            //$('#LoggedUserImage').append(appendTD);
            //END SETTING HEADER INFORMATION
            //GETTING USER MENU AND REQUIRED RIGHTS

            console.log("UNAME :: " + UNAME);
            console.log("UBRANCH :: " + UBRANCH);
            console.log("ULocation :: " + ULocation);
            console.log("LoggedInUser.EMPLOYEE_NUMBER :: " + LoggedInUser.EMPLOYEE_NUMBER);
            console.log("CLIENTIP :: " + CLIENTIP);
            console.log("CLIENTNAME :: " + CLIENTNAME);
        }

    }
    catch (e) {
        $.bigBox({
            title: "Error - CommonFunctions.js Document Ready",
            content: "Inner Exception : ' + e.InnerException + ' Exception : " + e,
            color: "#C46A69",
            icon: "fa fa-warning shake animated"
        });
    }
});
