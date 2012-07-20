$(function () {
    var timers = {};

    $("#sessions li").eq(CURRENT_SESSION + 1).addClass("active");

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
             // append li with class active
            // remove other lis active
         })
         .error(function () {

         });
    });

    $(".toggle").on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var $container = $this.closest("li");
        var id = $(".number", $container).text();

        if ($this.hasClass("stop")) {
            clearInterval(timers[id].interval);
            $this.removeClass("stop").addClass("start").find(".ui-btn-text").text("Start");

            return;
        }
        else
            $this.removeClass("start").addClass("stop").find(".ui-btn-text").text("Stop");

        var $display;

        if (timers[id])
            $display = timers[id].display;
        else {
            var $elapsed = $(".elapsed", $container);
            timers[id] = { lap: $elapsed.data("lap") };
            $display = timers[id].display = $elapsed;
        }

        timers[id].start = now();
        timers[id].interval = setInterval(function () { updateTimer(id); }, 10);
    });

    $(".split").on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var $container = $this.closest("li");
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

        timers[id].display.text(formatTime(time));
    }
});