using BloggingAPI.Data.VO;
using BloggingAPI.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BloggingAPI.Hypermedia.Enricher
{
    public class PostEnricher : ContentResponseEnricher<PostVO>
    {
        protected override Task EnrichModel(PostVO content, IUrlHelper urlHelper)
        {
            var path = "api/posts";
            string link = getLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return Task.CompletedTask;
        }

        private string getLink(int id, IUrlHelper urlHelper, string path)
        {
            lock (this)
            {
                var url = new { controller = path, id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
