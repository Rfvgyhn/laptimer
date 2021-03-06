﻿$("#create").on("pageinit", function () {
    var $page = $(this);
    $("form", $page).on("submit", function (e) {
        var name = $("#LocationName").val();

        if (name)
            $(this).append('<input type="hidden" value="' + name + '" name="LocationName" />');
    });
    $("#participants").on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).closest("li").remove();
    });

    $("#addParticipant").on("click", function (e) {
        e.preventDefault();

        var $number = $("#newNumber");
        var $name = $("#newName");
        var number = $number.val();
        var key = number;
        var name = $name.val();

        if (!number || !name)
            return;

        var duplicates = $("input[name^='participants[" + number + "']").length;

        if (duplicates > 0)
            key += "-" + (duplicates + 1);

        $number.val("");
        $name.val("");

        var html = $(
            '<li> \
                <input type="hidden" name="participants[{key}]" value="{name}" /> \
                {number} - {name} \
                <div class="sideButtons"><a data-role="button" data-icon="delete" data-iconpos="notext" class="sideButtons delete">Delete</a></div> \
            </li>'.replace(/{key}/g, key).replace(/{number}/g, number).replace(/{name}/g, name));

        var $list = $("#participants");
        $list.append(html).listview("refresh").find("a").button();
    });
});