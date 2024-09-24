using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Growth_Plan_Month_4_Assessment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace CustomFormattersSample.Formatters
{
    // <snippet_Class>
    // <snippet_ClassDeclaration>
    public class JsonOutputFormatter : TextOutputFormatter
    // </snippet_ClassDeclaration>
    {
        // <snippet_ctor>
        public JsonOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/json"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        // </snippet_ctor>

        // <snippet_CanWriteType>
        protected override bool CanWriteType(Type type)
        {
            return typeof(books).IsAssignableFrom(type) ||
                typeof(IEnumerable<books>).IsAssignableFrom(type);
        }
        // </snippet_CanWriteType>

        // <snippet_WriteResponseBodyAsync>
        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<JsonOutputFormatter>>();
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<books> books)
            {
                foreach (var book in books)
                {
                    FormatVcard(buffer, book, logger);
                }
            }
            else
            {
                FormatVcard(buffer, (books)context.Object, logger);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatVcard(
            StringBuilder buffer, books book, ILogger logger)
        {
            buffer.AppendLine("BEGIN:JSON");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendLine($"Price:{book.Price}");
            buffer.AppendLine($"Author:{book.Author}");
            buffer.AppendLine($"Title:{book.Title}");
            buffer.AppendLine("END:JSON");

            logger.LogInformation("Writing {Author} ",
                book.Author);
        }
        // </snippet_WriteResponseBodyAsync>
    }
    // </snippet_Class>
}
