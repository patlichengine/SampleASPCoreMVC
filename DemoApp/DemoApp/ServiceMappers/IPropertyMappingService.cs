using System.Collections.Generic;

namespace DemoApp.ServiceMappers
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields);
    }
}