using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebApplication1.DataModel
{
    public class CM_ORG
    {
        public virtual int Id { get; set; }
        public virtual  string SName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Ident { get; set; }
        //[MaxLength(50)]
        //public virtual string unicode { get; set; }
        //[MaxLength(50)]
        //public virtual string bin { get; set; }
        //[MaxLength(50)]
        //public virtual string unicode1 { get; set; }
    }

    public class CM_ORGMap : ClassMapping<CM_ORG>
    {
        public CM_ORGMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.SName, map =>
            {
                map.Column("s_name");
                map.Column(x => x.Length(50));
            });
            Property(x => x.Name, map =>
            {
                map.Column("name");
                map.Column(x => x.Length(500));
            });
            Property(x => x.Currency, map =>
            {
                map.Column("currency");
                map.Column(x => x.Length(3));
            });
            Property(x => x.Ident, map =>
            {
                map.Column("ident");
                map.Column(x => x.Length(4));
                map.Index("CM_ORGIDENT");
            });
        }
    }
}