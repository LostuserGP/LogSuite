window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 500 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
    }
}

function ShowConfirmationModal() {
    $('#confirmationModal').modal('show');
}

function HideConfirmationModal() {
    $('#confirmationModal').modal('hide');
}

$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

});