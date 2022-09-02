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
        public virtual DbSet<YixunInfoReport> YixunInfoReports { get; set; } = null!;
        public virtual DbSet<YixunNews> YixunNews { get; set; } = null!;
        public virtual DbSet<YixunProvince> YixunProvinces { get; set; } = null!;
        public virtual DbSet<YixunRecruited> YixunRecruiteds { get; set; } = null!;
        public virtual DbSet<YixunRelatedDp> YixunRelatedDps { get; set; } = null!;
        public virtual DbSet<YixunSearchinfo> YixunSearchinfos { get; set; } = null!;
        public virtual DbSet<YixunSearchinfoFocus> YixunSearchinfoFoci { get; set; } = null!;
        public virtual DbSet<YixunSearchinfoFollowup> YixunSearchinfoFollowups { get; set; } = null!;
        public virtual DbSet<YixunVolActivity> YixunVolActivities { get; set; } = null!;
        public virtual DbSet<YixunVolApply> YixunVolApplies { get; set; } = null!;
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
                entity.HasKey(e => e.AddressId)
                    .HasName("YIXUN_ADDRESS_PK");

                entity.ToTable("YIXUN_ADDRESS");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("AREA_ID");

                entity.Property(e => e.CityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CITY_ID");

                entity.Property(e => e.Detail)
                    .HasColumnType("CLOB")
                    .HasColumnName("DETAIL");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE_ID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.YixunAddresses)
                    .HasPrincipalKey(p => p.CityId)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_ADDRESS_FK2");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.YixunAddresses)
                    .HasPrincipalKey(p => p.ProvinceId)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_ADDRESS_FK3");
            });

            modelBuilder.Entity<YixunAdministrator>(entity =>
            {
                entity.HasKey(e => e.AdministratorId)
                    .HasName("YIXUN_ADMINISTRATORS_PK");

                entity.ToTable("YIXUN_ADMINISTRATORS");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.AdministratorCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATOR_CODE");

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
                entity.HasKey(e => e.RId)
                    .HasName("YIXUN_AREA_PK");

                entity.ToTable("YIXUN_AREA");

                entity.HasIndex(e => e.AreaId, "YIXUN_AREA_UK1")
                    .IsUnique();

                entity.Property(e => e.RId)
                    .HasPrecision(11)
                    .ValueGeneratedNever()
                    .HasColumnName("R_ID");

                entity.Property(e => e.Area)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("AREA");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("AREA_ID");

                entity.Property(e => e.Parent)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PARENT");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.YixunAreas)
                    .HasPrincipalKey(p => p.CityId)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("YIXUN_AREA_FK1");
            });

            modelBuilder.Entity<YixunCity>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("YIXUN_CITY_PK");

                entity.ToTable("YIXUN_CITY");

                entity.HasIndex(e => e.CityId, "YIXUN_CITY_UK1")
                    .IsUnique();

                entity.Property(e => e.CId)
                    .HasPrecision(11)
                    .ValueGeneratedNever()
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

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.YixunCities)
                    .HasPrincipalKey(p => p.ProvinceId)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("YIXUN_CITY_FK1");
            });

            modelBuilder.Entity<YixunClue>(entity =>
            {
                entity.HasKey(e => e.ClueId)
                    .HasName("YIXUN_CLUES_PK");

                entity.ToTable("YIXUN_CLUE");

                entity.Property(e => e.ClueId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_ID");

                entity.Property(e => e.ClueContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("CLUE_CONTENT");

                entity.Property(e => e.ClueDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CLUE_DATE")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.Isactive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISACTIVE")
                    .HasDefaultValueSql("'Y' ");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Searchinfo)
                    .WithMany(p => p.YixunClues)
                    .HasForeignKey(d => d.SearchinfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_CLUES_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunClues)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_CLUES_FK1");
            });

            modelBuilder.Entity<YixunCluesReport>(entity =>
            {
                entity.HasKey(e => e.ClueReportId)
                    .HasName("YIXUN_CLUES_REPORT_PK");

                entity.ToTable("YIXUN_CLUES_REPORT");

                entity.Property(e => e.ClueReportId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_REPORT_ID");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.ClueId)
                    .HasPrecision(10)
                    .HasColumnName("CLUE_ID");

                entity.Property(e => e.Ispass)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISPASS")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.Isreviewed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISREVIEWED")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.ReportContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("REPORT_CONTENT");

                entity.Property(e => e.ReportTime)
                    .HasColumnType("DATE")
                    .HasColumnName("REPORT_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.YixunCluesReports)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_CLUES_REPORT_FK3");

                entity.HasOne(d => d.Clue)
                    .WithMany(p => p.YixunCluesReports)
                    .HasForeignKey(d => d.ClueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_CLUES_REPORT_FK1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunCluesReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_CLUES_REPORT_FK2");
            });

            modelBuilder.Entity<YixunInfoReport>(entity =>
            {
                entity.HasKey(e => e.InfoReportId)
                    .HasName("YIXUN_INFO_REPORT_PK");

                entity.ToTable("YIXUN_INFO_REPORT");

                entity.Property(e => e.InfoReportId)
                    .HasPrecision(10)
                    .HasColumnName("INFO_REPORT_ID");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.Ispass)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISPASS")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.Isreviewed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISREVIEWED")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.ReportContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("REPORT_CONTENT");

                entity.Property(e => e.ReportTime)
                    .HasColumnType("DATE")
                    .HasColumnName("REPORT_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.YixunInfoReports)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_INFO_REPORT_FK3");

                entity.HasOne(d => d.Searchinfo)
                    .WithMany(p => p.YixunInfoReports)
                    .HasForeignKey(d => d.SearchinfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_INFO_REPORT_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunInfoReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_INFO_REPORT_FK1");
            });

            modelBuilder.Entity<YixunNews>(entity =>
            {
                entity.HasKey(e => e.NewsId)
                    .HasName("YIXUN_NEWS_PK");

                entity.ToTable("YIXUN_NEWS");

                entity.Property(e => e.NewsId)
                    .HasPrecision(10)
                    .HasColumnName("NEWS_ID");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.Isactive)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISACTIVE")
                    .HasDefaultValueSql("'Y' ");

                entity.Property(e => e.NewsContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("NEWS_CONTENT");

                entity.Property(e => e.NewsHeadlines)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NEWS_HEADLINES");

                entity.Property(e => e.NewsTime)
                    .HasColumnType("DATE")
                    .HasColumnName("NEWS_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.NewsTitlepagesUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("NEWS_TITLEPAGES_URL");

                entity.Property(e => e.NewsType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NEWS_TYPE");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.YixunNews)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_NEWS_FK1");
            });

            modelBuilder.Entity<YixunProvince>(entity =>
            {
                entity.HasKey(e => e.SId)
                    .HasName("YIXUN_PROVINCE_PK");

                entity.ToTable("YIXUN_PROVINCE");

                entity.HasIndex(e => e.ProvinceId, "YIXUN_PROVINCE_UK1")
                    .IsUnique();

                entity.Property(e => e.SId)
                    .HasPrecision(11)
                    .ValueGeneratedNever()
                    .HasColumnName("S_ID");

                entity.Property(e => e.Province)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE_ID");
            });

            modelBuilder.Entity<YixunRecruited>(entity =>
            {
                entity.HasKey(e => new { e.VolActId, e.VolId })
                    .HasName("YIXUN_RECRUITED_PK");

                entity.ToTable("YIXUN_RECRUITED");

                entity.Property(e => e.VolActId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ACT_ID");

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");

                entity.Property(e => e.Recruittime)
                    .HasColumnType("DATE")
                    .HasColumnName("RECRUITTIME")
                    .HasDefaultValueSql("sysdate ");

                entity.HasOne(d => d.VolAct)
                    .WithMany(p => p.YixunRecruiteds)
                    .HasForeignKey(d => d.VolActId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_RECRUITED_FK1");

                entity.HasOne(d => d.Vol)
                    .WithMany(p => p.YixunRecruiteds)
                    .HasForeignKey(d => d.VolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_RECRUITED_FK2");
            });

            modelBuilder.Entity<YixunRelatedDp>(entity =>
            {
                entity.HasKey(e => e.DpId)
                    .HasName("YIXUN_RELATED_DP_PK");

                entity.ToTable("YIXUN_RELATED_DP");

                entity.Property(e => e.DpId)
                    .HasPrecision(10)
                    .HasColumnName("DP_ID");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.ContactMethod)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_METHOD");

                entity.Property(e => e.DpName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DP_NAME");

                entity.Property(e => e.DpPicUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("DP_PIC_URL");

                entity.Property(e => e.Website)
                    .HasColumnType("CLOB")
                    .HasColumnName("WEBSITE");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.YixunRelatedDps)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("YIXUN_RELATED_DP_FK2");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.YixunRelatedDps)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_RELATED_DP_FK1");
            });

            modelBuilder.Entity<YixunSearchinfo>(entity =>
            {
                entity.HasKey(e => e.SearchinfoId)
                    .HasName("YIXUN_SEARCHINFO_PK");

                entity.ToTable("YIXUN_SEARCHINFO");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.ContactMethod)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_METHOD");

                entity.Property(e => e.Isactive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISACTIVE")
                    .HasDefaultValueSql("'Y' ");

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
                    .HasColumnName("SEARCHINFO_DATE")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.SearchinfoLostdate)
                    .HasColumnType("DATE")
                    .HasColumnName("SEARCHINFO_LOSTDATE");

                entity.Property(e => e.SearchinfoPhotoUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("SEARCHINFO_PHOTO_URL");

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

                entity.Property(e => e.SoughtPeopleHeight)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SOUGHT_PEOPLE_HEIGHT");

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

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.YixunSearchinfos)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("YIXUN_SEARCHINFO_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunSearchinfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_SEARCHINFO_FK1");
            });

            modelBuilder.Entity<YixunSearchinfoFocus>(entity =>
            {
                entity.HasKey(e => new { e.SearchinfoId, e.UserId })
                    .HasName("YIXUN_SEARCHINFO_FOCUS_PK");

                entity.ToTable("YIXUN_SEARCHINFO_FOCUS");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Focustime)
                    .HasColumnType("DATE")
                    .HasColumnName("FOCUSTIME")
                    .HasDefaultValueSql("sysdate ");

                entity.HasOne(d => d.Searchinfo)
                    .WithMany(p => p.YixunSearchinfoFoci)
                    .HasForeignKey(d => d.SearchinfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_SEARCHINFO_FOCUS_FK1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunSearchinfoFoci)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_SEARCHINFO_FOCUS_FK2");
            });

            modelBuilder.Entity<YixunSearchinfoFollowup>(entity =>
            {
                entity.HasKey(e => new { e.SearchinfoId, e.VolId })
                    .HasName("YIXUN_SEARCHINFO_FOLLOWUP_PK");

                entity.ToTable("YIXUN_SEARCHINFO_FOLLOWUP");

                entity.Property(e => e.SearchinfoId)
                    .HasPrecision(10)
                    .HasColumnName("SEARCHINFO_ID");

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");

                entity.Property(e => e.Followtime)
                    .HasColumnType("DATE")
                    .HasColumnName("FOLLOWTIME")
                    .HasDefaultValueSql("sysdate ");

                entity.HasOne(d => d.Searchinfo)
                    .WithMany(p => p.YixunSearchinfoFollowups)
                    .HasForeignKey(d => d.SearchinfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_SEARCHINFO_FOLLOWUP_FK2");

                entity.HasOne(d => d.Vol)
                    .WithMany(p => p.YixunSearchinfoFollowups)
                    .HasForeignKey(d => d.VolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_SEARCHINFO_FOLLOWUP_FK1");
            });

            modelBuilder.Entity<YixunVolActivity>(entity =>
            {
                entity.HasKey(e => e.VolActId)
                    .HasName("YIXUN_VOL_ACTIVITY_PK");

                entity.ToTable("YIXUN_VOL_ACTIVITY");

                entity.Property(e => e.VolActId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ACT_ID");

                entity.Property(e => e.ActContent)
                    .HasColumnType("CLOB")
                    .HasColumnName("ACT_CONTENT");

                entity.Property(e => e.ActPicUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("ACT_PIC_URL");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.ContactMethod)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_METHOD");

                entity.Property(e => e.ExpTime)
                    .HasColumnType("DATE")
                    .HasColumnName("EXP_TIME");

                entity.Property(e => e.Needpeople)
                    .HasPrecision(5)
                    .HasColumnName("NEEDPEOPLE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("DATE")
                    .HasColumnName("RELEASE_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.SignupPeople)
                    .HasPrecision(5)
                    .HasColumnName("SIGNUP_PEOPLE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.VolActName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VOL_ACT_NAME");

                entity.Property(e => e.VolInstId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_INST_ID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.YixunVolActivities)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("YIXUN_VOL_ACTIVITY_FK1");

                entity.HasOne(d => d.VolInst)
                    .WithMany(p => p.YixunVolActivities)
                    .HasForeignKey(d => d.VolInstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_VOL_ACTIVITY_FK2");
            });

            modelBuilder.Entity<YixunVolApply>(entity =>
            {
                entity.HasKey(e => e.VolApplyId)
                    .HasName("YIXUN_VOL_APPLY_PK");

                entity.ToTable("YIXUN_VOL_APPLY");

                entity.Property(e => e.VolApplyId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_APPLY_ID");

                entity.Property(e => e.AdministratorId)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.ApplicationTime)
                    .HasColumnType("DATE")
                    .HasColumnName("APPLICATION_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.Background)
                    .HasColumnType("CLOB")
                    .HasColumnName("BACKGROUND");

                entity.Property(e => e.Career)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CAREER");

                entity.Property(e => e.Ispass)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISPASS")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.Isreviewed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISREVIEWED")
                    .HasDefaultValueSql("'N' ");

                entity.Property(e => e.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SPECIALTY");

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.YixunVolApplies)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_VOL_APPLY_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.YixunVolApplies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_VOL_APPLY_FK1");
            });

            modelBuilder.Entity<YixunVolInst>(entity =>
            {
                entity.HasKey(e => e.VolInstId)
                    .HasName("YIXUN_VOL_INST_PK");

                entity.ToTable("YIXUN_VOL_INST");

                entity.Property(e => e.VolInstId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_INST_ID");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.ContactMethod)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_METHOD");

                entity.Property(e => e.FundationTime)
                    .HasColumnType("DATE")
                    .HasColumnName("FUNDATION_TIME");

                entity.Property(e => e.InstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("INST_NAME");

                entity.Property(e => e.InstPicUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("INST_PIC_URL");

                entity.Property(e => e.InstSlogan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("INST_SLOGAN");

                entity.Property(e => e.Passwords)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDS");

                entity.Property(e => e.PeopleCount)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PEOPLE_COUNT");

                entity.Property(e => e.VolInstCredUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("VOL_INST_CRED_URL");

                entity.Property(e => e.VolInstIntroduce)
                    .HasColumnType("CLOB")
                    .HasColumnName("VOL_INST_INTRODUCE");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.YixunVolInsts)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("YIXUN_VOL_INST_FK");
            });

            modelBuilder.Entity<YixunVolunteer>(entity =>
            {
                entity.HasKey(e => e.VolId)
                    .HasName("YIXUN_VOLUNTEER_PK");

                entity.ToTable("YIXUN_VOLUNTEER");

                entity.HasIndex(e => e.VolUserId, "YIXUN_VOLUNTEER_UK1")
                    .IsUnique();

                entity.Property(e => e.VolId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_ID");

                entity.Property(e => e.VolTime)
                    .HasPrecision(5)
                    .HasColumnName("VOL_TIME")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.VolUserId)
                    .HasPrecision(10)
                    .HasColumnName("VOL_USER_ID");

                entity.HasOne(d => d.VolUser)
                    .WithOne(p => p.YixunVolunteer)
                    .HasForeignKey<YixunVolunteer>(d => d.VolUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("YIXUN_VOLUNTEER_FK2");
            });

            modelBuilder.Entity<YixunWebUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("YIXUN_WEB_USER_PK");

                entity.ToTable("YIXUN_WEB_USER");

                entity.HasIndex(e => e.PhoneNum, "YIXUN_WEB_USER_UK1")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasPrecision(10)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.AddressId)
                    .HasPrecision(10)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.FundationTime)
                    .HasColumnType("DATE")
                    .HasColumnName("FUNDATION_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.Isactive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISACTIVE")
                    .HasDefaultValueSql("'Y' ");

                entity.Property(e => e.LastloginIp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTLOGIN_IP");

                entity.Property(e => e.LastloginTime)
                    .HasColumnType("DATE")
                    .HasColumnName("LASTLOGIN_TIME")
                    .HasDefaultValueSql("sysdate ");

                entity.Property(e => e.MailboxNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MAILBOX_NUM");

                entity.Property(e => e.PhoneNum)
                    .HasPrecision(11)
                    .HasColumnName("PHONE_NUM");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TOKEN");

                entity.Property(e => e.UserGender)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("USER_GENDER")
                    .HasDefaultValueSql("'女' ");

                entity.Property(e => e.UserHeadUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("USER_HEAD_URL");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserPasswords)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASSWORDS");

                entity.Property(e => e.UserState)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USER_STATE")
                    .HasDefaultValueSql("'Y' ");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.YixunWebUsers)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("YIXUN_WEB_USER_FK1");
            });

            modelBuilder.HasSequence("ADDRESS_SEQUENCE");

            modelBuilder.HasSequence("ADMINISTRATORS_SEQUENCE");

            modelBuilder.HasSequence("CLUES_REPORT_SEQUENCE");

            modelBuilder.HasSequence("CLUES_SEQUENCE");

            modelBuilder.HasSequence("INFO_REPORT_SEQUENCE");

            modelBuilder.HasSequence("NEWS_SEQUENCE");

            modelBuilder.HasSequence("RELATED_DP_SEQUENCE");

            modelBuilder.HasSequence("SEARCHINFO_SEQUENCE");

            modelBuilder.HasSequence("VOL_ACT_SEQUENCE");

            modelBuilder.HasSequence("VOL_APPLY_SEQUENCE");

            modelBuilder.HasSequence("VOL_INST_SEQUENCE");

            modelBuilder.HasSequence("VOLUNTEER_SEQUENCE");

            modelBuilder.HasSequence("WEB_USER_SEQUENCE");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
