using AutoMapper;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public MappingTests()
        {
            _configuration = new MapperConfiguration(config => {
                config.AddProfile<AutoMapperProfile>();
            });
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(ContactDTO), typeof(Contact))]
        [InlineData(typeof(AccountDTO), typeof(Account))]
        [InlineData(typeof(OpportunityDTO), typeof(Opportunity))]
        [InlineData(typeof(LeadDTO), typeof(Lead))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);
            _mapper.Map(instance, origin, destination);
        }
    }
}
