/* ASYNC MVC VALIDATTION JQuery PLUGIN
*
* Developed by Milos Mitrovic
*/
; (function ($) {

    $.async_validation = function (options) {

        var defaults = {
            form_selector: 'form',
            validation_url: '/Validation', 
            input_validation_trigger : 'change keyup paste blur'
        }

        var plugin = this;

        plugin.settings = {}

        var init = function () {
            plugin.settings = $.extend({}, defaults, options);

            if (options !== undefined) {

                if (options.form_selector !== undefined && options.form_selector) {
                    plugin.form_selector = options.form_selector;
                }

                if (options.validation_url !== undefined && options.validation_url) {
                    plugin.validation_url = options.validation_url;
                }

                if (options.input_validation_trigger !== undefined && options.input_validation_trigger) {
                    plugin.input_validation_trigger = options.input_validation_trigger;
                }
            }


            if (plugin.settings.validation_url) {

                $.each($(plugin.settings.form_selector), function (form_index, current_form) {

                    $.each($(current_form).find('input'), function (element_index, input_element) {
                        $(input_element).bind(plugin.settings.input_validation_trigger, function () {
                            plugin.validate_async(current_form, input_element, plugin.settings.validation_url);
                        });
                    });

                    $.each($(current_form).find('select'), function (element_index, selcet_element) {
                        $(selcet_element).bind('change', function () {
                            plugin.validate_async(current_form, selcet_element, plugin.settings.validation_url);
                        });
                    });
                    
                    $(current_form).bind('submit.async_validation', function (e) {
                        return validate_on_submit(current_form, plugin.settings.validation_url);
                    });
                });
            }
        }

        var validate_on_submit = function (current_form, validation_url) {

            var request = $.ajax({
                url: validation_url,
                data: $(current_form).serialize(),
                type: 'POST',
                success: function (data) {
                    if (data.Errors.length > 0) {

                        var errors = data.Errors;
                        $(plugin).trigger("onFormSubmitValidation", [$(current_form), errors]);
                        return false;
                    } else {

                        $(current_form).unbind('submit.async_validation');
                        $(current_form).submit();
                    }
                },
                error: function (request, status, error) {

                       $(current_form).unbind('submit.async_validation');
                       $(current_form).submit();
                },
                async: false
            });

            return request.responseJSON ? request.responseJSON.Errors.length == 0 : false;
        }

        plugin.validate_async = function (current_form, sender, validation_url) {

            var deferred = $.post(validation_url, $(current_form).serialize());

            deferred.promise().done(function (data) {

            if(data){
                    $.each(data.Errors, function (error_index, error) {
                        $(plugin).trigger("onBeforeAsyncValidation", [sender]);
                        if ($(sender).attr('name') == error.FieldName) {
                            var error_message = error.ErrorMessage;
                            $(plugin).trigger("onAsyncValidation", [sender, error_message]);
                            return false;
                        }
                    });
                }else{
                  $(plugin).trigger("onBeforeAsyncValidation", [sender]);
                }
            });
        }

        init();
    }

})(jQuery);