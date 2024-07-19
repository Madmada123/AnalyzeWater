using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeWater
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сохраните строку подключения или другие настройки
          //  Properties.Settings.Default.ConnectionString = txtConnectionString.Text;
         //   Properties.Settings.Default.Save();
            MessageBox.Show("Настройки сохранены.");
            this.Close();
        }

        
    }
}
