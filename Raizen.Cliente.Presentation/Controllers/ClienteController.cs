using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Raizen.Cliente.Application.Contracts;
using Raizen.Cliente.Application.Services;

namespace Raizen.Cliente.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;
        private readonly IValidator<ClienteModel> _validator;

        public ClienteController(IMapper mapper, IClienteService clienteService, IValidator<ClienteModel> validator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<IActionResult> Index(FiltroClienteModel? filtroClienteModel)
        {
            ModelState.Clear();
            ModelState.ClearValidationState(nameof(FiltroClienteModel));
            ModelState.ClearValidationState(nameof(ClienteModel));

            IEnumerable<ClienteModel> clientesModel = filtroClienteModel.Nome == null ? ListarCliente() : ListarCliente(filtroClienteModel);

            return View(clientesModel.Count() > 0 ? clientesModel : null);
        }

       

        public async Task<ActionResult> AddCliente()
        {
            return View("Create");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCliente(ClienteModel clienteModel)
        {

            var validationResult = await _validator.ValidateAsync(clienteModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View("Create", clienteModel);
  
            }


            var cliente = _mapper.Map<ClienteModel, Domain.Entities.Cliente>(clienteModel);

            if (cliente.Complemento.IsNullOrEmpty()) cliente.Complemento = string.Empty;

            var ret = _clienteService.Insert(cliente);

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });

        }


        public async Task<ActionResult> Edit(int id)
        {
            
            var cliente = await _clienteService.GetById(id);


            var clientesModel = _mapper.Map<Domain.Entities.Cliente, ClienteModel>(cliente);

            return View(clientesModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCliente(ClienteModel clienteModel)
        {

            var result = await _validator.ValidateAsync(clienteModel);

            if (!result.IsValid)
            {

                return View("Edit", clienteModel);

            }


            var cliente = _mapper.Map<ClienteModel, Domain.Entities.Cliente>(clienteModel);

            if (cliente.Complemento.IsNullOrEmpty()) cliente.Complemento = string.Empty;

            var ret = _clienteService.Update(cliente);

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });

        }


        public async Task<ActionResult> Delete(int id)
        {

            var cliente = await _clienteService.GetById(id);

            var clientesModel = _mapper.Map<Domain.Entities.Cliente, ClienteModel>(cliente);

            return View(clientesModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCliente(ClienteModel clienteModel)
        {

            var ret = _clienteService.Delete(clienteModel.Id);

            return RedirectToRoute(new { controller = "Cliente", action = "Index" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FilterCliente(FiltroClienteModel filtroClienteMode)
        {
            
            return RedirectToAction("Index", "Cliente", filtroClienteMode);


        }

        private IEnumerable<ClienteModel> ListarCliente(FiltroClienteModel filtroClienteModel)
        {
            Domain.Entities.Cliente cliente = new Domain.Entities.Cliente();

            cliente.Nome = filtroClienteModel.Nome;
            cliente.Email = filtroClienteModel.Email;
            cliente.Cidade = filtroClienteModel.Cidade;

            var clientes = _clienteService.GetAll(cliente).Result;

            var clientesModel = _mapper.Map<IEnumerable<Domain.Entities.Cliente>, IEnumerable<ClienteModel>>(clientes);
            return clientesModel;
        }


        private IEnumerable<ClienteModel> ListarCliente()
        {

            var clientes = _clienteService.GetAll().Result;

            var clientesModel = _mapper.Map<IEnumerable<Domain.Entities.Cliente>, IEnumerable<ClienteModel>>(clientes);
            return clientesModel;
        }
    }
}
