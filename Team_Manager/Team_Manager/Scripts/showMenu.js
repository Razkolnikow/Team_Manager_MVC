$(document).ready(function loadMenu() {
    var button = $('.menu-button');
    console.log(button);
    button.click(function (ev) {
        if ($(ev.target).parent().next('.menu').css('display') === 'none') {
            $(ev.target).parent().next('.menu').show(400);
        } else {
            $(ev.target).parent().next('.menu').hide(400);
        }
    });
})