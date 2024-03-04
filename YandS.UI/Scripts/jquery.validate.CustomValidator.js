$.validator.methods.date = function (value, element) { return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid(); }

$.validator.addMethod("selectzeroval", function (value, element, param) {
    console.log("selectzeroval");
    console.log(value);

    if (value == '0')
        return false;
    else
        return true;
});

$.validator.unobtrusive.adapters.add('selectzeroval', ['minvalue'], function (options) {
    options.rules['selectzeroval'] = { minvalue: options.params.minvalue };
    options.messages['selectzeroval'] = options.message;
});

$.validator.addMethod("restrictbackdates", function (value, element, param) {
    //var date = new Date(value);
    //var minDate = new Date(param.mindate);
    //return date >= minDate;
    if (value != '') {
        console.log(value);
        var date = moment(moment(value, 'DD/MM/YYYY').format("YYYY-MM-DD"));
        var minDate = moment(moment(new Date()).format("YYYY-MM-DD"));

        //console.log("param.mindate");
        //console.log(param.mindate);

        //console.log("date");
        //console.log(date);
        //console.log("minDate");
        //console.log(minDate);

        //console.log(moment('2010-10-20').isSameOrBefore('2010-10-21'));
        //console.log(moment(date).isSameOrBefore(minDate));

        console.log("restrictbackdates");
        console.log(date <= minDate);

        return date <= minDate;
    }
    return true;
});

$.validator.unobtrusive.adapters.add('restrictbackdates', ['mindate'], function (options) {
    options.rules['restrictbackdates'] = { mindate: options.params.mindate };
    options.messages['restrictbackdates'] = options.message;
});

$.validator.addMethod("isgreater", function (value, element, param) {
    var otherProp = $('#' + param.otherproperty);

    console.log(otherProp.val());
    console.log(value);

    if (otherProp.val() != '' && value != '') {
        var StartDate = moment(moment(otherProp.val(), 'DD/MM/YYYY').format("YYYY-MM-DD"));

        var Enddate = moment(moment(value, 'DD/MM/YYYY').format("YYYY-MM-DD"));;
        if (StartDate != '') {
            console.log("isgreater");
            console.log(Enddate >= StartDate);

            return Enddate >= StartDate;
        }
    }
    return true;
});

$.validator.unobtrusive.adapters.add('isgreater', ['otherproperty'], function (options) {
    options.rules['isgreater'] = { otherproperty: options.params.otherproperty };
    options.messages['isgreater'] = options.message;
});