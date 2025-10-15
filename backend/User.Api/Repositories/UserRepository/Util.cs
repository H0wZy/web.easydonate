// using System.Linq.Expressions;
// using Microsoft.EntityFrameworkCore;
// using User.Api.Data;
// using User.Api.Dto;
// using User.Api.Models;
//
// namespace User.Api.Repositories.UserRepository;
//
// public class Util(UserDbContext context) : IUserRepository
// {
//     public async Task<RespondeModel<UserModel>> GetByIdAsync(int id)
//     {
//         RespondeModel<UserModel> response = new();
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id);
//
//             if (user == null)
//             {
//                 response.Success = false;
//                 response.Message = "Nenhum usuário encontrado com o ID informado.";
//                 return response;
//             }
//
//             response.Data = user;
//             response.Message = "Usuário encontrado com sucesso!";
//         }
//         catch (Exception ex)
//         {
//             response.Success = false;
//             response.Message = ex.Message;
//             Console.WriteLine(ex);
//         }
//
//         return response;
//     }
//
//     public async Task<RespondeModel<List<UserModel?>>> GetAllAsync()
//     {
//         RespondeModel<List<UserModel?>> response = new();
//
//         try
//         {
//             var users = await context.Users.ToListAsync();
//
//             response.Data = users;
//             response.Success = true;
//             response.Message = "Usuários encontrados com sucesso!";
//         }
//         catch (Exception ex)
//         {
//             response.Success = false;
//             response.Message = ex.Message;
//             Console.WriteLine(ex.Message);
//         }
//
//         return response;
//     }
//
//     public async Task<RespondeModel<List<UserModel?>>> CreateUserAsync(CreateUserDto createUserDto)
//     {
//         RespondeModel<List<UserModel?>> response = new();
//
//         try
//         {
//             var user = new UserModel
//             {
//                 Username = createUserDto.Username,
//                 Email = createUserDto.Email,
//                 Firstname = createUserDto.Firstname,
//                 Lastname = createUserDto.Lastname,
//                 Password = createUserDto.Password,
//                 UserType = createUserDto.UserType
//             };
//
//             context.Add(user);
//             await context.SaveChangesAsync();
//
//             response.Data = await context.Users.ToListAsync();
//             response.Success = true;
//             response.Message = "Usuário criado com sucesso!";
//         }
//         catch (Exception ex)
//         {
//             response.Success = false;
//             response.Message = ex.Message;
//             Console.WriteLine(ex);
//         }
//
//         return response;
//     }
//
//     public async Task<RespondeModel<List<UserModel?>>> UpdateUserAsync(UpdateUserDto updateUserDto)
//     {
//         RespondeModel<List<UserModel?>> response = new();
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(user => user.Id == updateUserDto.Id);
//
//             if (user is null)
//             {
//                 response.Success = false;
//                 response.Message = "Usuário não encontrado.";
//                 return response;
//             }
//
//             user.Username = updateUserDto.Username;
//             user.Email = updateUserDto.Email;
//             user.Firstname = updateUserDto.Firstname;
//             user.Lastname = updateUserDto.Lastname;
//
//             context.Update(user);
//             await context.SaveChangesAsync();
//
//             response.Data = await context.Users.ToListAsync();
//             response.Success = true;
//             response.Message = "Usuário atualizado com sucesso!";
//         }
//         catch (Exception ex)
//         {
//             response.Success = false;
//             response.Message = ex.Message;
//             Console.WriteLine(ex);
//         }
//
//         return response;
//     }
//
//     public async Task<RespondeModel<List<UserModel?>>> DeleteUserAsync(int id)
//     {
//         RespondeModel<List<UserModel?>> response = new();
//
//         try
//         {
//             var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id);
//
//             if (user == null)
//             {
//                 response.Success = false;
//                 response.Message = "Nenhum usuário encontrado com o ID informado.";
//                 return response;
//             }
//
//             context.Remove(user);
//             await context.SaveChangesAsync();
//
//             response.Data = await context.Users.ToListAsync();
//             response.Success = true;
//             response.Message = $"Usuário {id} deletado com sucesso!";
//         }
//         catch (Exception ex)
//         {
//             response.Success = false;
//             response.Message = ex.Message;
//             Console.WriteLine(ex);
//         }
//
//         return response;
//     }
//
//     public Task<List<UserModel>> GetAllUsersAsync()
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<UserModel?> GetUserByIdAsync(int id)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<UserModel?> GetUserAsync(Expression<Func<UserModel?, bool>> predicate)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<UserModel?> CreateUserAsync(UserModel? user)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<UserModel?> UpdateUserAsync(UserModel? user)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<UserModel?> DeleteUserAsync(UserModel? user)
//     {
//         throw new NotImplementedException();
//     }
// }