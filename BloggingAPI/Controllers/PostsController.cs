using Asp.Versioning;
using BloggingAPI.Business;
using BloggingAPI.Data.VO;
using BloggingAPI.Hypermedia.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BloggingAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostBusiness _postBusiness;

        public PostsController(ILogger<PostsController> logger, IPostBusiness postBusiness)
        {
            _logger = logger;
            _postBusiness = postBusiness;
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<PostVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Find()
        {
            return Ok(_postBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PostVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Find(int id)
        {
            var post = _postBusiness.FindByID(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PostVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] PostVO post)
        {
            if (post == null) return BadRequest();
            return Ok(_postBusiness.Create(post));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PostVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update([FromBody] PostVO post)
        {
            if (post == null) return BadRequest();
            return Ok(_postBusiness.Update(post));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _postBusiness.Delete(id);
            return NoContent();
        }
    }
}
