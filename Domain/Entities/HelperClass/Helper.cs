using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace HelperClass
{
   public class Helper
    {
        
        //--------------------Helper Function --------------------
        private bool checkInjection(string text)
        {
            if (text.ToUpper().IndexOf(" OR ") > 0)
                return false;
            else
                return true;
        }

        private string removeLastSpace(string text)
        {
            if (text.Substring(text.Length, 1) == " ")
                return text.Substring(0, text.Length - 1);
            else
                return text;
        }

       


        public T mapObject<T>(DataTable dt, T content)
        {
            foreach (DataRow dr in dt.Rows)
            {
                Type type = content.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    property.SetValue(content, dr[property.Name]);
                }
            }
            return content;
        }

        public List<T> mapList<T>(DataTable dt, T content)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                Type type = content.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    property.SetValue(content, dr[property.Name]);
                }
                list.Add(content);
            }
            return list;
        }



        public dynamic mapObject(DataTable dt)
        {
            var content = new ExpandoObject() as IDictionary<string, Object>;
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn c in dr.Table.Columns)
                {
                    content.Add(c.ToString(), dr[c]);
                }
            }
            return content;
        }

    }
}
