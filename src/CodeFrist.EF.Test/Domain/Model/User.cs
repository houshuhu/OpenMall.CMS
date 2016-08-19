using System.ComponentModel.DataAnnotations;
using CPy.Domain.Entities;

namespace CodeFrist.EF.Test.Domain.Model
{
    public class User:Entity
    {
        [MaxLength(20)]
        public string UName { get; set; }

    }
}