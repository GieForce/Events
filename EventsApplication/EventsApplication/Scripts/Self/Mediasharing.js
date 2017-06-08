
$(document).ready(function () {
    var $loading = $("#loading").hide();
    
    $("#ShowPosts").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            $.ajax({
                url: "/Mediasharing/ShowPosts",
                type: "GET"
            })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });
    $("#ShowPostsById").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            $.ajax({
                url: "/Mediasharing/ShowPosts",
                type: "GET"
            })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });
    $("#ShowBijdragesByUserID").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            $.ajax({
                url: "/Mediasharing/ShowBijdragesByUserID",
                type: "GET"
            })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });
    $("#ShowAccount").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            $.ajax({
                url: "/Mediasharing/ShowAccountByID",
                type: "GET"
            })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });
});

