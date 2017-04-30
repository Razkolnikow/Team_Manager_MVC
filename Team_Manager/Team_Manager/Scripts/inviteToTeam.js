$(document).ready(function() {
    $('#team-add-button').click(function (ev) {
        $('#add-to-team-form').hide();
        var userId = $('#user-id').val();
        $('#add-to-team-form').load('/user/invitetoteam' + '?userId=' + userId);
        $('#add-to-team-form').show(500);
    });
})