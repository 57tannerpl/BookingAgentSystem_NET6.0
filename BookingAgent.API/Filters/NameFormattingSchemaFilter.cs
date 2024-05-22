using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SettlementBookingAgent_NET6._0.API.Filters
{
    public class NameFormattingSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

            if (context.MemberInfo is PropertyInfo propertyInfo)
            {
                // Check if the property is named "Attendee"
                if (propertyInfo.Name == "Attendee" || propertyInfo.Name == "Organizer" || propertyInfo.Name == "ClientName")
                {
                    // Modify the example value for the Attendee property
                    schema.Example = new OpenApiString(FormatNameExample());
                }
            }

        }
        private string FormatNameExample()
        {
            // Format example name as "First Last"
            var firstName = "John";
            var lastName = "Doe";
            return $"{firstName} {lastName}";
        }
    }
}
