using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;
using System.Data.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.DirectoryServices;
namespace Apostiller_Project
{
    public partial class Apostiller : Form
    {

        public string apostilleTitle;
        internal string printerName;
        private string theIdNumber;
        public string fullName;
        private string lastIdNumber;
        public string preferenceCity;
        public string preferenceCounty;

        public Apostiller(string apostille_text)
        {
            if (apostille_text == null)
            {
                throw new ArgumentNullException("APOSTILLE");
            }
            this.apostilleTitle = apostille_text;
        }

        public Apostiller()
        {
            InitializeComponent();
        }

        private void GetTotalYear()
        {
            using (PersonDataContext ctx = new PersonDataContext())
            {
                var idNumbers = ctx.apostilles.Count(p => p.date.Value.Year >= DateTime.Now.Year);

                idLabel.Text = "Antall i år: " + idNumbers.ToString();
                theIdNumber = (idNumbers + 1).ToString();

            }

        }
        string ExtractUserName(string path)
        {
            string[] userPath = path.Split(new char[] { '\\' });
            return userPath[userPath.Length - 1];
        }
        private void findUser()
        {
            /*
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            string accountname = ExtractUserName(wp.Identity.Name);
            string filter = string.Format("(SAMAccountName={0})",accountname);

            string[] properties = new string[] { "fullname","samaccountname","givenname","sn" };
            //LDAP://OU=Bruker,OU=Fylkesmannen,OU=Oslo,DC=fmoa,DC=no
            //LDAP://OU=Fylkesmannen,DC=fmve,DC=no
            DirectoryEntry adRoot = new DirectoryEntry("LDAP://OU=Brukere,OU=Fylkesmannen,OU=Oslo,DC=fmoa,DC=no");
            DirectorySearcher searcher = new DirectorySearcher(adRoot);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.ReferralChasing = ReferralChasingOption.All;
            searcher.PropertiesToLoad.AddRange(properties);
            searcher.Filter = filter;

            SearchResult result = searcher.FindOne();
            DirectoryEntry directoryEntry = result.GetDirectoryEntry();
            string firstName = directoryEntry.Properties["givenName"][0].ToString();
            string lastName = directoryEntry.Properties["sn"][0].ToString();
            fullName = firstName + " " + lastName;
             */
            fullName = "";
        }
        private string GetLastIdNumber()
        {
            using (PersonDataContext ctx = new PersonDataContext())
            {
                int idNumber = (from ap in ctx.apostilles select ap.ID).Max();
                return lastIdNumber = (idNumber + 1).ToString();
            }
        }
        public void SetPreference()
        {
            using (PreferenceDbDataContext prfctx = new PreferenceDbDataContext())
            {
                var pref = (from p in prfctx.preferences
                            select p).Single();

                document.Text = pref.pref_doc;
                capacity.Text = pref.pref_capacity;
                office.Text = pref.pref_office;

                preferenceCounty = pref.pref_county;
                preferenceCity = pref.pref_city;
            }
        }
        private void Apostiller_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'apostillerDataSet.apostille' table. You can move, or remove it, as needed.
            //this.apostilleTableAdapter.Fill(this.apostillerDataSet.apostille);
            monthCalendar1.Enabled = false;

            SetPreference();

            name.Select();

            // TODO: This line of code loads data into the 'countryes1.countryes' table. You can move, or remove it, as needed.
            this.countryesTableAdapter.Fill(this.countryes1.countryes);

            GetTotalYear();
            findUser();
            country.SelectedIndex = country.FindStringExact("Norge");
            countrySearch.SelectedIndex = countrySearch.FindStringExact("Norge");
        }

        private void printOutButton_Click(object sender, EventArgs e)
        {

            name.Select();

            bool isError = false;

            errorProvider1.Dispose();

            if (name.Text == "")
            {
                errorProvider1.SetError(name, "Feil: Navn feltet kan ikke være tomt!");
                isError = true;
            }
            if (title.Text == "")
            {
                errorProvider1.SetError(title, "Feil: Tittel feltet kan ikke være tomt!");
                isError = true;
            }
            if (capacity.Text == "")
            {
                errorProvider1.SetError(capacity, "Feil: Capacity feltet kan ikke være tomt!");
                isError = true;
            }
            if (office.Text == "")
            {
                errorProvider1.SetError(office, "Feil: Kontor feltet kan ikke være tomt!");
                isError = true;

            }
            if (document.Text == "")
            {
                errorProvider1.SetError(document, "Feil: Dokument feltet kan ikke være tomt!");
                isError = true;
            }
            if (about.Text == "")
            {
                errorProvider1.SetError(about, "Feil: Vedrørende feltet kan ikke være tomt!");
                isError = true;
            }
            if (sig.Text == "")
            {
                errorProvider1.SetError(sig, "Feil: Sig feltet kan ikke være tomt!");
                isError = true;
            }

            if (isError == false)
            {

                printerName = printDocument1.PrinterSettings.PrinterName;

                Apostille apostille = new Apostille();

                DateTime dt = monthCalendar1.SelectionStart;

                string theId = apostille.AddToDatabase(name.Text, dt.ToString("dd.MM.yyyy"), title.Text, capacity.Text, office.Text, document.Text, about.Text, sig.Text, country.Text, fullName);

                Apostiller aps = new Apostiller("APOSTILLE");

                aps.Print(printerName, name.Text, dt.ToString("dd.MM.yyyy"), title.Text, capacity.Text, document.Text, about.Text, sig.Text, country.Text, office.Text, theId, preferenceCity, preferenceCounty, fullName);

                GetTotalYear();

            }
        }
        public void Print(string p_printerName, string p_name, string p_date, string p_title, string p_capacity, string p_document,
              string p_about, string p_sig, string p_country, string p_office, string p_id, string p_prefCity, string p_prefCounty, string p_fullName)
        {
            if (p_printerName == null)
            {
                throw new ArgumentNullException("printerName");
            }
            if (p_name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (p_date == null)
            {
                throw new ArgumentNullException("date");
            }
            if (p_title == null)
            {
                throw new ArgumentNullException("title");
            }
            if (p_capacity == null)
            {
                throw new ArgumentNullException("capacity");
            }
            if (p_document == null)
            {
                throw new ArgumentNullException("document");
            }
            if (p_about == null)
            {
                throw new ArgumentNullException("about");
            }
            if (p_sig == null)
            {
                throw new ArgumentNullException("sig");
            }
            if (p_document == null)
            {
                throw new ArgumentNullException("document");
            }
            if (p_country == null)
            {
                throw new ArgumentNullException("countryText");
            }
            if (p_office == null)
            {
                throw new ArgumentNullException("office");
            }
            if (p_id == null)
            {
                throw new ArgumentNullException("id");
            }
            if (p_prefCity == null)
            {
                throw new ArgumentNullException("City");
            }
            if (p_prefCounty == null)
            {
                throw new ArgumentNullException("County");
            }

            if (p_fullName == null)
            {
                throw new ArgumentNullException("fullName");
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("N");
            sb.AppendLine("q812");
            sb.AppendLine("Q406,26");
            sb.AppendLine("rN");

            sb.AppendLine("N");
            sb.AppendLine("I8,E,001");
            sb.AppendLine("X20,90,3,800,850");
            sb.AppendLine(string.Format(
                 CultureInfo.InvariantCulture,
                 "A250,100,0,3,2,2,N,\"{0}\"", this.apostilleTitle));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,170,0,4,1,1,N,\"{0}\"", "( Convention de La Haye du octobre 1961 )"));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,250,0,3,0,0,N,\"{0}: {1}\"", "1. Country", "Norway"));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,300,0,3,0,0,N,\"{0}:\"", "2. This public document has been signed by"));
            sb.AppendLine(string.Format(
              CultureInfo.InvariantCulture,
              "A140,330,0,3,0,0,N,\"{0}\"", p_name));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,380,0,3,0,0,N,\"{0}:\"", "3. Acting in the capacity of"));
            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A140,410,0,3,0,0,N,\"{0}\"", p_capacity));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,460,0,3,0,0,N,\"{0}:\"", "4. bears the seal/stamp of"));

            int len = p_office.Length;
            if (len > 46)
            {
                sb.AppendLine(string.Format(
                   CultureInfo.InvariantCulture,
                   "A140,490,0,1,1,2,N,\"{0}\"", p_office));
            }
            else
            {
                sb.AppendLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "A140,490,0,3,0,0,N,\"{0}\"", p_office));

            }
            sb.AppendLine("LO20,550,778,3");

            sb.AppendLine(string.Format(
              CultureInfo.InvariantCulture,
              "A300,560,0,3,1,2,N,\"{0}\"", "Certified"));


            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,610,0,3,0,0,N,\"{0} {1}               {2} {3}\"", "5. at ", p_prefCity, "6. the", p_date));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,660,0,3,0,0,N,\"{0}\"", "7. by the Governor of the counties of"));
            sb.AppendLine(string.Format(
               CultureInfo.InvariantCulture,
               "A140,690,0,3,0,0,N,\"{0}\"", p_prefCounty));

            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A100,730,0,3,0,0,N,\"{0} {1}               {2} \"", "8. No", p_id, "10. Signature"));

            sb.AppendLine(string.Format(
               CultureInfo.InvariantCulture,
               "A100,780,0,3,0,0,N,\"{0} \"", "9. Seal/stamp"));

            sb.AppendLine("LO450,795,300,1");

            sb.AppendLine(string.Format(
               CultureInfo.InvariantCulture, "A100,810,0,3,0,0,N,\"{0}                        {1} \"", " ", p_fullName));

            sb.AppendLine("P1,1");

            RawPrinterHelper.SendStringToPrinter(p_printerName, sb.ToString());

        }


        private void clearData()
        {
            name.Clear();
            title.Clear();

            about.Clear();
            sig.Clear();
            SetPreference();

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newApostilleButton_Click(object sender, EventArgs e)
        {
            name.Select();

            bool isError = false;
            errorProvider1.Dispose();
            if (name.Text == "")
            {
                errorProvider1.SetError(name, "Feil: Navn feltet kan ikke være tomt!");
                isError = true;
            }
            if (title.Text == "")
            {
                errorProvider1.SetError(title, "Feil: Tittel feltet kan ikke være tomt!");
                isError = true;
            }
            if (capacity.Text == "")
            {
                errorProvider1.SetError(capacity, "Feil: Capacity feltet kan ikke være tomt!");
                isError = true;
            }
            if (office.Text == "")
            {
                errorProvider1.SetError(office, "Feil: Kontor feltet kan ikke være tomt!");
                isError = true;

            }
            if (document.Text == "")
            {
                errorProvider1.SetError(document, "Feil: Dokument feltet kan ikke være tomt!");
                isError = true;
            }
            if (about.Text == "")
            {
                errorProvider1.SetError(about, "Feil: Vedrørende feltet kan ikke være tomt!");
                isError = true;
            }
            if (sig.Text == "")
            {
                errorProvider1.SetError(sig, "Feil: Sig feltet kan ikke være tomt!");
                isError = true;
            }


            if (isError == false)
            {
                DateTime dt = monthCalendar1.SelectionStart;
                Apostille apostille = new Apostille();
                apostille.AddToDatabase(name.Text, dt.ToString("dd.MM.yyyy"), title.Text, capacity.Text, office.Text, document.Text,
                                about.Text, sig.Text, country.Text, fullName);
                clearData();

                GetTotalYear();
            }

        }

        private void copyToNextButton_Click(object sender, EventArgs e)
        {
            name.Select();

            bool isError = false;
            errorProvider1.Dispose();
            if (name.Text == "")
            {
                errorProvider1.SetError(name, "Feil: Navn feltet kan ikke være tomt!");
                isError = true;
            }
            if (title.Text == "")
            {
                errorProvider1.SetError(title, "Feil: Tittel feltet kan ikke være tomt!");
                isError = true;
            }
            if (capacity.Text == "")
            {
                errorProvider1.SetError(capacity, "Feil: Capacity feltet kan ikke være tomt!");
                isError = true;
            }
            if (office.Text == "")
            {
                errorProvider1.SetError(office, "Feil: Kontor feltet kan ikke være tomt!");
                isError = true;

            }
            if (document.Text == "")
            {
                errorProvider1.SetError(document, "Feil: Dokument feltet kan ikke være tomt!");
                isError = true;
            }
            if (about.Text == "")
            {
                errorProvider1.SetError(about, "Feil: Vedrørende feltet kan ikke være tomt!");
                isError = true;
            }
            if (sig.Text == "")
            {
                errorProvider1.SetError(sig, "Feil: Sig feltet kan ikke være tomt!");
                isError = true;
            }
            if (isError == false)
            {
                DateTime dt = monthCalendar1.SelectionStart;
                Apostille apostille = new Apostille();
                apostille.AddToDatabase(name.Text, dt.ToString("dd.MM.yyyy"), title.Text, capacity.Text, office.Text, document.Text,
                                about.Text, sig.Text, country.Text, fullName);
                GetTotalYear();
            }
        }


        private void normalSearchButton_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();

            form2.Show();

            using (PersonDataContext ctx = new PersonDataContext())
            {
                try
                {

                    if (idNumberSearch.Text == "" && nameSearch.Text == "")
                    {
                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.date == dateSearch.Value
                                                         select ap;

                        form2.dataGridView1.Enabled = true;

                    }

                    else if (idNumberSearch.Text == "")
                    {

                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.name.StartsWith(nameSearch.Text)
                                                         select ap;

                        form2.dataGridView1.Enabled = true;


                    }
                    else if (nameSearch.Text == "")
                    {
                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.ID == int.Parse(idNumberSearch.Text)
                                                         select ap;
                        form2.dataGridView1.Enabled = true;

                    }
                    else
                    {
                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.ID == int.Parse(idNumberSearch.Text) || ap.name.StartsWith(nameSearch.Text)
                                                         || ap.date == dateSearch.Value
                                                         select ap;
                        form2.dataGridView1.Enabled = true;

                    }
                }
                catch (Exception exe)
                {
                    Console.WriteLine("error {0}", exe.Message);
                }

            }
        }

        private void advanceSearchButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            form2.Show();

            if (dateRegisteredCheckbox.Checked == true && contentCheckbox.Checked == false)
            {

                using (PersonDataContext ctx = new PersonDataContext())
                {
                    try
                    {

                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.date >= DateTime.Parse(dateFromSearch.Text) && ap.date <= DateTime.Parse(dateToSearch.Text)
                                                         select ap;

                        form2.dataGridView1.Enabled = true;


                    }
                    catch (Exception exe)
                    {
                        Console.WriteLine(exe.Message);
                    }
                }
            }
            else if (contentCheckbox.Checked == true && dateRegisteredCheckbox.Checked == false)
            {

                using (PersonDataContext ctx = new PersonDataContext())
                {
                    try
                    {

                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where ap.country.StartsWith(countrySearch.Text) && ap.office.StartsWith(officeSearch.Text)
                                                         && ap.about.StartsWith(aboutSearch.Text) && ap.sig.StartsWith(sigSearch.Text)
                                                         select ap;

                        form2.dataGridView1.Enabled = true;


                    }
                    catch (Exception exe)
                    {
                        Console.WriteLine(exe.Message);
                    }
                }
            }

            else if (dateRegisteredCheckbox.Checked == true && contentCheckbox.Checked == true)
            {
                using (PersonDataContext ctx = new PersonDataContext())
                {
                    try
                    {

                        form2.dataGridView1.DataSource = from ap in ctx.apostilles
                                                         where (ap.date >= DateTime.Parse(dateFromSearch.Text) && ap.date <= DateTime.Parse(dateToSearch.Text))
                                                         && (ap.country.StartsWith(countrySearch.Text) && ap.office.StartsWith(officeSearch.Text)
                                                         && ap.about.StartsWith(aboutSearch.Text) && ap.sig.StartsWith(sigSearch.Text))
                                                         select ap;

                        form2.dataGridView1.Enabled = true;
                    }
                    catch (Exception exe)
                    {
                        Console.WriteLine(exe.Message);
                    }
                }
            }
        }



        private void showStatistics_Click(object sender, EventArgs e)
        {
            using (PersonDataContext pdc = new PersonDataContext())
            {
                var res = (from p in  pdc.apostilles
                           
                           group p by p.country);
                           
                

                Console.WriteLine(res.ToString());

               // foreach (var item in res)
                //{
                  //  Console.WriteLine(item);
                //}
                    //StatisticsCountry.Text += "\n"+ item.ToString(); 
              

            }
        }

      

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printOutButton_Click(sender, e);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            newApostilleButton_Click(sender, e);
        }

        private void preferanceToolTrip_Click(object sender, EventArgs e)
        {
            Preference prf = new Preference();

            prf.Show();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.countryesTableAdapter.FillBy(this.countryes1.countryes);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void StatisticsButton1_Click(object sender, EventArgs e)
        {
            using (PersonDataContext pdc = new PersonDataContext())
            {
                var res = (from ap in pdc.apostilles
                           where (ap.date >= DateTime.Parse(statisticsFromDate.Text) && ap.date <= DateTime.Parse(StatisticsToDate.Text))

                           select ap).Count();


                StatisticsResultLabel.Text = "Antall Apostiller produsert i perioden:  " + res.ToString();
            }
        }


    }

    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "APOSTILLE";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = (szString.Length + 1) * Marshal.SystemMaxDBCSCharSize;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}

