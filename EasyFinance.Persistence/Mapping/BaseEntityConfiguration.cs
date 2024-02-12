﻿using EasyFinance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyFinance.Persistence.Mapping
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreatedDate).IsRequired(false);
            builder.Property(p => p.ModifiedAt).IsRequired(false);
            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
