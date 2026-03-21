using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LivePolls.Domain.Modeles;
using LivePolls.Domain.Abstractions;

namespace LivePolls.Application.Services
{
    public class PollsService : IPollsService
    {
        private readonly IPollsRepo _pollsRepo;

        public PollsService(IPollsRepo pollsRepo)
        {
            _pollsRepo = pollsRepo;
        }


    }
}

//public async Task<Admin> Register(string email, string password)
//{
//    return await _adminRepo.Register(email, password);
//}

//public async Task<Admin> LoginAccount(string email, string password)
//{
//    return await _adminRepo.Login(email, password);
//}