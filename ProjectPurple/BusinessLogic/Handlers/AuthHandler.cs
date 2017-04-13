using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    class AuthHandler
    {
        IAuthRepository authRepository;
        public AuthHandler()
        {
            authRepository = new AuthRepository(new HotelDataModelContainer());
        }
    }
}
