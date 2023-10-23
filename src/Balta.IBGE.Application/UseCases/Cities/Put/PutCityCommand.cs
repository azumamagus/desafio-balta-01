using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Balta.IBGE.Application.UseCases.Cities.ViewModels;
using Balta.IBGE.Domain.Core;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Cities.Put
{
    public record PutCityCommand(int Id, string State, string Name) : IRequest<Result>;
}
