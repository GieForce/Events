$(document).ready(function () {
    console.log('jsfile loaded');
    $('#firstDd').change(function () {
        var value = $(this).val();
        if (value) {
            console.log(value);
            var $secondDd = $('#secondDd');
            $('option', $secondDd).prop("disabled", true);
            $('option[data-display-on*="' + value + '"]', $secondDd).prop("disabled", false);
            $('#secondDd').selectpicker('refresh');
        } else {
            $('option', $secondDd).prop("disabled", false);
            $('#secondDd').selectpicker('refresh');
        }
        $($secondDd).val(null);
    })
    $('#firstDd').change(); 
});