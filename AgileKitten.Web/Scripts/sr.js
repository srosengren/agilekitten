var sr = (function (sr) {

    sr.ajax = function (request) {
        request.type = request.type || 'GET';
        request.async = request.hasOwnProperty('async') ? request.async : true;

        var r = new XMLHttpRequest();
        r.onreadystatechange = function () {
            if (r.readyState === 4 && r.status === 200 && r.responseText && request.success) {
                request.success(r.responseText);
            }
        };
        r.open(request.type, request.url, request.async);
        r.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
        r.send(request.data);
    };

    return sr;
})({});