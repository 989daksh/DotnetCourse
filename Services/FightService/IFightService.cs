using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCourse.DTOs.Fight;

namespace DotnetCourse.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> FightMatch(FightRequestDto request);
        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();


    }
}