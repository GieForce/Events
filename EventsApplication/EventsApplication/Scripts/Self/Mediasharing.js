
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


    $("#CreatePost").on("click",
        function () {
            $("#CreatePost").html("");
            $loading.show();
            $.ajax({
                url: "/Mediasharing/CreatePost",
                    type: "GET"
                })
                .done(function (partialViewResult) {
                    $("#CreatePost").html(partialViewResult);
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
    $("#CreateNewBericht").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            $.ajax({
                    url: "/Mediasharing/CreateNewMediaBericht",
                    type: "GET"
                })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });

    $(".ShowCommentsById").on("click",
        function () {
            var $targetItem = $(this).attr("data-item");
            var $loadingcomments = $("#loadingcomments-" + $targetItem).hide();
            var $classname = $(".showComments" + "-" + $targetItem).attr("data-comment");

            var self = $(this);

            $(".showComments-" + $classname).html("");
            $loadingcomments.show();
            $.ajax({
                    url: "/Mediasharing/LoadBerichtenByPostId/" + $targetItem,
                    type: "GET"
                })
                .done(function (partialViewResult) {
                    $(".showComments-" + $classname).html(partialViewResult);
                    $loadingcomments.hide();
                    self.hide();
                });
        });
}); 

