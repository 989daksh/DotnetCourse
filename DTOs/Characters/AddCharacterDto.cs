using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCourse.DTOs.Characters
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Takeshi";
        public int Hitpoints { get; set; } = 100;
        public int Strength { get; set; } = 8;
        public int Defence { get; set; } = 9;
        public int Intelligence { get; set; } = 100;
        public RolePlayingClass Class { get; set; } = RolePlayingClass.King;
   
    }
}