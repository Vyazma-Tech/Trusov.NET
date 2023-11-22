﻿// <auto-generated />
using System;
using Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231120190001_UserFullName")]
    partial class UserFullName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Kernel.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Domain.Core.Order.OrderEntity", b =>
                {
                    b.HasBaseType("Domain.Kernel.Entity");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Paid")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QueueId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Ready")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("SubscriptionEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("CreationDate")
                        .IsDescending();

                    b.HasIndex("QueueId");

                    b.HasIndex("SubscriptionEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Core.Queue.QueueEntity", b =>
                {
                    b.HasBaseType("Domain.Kernel.Entity");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Expired")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasIndex("CreationDate")
                        .IsDescending();

                    b.ToTable("Queues");
                });

            modelBuilder.Entity("Domain.Core.Subscription.SubscriptionEntity", b =>
                {
                    b.HasBaseType("Domain.Kernel.Entity");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("QueueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("QueueId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Domain.Core.User.UserEntity", b =>
                {
                    b.HasBaseType("Domain.Kernel.Entity");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TelegramId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Core.Order.OrderEntity", b =>
                {
                    b.HasOne("Domain.Core.Queue.QueueEntity", "Queue")
                        .WithMany("Items")
                        .HasForeignKey("QueueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Core.Subscription.SubscriptionEntity", null)
                        .WithMany("Orders")
                        .HasForeignKey("SubscriptionEntityId");

                    b.HasOne("Domain.Core.User.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Queue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Core.Queue.QueueEntity", b =>
                {
                    b.OwnsOne("Domain.Core.ValueObjects.QueueActivityBoundaries", "ActivityBoundaries", b1 =>
                        {
                            b1.Property<Guid>("QueueEntityId")
                                .HasColumnType("uuid");

                            b1.Property<TimeOnly>("ActiveFrom")
                                .HasColumnType("time without time zone")
                                .HasColumnName("ActiveFrom");

                            b1.Property<TimeOnly>("ActiveUntil")
                                .HasColumnType("time without time zone")
                                .HasColumnName("ActiveUntil");

                            b1.HasKey("QueueEntityId");

                            b1.ToTable("Queues");

                            b1.WithOwner()
                                .HasForeignKey("QueueEntityId");
                        });

                    b.Navigation("ActivityBoundaries")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Core.Subscription.SubscriptionEntity", b =>
                {
                    b.HasOne("Domain.Core.Queue.QueueEntity", "Queue")
                        .WithOne()
                        .HasForeignKey("Domain.Core.Subscription.SubscriptionEntity", "QueueId");

                    b.HasOne("Domain.Core.User.UserEntity", "User")
                        .WithOne()
                        .HasForeignKey("Domain.Core.Subscription.SubscriptionEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Queue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Core.Queue.QueueEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Core.Subscription.SubscriptionEntity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
