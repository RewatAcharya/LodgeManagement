using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

public partial class LodgeSystemManagementContext : DbContext
{
    public LodgeSystemManagementContext()
    {
    }

    public LodgeSystemManagementContext(DbContextOptions<LodgeSystemManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodDetail> FoodDetails { get; set; }

    public virtual DbSet<FoodInvoice> FoodInvoices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomCart> RoomCarts { get; set; }

    public virtual DbSet<RoomCategory> RoomCategories { get; set; }

    public virtual DbSet<RoomInvoice> RoomInvoices { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=con");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD82B132EB");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ActualCheckInDate)
                .HasColumnType("datetime")
                .HasColumnName("actual_check_in_date");
            entity.Property(e => e.ActualCheckOutDate)
                .HasColumnType("datetime")
                .HasColumnName("actual_check_out_date");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CheckInBy).HasColumnName("check_in_by");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("datetime")
                .HasColumnName("check_in_date");
            entity.Property(e => e.CheckOutBy).HasColumnName("check_out_by");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("datetime")
                .HasColumnName("check_out_date");
            entity.Property(e => e.DepositAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Deposit_amount");
            entity.Property(e => e.NoOfPerson).HasColumnName("no_Of_Person");
            entity.Property(e => e.TotalRooms).HasColumnName("total_Rooms");

            entity.HasOne(d => d.CheckInByNavigation).WithMany(p => p.BookingCheckInByNavigations)
                .HasForeignKey(d => d.CheckInBy)
                .HasConstraintName("FK__Booking__check_i__4F7CD00D");

            entity.HasOne(d => d.CheckOutByNavigation).WithMany(p => p.BookingCheckOutByNavigations)
                .HasForeignKey(d => d.CheckOutBy)
                .HasConstraintName("FK__Booking__check_o__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.BookingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__UserId__4E88ABD4");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking___3214EC276FA1229B");

            entity.ToTable("Booking_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Booki__5DCAEF64");

            entity.HasOne(d => d.Room).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__RoomI__5CD6CB2B");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7976270ADE8");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.RoomId, "UQ__Cart__32863938295D8A90").IsUnique();

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("datetime")
                .HasColumnName("check_in_date");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("datetime")
                .HasColumnName("check_out_date");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__RoomId__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__UserId__71D1E811");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC271E3F6611");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__Foods__856DB3CB628CED51");

            entity.Property(e => e.FoodId).HasColumnName("FoodID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("image_path");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<FoodDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodDeta__3214EC079759B3F5");

            entity.ToTable("FoodDetail");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.FoodDetails)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__FoodDetai__Booki__628FA481");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodDetails)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK__FoodDetai__FoodI__6383C8BA");
        });

        modelBuilder.Entity<FoodInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FoodInvoice");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(22, 2)")
                .HasColumnName("Total_Price");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB57979C4B0");

            entity.ToTable("Invoice");

            entity.Property(e => e.BillingBy).HasColumnName("Billing_By");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.BillingByNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.BillingBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Billing__66603565");

            entity.HasOne(d => d.Booking).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Booking__6754599E");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__328639193C9BB959");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("image_path");
            entity.Property(e => e.NoOfBed).HasColumnName("no_of_bed");
            entity.Property(e => e.PricePerDay)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_day");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__CategoryI__5535A963");
        });

        modelBuilder.Entity<RoomCart>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RoomCart");

            entity.Property(e => e.CheckInDate)
                .HasColumnType("datetime")
                .HasColumnName("check_in_date");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("datetime")
                .HasColumnName("check_out_date");
            entity.Property(e => e.PricePerDay)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_day");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<RoomCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__RoomCate__19093A2BB0BB1C8C");

            entity.ToTable("RoomCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("rate");
        });

        modelBuilder.Entity<RoomInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RoomInvoice");

            entity.Property(e => e.DepositAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Deposit_amount");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NoOfDays).HasColumnName("no_of_days");
            entity.Property(e => e.PricePerDay)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price_per_day");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("Total_Price");
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CCAC5C4F6AF9");

            entity.ToTable("UserList");

            entity.HasIndex(e => e.PhoneNo, "UQ__UserList__F3EE33E2CF2DA3B8").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Customer')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
