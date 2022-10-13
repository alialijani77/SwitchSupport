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