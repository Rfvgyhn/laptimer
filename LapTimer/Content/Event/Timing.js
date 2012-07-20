$("#timing").on("pagecreate", function () {
    var $page = $(this);
    var timers = {};
    var $sessionContainer = $(".sessions", $page);
    var eventId = $sessionContainer.data("event");
    var currentSession = $("li:eq(1)", $sessionContainer).addClass("active").text();

    $("#newSession").on("click", function (e) {
        e.preventDefault();

        $('<div>').simpledialog2({
            mode: 'button',
            headerText: 'Add Session',
            headerClose: true,
            buttonPrompt: 'Name',
            buttonInput: true,
            buttons: {
                'Create': {
                    click: function () {
                        var name = $.mobile.sdLastInput;
                        $.post(ROOT_URL + "Event/AddSession", { eventId: eventId, name: name })
                         .success(function () {
                             $(".active", $sessionContainer).removeClass("active");
                             $sessionContainer.append('<li class="active"><a href="#">' + name + '</a></li>')
                                              .listview("refresh");
                             currentSession = name;
                             resetTimers();
                         })
                         .error(function () {

                         });
                    },
                    icon: "plus"
                },
                'Cancel': {
                    click: function () { },
                    icon: "delete",
                    theme: "c"
                }
            }
        });
    });

    $(".toggle", $page).on("click", function (e) {
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

    $(".split", $page).on("click", function (e) {
        e.preventDefault();

        var $this = $(this);
        var $container = $this.closest("li");
        var id = $(".number", $container).text();
        var timer = timers[id];
        var time = now();
        var elapsed = time - timer.start;

        timer.start = time;
        timer.lap++;

        $.post(ROOT_URL + "Event/AddLap", { lap: timer.lap, time: elapsed, eventId: eventId, sessionName: currentSession, participant: id });
    });

    function updateTimer(id) {
        var time = now() - timers[id].start;

        timers[id].display.text(formatTime(time));
    }

    function resetTimers() {
        for (var id in timers) {
            if (timers.hasOwnProperty(id))
                timers[id].display.text(formatTime(0));
        }
    }
});