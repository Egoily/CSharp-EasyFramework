using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.operation.contract.exceptions;

namespace com.etak.core.operation.aaa
{
    /// <summary>
    /// Helper class to check that a user can have access to the specified dealer
    /// </summary>
    public static class AuthorizationHelper
    {
        /// <summary>
        /// We should only give an error message to be sure that we don't give extra clues
        /// about our system, as for instance (this user exists, but the password is wrong)
        /// </summary>
        private static readonly String SingleErrorTextMessage = "User does not have enough permissions";       

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        /// <summary>
        /// Check if user has permission to the dealer.
        /// </summary>
        /// <param name="LoginInfo">the instance of user</param>
        /// <param name="dealer">the instance of dealer</param>
        /// <exception cref="ArgumentNullException">Thrown if any of the input parameters is null</exception>
        /// <exception cref="AuthorizationErrorException">Thrown if any of the  is null</exception>
        /// <returns>void</returns>
        public static void Authorize(LoginInfo LoginInfo, DealerInfo dealer)
        {
            if (LoginInfo == null)
                throw new ArgumentNullException("User cannot be null.");
            if (dealer == null)
                throw new ArgumentNullException("Dealer cannot be null.");

            if (LoginInfo.UserDealerInfo == null)
            {
                Logger.InfoFormat("Authorization fails, the user: {0} does not have permission to any dealer.", LoginInfo.UserID);
                throw new AuthorizationErrorException(SingleErrorTextMessage, OperationErrorCodes.UserDoesNotHavePermissionsForDealer);
            }

            List<Int32> dealersTree = null;

            #region Pick up the dealers to be checked with permission on the dealer tree.
            switch (dealer.DealerTypeID)
            {
                case (int)EDealerType.FiscalUnit:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value };
                    break;
                case (int)EDealerType.Reseller:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value };
                    break;
                case (int)EDealerType.Agent:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value, dealer.AgentID.Value };
                    break;
                case (int)EDealerType.Subagent:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value, dealer.AgentID.Value, dealer.SubagentID.Value };
                    break;
                default:
                    break;
            }
            if (dealersTree == null || dealersTree.Count == 0)
            {
                Logger.InfoFormat("Authorization fails, cannot get the  of dealer: {0}.", dealer.DealerID);
                throw new AuthorizationErrorException(SingleErrorTextMessage, OperationErrorCodes.UnknownDealerType);
            }
            #endregion

            if (!LoginInfo.UserDealerInfo.Where(x => dealersTree.Contains(x.DealerID)).Any())
            {
                Logger.InfoFormat("Authorization fails, the user: {0} has no permission to the dealer.", LoginInfo.UserID);
                throw new AuthorizationErrorException(SingleErrorTextMessage, -1);
            }
        }

        /// <summary>
        /// Check if user has permission to the dealer.
        /// </summary>
        /// <param name="LoginInfo">the instance of user</param>
        /// <param name="dealer">the instance of dealer</param>
        /// <exception cref="ArgumentNullException">Thrown if any of the input parameters is null</exception>
        /// <returns>true if the authorization was sucessfull, false if the authorization failed</returns>
        public static Boolean TryAuthorize(LoginInfo LoginInfo, DealerInfo dealer)
        {
            if (LoginInfo == null)
                throw new ArgumentNullException("User cannot be null.");
            if (dealer == null)
                throw new ArgumentNullException("Dealer cannot be null.");

            if (LoginInfo.UserDealerInfo == null)
            {
                Logger.InfoFormat("Authorization fails, the user: {0} does not have permission to any dealer.", LoginInfo.UserID);
                return false;
            }

            List<Int32> dealersTree = null;

            #region Pick up the dealers to be checked with permission on the dealer tree.
            switch (dealer.DealerTypeID)
            {
                case (int)EDealerType.FiscalUnit:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value };
                    break;
                case (int)EDealerType.Reseller:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value };
                    break;
                case (int)EDealerType.Agent:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value, dealer.AgentID.Value };
                    break;
                case (int)EDealerType.Subagent:
                    dealersTree = new List<Int32>() { dealer.FiscalUnitID.Value, dealer.ResellerID.Value, dealer.AgentID.Value, dealer.SubagentID.Value };
                    break;
                default:
                    break;
            }
            if (dealersTree == null || dealersTree.Count == 0)
            {
                Logger.InfoFormat("Authorization fails, cannot get the dealer tree of dealer: {0}.", dealer.DealerID);
                return false;
            }
            #endregion

            if (!LoginInfo.UserDealerInfo.Where(x => dealersTree.Contains(x.DealerID)).Any())
            {
                Logger.InfoFormat("Authorization fails, the user: {0} has no permission to the dealer.", LoginInfo.UserID);
                return false;
            }
            return (true);
        }
    }
}
