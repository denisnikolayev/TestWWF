using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            if (search.IIN == "111111111112")
            {
                throw new ValidationException("Сбой сервиса");
            }

            return null;
        }

        public static bool CheckIIN(string iin)
        {
            return iin == null || iin.Length == 12;
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