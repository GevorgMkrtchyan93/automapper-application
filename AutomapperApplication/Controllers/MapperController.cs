using AutoMapper;
using AutomapperApplication.Data.Interfaces;
using AutomapperApplication.Models;
using AutomapperApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace AutomapperApplication.Controllers
{
    public class MapperController : Controller
    {
        private IUserRepository _repo;

        public MapperController(IUserRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable Index()

        {
            var config = new MapperConfiguration(c => c.CreateMap<User, IndexUserView>());
            var mapper = new Mapper(config);
            var users = mapper.Map<List<IndexUserView>>(_repo.GetAllUsers());

            return users;
        }

        [HttpPost]
        public User Create([FromBody] CreateUserView model)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<CreateUserView, User>()
           .ForMember("Name", o => o.MapFrom(c => c.FirstName + " " + c.LastName))
           .ForMember("Email", o => o.MapFrom(s => s.Login)));

                var mapper = new Mapper(config);
                user = mapper.Map<CreateUserView, User>(model);

                _repo.Create(user);
                _repo.Save();
            }

            return user;
        }

        [HttpGet]
        public EditUserView Edit(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var config = new MapperConfiguration(c => c.CreateMap<User, EditUserView>()
              .ForMember("Login", o => o.MapFrom(s => s.Email)));
            
            var mapper = new Mapper(config);
            var user = mapper.Map<User, EditUserView>(_repo.GetUser(id.Value));

            return user;
        }

        [HttpPost]
        public User Edit([FromBody] EditUserView model)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<EditUserView, User>()
                  .ForMember("Email", o => o.MapFrom(s => s.Login)));
                var mapper = new Mapper(config);
                user = mapper.Map<EditUserView, User>(model);

                _repo.Update(user);
                _repo.Save(user);
            }

            return user;
        }
    }
}
