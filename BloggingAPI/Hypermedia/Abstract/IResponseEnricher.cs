using Microsoft.AspNetCore.Mvc.Filters;

namespace BloggingAPI.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
