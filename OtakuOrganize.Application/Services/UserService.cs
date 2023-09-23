using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Exceptions;
using OtakuOrganize.Core.Interfaces.Repositories;
using AutoMapper;
using BCrypt.Net;

namespace OtakuOrganize.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public ISecurityService securityService;
        private readonly IAuthService authService;
        public UserService(IUserRepository _userRepository, IMapper _mapper, ISecurityService _securityService , IAuthService _authService)
        {
            mapper = _mapper;
            userRepository = _userRepository;
            securityService = _securityService;
            authService = _authService;
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

        public async Task<string> Login(LoginInputModel user)
        {
            if (user == null) throw new ArgumentNullException();

            var model = await userRepository.GetByEmail(user.Email);
            if (model != null)
            {
                var verifyToken = await securityService.VerifyPassword(user.Password, model.Password) ;
                if (!verifyToken) throw new PasswordIncorrectException();
                var token = authService.GenerateJwtToken(model.Email, model.Role);
                return token;
            };
            return null;
        }

        public async Task PostUser(UserInputModel model)
        {
            if (model == null) throw new ArgumentNullException();

        
            model.Password = await securityService.EncryptPassword(model.Password);

            User user = new(model.Id, model.Username, model.Password,model.Email , model.role); 
            
            await userRepository.Post(user);
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
