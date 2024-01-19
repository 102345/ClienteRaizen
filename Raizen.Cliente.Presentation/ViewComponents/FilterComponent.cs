using Microsoft.AspNetCore.Mvc;
using Raizen.Cliente.Application.Contracts;

namespace Raizen.Cliente.Presentation.ViewComponents
{
    public class FilterComponent : ViewComponent
    {
        public FilterComponent()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filtroClienteModel = new FiltroClienteModel();

            filtroClienteModel.Nome = string.Empty;
            filtroClienteModel.Cidade = string.Empty;
            filtroClienteModel.Email = string.Empty;

            return View("Filter", filtroClienteModel);

        }

 

    }
}
