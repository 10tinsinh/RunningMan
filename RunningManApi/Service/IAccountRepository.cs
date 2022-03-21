using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IAccountRepository
    {
        List<AccountIdDTO> GetAllAccount( string search);
        AccountIdDTO GetInformationAccountLogin(int id);

        AccountIdDTO CreateAccount(AccountDTO account);

        ApiResponse Login(Login login);

        void Update(int id, AccountDTO account);

        void Delete(int id);
    }
}
