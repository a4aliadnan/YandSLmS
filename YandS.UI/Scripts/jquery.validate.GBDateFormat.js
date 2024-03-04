$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
    }
});
//$.validator.methods.date = function (value, element) { return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid(); }
