(function () {
    var CourtDecisionOrig = document.getElementById("CourtDecision");
    var ClaimSummaryOrig = document.getElementById("ClaimSummary");

    var CourtDecisiontxt = document.getElementById("txtCourtDecision");
    var ClaimSummarytxt = document.getElementById("txtClaimSummary");

    var CourtDecisionRepl = "";
    var ClaimSummaryRepl = "";


    if (typeof (CourtDecisionOrig) != 'undefined' && CourtDecisionOrig != null) {
        CourtDecisionOrig = document.getElementById("CourtDecision").value;
        CourtDecisionRepl = CourtDecisionOrig.replace(/(\r\n|\n|\r)/g, "<br />");

        if (typeof (CourtDecisiontxt) != 'undefined' && CourtDecisiontxt != null) 
            CourtDecisiontxt.innerHTML = CourtDecisionRepl;
    }

    if (typeof (ClaimSummaryOrig) != 'undefined' && ClaimSummaryOrig != null) {
        ClaimSummaryOrig = document.getElementById("ClaimSummary").value;
        ClaimSummaryRepl = ClaimSummaryOrig.replace(/(\r\n|\n|\r)/g, "<br />");

        if (typeof (ClaimSummarytxt) != 'undefined' && ClaimSummarytxt != null) 
            ClaimSummarytxt.innerHTML = ClaimSummaryRepl;
    }

})();
function CopyToClipboard(containerid) {
    console.log(containerid);
    if (document.selection) {
        var range = document.body.createTextRange();
        range.moveToElementText(document.getElementById(containerid));
        range.select().createTextRange();
        document.execCommand("copy");
    } else if (window.getSelection) {

        var selection = document.getSelection();
        var range = document.createRange();
        range.selectNode(document.getElementById(containerid));
        selection.removeAllRanges();
        selection.addRange(range);
        document.execCommand('copy');
        selection.removeAllRanges();

    }
}