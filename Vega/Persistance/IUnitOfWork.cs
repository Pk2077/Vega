using AutoMapper.Features;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Vega.Persistance
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
