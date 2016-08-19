using System;
using System.Data.Entity.ModelConfiguration;
using CPy.Domain.Entities;

namespace CPy.EntityFramework.FluentApi
{
    public class BaseEntityTypeConfiguration<TModel> : EntityTypeConfiguration<TModel> where TModel : class ,IEntity<Guid>
    {

    }
}