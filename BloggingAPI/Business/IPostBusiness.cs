using BloggingAPI.Data.VO;

namespace BloggingAPI.Business
{
    public interface IPostBusiness
    {
        PostVO Create(PostVO item);
        PostVO FindByID(int id);
        List<PostVO> FindAll();
        PostVO Update(PostVO item);
        void Delete(int id);
    }
}
