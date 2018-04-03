$(document).ready(function() {
    console.log('Pagina geladen, DOM klaar voor gebruik.');
    function loadcell(x, y) {
        $('.Map div:last').after('<div></div>');
    }

    $('.Map').ready(loadcell(0, 0));
});



