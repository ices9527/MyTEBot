using System.Data.Entity.ModelConfiguration;

namespace MyTEBot.Service.Entities
{
    /// <summary>
    /// The DB mapping configuration for the TimeReport entity.
    /// </summary>
    public class MyTEBotMapping : EntityTypeConfiguration<MyTEBotResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyTEBotMapping"/> class.
        /// </summary>
        public MyTEBotMapping()
        {
            /*
             * This is how to map the POCO MyTEBot class to its persisted DB
             * form using Entity Framework.
             * 
             * Note that while EF also supports annotations directly on the POCO TimeReport class,
             * it's better to keep your POCO entity class clean and consolidate all the EF-related
             * configuration here in a single location.
             */

            //this.HasKey(t => t.Id);
            this.ToTable("MyTEBot");

            //this.Property(t => t.Id).IsRequired();

            //this.Property(t => t.Version).IsConcurrencyToken();
            //this.Property(t => t.CreatedDateTime).HasColumnType("datetime2");
            //this.Property(t => t.UpdatedDateTime).HasColumnType("datetime2");
        }
    }
}
