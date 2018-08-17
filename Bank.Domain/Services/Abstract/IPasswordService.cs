using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Services.Abstract
{
    public interface IPasswordService
    {
        bool ComparePasswords(string enteredPassword, string hashedStoredPassword);

        string HashPassword(string password);
    }
}
