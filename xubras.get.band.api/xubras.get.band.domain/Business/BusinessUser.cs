using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using xubras.get.band.data.Entities;
using xubras.get.band.domain.Contract.Business;
using xubras.get.band.domain.Contract.Repository;
using xubras.get.band.domain.Contract.Repository.Base;
using xubras.get.band.domain.Contract.Services;
using xubras.get.band.domain.Domains.User;
using xubras.get.band.domain.Domains.Validation;
using xubras.get.band.domain.Models.User;
using xubras.get.band.domain.Util;
using xubras.get.band.domain.ValueObjects;
using xubras.get.band.domain.ValueObjects.Validation;

namespace xubras.get.band.domain.Business
{
    public sealed class BusinessUser : IBusinessUser
    {
        #region [ Attributes ]
        private readonly IUserSaveRepository _userSaveRepository;
        private readonly IUserListRepository _userListRepository;
        private readonly IEmailService _emailService;
        private readonly Configuration _configuration;
        #endregion

        #region [ Constructor ]
        public BusinessUser(IUserSaveRepository userSaveRepository, IUserListRepository userListRepository, IEmailService emailService, IOptions<Configuration> configuration)
        {
            _userSaveRepository = userSaveRepository;
            _emailService = emailService;
            _configuration = configuration.Value;
        }
        #endregion

        #region [ Public Methods ]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();

            // E-mail validation
            Email email = new Email(request.Email);
            EmailVallidator emailValidator = new EmailVallidator();
            ValidationResult emailValidation = emailValidator.Validate(email);
            if (!emailValidation.IsValid)
                response.AddError(emailValidation);

            // User validation
            User user = new User(email, request.Password, request.Name, request.Nickname);
            UserValidator userValidator = new UserValidator();
            ValidationResult userValidation = userValidator.Validate(user);
            if (!userValidation.IsValid)
                response.AddError(userValidation);

            if (!response.IsValid())
                return response;

            user.CryptPassword();

            // Persistence
            _userSaveRepository.Add(new UserEntity { Name = user.Name, NickName = user.NickName, Password = user.Password, Email = user.Email.EmailAddress});

            // Other Services
            _emailService.SendEmailCreateUser(user.Email.EmailAddress, user.Name, user.TokenConfirm);

            response.IdUser = user.IdUser;
            return response;
        }

        public LoginUserResponse Login(LoginUserRequest request)
        {
            LoginUserResponse response = new LoginUserResponse();

            // E-mail validation
            Email email = new Email(request.Email);
            EmailVallidator emailValidator = new EmailVallidator();
            ValidationResult emailValidation = emailValidator.Validate(email);
            if (!emailValidation.IsValid)
                response.AddError(emailValidation);

            // User validation
            User user = new User(email, request.Password);
            UserValidator userValidator = new UserValidator();
            ValidationResult userValidation = userValidator.Validate(user);
            if (!userValidation.IsValid)
                response.AddError(userValidation);

            if (!response.IsValid())
                return response;

            // Autenticando
            _userListRepository.GetFirst(s => s.Email.ToString().Trim().Equals(request.Email));

            return response;
        }
        #endregion

        #region [ Private Methods ]

        private string Paging(int skip = 1, int take = 10)
        {
            return $"{_configuration.BaseUrl}?page={skip}&numberOfRecords={take}";
        }

        private static List<string> GetIncludes()
        {
            return new List<string> { "" };
        }
        #endregion
    }
}