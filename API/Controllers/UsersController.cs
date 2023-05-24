using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers

{
    //ive made authorize commented as im not genearting any token
    // [Authorize]
    public class UsersController: BaseApiController//baseapi is my base class.I'm deriving controllers from that class
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }

        [HttpGet]
        //code is made async to perfrom multiple tasks. if we use sync code and our database is huge
        //if server gets multiple requests then it will try to complete single task. in order to avoid that single request async code is used.

        public async Task<ActionResult<IEnumerable<MemberDto>>>GetUsers()
        {
             var users = await _userRepository.GetMembersAsync();
             
            return Ok(users);
        }

        [HttpGet("{username}")]
        //since we are returning single user so we dont use IEnumerable
        public async Task<ActionResult<MemberDto>>GetUser(string username)
        {
            //after cleaning 
            return await _userRepository.GetMemberAsync(username);
            
            
        }
    }
}