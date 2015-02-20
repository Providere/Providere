$(function() {

	$('.IdRubro').change(function(event) {
		var rubro = $(this);

		$.ajax({
			url: '/SubRubro/Lista/'+rubro.find(":selected").val(),
			type: 'POST',
			success: function(msg){

				var msg = jQuery.parseJSON( msg );

					var contenedorTotal = rubro.parents('.combo-rubro-subrubro');
					var selectConSubRubros = contenedorTotal.find('.subrubros-select');
					var contenedorSelectConSubRubros = contenedorTotal.find('.subrubros-container');
					var contenedorUbicacion = contenedorTotal.find('.ubicacion-container');
					var contenedorRubros = contenedorTotal.find('.rubros-container');

				if (msg.length > 0) {

					var selectConRubros = '<select class="form-control" id="IdSubRubro" name="IdSubRubro">';

					msg.forEach(function(element, index){
						selectConRubros += '<option value="'+element['Id']+'">'+element['Nombre']+'</option>';
					});

					selectConRubros += '</select>';

					selectConSubRubros.html(selectConRubros);	

					if (selectConSubRubros.data('estilo') == 'vertical') {
						contenedorSelectConSubRubros.slideDown();	
					}else{

						contenedorUbicacion.removeClass('col-md-5').addClass('col-md-4');
						contenedorRubros.removeClass('col-md-5').addClass('col-md-3');	
						contenedorSelectConSubRubros.fadeIn();	
					}

				} else {
					
					if (selectConSubRubros.data('estilo') == 'vertical') {

						contenedorSelectConSubRubros.slideUp();	

					}else{

						if (contenedorSelectConSubRubros.is(':visible')) {
							contenedorSelectConSubRubros.stop().hide('fade', 250);
							contenedorUbicacion.removeClass('col-md-4').addClass('col-md-5');
							contenedorRubros.removeClass('col-md-3').addClass('col-md-5');

						};
					}
					selectConSubRubros.html('<select class="form-control disabled hidden" disabled="disabled" id="IdSubRubro" name="IdSubRubro"><option value="0" selected></option></select>');
					
				}

			}
		}).done(function(msg) {
			console.log("data "+msg);
		})



	
    });


$(window).scroll(function () {
    if ($(document).scrollTop() > 150) {
        $('.header').addClass('shrink');
    } else {
        $('.header').removeClass('shrink');
    }
});

});