using System.Web.Http;

using Rxn.Services;
using Rxn.WebApi.Helpers;
using Rxn.WebApi.Models;

namespace Rxn.WebApi.Controllers
{
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public Response GetItemMaster(IdentityModel model)
        {
            var itemMaster = _productService.GetItemMaster(model.Id);

            if (itemMaster == null)
            {
                return Failed("Entity not found.");
            }

            return Success(itemMaster);
        }

        [HttpPost]
        public Response GetInventorySites(IdentityModel model)
        {
            var itemMaster = _productService.GetItemMasterInventories(model.Id);

            return Success(itemMaster);
        }

        [HttpPost]
        public Response CreateOrUpdate(ItemMasterModel model)
        {
            _productService.CreateOrUpdateItemMaster(model.ToItemMasterDto());

            return Success();
        }

        [HttpGet]
        public Response ProductNumbers()
        {
            return Success(_productService.GetProductNumbers());
        }
    }
}