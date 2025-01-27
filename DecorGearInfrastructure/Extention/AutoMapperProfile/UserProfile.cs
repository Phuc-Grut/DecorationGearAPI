﻿using Application.DataTransferObj.User.Request;
using AutoMapper;
using DecorGearDomain.Data.Entities;
using Ecommerce.Application.DataTransferObj.User.Request;

namespace DecorGearInfrastructure.Extention.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Ánh xạ từ UserCreateRequest sang User
            CreateMap<UserCreateRequest, User>().ReverseMap();
            CreateMap<User, UserDto>();
            CreateMap<UserUpdateRequest, User>();
        }
    }
}
