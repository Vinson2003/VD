function Confirmation(message) {
    return new Promise(function (resolve) {

        $("#DelModal .modal-body").html(message);
        $('#DelModal').modal('show');

        $('#DelModal .GMCCOK').click(function () {
            resolve(true);
            setTimeout(function () { $('#DelModal').modal('hide'); }, 50)
        });
        $('#DelModal .GMCCRJCT').click(function () {
            resolve(false);
            $('#DelModal').modal('hide');
        });

        $('#DelModal').on('hidden.bs.modal', function () {
            resolve(false);
        });
    });
}