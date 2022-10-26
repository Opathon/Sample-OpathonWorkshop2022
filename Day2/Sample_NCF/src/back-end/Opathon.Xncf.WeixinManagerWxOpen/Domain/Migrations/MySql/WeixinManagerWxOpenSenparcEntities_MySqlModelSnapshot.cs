﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Opathon.Xncf.WeixinManagerWxOpen.Models;

namespace Opathon.Xncf.WeixinManagerWxOpen.Deomain.Migrations.Migrations.MySql
{
    [DbContext(typeof(WeixinManagerWxOpenSenparcEntities_MySql))]
    partial class WeixinManagerWxOpenSenparcEntities_MySqlModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Opathon.Xncf.WeixinManagerWxOpen.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdditionNote")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4");

                    b.Property<int>("Blue")
                        .HasColumnType("int");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Green")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Red")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Opathon_WeixinManagerWxOpen_Color");
                });
#pragma warning restore 612, 618
        }
    }
}