$("#btnUpload").click(function () {
    $("#error").hide();
    var file = $('#fileSudoku')[0].files[0]
    if (CheckFile(file)) {
        UploadFile(file)
    }
});

function CheckFile(file) {
    if (file == undefined) {
        $("#error").html("You should select file which you want to upload.");
        $("#error").show();
        return false;
    }
    return true;
}

function UploadFile(file) {
    var formData = new FormData();
    formData.append("fileSudoku", file);
    formData.append("uniqueControl", $("#switchUnique").prop('checked'));
    $("#overlay").show();

    $.ajax({
        url: '/Sudoku/SolverBoard',
        data: formData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (data) { 
            if (data.message != undefined) {
                $("#error").html(data.message);
                $("#error").show();
                $("#overlay").hide();

            } else {
                $('#gameBoard').html(data);
                $("#overlay").hide();
            }
        }
    });
}