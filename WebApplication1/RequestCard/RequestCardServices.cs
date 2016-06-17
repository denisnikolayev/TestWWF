using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.RequestCard
{
    public static class RequestCardServices
    {
        public static PersonInfo Search(SearchModel search)
        {
            PersonInfo client;

            if (Clients.TryGetValue(search.IIN, out client))
            {
                return client;
            }
            return null;
        }


        static Dictionary<string, PersonInfo> Clients = new Dictionary<string, PersonInfo>()
        {
            ["111111111111"] = new PersonInfo()
            {
                IIN = "111111111111",
                DocumentNumber = "15",
                BirthDay = DateTime.Parse("1.1.2016"),
                Fio = "Иванов Иван Иванович"
            }
        };

    }
}