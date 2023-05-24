using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllSync();
        //enumerator, 
        //which supports a simple iteration over a collection of a specified type
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string username);

        //for cleaning the data inorder to avoid getting dateofbirth which we dont use
        //refer section 97.
        Task<IEnumerable<MemberDto>> GetMembersAsync();

        Task<MemberDto> GetMemberAsync(string username);
    }
}