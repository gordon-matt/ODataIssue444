using System;
using System.Linq;
using System.Threading.Tasks;
using Extenso.AspNetCore.OData;
using Extenso.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using ODataIssue444.Areas.Admin.Localization.Models;
using ODataIssue444.Data.Entities;

namespace ODataIssue444.Areas.Admin.Localization.Controllers.Api
{
    public class LocalizableStringApiController : GenericODataController<LocalizableString, Guid>
    {
        public LocalizableStringApiController(IRepository<LocalizableString> repository)
            : base(repository)
        {
        }

        protected override Guid GetId(LocalizableString entity)
        {
            return entity.Id;
        }

        protected override void SetNewId(LocalizableString entity)
        {
            entity.Id = Guid.NewGuid();
        }

        public virtual async Task<IActionResult> GetComparitiveTable(
            [FromODataUri] string cultureCode,
            ODataQueryOptions<ComparitiveLocalizableString> options)
        {
            var connection = GetDisposableConnection();

            var query = connection.Query(x => (x.CultureCode == null || x.CultureCode == cultureCode))
                        .ToHashSet()
                        .GroupBy(x => x.TextKey)
                        .Select(grp => new ComparitiveLocalizableString
                        {
                            Key = grp.Key,
                            InvariantValue = grp.First(x => x.CultureCode == null).TextValue,
                            LocalizedValue = grp.FirstOrDefault(x => x.CultureCode == cultureCode) == null
                                ? string.Empty
                                : grp.First(x => x.CultureCode == cultureCode).TextValue
                        })
                        .AsQueryable();

            var results = options.ApplyTo(query, IgnoreQueryOptions);
            var response = await Task.FromResult((results as IQueryable<ComparitiveLocalizableString>).ToHashSet());
            return Ok(response);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PutComparitive([FromBody] ODataActionParameters parameters)
        {
            string cultureCode = (string)parameters["cultureCode"];
            string key = (string)parameters["key"];
            var entity = (ComparitiveLocalizableString)parameters["entity"];

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!key.Equals(entity.Key))
            {
                return BadRequest();
            }

            var localizedString = await Repository.FindOneAsync(x => x.CultureCode == cultureCode && x.TextKey == key);

            if (localizedString == null)
            {
                localizedString = new LocalizableString
                {
                    Id = Guid.NewGuid(),
                    CultureCode = cultureCode,
                    TextKey = key,
                    TextValue = entity.LocalizedValue
                };
                await Repository.InsertAsync(localizedString);
            }
            else
            {
                localizedString.TextValue = entity.LocalizedValue;
                await Repository.UpdateAsync(localizedString);
            }

            return Updated(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteComparitive([FromBody] ODataActionParameters parameters)
        {
            string cultureCode = (string)parameters["cultureCode"];
            string key = (string)parameters["key"];

            var entity = await Repository.FindOneAsync(x => x.CultureCode == cultureCode && x.TextKey == key);
            if (entity == null)
            {
                return NotFound();
            }

            entity.TextValue = null;
            await Repository.UpdateAsync(entity);
            //Repository.Delete(entity);

            return NoContent();
        }
    }
}