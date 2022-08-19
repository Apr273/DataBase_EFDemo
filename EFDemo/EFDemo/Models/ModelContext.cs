using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFDemo.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<YixunAddress> YixunAddresses { get; set; } = null!;
        public virtual DbSet<YixunAdministrator> YixunAdministrators { get; set; } = null!;
        public virtual DbSet<YixunArea> YixunAreas { get; set; } = null!;
        public virtual DbSet<YixunCity> YixunCities { get; set; } = null!;
        public virtual DbSet<YixunClue> YixunClues { get; set; } = null!;
        public virtual DbSet<YixunCluesReport> YixunCluesReports { get; set; } = null!;
        public virtual DbSet<YixunHandleClue> YixunHandleClues { get; set; } = null!;
        public virtual DbSet<YixunHandleInfo> YixunHandleInfos { get; set; } = null!;
        public virtual DbSet<YixunInfoReport> YixunInfoReports { get; set; } = null!;
        public virtual DbSet<YixunNews> YixunNews { get; set; } = null!;
        public virtual DbSet<YixunProvince> YixunProvinces { get; set; } = null!;
        public virtual DbSet<YixunRecruited> YixunRecruiteds { get; set; } = null!;
        public virtual DbSet<YixunRelatedDp> YixunRelatedDps { get; set; } = null!;
        public virtual DbSet<YixunReleased> YixunReleaseds { get; set; } = null!;
        public virtual DbSet<YixunSearchinfo> YixunSearchinfos { get; set; } = null!;
        public virtual DbSet<YixunSearchinfoFocus> YixunSearchinfoFoci { get; set; } = null!;
        public virtual DbSet<YixunSearchinfoFollowup> YixunSearchinfoFollowups { get; set; } = null!;
        public virtual DbSet<YixunVolActivity> YixunVolActivities { get; set; } = null!;
        public virtual DbSet<YixunVolInst> YixunVolInsts { get; set; } = null!;
        public virtual DbSet<YixunVolunteer> YixunVolunteers { get; set; } = null!;
        public virtual DbSet<YixunWebUser> YixunWebUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=8.130.101.207/yixun;Password=HqiuqiuLRM;User ID=C##DEVELOPER;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("C##DEVELOPER")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<YixunAddress>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_ADDRESS");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Detail)
                    .HasColumnType("CLOB")
                    .HasColumnName("DETAIL");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARENT_ID");
            });

            modelBuilder.Entity<YixunAdministrator>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_ADMINISTRATORS");

                entity.Property(e => e.AdministratorCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATOR_CODE");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.AdministratorName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATOR_NAME");

                entity.Property(e => e.AdministratorPhone)
                    .HasPrecision(11)
                    .HasColumnName("ADMINISTRATOR_PHONE");
            });

            modelBuilder.Entity<YixunArea>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_AREA");

                entity.Property(e => e.Area)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("AREA");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AREA_ID");

                entity.Property(e => e.Parent)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PARENT");

                entity.Property(e => e.RId)
                    .HasPrecision(11)
                    .HasColumnName("R_ID");
            });

            modelBuilder.Entity<YixunCity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_CITY");

                entity.Property(e => e.CId)
                    .HasPrecision(11)
                    .HasColumnName("C_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CITY_ID");

                entity.Property(e => e.Parent)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PARENT");
            });

            modelBuilder.Entity<YixunClue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_CLUES");

                entity.Property(e => e.ClueContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("CLUE_CONTENT");

                entity.Property(e => e.ClueDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CLUE_DATE");

                entity.Property(e => e.ClueId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_ID");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<YixunCluesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_CLUES_REPORT");

                entity.Property(e => e.ClueId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_ID");

                entity.Property(e => e.ClueReportId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_REPORT_ID");

                entity.Property(e => e.Isreviewed)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISREVIEWED");

                entity.Property(e => e.ReportContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("REPORT_CONTENT");

                entity.Property(e => e.ReportTime)
                    .HasColumnType("DATE")
                    .HasColumnName("REPORT_TIME");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<YixunHandleClue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_HANDLE_CLUES");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.ClueReportId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_REPORT_ID");
            });

            modelBuilder.Entity<YixunHandleInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_HANDLE_INFO");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.InfoReportId)
                    .HasPrecision(10)
                    .HasColumnName("INFO_REPORT_ID");
            });

            modelBuilder.Entity<YixunInfoReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_INFO_REPORT");

                entity.Property(e => e.InfoReportId)
                    .HasPrecision(10)
                    .HasColumnName("INFO_REPORT_ID");

                entity.Property(e => e.Isreviewed)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISREVIEWED");

                entity.Property(e => e.ReportContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("REPORT_CONTENT");

                entity.Property(e => e.ReportTime)
                    .HasColumnType("DATE")
                    .HasColumnName("REPORT_TIME");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<YixunNews>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_NEWS");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.NewsContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("NEWS_CONTENT");

                entity.Property(e => e.NewsHeadlines)
                    .HasColumnType("CLOB")
                    .HasColumnName("NEWS_HEADLINES");

                entity.Property(e => e.NewsId)
                    .HasPrecision(10)
                    .HasColumnName("NEWS_ID");

                entity.Property(e => e.NewsTime)
                    .HasColumnType("DATE")
                    .HasColumnName("NEWS_TIME");

                entity.Property(e => e.NewsTitlepages)
                    .HasColumnType("BLOB")
                    .HasColumnName("NEWS_TITLEPAGES");

                entity.Property(e => e.NewsType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NEWS_TYPE");
            });

            modelBuilder.Entity<YixunProvince>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_PROVINCE");

                entity.Property(e => e.Province)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.SId)
                    .HasPrecision(11)
                    .HasColumnName("S_ID");
            });

            modelBuilder.Entity<YixunRecruited>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_RECRUITED");

                entity.Property(e => e.VolActId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ACT_ID");

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");
            });

            modelBuilder.Entity<YixunRelatedDp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_RELATED_DP");

                entity.Property(e => e.Address)
                    .HasColumnType("CLOB")
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Contact)
                    .HasColumnType("CLOB")
                    .HasColumnName("CONTACT");

                entity.Property(e => e.DpId)
                    .HasPrecision(10)
                    .HasColumnName("DP_ID");

                entity.Property(e => e.DpName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DP_NAME");

                entity.Property(e => e.Website)
                    .HasColumnType("CLOB")
                    .HasColumnName("WEBSITE");
            });

            modelBuilder.Entity<YixunReleased>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_RELEASED");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("DATE")
                    .HasColumnName("RELEASE_TIME");

                entity.Property(e => e.VolActId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ACT_ID");

                entity.Property(e => e.VolInstId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_INST_ID");
            });

            modelBuilder.Entity<YixunSearchinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_SEARCHINFO");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Isreport)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ISREPORT");

                entity.Property(e => e.SearchType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SEARCH_TYPE");

                entity.Property(e => e.SearchinfoDate)
                    .HasColumnType("DATE")
                    .HasColumnName("SEARCHINFO_DATE");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.SearchinfoLostdate)
                    .HasColumnType("DATE")
                    .HasColumnName("SEARCHINFO_LOSTDATE");

                entity.Property(e => e.SearchinfoPhoto)
                    .HasColumnType("BLOB")
                    .HasColumnName("SEARCHINFO_PHOTO");

                entity.Property(e => e.SoughtPeopleBirthday)
                    .HasColumnType("DATE")
                    .HasColumnName("SOUGHT_PEOPLE_BIRTHDAY");

                entity.Property(e => e.SoughtPeopleDetail)
                    .HasColumnType("CLOB")
                    .HasColumnName("SOUGHT_PEOPLE_DETAIL");

                entity.Property(e => e.SoughtPeopleGender)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("SOUGHT_PEOPLE_GENDER");

                entity.Property(e => e.SoughtPeopleName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SOUGHT_PEOPLE_NAME");

                entity.Property(e => e.SoughtPeopleState)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SOUGHT_PEOPLE_STATE");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<YixunSearchinfoFocus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_SEARCHINFO_FOCUS");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<YixunSearchinfoFollowup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_SEARCHINFO_FOLLOWUP");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");
            });

            modelBuilder.Entity<YixunVolActivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_VOL_ACTIVITY");

                entity.Property(e => e.ActContent)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACT_CONTENT");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.ExpTime)
                    .HasColumnType("DATE")
                    .HasColumnName("EXP_TIME");

                entity.Property(e => e.VolActId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ACT_ID");

                entity.Property(e => e.VolActName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VOL_ACT_NAME");
            });

            modelBuilder.Entity<YixunVolInst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_VOL_INST");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.FundationTime)
                    .HasColumnType("DATE")
                    .HasColumnName("FUNDATION_TIME");

                entity.Property(e => e.InstHead)
                    .HasColumnType("BLOB")
                    .HasColumnName("INST_HEAD");

                entity.Property(e => e.InstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("INST_NAME");

                entity.Property(e => e.Passwords)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDS");

                entity.Property(e => e.VolInstCred)
                    .HasColumnType("BLOB")
                    .HasColumnName("VOL_INST_CRED");

                entity.Property(e => e.VolInstId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_INST_ID");
            });

            modelBuilder.Entity<YixunVolunteer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_VOLUNTEER");

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");

                entity.Property(e => e.VolInstId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_INST_ID");

                entity.Property(e => e.VolScore)
                    .HasPrecision(2)
                    .HasColumnName("VOL_SCORE");
            });

            modelBuilder.Entity<YixunWebUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("YIXUN_WEB_USER");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.FundationTime)
                    .HasColumnType("DATE")
                    .HasColumnName("FUNDATION_TIME");

                entity.Property(e => e.MailboxNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MAILBOX_NUM");

                entity.Property(e => e.PhoneNum)
                    .HasPrecision(11)
                    .HasColumnName("PHONE_NUM");

                entity.Property(e => e.PriorPnum)
                    .HasPrecision(4)
                    .HasColumnName("PRIOR_PNUM");

                entity.Property(e => e.UserGender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("USER_GENDER");

                entity.Property(e => e.UserHead)
                    .HasColumnType("BLOB")
                    .HasColumnName("USER_HEAD");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserPasswords)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASSWORDS");

                entity.Property(e => e.UserState)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("USER_STATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
