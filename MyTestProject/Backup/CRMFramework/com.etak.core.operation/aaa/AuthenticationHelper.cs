using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.util;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.operation.aaa
{ 
    /// <summary>
    /// Helper class to authenticate the credentials provided.
    /// </summary> 
    public static class AuthenticationHelper
    {
        /// <summary>
        /// We should only give an error message to be sure that we don't give extra clues
        /// about our system, as for instance (this user exists, but the password is wrong)
        /// </summary>
        private const String SingleErrorTextMessage = "The username and password combination is not valid";

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Provides the user authentication.
        /// </summary>
        /// <param name="user">the id of user</param>
        /// <param name="password">the password of user</param>
        /// <exception cref="AuthenticationErrorException"> Thrown if user is invalid.</exception>
        /// <returns>if success, return the user entity. otherwise, will throw exceptions.</returns>
        public static LoginInfo Authenticate(string user, string password)
        {
            int userId;
            if (!Int32.TryParse(user.Trim(), out userId))
            {
                Logger.ErrorFormat("Authentication fails, the user: {0} is not an user id.", user);
                throw new AuthenticationErrorException(SingleErrorTextMessage, -90);
            }

            ILoginInfoRepository<LoginInfo> userRepo = RepositoryManager.GetRepository<ILoginInfoRepository<LoginInfo>>();
            LoginInfo userInfo = userRepo.GetByUserId(userId).FirstOrDefault();

            if (userInfo == null)
            {
                Logger.ErrorFormat("Authentication fails, cannot get user entity by userId: {0}.", userId);
                throw new AuthenticationErrorException(SingleErrorTextMessage, -90);
            }

            if (userInfo.Password != MD5Utility.ComputeHash(password))
            {
                Logger.ErrorFormat("Authentication fails, the input password for the user: {0} is incorrect.", userId);
                throw new AuthenticationErrorException(SingleErrorTextMessage, -90);
            }

            if (!userInfo.Status.HasValue || userInfo.Status.Value == (int)EUserStatus.Deleted)
            {
                Logger.ErrorFormat("Authentication fails, the status of the user: {0} is incorrect.", userId);
                throw new AuthenticationErrorException(SingleErrorTextMessage, -90);
            }

            return userInfo;
        }
    }
}
