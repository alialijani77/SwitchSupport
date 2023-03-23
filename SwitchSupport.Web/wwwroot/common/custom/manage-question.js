function DeleteQuestion(questionId) {
    Swal.fire({
        title: 'آیا از حذف مورد انتخابی اطمینان دارید؟',
        text: "شما در حال حذف می باشید!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف کن!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/question/DeleteQuestion",
                type: "post",
                data: {
                    questionId: questionId
                },
                success: function (response) {
                    if (response.status === "error") {
                        Swal.fire(
                            'خطا',
                            response.msg,
                            'error'
                        )
                    }
                    else {
                        $(`#question-row-${questionId}`).fadeOut(500);
                        Swal.fire(
                            'موفق',
                            response.msg,
                            'success')
                    }
                },
                error: function () {
                    Swal.fire(
                        'خطا',
                        response.msg,
                        'error'
                    )
                }
            });

        }
    })
}






function changeQuestionIsCheckedStatus(questionId) {
    Swal.fire({
        title: 'آیا از مورد انتخابی اطمینان دارید؟',
        text: "شما در حال تغییروضعیت می باشید!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله انجام بده!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/question/changeQuestionIsCheckedStatus",
                type: "post",
                data: {
                    questionId: questionId
                },
                success: function (response) {
                    if (response.status === "error") {
                        Swal.fire(
                            'خطا',
                            response.msg,
                            'error'
                        )
                    }
                    else {
                        let element = $(`#question-is-checked-status-${questionId}`);
                        element.removeClass("danger").addClass("success");
                        element.html("بررسی شده");
                        $(`#question-is-checked-button-${questionId}`).css("display", "none");
                        Swal.fire(
                            'موفق',
                            response.msg,
                            'success')
                    }
                },
                error: function () {
                    Swal.fire(
                        'خطا',
                        response.msg,
                        'error'
                    )
                }
            });

        }
    })
}