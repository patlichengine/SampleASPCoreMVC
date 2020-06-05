using AutoMapper;
using DemoApp.Entities;
using DemoApp.Extensions;
using DemoApp.Models;
using DemoApp.ResourceParameters;
using DemoApp.ServiceMappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class CentreRepository : IDisposable, ICentreRepository
    {
        private readonly SchoolRecognitionContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public CentreRepository(SchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<CentresDto> Create(CentreCreateDto data)
        {

            return await Task.Run(async () =>
            {

                //map the enetity with the dto
                var entity = _mapper.Map<Centres>(data);

                //check if user exists
                _context.Centres.Add(entity);

                //call the save method
                bool saveResult = await Save();

                return _mapper.Map<CentresDto>(entity);
            });
        }


        public async Task<CentresDto> Delete(Guid id)
        {

            return await Task.Run(async () =>
            {
                //check if the record exists
                var checkData = await _context.Centres.FirstOrDefaultAsync(c => c.Id == id);

                _context.Centres.Remove(checkData);
                //call the save method
                await Save();

                return _mapper.Map<CentresDto>(checkData);
            });
        }

        public async Task<CentresDto> Get(Guid id)
        {

            return await Task.Run(async () =>
            {
                //check if the record exists
                var checkData = await _context.Centres
                    .Include(c => c.SchoolCategory)
                    .Include(c => c.CentreSanctions)
                    .Include(c => c.CreatedByNavigation)
                    .FirstOrDefaultAsync(c => c.Id == id);

                //call the save method
                await Save();

                return _mapper.Map<CentresDto>(checkData);
            });
        }

        public async Task<IEnumerable<CentresDto>> List()
        {

            return await Task.Run(async () =>
            {
                //check if the record exists
                var checkData = await _context.Centres
                    .Include(c => c.SchoolCategory)
                    .Include(c => c.CentreSanctions)
                    .Include(c => c.CreatedByNavigation)
                    .ToListAsync();

                //call the save method
                await Save();

                return _mapper.Map<IEnumerable<CentresDto>>(checkData);
            });
        }

        public async Task<PagedList<Centres>> List(CentreResourceParameters resourceParameters)
        {
            return await Task.Run(async () =>
            {
                if (resourceParameters == null)
                {
                    throw new ArgumentNullException(nameof(resourceParameters));
                }

                //cast the collection into an IQueriable object
                var collection = _context.Centres
                            .Include(c => c.CreatedByNavigation)
                            .Include(c => c.CentreSanctions)
                            .Include(c => c.SchoolCategory) as IQueryable<Centres>;

                if (!string.IsNullOrWhiteSpace(resourceParameters.CentreNo))
                {
                    var centreNo = resourceParameters.CentreNo.Trim();
                    collection = collection.Where(c => c.CentreNo == (centreNo));
                }

                if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
                {
                    var searchQuery = resourceParameters.SearchQuery.Trim();
                    collection = collection.Where(c => c.CentreNo.Contains(searchQuery)
                    || c.CentreName.Contains(searchQuery));
                }

                if (!string.IsNullOrWhiteSpace(resourceParameters.OrderBy))
                {
                    //get property mapping
                    var usersPropertyMappingDictionary =
                        _propertyMappingService.GetPropertyMapping<Models.CentresDto, Centres>();

                    collection = collection.ApplySort(resourceParameters.OrderBy,
                        usersPropertyMappingDictionary);
                }


                //return the paginated data
                return await PagedList<Centres>.CreateAsync(collection, resourceParameters.PageNumber, resourceParameters.PageSize);
            });
        }

        public async Task<CentresDto> Update(CentreUpdateDto data)
        {

            return await Task.Run(async () =>
            {
                //check if the record exists
                var checkData = await _context.Centres
                    .Include(c => c.SchoolCategory)
                    .Include(c => c.CentreSanctions)
                    .Include(c => c.CreatedByNavigation)
                    .FirstOrDefaultAsync(c => c.Id == data.Id);

                if (checkData != null)
                {
                    //map the enetity with the dto
                    _mapper.Map(data, checkData);
                }

                //call the save method
                await Save();

                return _mapper.Map<CentresDto>(checkData);
            });
        }


        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
