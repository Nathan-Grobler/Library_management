using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryModel
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(23)")]
        public string BookName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(23)")]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Pages { get; set; }

        [Column(TypeName = "nvarchar(23)")]
        public string Read { get; set; }

        [Column(TypeName = "int")]
        public int Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd}")]
        public DateTime Date { get; set; }
    }
}
