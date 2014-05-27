MVC Async Validation
====================

This plugin provides ajax server side validation for ASP.MVC applications. You can avoid complex client side validation and instead of that you can perform only server side validation in client side style. Validations are done via ajax calls, so validations still  appears like they are done on client side.

##How To Use

###Index.cshtml

        @using (Html.BeginForm())
        {
          <div>
              @Html.TextBoxFor(model => model.Text1)
              @Html.ValidationMessageFor(model => model.Text1)
          </div>
          
           <input type="submit" value="Submit"  />
         }
         
         
         
    <script src="~/Scripts/jquery.async.validation.js"></script>
    <script type="text/javascript">
    
      $(function () {
     
           var async_val = new $.async_validation();
    
            //Event that is fired for every form element that has validation error
            $(async_val).bind('onAsyncValidation', function (event, sender, message) {
                  alert($(sender).attr('name') + ' : ' + message);
            });

            //Event that is fired if form has any errors
            $(async_val).bind('onFormSubmitValidation', function (event, form, errors) {
                alert("This form has errors");
            });
            
            //Event that is fired for each element that needs validated
             $(async_val).bind('onBeforeAsyncValidation', function (event, sender) {
             
             });
            
      </script>
      
    
###Model

            public class FormModel 
            {
                [Required(ErrorMessage = "Text1 Error Message")]
                public string Text1 { get; set; }
            }

