using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PracticeTimer.Data;

namespace practnet.Migrations
{
    [DbContext(typeof(PracticeTimerContext))]
    [Migration("20160827192230_inital igration")]
    partial class initaligration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("PracticeTimer.Data.Entities.Timer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<bool>("Paused");

                    b.Property<int>("StartTime");

                    b.Property<bool>("Ticking");

                    b.Property<int>("Time");

                    b.Property<Guid>("TimerGroupId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("TimerGroupId");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("PracticeTimer.Data.Entities.TimerGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("TimerGroups");
                });

            modelBuilder.Entity("PracticeTimer.Data.Entities.Timer", b =>
                {
                    b.HasOne("PracticeTimer.Data.Entities.TimerGroup", "TimerGroup")
                        .WithMany("Timers")
                        .HasForeignKey("TimerGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
