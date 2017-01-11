using System;
using System.Collections.Generic;
namespace MyTEBot.Service.Entities
{
    /// <summary>
    /// Entity to store MyTEBot
    /// </summary>    
    public class MyTEBotResult
    {
        /// <summary>
        /// 
        /// </summary>
        public MyTEBotResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Entity Framework proxies requrie it.")]
        public MyTEBotResult(Guid id)
        {
            Id = id;
        }


        /// <summary>
        /// Gets or sets Id column.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        /// <value>
        /// The created user.
        /// </value>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>
        /// The updated date time.
        /// </value>
        public DateTime UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the updated user.
        /// </summary>
        /// <value>
        /// The updated user.
        /// </value>
        public string UpdatedUser { get; set; }

    }
}
