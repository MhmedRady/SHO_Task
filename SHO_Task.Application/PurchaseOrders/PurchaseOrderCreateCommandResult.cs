using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.ShippingOrders
{
    public sealed record ShippingOrderCreateCommandResult(DateTime IssueDate, string SHONumber);
}
