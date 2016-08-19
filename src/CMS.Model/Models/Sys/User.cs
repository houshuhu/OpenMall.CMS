using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPy.Domain.Entities;

namespace CMS.Model.Models.Sys
{
    public class User:Entity
    {
        [MaxLength(20)]
        public string UName { get; set; }
    }
}