using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCourse.DTOs;

namespace DotnetCourse.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}