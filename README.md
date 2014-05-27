MVC Async Validation
====================

This plugin provides ajax server side validation for ASP.MVC applications. You can avoid complex client side validation and instead of that you can perform only server side validation in client side style. Validations are done via ajax calls, so validations still  appears like they are done on client side. This plugin also includes special mvc validation attributes introduced in: https://github.com/milosmitrovic/Special-MVC-Validation-Attributes 

##How To Use

###Model

            public class FormModel 
            {
                [Required(ErrorMessage = "Text1 Error Message")]
                public string Text1 { get; set; }
            }


###View

            @model ExampleWebApp.Models.FormModel
            
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
      
    
###Controller

    public class FormController : Controller
    {
        //
        // GET: /Form/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new FormModel());
        }
        
        [HttpPost]
        public ActionResult Index(FormModel model)
        {
            return View(model);
        }
    }
    
    
###Validation Controller

    public class ValidationController : Controller
    {
        [HttpPost]
        public JsonResult Index(FormModel model)
        {
            return this.GetModelErrors<FormModel>(model);
        }
     }
     
     
     
     
##Plugin Options

            //When no options are provided for plugin, these will be considered as default values
             var async_val = new $.async_validation({
             
               form_selector: 'form',
               validation_url: '/Validation',
               input_validation_trigger : 'change keyup paste blur'
               
             });
     
     
     
     



