using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ITMO._2022.CSharp.WinForms.FINAL_TASK_project
{
    public partial class ServerInput : Form
    {
        public bool Inputsuccess = false;

        public ServerInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinForms;Data Source={textBox1.Text}";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Open();
                    Inputsuccess = true;
                    ServerName.txt = textBox1.Text;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Не удалось подключиться к серверу  c базой данных WinForms \n Ошибка: {ex.Message}");
                }
            }
            Close();
        }
       
    }
    static class ServerName
    {
        public static string txt;
    }

}
