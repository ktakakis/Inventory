function ShowMessage(msg) {
    toastr.success(msg);
}

function ShowMessageError(msg) {
    toastr.error(msg);
}

//Initialize Select2 Elements
$('.select2').each(function (i, obj) {
    if (!$(obj).hasClass("select2-hidden-accessible")) {
        $(obj).select2({
            placeholder: "Κάντε μια Επιλογή",
            dropdownAutoWidth: 'true',
            width: '100%'
        });
    }
});

//Date picker
$('.datepicker').datepicker({
    language: 'el',
    autoclose: true
});


