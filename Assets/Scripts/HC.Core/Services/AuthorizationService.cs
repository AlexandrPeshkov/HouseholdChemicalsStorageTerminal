using System.Threading.Tasks;
using Core.Logic;
using DataAccess;
using JetBrains.Annotations;
using UniRx;

namespace Core.Services
{
    public class AuthorizationService
    {
        private readonly EntityRepository _entityRepository;

        private readonly ReactiveProperty<User> _userRx;

        public IReadOnlyReactiveProperty<User> CurrentUser { get; private set; }

        public AuthorizationService(EntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
            _userRx = new ReactiveProperty<User>(null);
        }

        public async Task<bool> TrySignIn(string userName)
        {
            var user = await _entityRepository.Users.FirstOrDefault(x => x.Name == userName);

            if (user != null)
            {
                SignIn(user);
                return true;
            }

            return false;
        }

        private void SignIn([NotNull] User user)
        {
            _userRx.Value = user;
        }

        public async Task<bool> TrySignUp(User user)
        {
            var createdUser = await _entityRepository.Users.Create(user);

            if (createdUser != null)
            {
                SignIn(createdUser);
                return true;
            }

            return false;
        }
    }
}