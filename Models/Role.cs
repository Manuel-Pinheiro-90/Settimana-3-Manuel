﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Settimana_3_Manuel.Models
{
    public class Role
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        public List<User> Users { get; set; } = [];


    }
}
