using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Library
{
    public static class Extensions
    {
        //session obje olarak tutmuyor, string tutuyor ama var olarak tanımladığımız admin bir obje: o yüzden bu iki metoda ihtiyaç duyduk
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string strValue = JsonConvert.SerializeObject(value);
            session.SetString(key, strValue);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            string strValue = session.GetString(key);
            T obj = JsonConvert.DeserializeObject<T>(strValue);
            return obj;
        }
    }
}
