using ODataIssue444.Data.Entities;
using Extenso.AspNetCore.OData;
using Extenso.Data.Entity;

namespace ODataIssue444.Controllers.Api
{
    public class PersonApiController : GenericODataController<Person, int>
    {
        public PersonApiController(IRepository<Person> repository)
            : base(repository)
        {
        }

        protected override int GetId(Person entity)
        {
            return entity.Id;
        }

        protected override void SetNewId(Person entity)
        {
        }
    }
}