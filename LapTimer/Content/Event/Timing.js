$(function () {
    var timers = {};

    $("#newSession").on("click", function (e) {
        e.preventDefault();

        $("#sessionDialog").show();
    });

    $("#closeDialog").on("click", function (e) {
        e.preventDefault();

        $("#sessionDialog").hide();
    });

    $("#saveSession").on("click", function (e) {
        e.preventDefault();

        $.post("", {})
         .success(function () {

         })
         .error(function () {

         });
    });

    $(".toggle").on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var $container = $this.parent();
        var id = $(".number", $container).text();

        if ($this.hasClass("stop")) {
            clearInterval(timers[id].interval);
            $this.removeClass("stop").addClass("start").text("Start");

            return;
        }
        else
            $this.removeClass("start").addClass("stop").text("Stop");

        var $display;

        if (timers[id])
            $display = timers[id].display;
        else {
            var $elapsed = $(".elapsed", $container);
            timers[id] = { lap: $elapsed.data("lap") };
            $display = timers[id].display = $elapsed;
        }

        timers[id].start = now();
        timers[id].interval = setInterval(updateTimer, 10, id);
    });

    $(".split").on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var $container = $this.parent();
        var id = $(".number", $container).text();
        var timer = timers[id];
        var time = now();
        var elapsed = time - timer.start;

        timer.start = time;
        timer.lap++;
        
        $.post(ROOT_URL + "Event/AddLap", { lap: timer.lap, time: elapsed, eventId: EVENT_ID, sessionId: CURRENT_SESSION, participant: id });
    });

    function updateTimer(id) {
        var time = now() - timers[id].start;
        var milliseconds = parseInt(time % 1000, 10);
        var seconds = parseInt((time / 1000) % 60, 10);
        var minutes = parseInt((time / (1000 * 60)) % 60, 10);
        var hours = parseInt((time / (1000 * 60 * 60)) % 24, 10);

        timers[id].display.text(pad(hours, 2) + ":" + pad(minutes, 2) + ":" + pad(seconds, 2) + "." + pad(milliseconds, 3));
    }
});