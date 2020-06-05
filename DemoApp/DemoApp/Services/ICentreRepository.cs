using DemoApp.Entities;
using DemoApp.Extensions;
using DemoApp.Models;
using DemoApp.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public interface ICentreRepository
    {
        Task<CentresDto> Create(CentreCreateDto data);
        Task<CentresDto> Delete(Guid id);
        Task<CentresDto> Get(Guid id);
        Task<IEnumerable<CentresDto>> List();
        Task<PagedList<Centres>> List(CentreResourceParameters resourceParameters);
        Task<bool> Save();
        Task<CentresDto> Update(CentreUpdateDto data);
    }
}