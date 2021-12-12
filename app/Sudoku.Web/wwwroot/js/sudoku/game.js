$(function () {
    GetBoard();
});

function GetBoard() {
    var diffuculty = $("#difficulty").val();
    var uniqueControl = $("#switchUnique").prop('checked')
    $("#overlay").show();

    $.get("/Sudoku/GameBoard?uniqueControl=" + uniqueControl + "&diffuculty=" + diffuculty  , function (data) {
        if (data.message != undefined) {
            $("#error").html(data.message);
            $("#error").show();
            $("#overlay").hide();
        } else {
            $('#gameBoard').html(data);
            $("#overlay").hide();
        }
    });
}

$("#btnNewGame").click(function () {
    GetBoard();
});

$('#difficulty').change(function () {
    GetBoard();
});