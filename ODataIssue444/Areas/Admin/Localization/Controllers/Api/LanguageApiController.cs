using System;
using System.Threading.Tasks;
using Extenso.AspNetCore.OData;
using Extenso.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using ODataIssue444.Data.Entities;

namespace ODataIssue444.Areas.Admin.Localization.Controllers.Api
{
    public class LanguageApiController : GenericODataController<Language, Guid>
    {
        public LanguageApiController(IRepository<Language> repository)
            : base(repository)
        {
        }

        protected override Guid GetId(Language entity)
        {
            return entity.Id;
        }

        protected override void SetNewId(Language entity)
        {
            entity.Id = Guid.NewGuid();
        }

        [HttpPost]
        public virtual async Task<IActionResult> ResetLocalizableStrings([FromBody] ODataActionParameters parameters)
        {
            // Not implemented
            return Ok();
        }
    }
}