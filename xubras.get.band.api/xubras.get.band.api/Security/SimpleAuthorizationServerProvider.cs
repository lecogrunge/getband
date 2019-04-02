namespace xubras.get.band.api.Security
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Linq;

    public class ApplySwaggerImplementationNotesFilterAttributes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attr = (SwaggerImplementationNotesAttribute)apiDescription.ControllerAttributes().FirstOrDefault();
            if (attr != null)
            {
                operation.Description = attr.ImplementationNotes;
            }
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerImplementationNotesAttribute : Attribute
    {
        public string ImplementationNotes { get; private set; }

        public SwaggerImplementationNotesAttribute(string implementationNotes)
        {
            this.ImplementationNotes = implementationNotes;
        }
    }
}