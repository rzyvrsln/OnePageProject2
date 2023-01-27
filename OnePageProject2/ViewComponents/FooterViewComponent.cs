using Microsoft.AspNetCore.Mvc;

namespace OnePageProject2.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
