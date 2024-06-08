using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;
using static API.Model.Core.Enums;

namespace API.Model.Core
{
    public class Todo
    {
        public int TodoID { get; set; }
        public string Caption { get; set; }
        public TodoStatus Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? FinishDate { get; set; }
        public string FinishBy { get; set; }
        public TodoCategory Category { get; set; }

        public class Config : IEntityTypeConfiguration<Todo>
        {
            public void Configure(EntityTypeBuilder<Todo> builder)
            {
                builder.ToTable("Todo");
                builder.HasKey(t => t.TodoID);
                builder.Property(t => t.TodoID)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1,1)
                    .IsRequired();
                builder.Property(t => t.Caption)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode();
                builder.Property(t => t.Active)
                    .HasDefaultValue(true)
                    .IsRequired();
                builder.Property(t => t.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasConversion(
                        v => v.ToString(),
                        v => (TodoStatus)Enum.Parse(typeof(TodoStatus), v));
                builder.Property(t => t.CreatedDate)
                    .HasColumnType("DateTime")
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
                builder.Property(t => t.FinishDate)
                    .HasColumnType("DateTime")
                    .IsRequired(false);
                builder.Property(t => t.FinishBy)
                    .HasMaxLength(20)
                    .IsRequired(false)
                    .IsUnicode();
            }
        }
    }
}
