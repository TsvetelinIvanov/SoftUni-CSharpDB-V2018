using System;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core
{
    public  class AuthenticationManager
    {
        //private static User currentUser;
        //private AuthenticationManager instance;

        public AuthenticationManager()
        {

        }        

        //public AuthenticationManager Instance
        //{
        //    get
        //    {
        //        if (this.instance == null)
        //        {
        //            this.instance = new AuthenticationManager();
        //        }

        //        return this.instance;
        //    }
        //}

        public User LogedInUser { get; private set; }

        public void Login (User user)
        {
            this.LogedInUser = user;
        }

        public void Logout()
        {
            this.Authorize();
            this.LogedInUser = null;
        }

        public void Authorize()
        {
            if(this.LogedInUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public bool IsAuthenticated()
        {
            return this.LogedInUser != null;
        }

        public User GetCurrentUser()
        {
            this.Authorize();

            return this.LogedInUser;
        }
    }
}