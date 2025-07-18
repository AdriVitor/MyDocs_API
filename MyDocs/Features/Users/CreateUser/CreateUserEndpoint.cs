﻿using FastEndpoints;
using MyDocs.Features.Users.Create;

namespace MyDocs.Features.Users.CreateUser
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest>
    {
        private readonly ICreateUserService _createUserService;
        public CreateUserEndpoint(ICreateUserService createUserService)
        {
            _createUserService = createUserService;
        }

        public override void Configure()
        {
            Post("User/Create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _createUserService.AddUser(request);

                await SendAsync(new { Message = $"Usuário {request.Name} criado com sucesso" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
