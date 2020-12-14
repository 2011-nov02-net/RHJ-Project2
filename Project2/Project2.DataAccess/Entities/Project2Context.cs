using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class Project2Context : DbContext
    {
        public Project2Context()
        {
        }

        public Project2Context(DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionDetail> AuctionDetails { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Pack> Packs { get; set; }
        public virtual DbSet<StoreInventory> StoreInventories { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<TradeDetail> TradeDetails { get; set; }
        public virtual DbSet<UserCardInventory> UserCardInventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auction", "TEAM");

                entity.Property(e => e.AuctionId).HasMaxLength(40);

                entity.Property(e => e.BuyerId).HasMaxLength(40);

                entity.Property(e => e.CardId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.SellDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SellerId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.AuctionBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__Auction__BuyerId__0D44F85C");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__Auction__CardId__0E391C95");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.AuctionSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auction__SellerI__0C50D423");
            });

            modelBuilder.Entity<AuctionDetail>(entity =>
            {
                entity.HasKey(e => e.AuctionId)
                    .HasName("PK__AuctionD__51004A4CA5356A48");

                entity.ToTable("AuctionDetail", "TEAM");

                entity.Property(e => e.AuctionId).HasMaxLength(40);

                entity.Property(e => e.ExpDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SellType).HasMaxLength(40);

                entity.HasOne(d => d.Auction)
                    .WithOne(p => p.AuctionDetail)
                    .HasForeignKey<AuctionDetail>(d => d.AuctionId)
                    .HasConstraintName("FK__AuctionDe__Aucti__14E61A24");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card", "TEAM");

                entity.Property(e => e.CardId).HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Customer__1788CC4CCA09C793");

                entity.ToTable("Customer", "TEAM");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534AD9F3B0C")
                    .IsUnique();

                entity.Property(e => e.UserId).HasMaxLength(40);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.First)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Last)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "TEAM");

                entity.Property(e => e.OrderId).HasMaxLength(40);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__UserId__19AACF41");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderItem", "TEAM");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.PackId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__Order__1C873BEC");

                entity.HasOne(d => d.Pack)
                    .WithMany()
                    .HasForeignKey(d => d.PackId)
                    .HasConstraintName("FK__OrderItem__PackI__1D7B6025");
            });

            modelBuilder.Entity<Pack>(entity =>
            {
                entity.ToTable("Pack", "TEAM");

                entity.Property(e => e.PackId).HasMaxLength(40);

                entity.Property(e => e.DateReleased)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<StoreInventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StoreInventory", "TEAM");

                entity.Property(e => e.PackId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Pack)
                    .WithMany()
                    .HasForeignKey(d => d.PackId)
                    .HasConstraintName("FK__StoreInve__PackI__078C1F06");
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade", "TEAM");

                entity.Property(e => e.TradeId).HasMaxLength(40);

                entity.Property(e => e.BuyerId).HasMaxLength(40);

                entity.Property(e => e.OffererId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.TradeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TradeBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__Trade__BuyerId__2334397B");

                entity.HasOne(d => d.Offerer)
                    .WithMany(p => p.TradeOfferers)
                    .HasForeignKey(d => d.OffererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trade__OffererId__22401542");
            });

            modelBuilder.Entity<TradeDetail>(entity =>
            {
                entity.HasKey(e => e.TradeId)
                    .HasName("PK__TradeDet__3028BB5BAC3E56F2");

                entity.ToTable("TradeDetail", "TEAM");

                entity.Property(e => e.TradeId).HasMaxLength(40);

                entity.Property(e => e.BuyerCardId).HasMaxLength(40);

                entity.Property(e => e.OfferCardId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.BuyerCard)
                    .WithMany(p => p.TradeDetailBuyerCards)
                    .HasForeignKey(d => d.BuyerCardId)
                    .HasConstraintName("FK__TradeDeta__Buyer__27F8EE98");

                entity.HasOne(d => d.OfferCard)
                    .WithMany(p => p.TradeDetailOfferCards)
                    .HasForeignKey(d => d.OfferCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeDeta__Offer__2704CA5F");

                entity.HasOne(d => d.Trade)
                    .WithOne(p => p.TradeDetail)
                    .HasForeignKey<TradeDetail>(d => d.TradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeDeta__Trade__2610A626");
            });

            modelBuilder.Entity<UserCardInventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserCardInventory", "TEAM");

                entity.Property(e => e.CardId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Card)
                    .WithMany()
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__UserCardI__CardI__04AFB25B");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserCardI__UserI__03BB8E22");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
