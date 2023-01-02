

function AnswerQuestionFormDone(response) {

    if (response.status === "success") {
        Swal.fire(
            'اعلان',
            'پاسخ شما با موفقیت ثبت شد .',
            'success'
        )
    }
    else if (response.status === "empty") {
        Swal.fire(
            'هشدار',
            'متن پاسخ شما نمی تواند خالی  باشد .',
            'warning'
        )
    }
    else if (response.status === "error") {
        Swal.fire(
            'خطا',
            'خطایی رخ داده است لطفا مجدد تلاش کنید .',
            'error'
        )
    }
}