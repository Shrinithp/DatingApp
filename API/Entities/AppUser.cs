using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        //use the same convention as ID because entity framework is based on convention based.
        //if i give theId instead ID then have say that externally that theID is a key.
        //[key] should be written if we use other name,
        public int Id{get; set;}

        public string UserName{get; set;}

        public byte[] PasswordHash{get; set;}

        public byte[] PasswordSalt{get; set;}







    }
}