using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repositories;
using AutoMapper;

namespace AnimesControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository _userRepository, IMapper _mapper)
        {
            mapper = _mapper;
            userRepository = _userRepository;

        }
        public async Task DeleteUser(Guid? id)
        {
            if (id == null) throw new ArgumentNullException();
            var user = await userRepository.GetById(id);
            userRepository.Delete(user);
        }

        public async Task<UserViewModel> GetByIdUser(Guid? id)
        {
            if (id == null) throw new ArgumentNullException();
            var user = await userRepository.GetById(id);
            var userMap =  mapper.Map<UserViewModel>(user);
            return userMap;

        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            IEnumerable<User> users = await userRepository.GetAll();

            var usersMap = mapper.Map<IEnumerable<UserViewModel>>(users);
            return usersMap.OrderBy(u => u.Id);
        }

        public void PostUser(UserInputModel user)
        {
            if (user == null) throw new ArgumentNullException();

            var userMap = mapper.Map<User>(user);

            userRepository.Post(userMap);

        }

        public void PutUser(Guid? id, UserInputModel user)
        {
            if (user == null) throw new ArgumentNullException();
            if (id == null) throw new ArgumentNullException();

            var userMap = mapper.Map<User>(user);

            userRepository.Put(userMap);


        }
    }
}
