$("#edit").on("pageinit", function (event) {
    var $page = $(this);
    var timers = {};
    var $sessionContainer = $(".sessions", $page);
    var eventId = $sessionContainer.data("event");
    var currentSession = $("li:eq(1)", $sessionContainer).addClass("active").text();

    initTimers($(".times li", $page));

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

        if (!currentSession)
            return;

        var $this = $(this);
        var $container = $this.closest("li");
        var id = $container.data("number");

        if ($this.hasClass("stop")) {
            stopTimer(id, $this);

            return;
        }
        else {
            $this.removeClass("start").addClass("stop").siblings().find(".ui-btn-text").text("Stop");
            $this.parent().prev().find("button").button("enable");
        }

        timers[id].start = now();
        timers[id].interval = setInterval(function () { updateTimer(id); }, 10);
    });

    $(".split", $page).on("click", function (e) {
        e.preventDefault();

        if (!currentSession)
            return;

        var $this = $(this);
        var $container = $this.closest("li");
        var id = $container.data("number");
        var timer = timers[id];
        var time = now();
        var elapsed = time - timer.start;

        timer.start = time;
        timer.lap++;

        $.post(ROOT_URL + "Event/AddLap", { lap: timer.lap, time: elapsed, eventId: eventId, sessionName: currentSession, participant: id });
    });

    $(".sessions", $page).on("click", "a", function (e) {
        e.preventDefault();

        var $this = $(this);
        var sessionName = $this.text();

        $.mobile.showPageLoadingMsg();
        $.get(ROOT_URL + "Event/GetTimes", { eventId: eventId, sessionName: sessionName })
         .success(function (data) {
             resetTimers();
             var $times = $(".times li", $page);
             $(data).each(function () {
                 var timer = timers[this.number];
                 timer.lap = this.times.length + 1;

                 timer.display.text(formatTime(this.times.pop() || 0));
             });

             $this.closest("li").addClass("active").siblings(".active").removeClass("active");
             currentSession = sessionName;
             $.mobile.hidePageLoadingMsg();
         })
         .error(function () {
             $.mobile.hidePageLoadingMsg();
         });
    }).on("taphold", "a", function (e) {
        e.preventDefault();

        var $this = $(this);
        var name = $this.text();

        $('<div>').simpledialog2({
            mode: 'button',
            headerText: 'Edit Session',
            headerClose: true,
            buttonInputDefault: name,
            buttonInput: true,
            buttons: {
                'Update': {
                    click: function () {
                        var newName = $.mobile.sdLastInput;

                        $.post(ROOT_URL + "Event/EditSession", { eventId: eventId, name: name, newName: newName })
                         .success(function () {
                             $this.text(newName);
                         })
                         .error(function () {

                         });
                    },
                    icon: "plus"
                },
                'Delete': {
                    click: function () {
                        $.post(ROOT_URL + "Event/DeleteSession", { eventId: eventId, name: name })
                         .success(function () {
                             currentSession = null;

                             $this.closest("li").remove();
                         })
                         .error(function () {

                         });
                    },
                    icon: "delete"
                },
                'Cancel': {
                    click: function () { },
                    icon: "back",
                    theme: "c"
                }
            }
        });
    });

    function stopTimer(id, $button) {
        if (!$button)
            $button = $(".times li[data-number=" + id + "] .toggle", $page);

        clearInterval(timers[id].interval);
        $button.removeClass("stop").addClass("start").siblings().find(".ui-btn-text").text("Start");
        $button.parent().prev().find("button").button("disable");
    }

    function updateTimer(id) {
        var time = now() - timers[id].start;

        timers[id].display.text(formatTime(time));
    }

    function resetTimers() {
        for (var id in timers) {
            if (timers.hasOwnProperty(id)) {
                timers[id].display.text(formatTime(0));

                stopTimer(id);
            }
        }
    }

    function initTimers($participants) {
        $participants.each(function () {
            var id = $(this).data("number");
            var $elapsed = $(".elapsed", this);

            $(".split", this).button("disable");
            timers[id] = { lap: $elapsed.data("lap") || 0, display: $elapsed };
        });
    }
});