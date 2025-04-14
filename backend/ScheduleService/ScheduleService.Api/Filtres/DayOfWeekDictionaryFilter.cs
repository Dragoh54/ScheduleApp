using Microsoft.OpenApi.Models;
using ScheduleService.Application.Dto;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ScheduleService.Api.Filtres;

public class DayOfWeekDictionaryFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Dictionary<DayOfWeek, List<TimeSlotDto>>))
        {
            schema.Type = "object";
            schema.Properties = new Dictionary<string, OpenApiSchema>
            {
                ["Monday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Tuesday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Wednesday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Thursday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Friday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Saturday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                },
                ["Sunday"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "TimeSlotDto" }
                    }
                }
            };
            schema.AdditionalPropertiesAllowed = false;
        }
    }
}