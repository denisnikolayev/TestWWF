using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace WebApplication1.DataModel
{
    public class UserTask
    {
        public virtual Guid Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual string QueueName { get; set; }
        public virtual string ViewName { get; set; }
        public virtual string ViewInputModel { get; set; }

        public virtual Guid WWFId { get; set; }

        public virtual int NumberDoc { get; set; }

        //public HashSet<ButtonEntry> Buttons { get; set; } = new HashSet<ButtonEntry>();
    }

    public class UserTaskMap : ClassMapping<UserTask>
    {
        public UserTaskMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Guid));
            Property(x => x.Caption, m => m.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1));
            Property(x => x.QueueName);
            Property(x => x.ViewName);
            Property(x => x.ViewInputModel);
            Property(x => x.WWFId);
            Property(x => x.NumberDoc);
        }
    }
}
