using System;
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Reports.Infrastructure.Entities;

namespace Reports.Infrastructure.Data;

public partial class DwhDbContext : DbContext
{
    public DwhDbContext (DbContextOptions<DwhDbContext> options) : base(options) {}
    
    public virtual DbSet<CpDwhKa0133> CpDwhKa0133s { get; set; }
    public virtual DbSet<CpDwhSy0012Sy8212Sy9014Sy9118> CpDwhSy0012Sy8212Sy9014Sy9118s { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<CpDwhKa0133>(entity =>
        {
            entity.HasKey(e => new { e.Ka0133RecNr, e.Ka2422ChargenNr }).HasName("KA0133_PK");

            entity.ToTable("cp_DWH_KA0133", "dbo");

            entity.HasIndex(e => new { e.Ka0133ProdKstNr, e.Ka0133PlanDatum, e.Ka0133Prioritaet, e.Ka0133RezNr, e.Ka0133VarNr, e.Ka0133ProdAuftrNr, e.Ka0133ProdAuftrLfdNr, e.Ka2422ChargenNr, e.Ka0133RecNr }, "KA0133_SK1").IsUnique();

            entity.HasIndex(e => new { e.Ka0133PlanDatum, e.Ka0133ProdKstNr, e.Ka0133Prioritaet, e.Ka0133RezNr, e.Ka0133VarNr, e.Ka0133ProdAuftrNr, e.Ka0133ProdAuftrLfdNr, e.Ka2422ChargenNr, e.Ka0133RecNr }, "KA0133_SK2").IsUnique();

            entity.Property(e => e.Ka0133RecNr).HasColumnName("KA0133_REC_NR");
            entity.Property(e => e.Ka2422ChargenNr).HasColumnName("KA2422_CHARGEN_NR");
            entity.Property(e => e.Ka0133AbgKstNr).HasColumnName("KA0133_ABG_KST_NR");
            entity.Property(e => e.Ka0133AnlDatum).HasColumnName("KA0133_ANL_DATUM");
            entity.Property(e => e.Ka0133AnlUser).HasColumnName("KA0133_ANL_USER");
            entity.Property(e => e.Ka0133AnlZeit).HasColumnName("KA0133_ANL_ZEIT");
            entity.Property(e => e.Ka0133AnzChargen).HasColumnName("KA0133_ANZ_CHARGEN");
            entity.Property(e => e.Ka0133AnzChargenGenau).HasColumnName("KA0133_ANZ_CHARGEN_GENAU");
            entity.Property(e => e.Ka0133AnzChargenIst).HasColumnName("KA0133_ANZ_CHARGEN_IST");
            entity.Property(e => e.Ka0133CfgChPEmpfLt).HasColumnName("KA0133_CFG_CH_P_EMPF_LT");
            entity.Property(e => e.Ka0133CfgGenChPosten).HasColumnName("KA0133_CFG_GEN_CH_POSTEN");
            entity.Property(e => e.Ka0133CharGrSge)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("KA0133_CHAR_GR_SGE");
            entity.Property(e => e.Ka0133EndeDatumIst).HasColumnName("KA0133_ENDE_DATUM_IST");
            entity.Property(e => e.Ka0133EndeDatumSoll).HasColumnName("KA0133_ENDE_DATUM_SOLL");
            entity.Property(e => e.Ka0133EndeZeitIst).HasColumnName("KA0133_ENDE_ZEIT_IST");
            entity.Property(e => e.Ka0133EndeZeitSoll).HasColumnName("KA0133_ENDE_ZEIT_SOLL");
            entity.Property(e => e.Ka0133FaAdrNr).HasColumnName("KA0133_FA_ADR_NR");
            entity.Property(e => e.Ka0133FaBsNr).HasColumnName("KA0133_FA_BS_NR");
            entity.Property(e => e.Ka0133Gedruckt).HasColumnName("KA0133_GEDRUCKT");
            entity.Property(e => e.Ka0133GesMengeSge)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("KA0133_GES_MENGE_SGE");
            entity.Property(e => e.Ka0133Mhd).HasColumnName("KA0133_MHD");
            entity.Property(e => e.Ka0133NfProdAuftrLfdNr).HasColumnName("KA0133_NF_PROD_AUFTR_LFD_NR");
            entity.Property(e => e.Ka0133NfProdAuftrNr).HasColumnName("KA0133_NF_PROD_AUFTR_NR");
            entity.Property(e => e.Ka0133NfProdKstNr).HasColumnName("KA0133_NF_PROD_KST_NR");
            entity.Property(e => e.Ka0133NfRezNr).HasColumnName("KA0133_NF_REZ_NR");
            entity.Property(e => e.Ka0133NfVarNr).HasColumnName("KA0133_NF_VAR_NR");
            entity.Property(e => e.Ka0133PlanCharGr)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("KA0133_PLAN_CHAR_GR");
            entity.Property(e => e.Ka0133PlanDatum).HasColumnName("KA0133_PLAN_DATUM");
            entity.Property(e => e.Ka0133Prioritaet).HasColumnName("KA0133_PRIORITAET");
            entity.Property(e => e.Ka0133ProdAuftrLfdNr).HasColumnName("KA0133_PROD_AUFTR_LFD_NR");
            entity.Property(e => e.Ka0133ProdAuftrNr).HasColumnName("KA0133_PROD_AUFTR_NR");
            entity.Property(e => e.Ka0133ProdKstNr).HasColumnName("KA0133_PROD_KST_NR");
            entity.Property(e => e.Ka0133RezNr).HasColumnName("KA0133_REZ_NR");
            entity.Property(e => e.Ka0133RezVersNr).HasColumnName("KA0133_REZ_VERS_NR");
            entity.Property(e => e.Ka0133StartDatumIst).HasColumnName("KA0133_START_DATUM_IST");
            entity.Property(e => e.Ka0133StartDatumSoll).HasColumnName("KA0133_START_DATUM_SOLL");
            entity.Property(e => e.Ka0133StartZeitIst).HasColumnName("KA0133_START_ZEIT_IST");
            entity.Property(e => e.Ka0133StartZeitSoll).HasColumnName("KA0133_START_ZEIT_SOLL");
            entity.Property(e => e.Ka0133Status).HasColumnName("KA0133_STATUS");
            entity.Property(e => e.Ka0133UpdDatum).HasColumnName("KA0133_UPD_DATUM");
            entity.Property(e => e.Ka0133UpdUser).HasColumnName("KA0133_UPD_USER");
            entity.Property(e => e.Ka0133UpdZeit).HasColumnName("KA0133_UPD_ZEIT");
            entity.Property(e => e.Ka0133VarErm).HasColumnName("KA0133_VAR_ERM");
            entity.Property(e => e.Ka0133VarNr).HasColumnName("KA0133_VAR_NR");
        });

        modelBuilder.Entity<CpDwhSy0012Sy8212Sy9014Sy9118>(entity =>
        {
            entity.HasKey(e => e.Sy0012Nr).HasName("cp_DWH_SY0012_SY8212_SY9014_SY9118_PK");

            entity.ToTable("cp_DWH_SY0012_SY8212_SY9014_SY9118", "dbo");

            entity.HasIndex(e => new { e.Sy0012Suchbegriff, e.Sy0012Nr }, "cp_DWH_SY0012_SY8212_SY9014_SY9118_SK1").IsUnique();

            entity.HasIndex(e => new { e.Sy0012Warengruppe, e.Sy0012Nr }, "cp_DWH_SY0012_SY8212_SY9014_SY9118_SK2").IsUnique();

            entity.HasIndex(e => new { e.Sy0012ArtGruppe, e.Sy0012Nr }, "cp_DWH_SY0012_SY8212_SY9014_SY9118_SK3").IsUnique();

            entity.Property(e => e.Sy0012Nr)
                .ValueGeneratedNever()
                .HasColumnName("SY0012_NR");
            entity.Property(e => e.Sy0012AbcKlasse).HasColumnName("SY0012_ABC_KLASSE");
            entity.Property(e => e.Sy0012AnlDatum).HasColumnName("SY0012_ANL_DATUM");
            entity.Property(e => e.Sy0012AnlZeit).HasColumnName("SY0012_ANL_ZEIT");
            entity.Property(e => e.Sy0012ArtGruppe).HasColumnName("SY0012_ART_GRUPPE");
            entity.Property(e => e.Sy0012Bez)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("SY0012_BEZ");
            entity.Property(e => e.Sy0012Block).HasColumnName("SY0012_BLOCK");
            entity.Property(e => e.Sy0012Breite)
                .HasColumnType("decimal(12, 6)")
                .HasColumnName("SY0012_BREITE");
            entity.Property(e => e.Sy0012EkBe)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SY0012_EK_BE");
            entity.Property(e => e.Sy0012EkKgme)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("SY0012_EK_KGME");
            entity.Property(e => e.Sy0012EkMe)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SY0012_EK_ME");
            entity.Property(e => e.Sy0012EkMebe)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("SY0012_EK_MEBE");
            entity.Property(e => e.Sy0012HbkZeit).HasColumnName("SY0012_HBK_ZEIT");
            entity.Property(e => e.Sy0012HbkZeit1Std).HasColumnName("SY0012_HBK_ZEIT_1_STD");
            entity.Property(e => e.Sy0012Hoehe)
                .HasColumnType("decimal(12, 6)")
                .HasColumnName("SY0012_HOEHE");
            entity.Property(e => e.Sy0012HostArtNr)
                .HasMaxLength(13)
                .IsFixedLength()
                .UseCollation("Latin1_General_BIN")
                .HasColumnName("SY0012_HOST_ART_NR");
            entity.Property(e => e.Sy0012LaKgme)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("SY0012_LA_KGME");
            entity.Property(e => e.Sy0012LaMe)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SY0012_LA_ME");
            entity.Property(e => e.Sy0012Laenge)
                .HasColumnType("decimal(12, 6)")
                .HasColumnName("SY0012_LAENGE");
            entity.Property(e => e.Sy0012LagTemp)
                .HasColumnType("decimal(7, 3)")
                .HasColumnName("SY0012_LAG_TEMP");
            entity.Property(e => e.Sy0012LagerNr).HasColumnName("SY0012_LAGER_NR");
            entity.Property(e => e.Sy0012Mwst).HasColumnName("SY0012_MWST");
            entity.Property(e => e.Sy0012RvRelevant).HasColumnName("SY0012_RV_RELEVANT");
            entity.Property(e => e.Sy0012Suchbegriff)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("SY0012_SUCHBEGRIFF");
            entity.Property(e => e.Sy0012Toleranz).HasColumnName("SY0012_TOLERANZ");
            entity.Property(e => e.Sy0012UpdDatum).HasColumnName("SY0012_UPD_DATUM");
            entity.Property(e => e.Sy0012UpdZeit).HasColumnName("SY0012_UPD_ZEIT");
            entity.Property(e => e.Sy0012VerpGew)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("SY0012_VERP_GEW");
            entity.Property(e => e.Sy0012VerpGewPaz)
                .HasColumnType("decimal(13, 3)")
                .HasColumnName("SY0012_VERP_GEW_PAZ");
            entity.Property(e => e.Sy0012Vst).HasColumnName("SY0012_VST");
            entity.Property(e => e.Sy0012Warengruppe).HasColumnName("SY0012_WARENGRUPPE");
            entity.Property(e => e.Sy9014Bez)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("SY9014_BEZ");
            entity.Property(e => e.Sy9118Bez)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("SY9118_BEZ");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine,LogLevel.Information);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
