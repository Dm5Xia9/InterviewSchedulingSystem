using AutoMapper;
using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public abstract class AdminController : Controller
    {
        protected IMapper _mapper;

        protected RepositoriesUnitOfWork _repositoriesUnitOfWork;

        protected UserManager<User> _userManager;
        public AdminController()
        {
        }

        public AdminController(RepositoriesUnitOfWork repositoriesUnitOfWork,
            UserManager<User> manager)
        {
            _repositoriesUnitOfWork = repositoriesUnitOfWork;
            _userManager = manager;
        }

        public AdminController(IMapper mapper, RepositoriesUnitOfWork repositoriesUnitOfWork,
            UserManager<User> manager)
        {
            _mapper = mapper;
            _repositoriesUnitOfWork = repositoriesUnitOfWork;
            _userManager = manager;
        }
    }
}
