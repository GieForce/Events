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
    $("#Reportedposts").on("click",
        function () {
            $("#AdminPanel").html("");
            $loading.show();
            $.ajax({
                    url: "/Mediasharing/AdminPanel",
                    type: "GET"
                })
                .done(function (partialViewResult) {
                    $("#AdminPanel").html(partialViewResult);
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

    $("#btnVoegToe").on("click",
        function () {
     //       $("#CreateNewCategorie").html("");
       //     $loading.show();
            $.ajax({
                    url: "/Mediasharing/CreateNewCategorie",
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $("#CreateNewBericht").html(partialViewResult);
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
    $(".addPost").on("click",
        function (event) {

            event.preventDefault();

            var $targetItem = $(this).attr("data-item");
            var $classname = $(".showComments" + "-" + $targetItem).attr("data-comment");
            var $text = $(".postText" + "-" + $targetItem).val();

            $(".showComments-" + $classname).html("");
            $.ajax({
                    url: "/Mediasharing/CreateComment",
                    data: {
                        'id': $targetItem,
                        'text': $text
                    },
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $(".showComments-" + $classname).html(partialViewResult);
                });
        });

    $(".delete").on("click",
        function () {
            //         $("#showPosts").html("");
            //      $loading.show();
            var $targetItem = $(this).attr("data-item");
            $.ajax({
                    url: "/Mediasharing/DeletePosts/" + $targetItem,
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    //$loading.hide();
                });
        });

    $(".Admindelete").on("click",
        function () {
            //         $("#showPosts").html("");
            //      $loading.show();
            var $targetItem = $(this).attr("data-item");
            $.ajax({
                    url: "/Mediasharing/AdminDeletePosts/" + $targetItem,
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $("#Reportedposts").html(partialViewResult);
                    //$loading.hide();
                });
        });

    $(".likePost").on("click",
        function () {
                     $("#showPosts").html("");
                  $loading.show();
            var $targetItem = $(this).attr("data-item");
            $.ajax({
                    url: "/Mediasharing/GiveALike/" + $targetItem,
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });


    $(".reportPost").on("click",
        function () {
            $("#showPosts").html("");
            $loading.show();
            var $targetItem = $(this).attr("data-item");
            $.ajax({
                    url: "/Mediasharing/Report/" + $targetItem,
                    type: "POST"
                })
                .done(function (partialViewResult) {
                    $("#showPosts").html(partialViewResult);
                    $loading.hide();
                });
        });


}); 
