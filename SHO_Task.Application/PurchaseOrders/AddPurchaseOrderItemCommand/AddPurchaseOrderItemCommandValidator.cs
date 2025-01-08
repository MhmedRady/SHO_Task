﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.ShippingOrders;

public class AddShippingOrderItemCommandValidator : AbstractValidator<AddShippingOrderItemCommand>
{
    public AddShippingOrderItemCommandValidator()
    {
        RuleFor(x => x.SHONumber).NotEmpty().WithMessage("Purchase Order Number is required.");
        RuleFor(x => x.GoodCode).NotEmpty().WithMessage("Good Code is required And Must Be qunie in the same Purchase Order.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.PriceCurrencyCode).NotEmpty().WithMessage("Price Currency Code is required.");
    }
}