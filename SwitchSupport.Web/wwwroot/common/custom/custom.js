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


