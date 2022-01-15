using System;
using Extenso.AspNetCore.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
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
            return builder;
        }
    }
}