using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mUtility.Validation;
using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class FormModel 
    {
        public FormModel()
        {
            LoadListData();
        }

        [Required(ErrorMessage = "Text1 Error Message")]
        public string Text1 { get; set; }

        [Required(ErrorMessage = "Text2 Error Message")]
        public string Text2 { get; set; }

        [Required(ErrorMessage = "Text3 Error Message")]
        public string Text3 { get; set; }

        [Required(ErrorMessage = "Select1 Error Message")]
        public string Select1 { get; set; }

        public IEnumerable<SelectListItem> Select1Options { get; set; }

        private void LoadListData()
        {
            this.Select1Options = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="Item1", Value="Item1"},
                new SelectListItem(){ Text="Item2", Value="Item2"},
                new SelectListItem(){ Text="Item3", Value="Item3"},
                new SelectListItem(){ Text="Item4", Value="Item4"}
            };
        }

    }
}