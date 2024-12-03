using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models;

namespace SocialNetwork.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
    }
}