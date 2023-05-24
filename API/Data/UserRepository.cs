using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        //return propertes from data that is requires(password is excluded)
        //this is just cleaning even if i dont run this program will work fine.
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                    .Where(x=> x.UserName == username)
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
          return await _context.Users
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }
        

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            //this earger loading the entity to load photos and others.
            return await _context.Users
            .Include(p=>p.Photos)
            //this is returning everything from database even though we are not using dateofbirth.
            .SingleOrDefaultAsync(x=>x.UserName==username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //this earger loading the entity to load photos and others.
            return await _context.Users
            .Include(p=>p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllSync()
        {
            //return false for zero as nothing is going to return
            //return true for>0
            return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUser user)
        {
            //changes in entityframework are made here
            _context.Entry(user).State=EntityState.Modified;
        }
    }
}