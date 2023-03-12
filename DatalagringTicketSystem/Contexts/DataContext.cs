using DatalagringTicketSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace DatalagringTicketSystem.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Linus Lund\\Documents\\Cskarpt\\DatalagringTicketSystem\\DatalagringTicketSystem\\Contexts\\SupportTicketDB.mdf\";Integrated Security=True;Connect Timeout=30";

        #region constructors

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region overrides

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<TicketEntity> Tickets { get; set; } = null!;
        public DbSet<TicketStatusEntity> TicketCategories { get; set; } = null!;
    }
}
