using OTPGenerator.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Service.Service
{
    public interface IUserManagementService
    {
        User GetPasswordForUser(User user);
        User ValidatePasswordForUser(User user);
    }
}
