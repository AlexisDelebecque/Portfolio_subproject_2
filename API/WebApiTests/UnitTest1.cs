using System;
using System.Linq;
using WebApi.Domain.FunctionDomain;
using WebApi.Services.FunctionService;
using Xunit;


namespace WebApiTests
{
    public class UnitTest1
    {
        //Burde måske bare lave test for nogle af objekterne. Simpel unit test.
        [Fact]
        public void Test()
        {
            
            FunctionService functions = new FunctionService("host=localhost;db=imdb;uid=postgres;pwd=SarahPalin"); //Added connectionString for easier testing
            
            
            //SearchResultStructuredActorSearch searchResultStructuredActorSearch = new SearchResultStructuredActorSearch();
            //searchResultStructuredActorSearch.NameId = "nm0586568";

            var structuredActorSearch = functions.StructuredActorSearch("", "see", "", "Mads miKKelsen");
            Assert.Equal("nm0586568", structuredActorSearch.First().NameId);
            //skal returne nm0586568, Mads Mikkelsen
            
        }
    }
}
