using Microsoft.EntityFrameworkCore;
using pawrser.classes.entities;

namespace pawrser.classes {

	public class SqliteContext : DbContext {

		public DbSet<Title> Titles {
			get; set;
		}

		public DbSet<Episode> Episodes {
			get; set;
		}

		public DbSet<Tag> Tags {
			get; set;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<TagRef>()
				.HasKey(t => new { t.TitleId, t.TagId });

			modelBuilder.Entity<TagRef>()
				.HasOne(tr => tr.Title)
				.WithMany(t => t.TagRefs)
				.HasForeignKey(tr => tr.TitleId);

			modelBuilder.Entity<TagRef>()
				.HasOne(tr => tr.Tag)
				.WithMany(t => t.TagRefs)
				.HasForeignKey(tr => tr.TagId);
		}
	}

}
