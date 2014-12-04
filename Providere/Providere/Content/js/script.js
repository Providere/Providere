/*$(function() {

	$('#IdRubro').change(function(event) {
		var rubro = $(this);

		$.ajax({
			url: '/SubRubro/Lista/'+rubro.find(":selected").val(),
			type: 'POST',
			success: function(msg){

				var msg = jQuery.parseJSON( msg );

				if (msg.length > 0) {

					var selectConRubros = '<select class="form-control" id="IdSubRubro" name="IdSubRubro">';
					console.log("data "+msg);

					msg.forEach(function(element, index){
						selectConRubros += '<option value="'+element['Id']+'">'+element['Nombre']+'</option>';
					});

					selectConRubros += '</select>';

					$('#subrubros-select').html(selectConRubros);	

					if ($('#subrubros-select').data('animation') == 'slide') {
						$('#subrubros-container').slideDown();	
					}else{
						$('#subrubros-container').fadeIn();	
					}

				} else {
					if ($('#subrubros-select').data('animation') == 'slide') {
						$('#subrubros-container').slideUp();	
					}else{
						$('#subrubros-container').fadeOut();	
					}
					$('#subrubros-select').html('<select class="form-control disabled hidden" disabled="disabled" id="IdSubRubro" name="IdSubRubro"><option value="0" selected></option></select>');
					
				}

			}
		}).done(function(msg) {
			console.log("data "+msg);
		})



	});

});*/