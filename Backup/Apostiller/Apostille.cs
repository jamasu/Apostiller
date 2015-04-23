using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Apostiller_Project
{
    class Apostille
    {
                
        public string AddToDatabase(string p_name, string p_date, string p_title, string p_capacity,
                    string p_office, string p_document, string p_about, string p_sig, string p_country)
        {
            using (PersonDataContext ctx = new PersonDataContext())
            {
              
                apostille ap = new apostille
                {
                   
                    name = p_name,
                    date = DateTime.Parse(p_date),
                    title = p_title,
                    capacity = p_capacity,
                    office = p_office,
                    doc = p_document,
                    about = p_about,
                    sig = p_sig,
                    country = p_country
                };
                ctx.apostilles.InsertOnSubmit(ap);
                ctx.SubmitChanges();
                return  ap.ID.ToString();
            }
        }

    }
}

