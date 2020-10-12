function SweetConfirm(title, msg) {
    return new Promise((resolve) => {
        Swal.fire({
            title: title,
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: '刪除'
        }).then((result) => {
            if (result.isConfirmed) {
                result.value ? resolve(true) : resolve(false);
            }
        });
    });
}