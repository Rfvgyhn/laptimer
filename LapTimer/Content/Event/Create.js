$(function () {
    $("#participants").on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent().remove();
    });

    $("#addParticipant").on("click", function (e) {
        e.preventDefault();

        var $number = $("#newNumber");
        var $name = $("#newName");
        var number = $number.val();
        var name = $name.val();

        if (!number || !name)
            return;

        $number.val("");
        $name.val("");

        var html =
            '<li> \
                <input type="hidden" name="participants[{number}]" value="{name}" /> \
                {number} - {name} <a href="#" class="delete">Delete</a> \
            </li>';
        $("#participants").append(html.replace(/{number}/g, number).replace(/{name}/g, name));
    });
});