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

                entity.Property(e => e.BuyerId)
                    .IsRequired()
                    .HasMaxLength(40);

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auction__BuyerId__59C55456");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__Auction__CardId__5AB9788F");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.AuctionSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auction__SellerI__58D1301D");
            });

            modelBuilder.Entity<AuctionDetail>(entity =>
            {
                entity.HasKey(e => e.AuctionId)
                    .HasName("PK__AuctionD__51004A4C522BFA12");

                entity.ToTable("AuctionDetail", "TEAM");

                entity.Property(e => e.AuctionId).HasMaxLength(40);

                entity.Property(e => e.ExpDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SellType)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Auction)
                    .WithOne(p => p.AuctionDetail)
                    .HasForeignKey<AuctionDetail>(d => d.AuctionId)
                    .HasConstraintName("FK__AuctionDe__Aucti__6166761E");
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
                    .HasName("PK__Customer__1788CC4CAEC90832");

                entity.ToTable("Customer", "TEAM");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053427114548")
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
                    .HasConstraintName("FK__Order__UserId__662B2B3B");
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
                    .HasConstraintName("FK__OrderItem__Order__690797E6");

                entity.HasOne(d => d.Pack)
                    .WithMany()
                    .HasForeignKey(d => d.PackId)
                    .HasConstraintName("FK__OrderItem__PackI__69FBBC1F");
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
                    .HasConstraintName("FK__StoreInve__PackI__540C7B00");
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade", "TEAM");

                entity.Property(e => e.TradeId).HasMaxLength(40);

                entity.Property(e => e.BuyerId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OffererId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.TradeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TradeBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trade__BuyerId__6EC0713C");

                entity.HasOne(d => d.Offerer)
                    .WithMany(p => p.TradeOfferers)
                    .HasForeignKey(d => d.OffererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trade__OffererId__6DCC4D03");
            });

            modelBuilder.Entity<TradeDetail>(entity =>
            {
                entity.HasKey(e => e.TradeId)
                    .HasName("PK__TradeDet__3028BB5B55FF74C7");

                entity.ToTable("TradeDetail", "TEAM");

                entity.Property(e => e.TradeId).HasMaxLength(40);

                entity.Property(e => e.BuyerCardId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OfferCardId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.BuyerCard)
                    .WithMany(p => p.TradeDetailBuyerCards)
                    .HasForeignKey(d => d.BuyerCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeDeta__Buyer__73852659");

                entity.HasOne(d => d.OfferCard)
                    .WithMany(p => p.TradeDetailOfferCards)
                    .HasForeignKey(d => d.OfferCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeDeta__Offer__72910220");

                entity.HasOne(d => d.Trade)
                    .WithOne(p => p.TradeDetail)
                    .HasForeignKey<TradeDetail>(d => d.TradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradeDeta__Trade__719CDDE7");
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
                    .HasConstraintName("FK__UserCardI__CardI__51300E55");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserCardI__UserI__503BEA1C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
