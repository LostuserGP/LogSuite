window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 10000 });
    }
}

function ShowConfirmationModal() {
    $('#confirmationModal').modal('show');
}

function HideConfirmationModal() {
    $('#confirmationModal').modal('hide');
}