using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;

using Rebar.Web;

namespace MyTEBot.Service.Config
{
    /// <summary>The MyTEBot service route config.</summary>
    public static class MyTEBotServiceRouteConfig
    {
        /// <summary>Registers the routes.</summary>
        /// <param name="config">The config.</param>
        /// <param name="handlers">The delegating handlers</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Suppression OK, need a generic way to pass around constructors")]
        public static void RegisterRoutes(HttpConfiguration config, IEnumerable<Func<DelegatingHandler>> handlers)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            /*
             * NOTES:
             * RegisterRoutes handles the following:
             * * Creates an Entity Data Model (EDM) for the OData endpoint
             * * Adds the MyTEBot entity set to the EDM
             * * - Entities.MyTEBot is our POCO entity class defined in CostCollector.cs.
             * * - "MyTEBot" is the name of the entity set IDbSet<PeoplePicker> defined in PeoplePickerModelContainer.cs.
             * * Sets up the OData URIs and configures the endpoint
             */

            const string ServiceUriPrefix = "MyTEBot-service";
            const string EntitySetName = "MyTEBot";

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            var entitySet = modelBuilder.EntitySet<Entities.MyTEBotResult>(EntitySetName);
            modelBuilder.Namespace = "MyTEBot.Service.Entities";
            entitySet.HasFeedSelfLink(
                x =>
                    {
                        var linkUri = new UriBuilder(x.Request.RequestUri).AddFiddler();
                        return linkUri.Uri;
                    });

            entitySet.HasReadLink(x => LinkFactory(x, ServiceUriPrefix, EntitySetName), true);

            entitySet.HasIdLink(x => LinkFactory(x, ServiceUriPrefix, EntitySetName), true);

            entitySet.HasEditLink(x => LinkFactory(x, ServiceUriPrefix, EntitySetName), true);

            var model = modelBuilder.GetEdmModel();

            /*
             * Be aware that the name of the controller must match the name of the entity set. That means that there must be a 
             * MyTEBotController.
             * 
             * Our URI will be ~/MyTEBot-service.
             */
            config.MapRebarODataRoute("MyTEBotService-OData", ServiceUriPrefix, model, handlers);
        }

        private static string LinkFactory(EntityInstanceContext<Entities.MyTEBotResult> context, string ServiceUriPrefix, string EntitySetName)
        {
            var linkUri = context.Request.RequestUri.FindParentUri(ServiceUriPrefix);
            object id = new object();
            context.EdmObject.TryGetPropertyValue("Id", out id);
            linkUri.Query = string.Empty;
            linkUri.Fragment = string.Empty;
            linkUri.Path = string.Format(
              CultureInfo.InvariantCulture,
              "{0}{1}({2})",
              linkUri.Path,
              EntitySetName,
              (int)id);
              
            linkUri.AddFiddler();
            return linkUri.Uri.ToString();
        }
    }
}
