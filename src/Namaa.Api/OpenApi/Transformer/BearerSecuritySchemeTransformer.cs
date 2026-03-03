using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Namaa.Api.OpenApi.Transformer;

 internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer, IOpenApiOperationTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes=await authenticationSchemeProvider.GetAllSchemesAsync();
        if(authenticationSchemes.Any(a => a.Name == "Bearer"))
        {
            var requirements=new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"]=new OpenApiSecurityScheme
                {
                    Type=SecuritySchemeType.Http,
                    Scheme="bearer",
                    In=ParameterLocation.Header,
                    BearerFormat="JWT",
                    Description="Enter your JWT token here."
                }
            };
            document.Components??=new();
            document.Components.SecuritySchemes=requirements;
        }
    }

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var authAttributes=context.Description.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();
        if (authAttributes.Any())
        {
            operation.Security??=new List<OpenApiSecurityRequirement>();
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                    Reference=new OpenApiReference
                    {
                        Id="Bearer",
                        Type=ReferenceType.SecurityScheme
                    }
                }]=Array.Empty<string>()
            });
        }
        return Task.CompletedTask;
    }
}