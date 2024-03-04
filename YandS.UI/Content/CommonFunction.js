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

function PadDigits(n, totalDigits) {
    n = n.toString();
    var pd = '';
    if (totalDigits > n.length) {
        for (i = 0; i < (totalDigits - n.length); i++) {
            pd += '0';
        }
    }
    return pd + n.toString();
}

function InitPartialViews() {
    
}
