using AspNetCoreMvcModalForm.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcModalForm.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ModalForm(string modalId)
        {
            var htmlModal = Helper.RenderRazorViewToString(this, "_ModalForm");
            htmlModal = htmlModal.Replace("[id]", modalId);
            return Json(htmlModal);
        }
    }
}
