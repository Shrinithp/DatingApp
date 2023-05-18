using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers

{
    [Authorize]
    public class UsersController: BaseApiController//baseapi is my base class.I'm deriving controllers from that class
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        //code is made async to perfrom multiple tasks. if we use sync code and our database is huge
        //if server gets multiple requests then it will try to complete single task. in order to avoid that single request async code is used.

        public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        [Authorize]
        [HttpGet("{id}")]
        //since we are returning single user so we dont use IEnumerable
        public async Task<ActionResult<AppUser>>GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}