(function () {
    var CourtDecisionOrig = document.getElementById("CourtDecision").value;
    var ClaimSummaryOrig = document.getElementById("ClaimSummary").value;
    var CourtDecisionRepl = CourtDecisionOrig.replace(/(\r\n|\n|\r)/g, "<br />");
    var ClaimSummaryRepl = ClaimSummaryOrig.replace(/(\r\n|\n|\r)/g, "<br />");

    document.getElementById("txtCourtDecision").innerHTML = CourtDecisionRepl;
    document.getElementById("txtClaimSummary").innerHTML = ClaimSummaryRepl;


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