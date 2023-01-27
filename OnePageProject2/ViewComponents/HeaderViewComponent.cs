using Microsoft.AspNetCore.Mvc;

namespace OnePageProject2.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
