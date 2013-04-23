using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace athena.AR.Data
{
    internal sealed class UserManager : IDisposable
    {
        private AREntities _context;

        public UserManager()
        {
            _context = new AREntities(ConnectionHelper.GetConnection());
        }

        internal USER GetUserByUserName(string userName)
        {
                 var user = _context.USERs.Where(u => u.USERNAME.ToLower() == userName.ToLower()).FirstOrDefault();
                return user;
        }

        internal USERLOG GetUserLastestLogin(string userName) { 
            var user = GetUserByUserName(userName);
            var userLatestLog = _context.USERLOGs.Where(u => u.USERID == user.USERID).OrderByDescending(u => u.DATETIMELOGIN).FirstOrDefault();
            //var userLatestLog = _context.USERLOGs.Where(u => u.USER == user && u.DATETIMELOGOUT == DateTime.MinValue).FirstOrDefault();
            return userLatestLog;
        }

        internal void AddToUserLog(USERLOG userlog)
        {
            _context.AddToUSERLOGs(userlog);
        }

        internal int Save()
        {
            var hasChanges = _context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Modified).Any();
            if (hasChanges)
            {
                try
                {
                    return _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Common.ExceptionLogger.AppendAsync(ex);
                    return 0;
                }
            }
            return 0;
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex) { 
                Common.ExceptionLogger.AppendAsync(ex); 
                _context = null; }
        }
    }
}