using System;
using Assignment_Bosch.Integration;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Bosch.Services
{
	public class ValidateUserService
	{
        public readonly AuthContext authContext;
        private readonly ILogger<ValidateUserService> _logger;

		public ValidateUserService(AuthContext _authContext, ILogger<ValidateUserService> logger)
		{
            authContext = _authContext;
            _logger = logger;
		}

        public async Task<UserInfo?> GetUserData(string username, string password)
        {
            try
            {
                // Retrieve the username and password from Database if exists
                var results = await authContext.UserInfos.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
                return results;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}

