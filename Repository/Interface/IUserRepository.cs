﻿using BussinessObject.DTOs;
using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<UserInfoDTO> GetByIdAsync(int id);
        Task<bool> AuthenticateAsync(string username, string password);
        Task CreateUserAsync(string username, string password,
                string fullName, string email, string? phoneNumber,
                string? address, string? gender, string role, DateTime? birthdate);
        Task<bool> UsernameExistsAsync(string username);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string? role);
        Task<IEnumerable<UserInfoDTO>> GetAllUsers();
        Task UpdateUserAsync(UserInfoDTO user);
        Task CreateEmployeeUserAsync(UserInfoDTO user);

        Task<List<UserInfoDTO>> GetAllUsersAsync();

        Task<List<User>> GetEmployeeUserAsync(string[]? roles = null);
        Task DeleteUserAsync(int id);
    }
}
