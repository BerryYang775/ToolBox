using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace API.Model.Core
{
    public class TodoCategory
    {
        public int TodoCategoryID { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        [JsonIgnore]
        public ICollection<Todo> Todos { get; set; }
        public class Config : IEntityTypeConfiguration<TodoCategory>
        {
            public void Configure(EntityTypeBuilder<TodoCategory> builder)
            {
                builder.ToTable("TodoCategory");
                builder.HasKey(t => t.TodoCategoryID);
                builder.Property(t => t.TodoCategoryID)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .IsRequired();
                builder.Property(t => t.Title)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsUnicode();
                builder.Property(t => t.Active)
                    .HasDefaultValue(true)
                    .IsRequired();
                builder.Property(t => t.CreatedDate)
                    .HasColumnType("DateTime")
                    .HasDefaultValueSql("getdate()")
                    .IsRequired();
                builder.Property(t => t.CreatedBy)
                    .HasMaxLength(20)
                    .IsRequired()
                    .IsUnicode();
                builder.Property(t => t.UpdatedDate)
                    .HasColumnType("DateTime")
                    .IsRequired(false);
                builder.Property(t => t.UpdatedBy)
                    .HasMaxLength(20)
                    .IsRequired(false)
                    .IsUnicode();
                builder.HasData([
                    new TodoCategory{
                        TodoCategoryID = 1,
                        Title = "Target",
                        Active = true,
                        CreatedBy = "System"
                    },
                    new TodoCategory{
                        TodoCategoryID = 2,
                        Title = "Daily",
                        Active = true,
                        CreatedBy = "System",
                    },
                    new TodoCategory{
                        TodoCategoryID = 3,
                        Title = "Study",
                        Active = true,
                        CreatedBy = "System"
                    }
                ]);
            }
        }
    }
}
