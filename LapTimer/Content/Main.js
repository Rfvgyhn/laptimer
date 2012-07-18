function now() {
    var performance = window.performance;

    if (performance && performance.now)
        return performance.now();
    else {
        if (performance && performance.webkitNow)
            return performance.webkitNow();
        else
            return Date().getTime();
    }
}

function pad(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function ensureEndsWith(str, suffix) {
    if (str.indexOf(suffix, str.length - suffix.length) === -1)
        return str + '/';

    return str;
}