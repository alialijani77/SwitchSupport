$("#CountryId").on("change", function loadcity() {
    var countryId = $("#CountryId").val();
    if (countryId !== '' && countryId.length) {
        $.ajax({
            url: $("#CountryId").attr("data-url"),
            type: "get",
            data: {
                countryId: countryId
            },
            success: function (response) {
                $("#CityId option:not(:first)").remove();
                $("#CityId").prop("disabled", false);
                for (var city of response) {
                    $("#CityId").append(`<option value="${city.id}">${city.title}</option>`);
                }
            },
            error: function () {
                swal({
                    title: "خطا",
                    text: "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .",
                    icon: "error",
                    button: "باشه"
                });
            }
        })
    }
    else {
        $("#CityId option:not(:first)").remove();
        $("#CityId").prop("disabled", true);
    }
});


function LoadUrl(url) {
    location.href = url;
}

var datepickers = document.querySelectorAll(".datepicker");
if (datepickers.length) {
    for (datepicker of datepickers) {
        var id = $(datepicker).attr("id");
        kamaDatepicker(id, {
            placeholder: 'مثال : 1400/01/01',
            twodigit: true,
            closeAfterSelect: false,
            forceFarsiDigits: true,
            markToday: true,
            markHolidays: true,
            highlightSelectedDay: true,
            sync: true,
            gotoToday: true
        })
    }
}

$(function () {
    if ($("#CountryId").val() === '') {
        $("#CityId").prop("disabled", true)
    }
});

var editorsArray = [];
var editors = document.querySelectorAll(".editor");
if (editors.length) {
    console.log("ali")
    $.getScript("/common/ckeditor/build/ckeditor.js", function (data, textStatus, jqxhr) {
        for (editor of editors) {
            ClassicEditor
                .create(editor, {

                    licenseKey: '',
                    simpleUpload: {
                        // The URL that the images are uploaded to.
                        uploadUrl: '/Home/UploadCkeditor'
                    }
                })
                .then(editor => {
                    window.editor = editor;
                    editorsArray.push(editor);
                })
                .catch(error => {
                    console.log(error);
                });
        }
    });

}

function SubmitFilterFormPagination(pageId) {
    $("#CurrentPage").val(pageId);
    $("#filter_form").submit();
}

function SubmitQuestionForm() {
    $("#filter_form").submit();
}

function SubmitTagForm() {
    $("#filter_form").submit();
}
function AnswerQuestionFormDone(response) {

    if (response.status === "success") {
        Swal.fire(
            'اعلان',
            'پاسخ شما با موفقیت ثبت شد .',
            'success'
        )
        $("#AnswersBox").load(location.href + " #AnswersBox");

        //$('html, body').animate({
        //    scrollTop: $("#AnswersBox").offset().top
        //}, 1000);
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
    for (var editor of editorsArray) {
        editor.setData('');
    }



}

//var magicsuggests = document.querySelectorAll(".magicsuggest");
//if (magicsuggests.length) {
//    $('head').append($('<link rel="stylesheet" type="text/css" />').attr('href', '/common/magicsuggest/magicsuggest.css'));
//    $.getScript("/common/magicsuggest/magicsuggest.js", function (data, textStatus, jqxhr) {
//        for (magicsuggest of magicsuggests) {
//            $(`#${magicsuggest.id}`).magicSuggest({
//                method: 'get',
//                queryParam: 'name',
//                data: 'get-tags',
//                minChars: 2,
//                    placeholder: 'لطفا تگ های مورد نظر خود را وارد کنید',
//                    style: 'direction: ltr !important',
//                    minCharsRenderer: function (v) {
//                        return 'لطفا حداقل 2 کاراکتر را وارد کنید';
//                    },
//                    noSuggestionText: '{{name}} وارد شده در پیشنهادات سایت موجود نیست',
//            });
//        }
//    });
//}
function SelectTrueAnswer(answerId) {
    var token = $("input[name = __RequestVerificationToken]").val();

    $.ajax({
        url:"/SelectTrueAnswer",
        type: "post",
        data: {
            answerId: answerId,
            __RequestVerificationToken : token
        },
        success: function (response) {
            if (response.status === "Success") {
                Swal.fire(
                    'اعلان',
                    'پاسخ شما با موفقیت ثبت شد .',
                    'success'
                )
                $("#AnswersBox").load(location.href + " #AnswersBox");
            }
            else if (response.status === "NotAuth") {             
                Swal.fire(
                    'خطا',
                    'خواهشمند است وارد سامانه شوید .',
                    'error'
                )
            }
            else if (response.status === "NotAccess") {              
                Swal.fire(
                    'خطا',
                    'شما دسترسی به این عملیات را ندارید.',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}




function ScoreUpForAnswer(answerId) {
    var token = $("input[name = __RequestVerificationToken]").val();


    $.ajax({
        url: "/ScoreUpForAnswer",
        type: "post",
        data: {
            answerId: answerId,
            __RequestVerificationToken: token
        },
        success: function (response) {
            if (response.status === "success") {
                Swal.fire(
                    'اعلان',
                    'امتیاز شما با موفقیت ثبت شد .',
                    'success'
                )
                $("#AnswersBox").load(location.href + " #AnswersBox");
            }
            else if (response.status === "MinScoreForUpScoreAnswer") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }
            else if (response.status === "MinScoreForDownScoreAnswer") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }

            else if (response.status === "IsExistsUserScoreForScore") {
                Swal.fire(
                    'خطا',
                    'قبلا امتیاز ثبت گردیده است .',
                    'error'
                )
            }

            else if (response.status === "error") {
                Swal.fire(
                    'خطا',
                    'مشکلی پیش آمده است . مجددا تلاش نمایید .',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}

function ScoreDownForAnswer(answerId) {
    var token = $("input[name = __RequestVerificationToken]").val();


    $.ajax({
        url: "/ScoreDownForAnswer",
        type: "post",
        data: {
            answerId: answerId,
            __RequestVerificationToken: token
        },
        success: function (response) {
            if (response.status === "success") {
                Swal.fire(
                    'اعلان',
                    'امتیاز شما با موفقیت ثبت شد .',
                    'success'
                )
                $("#AnswersBox").load(location.href + " #AnswersBox");
            }
            else if (response.status === "MinScoreForUpScoreAnswer") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }
            else if (response.status === "MinScoreForDownScoreAnswer") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }

            else if (response.status === "IsExistsUserScoreForScore") {
                Swal.fire(
                    'خطا',
                    'قبلا امتیاز ثبت گردیده است .',
                    'error'
                )
            }

            else if (response.status === "error") {
                Swal.fire(
                    'خطا',
                    'مشکلی پیش آمده است . مجددا تلاش نمایید .',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}




function ScoreUpForQuestion(questionId) {
    var token = $("input[name = __RequestVerificationToken]").val();


    $.ajax({
        url: "/ScoreUpForQuestion",
        type: "post",
        data: {
            questionId: questionId,
            __RequestVerificationToken: token
        },
        success: function (response) {
            if (response.status === "success") {
                Swal.fire(
                    'اعلان',
                    'امتیاز شما با موفقیت ثبت شد .',
                    'success'
                )
                $("#questindetails").load(location.href + " #questindetails");

            }
            else if (response.status === "MinScoreForUpScoreQuestion") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }
            else if (response.status === "MinScoreForDownScoreQuestion") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }

            else if (response.status === "IsExistsUserScoreForQuestion") {
                Swal.fire(
                    'خطا',
                    'قبلا امتیاز ثبت گردیده است .',
                    'error'
                )
            }

            else if (response.status === "error") {
                Swal.fire(
                    'خطا',
                    'مشکلی پیش آمده است . مجددا تلاش نمایید .',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}


function ScoreDownForQuestion(questionId) {
    var token = $("input[name = __RequestVerificationToken]").val();


    $.ajax({
        url: "/ScoreDownForQuestion",
        type: "post",
        data: {
            questionId: questionId,
            __RequestVerificationToken: token
        },
        success: function (response) {
            if (response.status === "success") {
                Swal.fire(
                    'اعلان',
                    'امتیاز شما با موفقیت ثبت شد .',
                    'success'
                )
                $("#questindetails").load(location.href + " #questindetails");
            }
            else if (response.status === "MinScoreForUpScoreQuestion") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }
            else if (response.status === "MinScoreForDownScoreQuestion") {
                Swal.fire(
                    'خطا',
                    'امتیاز شما کافی نمی باشد. ',
                    'error'
                )
            }

            else if (response.status === "IsExistsUserScoreForQuestion") {
                Swal.fire(
                    'خطا',
                    'قبلا امتیاز ثبت گردیده است .',
                    'error'
                )
            }

            else if (response.status === "error") {
                Swal.fire(
                    'خطا',
                    'مشکلی پیش آمده است . مجددا تلاش نمایید .',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}


function AddQuestionToBookmark(questionId) {
    var token = $("input[name = __RequestVerificationToken]").val();


    $.ajax({
        url: "/AddQuestionToBookmark",
        type: "post",
        data: {
            questionId: questionId,
            __RequestVerificationToken : token
        },
        success: function (response) {
            if (response.status === "success") {
                Swal.fire(
                    'اعلان',
                    'با موفقیت ثبت شد .',
                    'success'
                )
                $("#questindetails").load(location.href + " #questindetails");
            }                     
            else if (response.status === "error") {
                Swal.fire(
                    'خطا',
                    'مشکلی پیش آمده است . مجددا تلاش نمایید .',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'خطا',
                'شما دسترسی به این عملیات را ندارید.',
                'error'
            )
        }
    })
}

