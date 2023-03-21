function loadTagsModal(url) {
    $.ajax({
        url: url,
        type: "get",
        //beforeSend: function () {
        //    StartLoading("#LargeModal");
        //},
        success: function (response) {
           /* EndLoading("#LargeModalBody")*/;
            $("#LargeModalBody").html(response);
            $("#LargeModalLabel").html(`<span>مدیریت تگ ها</span>
                                        <button onclick="createTagModal()" class="btn btn-success btn-xs mr-5">افزودن تگ جدید</button>
            `);
            $("#LargeModal").modal("show");
        },
        error: function () {
            /*EndLoading();*/
            Swal.fire(
                'خطا',
                'عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .',
                'error'
            )
        }
    });
}
function createTagModal(url) {
    $.ajax({
        url: "/admin/home/loadCreateTagModal",
        type: "get",
        //beforeSend: function () {
        //    StartLoading("#LargeModal");
        //},
        success: function (response) {
           /* EndLoading("#LargeModalBody")*/;
            $("#MediumModalLabel").html("افزودن تگ جدید");
            $("#MediumModalBody").html(response);
            $('#create-tag-form').removeData('validator', 'unobtrusiveValidation');
            $.validator.unobtrusive.parse('#create-tag-form');
            $("#MediumModal").modal("show");
        },
        error: function () {
            /*EndLoading();*/
            Swal.fire(
                'خطا',
                'عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .',
                'error'
            )
        }
    });
}


function createTag(response) {
    if (response.status === "error") {
        Swal.fire(
            'خطا',
            response.msg,
            'error'
        )
    }
    else {
        $("#MediumModal").modal("hide");
        $('#filter_ajax_form').submit();
        Swal.fire(
            'موفق',
            response.msg,
            'success')
    }
}



function SubmitFilterFormAjaxPagination(pageId) {
    $("#CurrentPage").val(pageId);
    $("#filter_ajax_form").submit();
}