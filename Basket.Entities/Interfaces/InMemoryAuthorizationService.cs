using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShoppingBasket.Core.Interfaces
{
  

    public class InMemoryAuthorizationService : ICustomAuthorizationService
    {

        private readonly List<Permission> _permissionList = new List<Permission>();


        public InMemoryAuthorizationService()
        {
            this.SetPermissionTable();
        }

        public bool IsConsumerAuthorized(string secretKey, string userId)
        {
            return _permissionList.Any(x => x.ConsumerSecretKey == secretKey && x.UserId == userId);
        }

        private void SetPermissionTable()
        {
            this._permissionList.Add(new Permission {ConsumerSecretKey = "secret1",UserId = "User1"});
            this._permissionList.Add(new Permission { ConsumerSecretKey = "secret2", UserId = "User2" });
            this._permissionList.Add(new Permission { ConsumerSecretKey = "secret3", UserId = "User3" });
            this._permissionList.Add(new Permission { ConsumerSecretKey = "secret4", UserId = "User4" });
        }

        private class Permission
        {
            public string ConsumerSecretKey { get; set; }
            public string UserId { get; set; }
        }
       
    }
}
