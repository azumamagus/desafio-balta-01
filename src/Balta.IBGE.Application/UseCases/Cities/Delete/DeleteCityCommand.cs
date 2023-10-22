using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Domain.Cities;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Delete
{
    public record DeleteCityCommand(int id) : IRequest<Result>;
   
}
