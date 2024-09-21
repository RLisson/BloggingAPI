using BloggingAPI.Data.Converter.Implementations;
using BloggingAPI.Data.VO;
using BloggingAPI.Model;
using BloggingAPI.Repository.Generic;
using Microsoft.AspNetCore.Identity;

namespace BloggingAPI.Business.Implementations
{
    public class PostBusinessImplementation : IPostBusiness
    {
        private readonly IRepository<Post> _repository;
        private readonly PostConverter _converter;

        public PostBusinessImplementation(IRepository<Post> repository)
        {
            _repository = repository;
            _converter = new PostConverter();
        }

        public PostVO Create(PostVO item)
        {
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            var itemEntity = _converter.Parse(item);
            itemEntity = _repository.Create(itemEntity);
            return _converter.Parse(itemEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<PostVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PostVO FindByID(int id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PostVO Update(PostVO item)
        {
            item.UpdatedAt = DateTime.Now;

            var itemEntity = _converter.Parse(item);
            itemEntity = _repository.Update(itemEntity);
            return _converter.Parse(itemEntity);
        }
    }
}
