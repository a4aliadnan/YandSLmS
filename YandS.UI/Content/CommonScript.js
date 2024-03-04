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

$.blockUI.defaults.message = '<i class="fas fa-2x fa-sync-alt fa-spin"></i>';

$(document).ready(function () {
    console.log("Common Script Page");
    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

    $.fn.regexMask = function (mask) {
        $(this).keypress(function (event) {
            if (!event.charCode) return true;
            var part1 = this.value.substring(0, this.selectionStart);
            var part2 = this.value.substring(this.selectionEnd, this.value.length);
            if (!mask.test(part1 + String.fromCharCode(event.charCode) + part2))
                return false;
        });
    };
});