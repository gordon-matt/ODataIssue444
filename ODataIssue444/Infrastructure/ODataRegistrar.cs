using Extenso.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using ODataIssue444.Areas.Admin.Localization.Models;
using ODataIssue444.Data.Entities;

namespace ODataIssue444.Infrastructure
{
    public class ODataRegistrar : IODataRegistrar
    {
        public void Register(ODataOptions options)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("PersonApi");

            // Localization
            builder.EntitySet<Language>("LanguageApi");
            builder.EntitySet<LocalizableString>("LocalizableStringApi");

            //var resetLocalizableStringsAction = builder.EntityType<Language>().Collection.Action("ResetLocalizableStrings");
            //resetLocalizableStringsAction.Returns<IActionResult>();

            var getComparitiveTableFunction = builder.EntityType<LocalizableString>().Collection.Function("GetComparitiveTable");
            getComparitiveTableFunction.Parameter<string>("cultureCode");
            getComparitiveTableFunction.Returns<IActionResult>();

            var putComparitiveAction = builder.EntityType<LocalizableString>().Collection.Action("PutComparitive");
            putComparitiveAction.Parameter<string>("cultureCode");
            putComparitiveAction.Parameter<string>("key");
            putComparitiveAction.Parameter<ComparitiveLocalizableString>("entity");

            var deleteComparitiveAction = builder.EntityType<LocalizableString>().Collection.Action("DeleteComparitive");
            deleteComparitiveAction.Parameter<string>("cultureCode");
            deleteComparitiveAction.Parameter<string>("key");

            options.AddRouteComponents("odata", builder.GetEdmModel());
        }
    }
}