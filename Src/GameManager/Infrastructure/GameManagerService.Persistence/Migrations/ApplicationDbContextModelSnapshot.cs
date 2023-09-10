﻿// <auto-generated />
using System;
using GameManagerService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameManagerService.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameManagerService.Domain.Entities.FriendGameRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("PlayerRecieverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerSenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerRecieverId");

                    b.HasIndex("PlayerSenderId");

                    b.ToTable("FriendGameRequests");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BetAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.GamePlayers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("PlayerOneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerTwoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("GamePlayers");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.GameState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoardState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("CurrentPlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentPlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.MatchMaking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("PlayerSenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RandomMatchMakerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerSenderId");

                    b.HasIndex("RandomMatchMakerId");

                    b.ToTable("MatchMakings");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Move", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DiceRollOne")
                        .HasColumnType("int");

                    b.Property<int>("DiceRollTwo")
                        .HasColumnType("int");

                    b.Property<int>("FromPoint")
                        .HasColumnType("int");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamePlayersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ToPoint")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GamePlayersId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PlayerColor")
                        .HasColumnType("int");

                    b.Property<int>("TotalLose")
                        .HasColumnType("int");

                    b.Property<int>("TotalWin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.FriendGameRequest", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Player", "PlayerReciever")
                        .WithMany("SenderFriendGameRequests")
                        .HasForeignKey("PlayerRecieverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameManagerService.Domain.Entities.Player", "PlayerSender")
                        .WithMany("RecieverFriendGameRequests")
                        .HasForeignKey("PlayerSenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PlayerReciever");

                    b.Navigation("PlayerSender");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Game", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Player", null)
                        .WithMany("Games")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.GamePlayers", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Game", "Game")
                        .WithOne("GamePlayers")
                        .HasForeignKey("GameManagerService.Domain.Entities.GamePlayers", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameManagerService.Domain.Entities.Player", "PlayerOne")
                        .WithMany("PlayerTwoGamePlays")
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameManagerService.Domain.Entities.Player", "PlayerTwo")
                        .WithMany("PlayerOneGamePlays")
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("PlayerOne");

                    b.Navigation("PlayerTwo");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.GameState", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Player", "CurrentPlayer")
                        .WithMany()
                        .HasForeignKey("CurrentPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameManagerService.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentPlayer");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.MatchMaking", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Player", "PlayerSender")
                        .WithMany("RandomMatchMakingRequests")
                        .HasForeignKey("PlayerSenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameManagerService.Domain.Entities.Player", "RandomMatchMaker")
                        .WithMany("SenderMatchMakingRequests")
                        .HasForeignKey("RandomMatchMakerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PlayerSender");

                    b.Navigation("RandomMatchMaker");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Move", b =>
                {
                    b.HasOne("GameManagerService.Domain.Entities.Game", null)
                        .WithMany("Moves")
                        .HasForeignKey("GameId");

                    b.HasOne("GameManagerService.Domain.Entities.GamePlayers", "GamePlayers")
                        .WithMany()
                        .HasForeignKey("GamePlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GamePlayers");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Game", b =>
                {
                    b.Navigation("GamePlayers")
                        .IsRequired();

                    b.Navigation("Moves");
                });

            modelBuilder.Entity("GameManagerService.Domain.Entities.Player", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("PlayerOneGamePlays");

                    b.Navigation("PlayerTwoGamePlays");

                    b.Navigation("RandomMatchMakingRequests");

                    b.Navigation("RecieverFriendGameRequests");

                    b.Navigation("SenderFriendGameRequests");

                    b.Navigation("SenderMatchMakingRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
