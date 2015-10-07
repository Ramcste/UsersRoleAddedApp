//var webServiceURL = baseUrl;
var webServiceURL = baseUrl + "api/";
var webServiceURLProxy = "";
var IsInternetExplorer = false;
var webServiceCallStack = [];
function callWebService(webmethodname, json, type, callback) {
    var req = $.ajax({
        url: ((IsInternetExplorer) ? webServiceURLProxy : webServiceURL) + webmethodname,
        type: type,
        dataType: 'json',
        data: json,
        contentType: 'application/json; charset=utf-8',
        headers: { "cache-control": "no-cache" },
        success: function (response) {
            if (response) {
                //response = $.parseJSON(response); // Remove once in mobile
                if (response.data === "NotAuthorized") {
                    //msgBox('Your session has been expired. Please login again!');

                    return;
                }
                if (callback) {
                    callback(response);
                }
            }
            else {
                callback(null);
            }

        },
        error: function (xhr, message) {
            if (message !== 'abort') {
                //alert('Server not responding! Please try later.');
            }

        },
        complete: function (xhr) {
            //Remove from callstack
            webServiceCallStack = $.grep(webServiceCallStack, function (val) {
                return val !== xhr;
            });
        }
    });

    webServiceCallStack.push(req);
    return;
}

function fail(error) {
    //$.mobile.hidePageLoadingMsg();
    alert("FileErrorCode=" + error.code);
}

function ClearWebServiceCallStack() {
    $.each(webServiceCallStack, function (i) {
        var req = webServiceCallStack[i];
        if (req && req.abort) {
            req.abort();
        }
    });

    webServiceCallStack = [];
}