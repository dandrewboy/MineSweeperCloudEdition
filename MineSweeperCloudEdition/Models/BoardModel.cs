using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    public class BoardModel
    {
        [Required]
        [Range(5,13)]
        [DisplayName("Board Size: between 5 and 13")]
        public int Size { get; set; }
        [Required]
        [Range(1,5)]
        [DisplayName("Set Difficulty: 1 to 5")]
        public int Difficulty { get; set; }
    }
}
