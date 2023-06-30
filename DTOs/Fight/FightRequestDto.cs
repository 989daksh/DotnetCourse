using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCourse.DTOs.Fight
{
    public class FightRequestDto
    {
        public List<int> CharactersIds { get; set; } = new List<int>();
    }
}