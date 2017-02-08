using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Merchant;
using WM.Merchant.Models;
using WM.Merchant.Helpers;

namespace WM.Merchant.Web.Controllers
{
    public class OrderController : Controller
    {
        protected WMService _service = null;

        public WMService Service
        {
            get {
                if (_service == null)
                {
                    _service = new WMService();
                }

                return _service;
            }
        }

        // GET: Order
        public ActionResult Create()
        {
            
            var model = createSampleOrderRequest();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateOrderRequest model)
        {
            var response = Service.CreateOrder(model);
            if (!response.IsError())

            ViewData["Response"] = response;
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(string transactionID, string status)
        {
            var model = new UpdateOrderRequest();
            var modelView = new ViewOrderRequest();
            var modelViewResponse = new WMResponseHandler<ViewOrderResponse>();

            model.TransactionID = transactionID;
            model.status = status;

            var response = Service.UpdateOrder(model);

            if (!response.IsError())
            {
                modelViewResponse.Object = new ViewOrderResponse();
                modelViewResponse.Object.AdditionalInfo = response.Object.AdditionalInfo;
                modelViewResponse.Object.CustomerAddress = response.Object.CustomerAddress;
                modelViewResponse.Object.CustomerEmail = response.Object.CustomerEmail;
                modelViewResponse.Object.CustomerGender = response.Object.CustomerGender;
                modelViewResponse.Object.CustomerName = response.Object.CustomerName;
                modelViewResponse.Object.CustomerPhone = response.Object.CustomerPhone;
                modelViewResponse.Object.Description = response.Object.Description;
                modelViewResponse.Object.InvoiceID = response.Object.InvoiceID;
                modelViewResponse.Object.Status = response.Object.Status;
                modelViewResponse.Object.TotalAmount = response.Object.TotalAmount;
                modelViewResponse.Object.TransactionID = response.Object.TransactionID;

                modelView.MerchantTransactionID = response.Object.mTransactionID;
                ViewData["Response"] = modelViewResponse;
            }
            
            return View("Detail", modelView);
        }


        public ActionResult Detail()
        {
            var model = new ViewOrderRequest();            

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(ViewOrderRequest model)
        {
            var response = Service.ViewOrder(model);
            ViewData["Response"] = response;
            return View(model);
        }

        public ActionResult Success()
        {
            return actionResult("success", WMService.SUCCESS_STATUS);
        }

        public ActionResult Failed()
        {
            return actionResult("failed", WMService.FAILED_STATUS);
        }

        public ActionResult Canceled()
        {
            return actionResult("canceled", WMService.CANCELED_STATUS);
        }

        protected ActionResult actionResult(string type, string status)
        {
            ViewBag.Type = type;
            string error = Service.ValidateResultURL(status);
            if (error != string.Empty)
            {
                ViewBag.ErrorMessage = error;
                return View("Error");
            }
            string mTransactionID = this.Request.Params.Get("transaction_id");
            ViewBag.MerchantTransactionID = mTransactionID;

            ViewData["Response"] = Service.ViewOrder(new ViewOrderRequest()
            {
                MerchantTransactionID = mTransactionID
            });

            return View("Result");
        }

        protected CreateOrderRequest createSampleOrderRequest()
        {

            return new CreateOrderRequest()
            {
                MerchantTransactionID = Guid.NewGuid().ToString(),
                CustomerName = "Nguyen Van A",
                CustomerAddress = "Ho Chi Minh City",
                CustomerGender = CreateOrderRequest.GENDER_MALE,
                CustomerEmail = "merchant@example.com",
                CustomerPhone = "012345678",
                Description = "Mua hàng tại cửa hàng ABC",
                TotalAmount = 100,
                ResultURL = Url.Action("Success", "Order", null, Request.Url.Scheme),
                CancelURL = Url.Action("Canceled", "Order", null, Request.Url.Scheme),
                ErrorURL = Url.Action("Failed", "Order", null, Request.Url.Scheme)
            };
        }
    }
}