using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    public class UserTaskEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Caption { get; set; }
        public string QueueName { get; set; }
        public string ViewName { get; set; }
        public string ViewInputModel { get; set; }

        public Guid WWFId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        
        public HashSet<ButtonEntry> Buttons { get; set; } = new HashSet<ButtonEntry>();
    }


    public class ButtonEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }
        public string Style { get; set; }
        public string TypeButton { get; set; }

        [Required, InverseProperty("Buttons"), JsonIgnore]
        public UserTaskEntry Parent { get; set; }
    }


    public class Db : DbContext
    {
        public DbSet<UserTaskEntry> UserTasks { get; set; }
        public DbSet<ButtonEntry> Buttons { get; set; }
    }
}