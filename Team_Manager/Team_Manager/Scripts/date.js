
$(function () {
    // This will make every element with the class "date-picker" into a DatePicker element
    $('.date-picker').datepicker();
    
    $('.date-picker').datepicker("setDate", new Date());
})
