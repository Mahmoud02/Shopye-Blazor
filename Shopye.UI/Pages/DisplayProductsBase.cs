using Microsoft.AspNetCore.Components;
using Shopye.Models.Dtos;

namespace Shopye.UI.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    
    }
}
