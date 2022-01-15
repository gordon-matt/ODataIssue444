using System;
using Extenso.AspNetCore.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ODataIssue444.Areas.Admin.Localization.Models;
using ODataIssue444.Data.Entities;

namespace ODataIssue444.Infrastructure
{
    public class ODataRegistrar : IODataRegistrar
    {
        public void Register(IRouteBuilder routes, IServiceProvider services)
        {
            var builder = GetBuilder(services);
            routes.MapODataServiceRoute("OData", "odata", builder.GetEdmModel());
        }

        public void Register(IEndpointRouteBuilder endpoints, IServiceProvider services)
        {
            var builder = GetBuilder(services);
            endpoints.MapODataRoute("OData", "odata", builder.GetEdmModel());
        }

        private ODataModelBuilder GetBuilder(IServiceProvider services)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder(services);
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

            return builder;
        }
    }
}