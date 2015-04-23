using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apostiller_Project
{
    public partial class Preference : Form
    {
        public Preference()
        {
            InitializeComponent();
        }

        private void Preference_Load(object sender, EventArgs e)
        {
            using (PreferenceDbDataContext ctx = new PreferenceDbDataContext())
            {

                var prf = (from p in ctx.preferences
                           select p).Single();


                preferanceCapacity.Text = prf.pref_capacity;
                preferanceDoc.Text = prf.pref_doc;
                preferanceOffice.Text = prf.pref_office;
            }

        }

        private void cancelPreference_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okPreference_Click(object sender, EventArgs e)
        {

            using (PreferenceDbDataContext pfctx = new PreferenceDbDataContext())
            {

                try
                {
                    var pref = (from p in pfctx.preferences

                                select p).Single(a=> a.id == 1);
                    
                    pref.pref_capacity = preferanceCapacity.Text;
                    pref.pref_doc = preferanceDoc.Text;
                    pref.pref_office = preferanceOffice.Text;
                 
                    pfctx.SubmitChanges();
                    Apostiller ap = (Apostiller)Application.OpenForms["Apostiller"];
                    ap.capacity.Text = preferanceCapacity.Text;
                    ap.document.Text = preferanceDoc.Text;
                    ap.office.Text = preferanceOffice.Text;

                    Close();
                }
                catch (Exception exe) { Console.WriteLine(exe.Message); }
            }

        }
    }
}
