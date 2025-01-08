using Aswaq.Api.Controllers;
using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SHO_Task.Application.ShippingOrders;

namespace SHO_Task.Api.Controllers
{
    [ApiController]
    [Route($"api/v{ApiVersions.V1}/ShippingOrders")]

    public class ShippingOrderController : ControllerBase
    {
        private readonly ISender _sender;

        public ShippingOrderController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] AddShippingOrderRequest request)
        {
            AddShippingOrderCommand command = request;
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpPost("create-multiple")]
        public async Task<IActionResult> CreateMultiple([FromBody] BulkShippingOrderCreateRequest requests)
        {
            ValidationResult validationResult = await new BulkShippingOrderCreateRequestValidator().ValidateAsync(requests);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            BulkShippingOrderCreateCommand bulkShippingOrderCommand = requests;
            var listPOIDs = await _sender.Send(bulkShippingOrderCommand);
            return !listPOIDs.Any()? BadRequest(): Ok(listPOIDs);
        }
    }
}
