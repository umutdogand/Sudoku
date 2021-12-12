// If user had checked "Auto Check Mistakes", This method controls sudoku game rules for every input of digit.
// If there are any conflict in same row, column or room this method show them on board.
function CheckMistake(row, column, value) {
    RemoveBackgorund();
    var roomValue = Math.floor((row - 1) / 3) + "" + Math.floor((column - 1) / 3);

    for (var i = 1; i < 10; i++) {
        //Checking for same row
        CheckValue(row, i, value);

        //Checking for same column
        CheckValue(i, column, value);

        ////Checking for same room
        for (var j = 1; j < 10; j++) {
            var currentRoom = Math.floor((i - 1) / 3) + "" + Math.floor((j - 1) / 3);
            if (currentRoom === roomValue) {
                CheckValue(i, j, value);
            }
        }
    }
    $("#cell" + row + column).css("background-color", "");
}

// This metod sets backround of the cell if the value equals to specified row and column. 
function CheckValue(row, column, value) {
    var cellValue = $("#cell" + row + column).val();
    if (cellValue != "" && cellValue == value) {
        $("#cell" + row + column).css("background-color", "red");
    }
}

// After conflict step, if user input new value, this method removes all cell's background.
function RemoveBackgorund() {
    $(".cell").css("background-color", "");
}

// When the clicked left arrow key the cursor focueses the left cell of the current cell.
function FocusLeft(row, column) {
    if (column == 1) column = 10;
    if ($("#cell" + row + (column - 1)).prop('disabled')) FocusLeft(row, column - 1);
    else $("#cell" + row + (column - 1)).focus();
}

// When the clicked right arrow key the cursor focueses the right cell of the current cell.
function FocusRight(row, column) {
    if (column == 9) column = 0;
    if ($("#cell" + row + (column + 1)).prop('disabled')) FocusRight(row, column + 1);
    else $("#cell" + row + (column + 1)).focus();
}

// When the clicked up arrow key the cursor focueses the up cell of the current cell.
function FocusUp(row, column) {
    if (row == 1) row = 10;
    if ($("#cell" + (row - 1) + column).prop('disabled')) FocusUp(row - 1 ,column);
    else $("#cell" + (row - 1) + column).focus();
}

// When the clicked down arrow key the cursor focueses the down cell of the current cell.
function FocusDown(row, column) {
    if (row == 9) row = 0;
    if ($("#cell" + (row + 1) + column).prop('disabled')) FocusDown(row + 1, column);
    else $("#cell" + (row + 1) + column).focus();
}

// Moves the cursor according to the key code
function ArrowKeyControl(keyCode, row, column) {
    if (keyCode == 37) {
        FocusLeft(row, column);
        return true;
    } else if (keyCode == 38) {
        FocusUp(row, column);
        return true;
    } else if (keyCode == 39) {
        FocusRight(row, column);
        return true;
    } else if (keyCode == 40) {
        FocusDown(row, column);
        return true;
    }
    return false;
}

//Checks if the input is valid or not
function DigitControl(keyCode) {
    if (!(keyCode == 8 || (keyCode > 48 && keyCode < 58) || (keyCode > 96 && keyCode < 106))) {
        $("#error").html("You can only enter the digits, except 0.")
        $("#error").show();
        return true;
    }
    return false;
}

$(function () {
    $(".cell").keydown(function (event) {
        $("#error").hide();
        var row = $(this).data("row");
        var column = $(this).data("column");

        if (ArrowKeyControl(event.which, row, column)) return;
        if (DigitControl(event.which)) return false;
        this.value = this.value.replace(/^[0-9]+$/, '');
    });

    $(".cell").keyup(function (event) {
        var row = $(this).data("row");
        var column = $(this).data("column");
        if ($("#switchMistakes").prop('checked')) CheckMistake(row, column, this.value);
    });
});