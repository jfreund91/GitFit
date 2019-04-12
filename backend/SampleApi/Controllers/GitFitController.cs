using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;

namespace SampleApi.Controllers
{
    abstract public class GitFitController : ControllerBase
    {
        protected IUserDAO userDao;

        private SampleApi.Models.User currentUser = null;

        public SampleApi.Models.User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    if (base.User != null)
                    {
                        // Returns user id: currentUser.id
                        currentUser = userDao.GetUser(base.User.Identity.Name);
                    }
                }
                return currentUser;
            }
        }

        public GitFitController (IUserDAO userDao)
        {
            this.userDao = userDao;
        }

    }
}