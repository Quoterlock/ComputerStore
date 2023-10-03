﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models.Domains
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUri { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
    }
}
