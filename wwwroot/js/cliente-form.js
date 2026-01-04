$(document).ready(function () {
    
    // --- INICIO DE LA CORRECCIÓN DE VALIDACIÓN ---
    // Forzamos a que las reglas de minlength y maxlength de jQuery Validation
    // usen el mismo mensaje que nuestra data annotation. Esto evita el doble mensaje.
    if ($.validator && $.validator.unobtrusive) {
        var validator = $('form').validate();
        if (validator) {
            // Asegurarse de que las reglas para el campo CUIT existan antes de modificarlas
            if (!validator.settings.rules.CUIT) {
                validator.settings.rules.CUIT = {};
            }
            validator.settings.rules.CUIT.minlength = 11;
            validator.settings.rules.CUIT.maxlength = 11;

            // Asegurarse de que los mensajes para el campo CUIT existan antes de modificarlos
             if (!validator.settings.messages.CUIT) {
                validator.settings.messages.CUIT = {};
            }
            validator.settings.messages.CUIT = {
                minlength: "El CUIT debe tener 11 caracteres.",
                maxlength: "El CUIT debe tener 11 caracteres.",
                rangelength: "El CUIT debe tener 11 caracteres."
            };
        }
    }
    // --- FIN DE LA CORRECCIÓN DE VALIDACIÓN ---

    function cleanCuit(cuitValue) {
        if (!cuitValue) return '';
        return cuitValue.replace(/[^0-9]/g, ''); // Elimina cualquier caracter que no sea un número
    }

    // Eventos para el campo CUIT  
    $('#cuitInput').on('keypress', function (e) {
        // Permitir solo dígitos (0-9)
        if (e.which < 48 || e.which > 57) {
            e.preventDefault();
        }
    });

    $('#cuitInput').on('paste', function (e) {
        setTimeout(() => {
            // Esperar a que el pegado termine antes de limpiar el valor
            var cleanedValue = cleanCuit($(this).val());
            $(this).val(cleanedValue);
        }, 0);
    });

    $('#cuitInput').on('blur', function () {
        // Limpiar el valor en blur, por si se escribió algo no numérico o se pegó sin guiones
        var cleanedValue = cleanCuit($(this).val());
        $(this).val(cleanedValue);
        // Forzar la revalidación en el blur para que el mensaje de error aparezca/desaparezca
        $(this).valid();
    });


    // El evento ahora se asocia al clic del nuevo botón 'validateCuitBtn'.
    $('#validateCuitBtn').on('click', function () {
        var cuit = cleanCuit($('#cuitInput').val());
        $('#cuitInput').val(cuit);

        var razonSocialInput = $('#razonSocialInput');
        var statusDiv = $('#razonSocialStatus');

        razonSocialInput.val('');
        statusDiv.html('');

        // Forzamos la validación completa del campo CUIT antes de la llamada AJAX
        if ($('#cuitInput').valid()) { 
            statusDiv.html('<span class="text-info">Buscando Razón Social...</span>');

            $.ajax({
                url: '/api/ClienteApi/GetNombrePorCuit', // Usar URL relativa directa
                type: 'GET',
                data: { cuit: cuit },
                success: function (response) {
                    razonSocialInput.val(response.name);
                    statusDiv.html('');
                },
                error: function (xhr) {
                    var errorMessage = "No se pudo contactar al servicio.";
                    if (xhr.responseJSON && xhr.responseJSON.message) {
                        errorMessage = xhr.responseJSON.message;
                    }
                    statusDiv.html('<span class="text-danger">' + errorMessage + '</span>');
                }
            });
        }
    });
});
