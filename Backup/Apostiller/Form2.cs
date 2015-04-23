using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Drawing.Printing;

namespace Apostiller_Project
{
    public partial class Form2 : Form
    {
        public Apostiller ap;
        public int _Id;
        public DateTime _Date;
        public string _Name;
        public string _Title;
        public string _Capacity;
        public string _Document;

        public string _About;

        public string _Sig;

        public string _Country;

        public string _Office;

        public Form2()
        {

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }


        private void dataGridView1_CellContentClick(Object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                DataGridViewRow row = sender as DataGridViewRow;

                _Id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());

                _Name = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                _Date = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString());
                _Title = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                _Capacity = dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();

                _Document = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                _About = dataGridView1.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                _Sig = dataGridView1.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
                _Country = dataGridView1.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
                _Office = dataGridView1.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
                //Console.WriteLine("ID = {0} Name = {1} Date = {2} Title = {3} Capacity = {4} Documet = {5} About = {6} Sig = {7} Country = {8} Office = {9} ", id, name, date.ToString("dd.MM.yyyy"), title, capacity, document, about, sig, country, office);

                if (dataGridView1.CurrentCell.Value.ToString() == "Oppdater")
                {
                    using (PersonDataContext pdc = new PersonDataContext())
                    {
                        try
                        {   

                            var apos = pdc.apostilles.Single(p => p.ID == _Id);

                            apos.name = _Name;
                            apos.date = _Date;
                            apos.title = _Title;
                            apos.capacity = _Capacity;
                            apos.doc = _Document;
                            apos.about = _About;
                            apos.sig = _Sig.ToString();
                            apos.country = _Country;
                            apos.office = _Office;
                            pdc.SubmitChanges();

                        }
                        catch (Exception exe)
                        {
                            Console.WriteLine(exe.Message);
                        }
                    }
                }
                else if (dataGridView1.CurrentCell.Value.ToString() == "Skriv ut")
                {
                    using (PersonDataContext pdc = new PersonDataContext())
                    {
                        try
                        {
                        
                            var apos = pdc.apostilles.Single(p => p.ID == _Id);
                            ap = new Apostiller();
                            ap.apostilleTitle = "APOSTILLE";
                            ap.Print(ap.printDocument1.PrinterSettings.PrinterName, apos.name, Convert.ToDateTime(apos.date).ToString("dd.MM.yyyy"), apos.title, apos.capacity, apos.doc, apos.about, apos.sig, apos.country, apos.office, apos.ID.ToString());
                        }

                        catch (Exception exe)
                        {
                            Console.WriteLine(exe.Message);
                        }
                    }

                }
                else if (dataGridView1.CurrentCell.Value.ToString() == "Slett")
                {
                    using (PersonDataContext pdc = new PersonDataContext())
                    {
                        try
                        {

                            DialogResult result = MessageBox.Show("Apostillen slettes, er du sikker?", "Slett", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                var apos = pdc.apostilles.Single(p => p.ID == _Id);
                                pdc.apostilles.DeleteOnSubmit(apos);                            
                                pdc.SubmitChanges();
                                
                            }
                        }

                        catch (Exception exe)
                        {
                            Console.WriteLine(exe.Message);
                        }
                    }
                    
                   
                }

            }
            catch (Exception exe) { Console.WriteLine(exe.Message); }

        }
    }
}