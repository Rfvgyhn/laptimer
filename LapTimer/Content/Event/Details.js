$("#details").on("pageinit", function () {
    var $page = $(this);
    var summaryTmpl = '<h3>Best was <em>{name}</em> with a {time}</h3>';
    var timeTmpl = '<li>{time}</li>';
    var timeBestTmpl = '<li class="best">{time}</li>';
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

    $("ul a", $page).on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var sessionName = $this.text();
        var eventId = $this.closest("ul").data("event");

        $.mobile.showPageLoadingMsg();
        $.get(ROOT_URL + "Event/GetTimes", { eventId: eventId, sessionName: sessionName })
         .success(function (data) {
             var best;
             var html = '';

             $(data).each(function () {
                 var hasTimes = this.times.length > 0;
                 var min = 0;

                 if (hasTimes) {
                     min = this.times.min();

                     if (!best || min < best.time)
                         best = { number: this.number, name: this.name, time: min };
                 }

                 var times = '';
                 $(this.times).each(function () {
                     times += (this == min ? timeBestTmpl : timeTmpl).replace("{time}", formatTime(this));
                 });

                 html += personTmpl.replace("{number}", this.number.replace(/-\d+/, ""))
                                   .replace("{name}", this.name)
                                   .replace("{times}", hasTimes ? times : noTimes);
             });

             if (best) {
                 html = summaryTmpl.replace("{name}", best.name)
                                   .replace("{time}", formatTime(best.time))
                    + html;
             }

             $("#sessionDetails").html("").append(html);
             $.mobile.hidePageLoadingMsg();
             $this.closest("li").addClass("active").siblings(".active").removeClass("active");
         })
         .error(function () {
             $.mobile.hidePageLoadingMsg();
         });
    });
});