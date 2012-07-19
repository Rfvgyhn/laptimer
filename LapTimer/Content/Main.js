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

function formatTime(time) {
    var milliseconds = parseInt(time % 1000, 10);
    var seconds = parseInt((time / 1000) % 60, 10);
    var minutes = parseInt((time / (1000 * 60)) % 60, 10);
    var hours = parseInt((time / (1000 * 60 * 60)) % 24, 10);

    return pad(hours, 2) + ":" + pad(minutes, 2) + ":" + pad(seconds, 2) + "." + pad(milliseconds, 3);
}

Array.prototype.max = function () {
    return Math.max.apply(Math, this);
};

Array.prototype.min = function () {
    return Math.min.apply(Math, this);
};