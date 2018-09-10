using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodefirstEntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePickerOEBDate.MaxDate = DateTime.Today;

            EfContext efContext = new EfContext();
            branchBindingSource.DataSource = efContext.Branches.ToList();

            List<Branch> branches = efContext.Branches.Include(b => b.Teachers).ToList();
            comboBoxOEBrans.DataSource = branches;
            efContext.Dispose();
        }

        private void buttonBEkleme_Click(object sender, EventArgs e)
        {
            Branch branch = new Branch
            {
                Name = textBoxBEkleme.Text
            };

            EfContext efContext = new EfContext();
            efContext.Branches.Add(branch);

            try
            {
                efContext.SaveChanges();
                MessageBox.Show("Kaydedildi");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Kaydedilmedi");
            }

            efContext.Dispose();
        }

        private void buttonOEKaydet_Click(object sender, EventArgs e)
        {
            Branch branch = comboBoxOEBrans.SelectedItem as Branch;
            if (branch == null)
            {
                MessageBox.Show("Branş seçilmelidir.");
                return;
            }

            Teacher teacher = new Teacher
            {
                Name = textBoxOKAd.Text,
                Surname = textBoxOESoyad.Text,
                BirthDate = dateTimePickerOEBDate.Value,
                BranchId = branch.Id
            };

            EfContext efContext = new EfContext();
            efContext.Teachers.Add(teacher);
            try
            {
                efContext.SaveChanges();
                MessageBox.Show("Kaydedildi");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Kaydedilmedi");
            }
        }

        private Branch branchEditing;
        private void buttonBGGetir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxBid.Text);

            EfContext context = new EfContext();
            branchEditing = context.Branches.FirstOrDefault(b => b.Id == id);

            if (branchEditing == null)
            {
                MessageBox.Show("Branş bulunamadı");
                return;
            }

            textBoxBGAd.Text = branchEditing.Name;
        }

        private void buttonBGuncelleme_Click(object sender, EventArgs e)
        {
            branchEditing.Name = textBoxBGAd.Text;

            EfContext efContext = new EfContext();
            Branch originBranch = efContext.Branches.FirstOrDefault(b => b.Id == branchEditing.Id);

            efContext.Entry(originBranch).CurrentValues.SetValues(branchEditing);
            try
            {
                efContext.SaveChanges();
                MessageBox.Show("Kaydedildi");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Kaydedilmedi");
            }
        }

        private void buttonBSil_Click(object sender, EventArgs e)
        {
            if (branchEditing == null)
            {
                MessageBox.Show("Silinecek branş bulunmadı");
                return;
            }

            EfContext efContext = new EfContext();
            
            efContext.Branches.Remove(efContext.Branches.Find(branchEditing.Id));
            try
            {
                efContext.SaveChanges();
                MessageBox.Show("Silindi");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Silinmedi");
            }
        }
    }
}
