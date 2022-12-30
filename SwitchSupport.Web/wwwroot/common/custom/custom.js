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


function SubmitQuestionForm() {
    $("#filter_form").submit();
}

function SubmitTagForm() {
    $("#filter_form").submit();
}

function SubmitFilterFormPagination(pageId) {
    $("#CurrentPage").val(pageId);
    $("#filter_form").submit();
}

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
    
    
        
                        

