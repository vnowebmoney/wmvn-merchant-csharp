using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Merchant.Models;

namespace WM.Merchant.Web.Controllers
{
    public class ProductController : Controller
    {
        protected WMService _service = null;

        public WMService Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new WMService();
                }

                return _service;
            }
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            SetViewBag();
            var model = new CreateOrderRequest();
            model.TotalAmount = 1000;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Pay(CreateOrderRequest model)
        {            
            SetViewBag(model.CustomerGender);
            model.MerchantTransactionID = Guid.NewGuid().ToString();

            model.TotalAmount = 1000;

            model.Description = "Mua hàng tại cửa hàng ABC";            
            model.ResultURL = Url.Action("Success", "Product", null, Request.Url.Scheme);
            model.CancelURL = Url.Action("Cancel", "Product", null, Request.Url.Scheme);
            model.ErrorURL = Url.Action("Failed", "Product", null, Request.Url.Scheme);

            var response = Service.CreateOrder(model);
            Log.Error("response {0}", response.IsError());

            if (!response.IsError())
            {
                Log.Error("response.Object.RedirectURL {0}", response.Object.RedirectURL);
                return Redirect(response.Object.RedirectURL);
            }
            return View(model);
            
        }
        
        public ActionResult Failed()
        {   
            return View();
        }
        
        public ActionResult Success()
        {  
            return View();
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            return View();
        }
               
        [HttpPost]
        public ActionResult Cancel(string transaction_id, string checksum)
        {
            CreateOrderRequest model = new CreateOrderRequest();
            model.MerchantTransactionID = Guid.NewGuid().ToString();

            model.Description = "Mua hàng tại cửa hàng ABC -- test off POST Method";
            model.TotalAmount = 100000;
            model.ResultURL = Url.Action("Success", "Product", null, Request.Url.Scheme);
            model.CancelURL = Url.Action("Cancel", "Product", null, Request.Url.Scheme);
            model.ErrorURL = Url.Action("Failed", "Product", null, Request.Url.Scheme);

            var response = Service.CreateOrder(model);
            if (!response.IsError())
            {
                return Redirect(response.Object.RedirectURL);
            }
            return View(model);
        }
            
        public void SetViewBag(string CustomerGender = "M")
        {
            var statusOption = new List<SelectListItem>
            {
                new SelectListItem { Selected=false, Text="Male", Value="M"},
                new SelectListItem { Selected=false, Text="Female", Value="F"}
            };
            ViewBag.CustomerGender = new SelectList(statusOption, "Value", "Text", CustomerGender);
            
        }
    }
}