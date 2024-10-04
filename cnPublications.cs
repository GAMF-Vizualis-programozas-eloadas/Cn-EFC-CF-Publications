using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CnEFCF_Publications
{
	public partial class cnPublications : DbContext
	{
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Article> Articles { get; set; }
    public cnPublications() : base() { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(GetConnectionString("csPublications"));
      base.OnConfiguring(optionsBuilder);
    }
    private static string GetConnectionString(string connectionStringName)
    {
      var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
      var configuration = configurationBuilder.Build();
      return configuration.GetConnectionString(connectionStringName);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
			base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Article>().ToTable(@"ARTICLES");
      modelBuilder.Entity<Article>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
      modelBuilder.Entity<Article>().Property(x => x.Title).HasColumnName(@"Title").IsRequired().ValueGeneratedNever();
      modelBuilder.Entity<Article>().HasKey(x => x.Id);
      modelBuilder.Entity<Author>().ToTable(@"AUTHORS");
      modelBuilder.Entity<Author>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ValueGeneratedOnAdd();
      modelBuilder.Entity<Author>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
      modelBuilder.Entity<Author>().HasKey(x => x.Id);
      modelBuilder.Entity<Author>().HasMany(x => x.Articles).WithMany(op => op.Authors);
    }
    public bool HasChanges()
    {
      return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
    }
  }

}
