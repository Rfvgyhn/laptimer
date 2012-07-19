$(function () {
    var summaryTmpl = 'Best: {number} - {name} - {time}';
    var timeTmpl = '<li>{time}</li>';
    var noTimes = '<li>No Times</li>';
    var personTmpl =
        '<ul>\
            <li>\
                <span>{number} - {name}</span>\
                <ul>\
                    {times}\
                </ul>\
            </li>\
        </ul>';

    $("#details ul a").on("click", function (e) {
        e.preventDefault();

        var sessionId = $(this).data("session");
        $.mobile.showPageLoadingMsg();
        $.get(ROOT_URL + "Event/GetTimes", { eventId: eventId, sessionId: sessionId })
         .success(function (data) {
             var best;
             var html = '';

             $(data).each(function () {
                 var hasTimes = this.times.length > 0;

                 if (hasTimes) {
                     var min = this.times.min();

                     if (!best || min < best.time)
                         best = { number: this.number, name: this.name, time: min };
                 }

                 var times = '';
                 $(this.times).each(function () {
                     times += timeTmpl.replace("{time}", formatTime(this));
                 });

                 html += personTmpl.replace("{number}", this.number)
                                   .replace("{name}", this.name)
                                   .replace("{times}", hasTimes ? times : noTimes);
             });

             if (best) {
                 html = summaryTmpl.replace("{number}", best.number)
                               .replace("{name}", best.name)
                               .replace("{time}", formatTime(best.time))
                    + html;
             }

             $("#sessionDetails ul").replaceWith(html);
             $.mobile.hidePageLoadingMsg();
         })
         .error(function () {
             $.mobile.hidePageLoadingMsg();
         });
    });
});