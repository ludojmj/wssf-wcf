function callGet(obj) {
    var results = document.getElementById("results");
    obj.addEventListener("click", function (e) {
        var bustCache = "&" + new Date().getTime(),
            oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState !== 4) {
                results.innerHTML = "Loading ...";
                return;
            }
            if (oReq.status !== 200) {
                results.innerHTML = "Error : " + oReq.responseText;
                return;
            }
            results.innerHTML = "Result : " + oReq.responseText;
        };
        oReq.open("GET", e.target.dataset.url + bustCache, true);
        oReq.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        oReq.setRequestHeader("x-vanillaAjaxWithoutjQuery-version", "1.0");
        oReq.send();
    });
}

(function () {
    // GET
    for (var idx =0; idx < 5; idx++) {
        var action = document.getElementById("action" + idx);
        callGet(action);
    }

}());