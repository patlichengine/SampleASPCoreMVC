using DemoApp.Entities;
using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.ServiceMappers
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _centrePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "CentreNumber", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
                { "CentreName", new PropertyMappingValue(new List<string>() { "CentreName"}) }
            };

        //private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<CentresDto, Centres>(_centrePropertyMapping));
        }

        //Add a method to validate the sorting parameters mapping
        public bool ValidateMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if(string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            //the string is separated by ",", so we split it
            var fieldsAfterSplit = fields.Split(',');

            //then we run through all the fileds
            foreach (var field in fieldsAfterSplit)
            {
                //trim
                var trimmedField = field.Trim();

                //remove everything after the first " " - if the fields 
                //are coming from an orderBy string, this part must be
                //ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                //find the matching property
                if(!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            //get the matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)}, {typeof(TDestination)}");
        }
    }
}
