$(document).ready(function () {

  //  $("#getting-started")
  //.countdown("2017/07/01", function (event) {
  //    $(this).text(
  //      event.strftime('%D days %H:%M:%S')
  //    );
    //});
    
    $("#getting-started")
  .countdown($('#final-date').val(), function (event) {
      $(this).text(
        event.strftime('%D days %H:%M:%S')
      );
  });
})