using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    public class AuthHandler
    {
        IAuthRepository authRepository;
        public AuthHandler()
        {
            authRepository = new AuthRepository(new HotelDataModelContainer());
        }
    }
}
