﻿// <auto-generated />
using System;
using Coupon.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coupon.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CouponContext))]
    partial class CouponContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Coupon.Domain.Entities.PromoCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Code");

                    b.ToTable("PromoCodes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
