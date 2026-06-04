using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static GetUserDto ToGetUserDto(this User user)
        {
            return new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}