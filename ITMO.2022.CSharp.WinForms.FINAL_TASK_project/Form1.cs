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
    public partial class Form1 : Form
    {
       private DataSet dataset;
       private SqlDataAdapter adapter;
       private SqlCommandBuilder commandBuilder;
        private SqlConnection connection;
      


        public Form1()
        {
            InitializeComponent();            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string conString = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinForms;Data Source={ServerName.txt}";
            connection = new SqlConnection(conString);

                dataset.Clear ();
                adapter.Fill(dataset, "People");
                dataGridView1.DataSource = dataset.Tables["People"];
                dataGridView1.Columns["Id"].ReadOnly = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
                int newRow = dataGridView1.Rows.Count - 2;
            DataRow row = dataset.Tables["People"].NewRow();
            row["Name"] = dataGridView1.Rows[newRow].Cells["Name"].Value;
            
                row["age"] = dataGridView1.Rows[newRow].Cells["age"].Value;
            dataset.Tables["People"].Rows.Add(row);
            dataset.Tables["People"].Rows.RemoveAt(dataset.Tables["People"].Rows.Count- 1) ;
            adapter.Update(dataset, "People");
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
            adapter.Update(dataset, "People");
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string conString = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinForms;Data Source={ServerName.txt}";
            connection = new SqlConnection(conString);

            adapter = new SqlDataAdapter("select * from People", connection);
            commandBuilder = new SqlCommandBuilder(adapter);
            commandBuilder.GetDeleteCommand();
            commandBuilder.GetInsertCommand();
            commandBuilder.GetUpdateCommand();

            dataset = new DataSet();
                try
                {
                    adapter.Fill(dataset, "People");
                }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = dataset.Tables["People"];
            dataGridView1.Columns["Id"].ReadOnly = true;
            ToolTip tt_insert = new ToolTip();
            tt_insert.SetToolTip(button2, "Подвердите вставку данных нажатием INSERT и UPDATE");
            ToolTip tt_delete = new ToolTip();
            tt_delete.SetToolTip(button3, "Выделите строки для удаления, подвердите нажатием DELETE и UPDATE");
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
            e.Control.KeyPress -= new KeyPressEventHandler(NotDigit_KeyPress);
            e.Control.KeyPress -= new KeyPressEventHandler(NotLetter_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                TextBox digit = e.Control as TextBox;
                if (digit != null)
                    digit.KeyPress += new KeyPressEventHandler(NotLetter_KeyPress);
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                TextBox text = e.Control as TextBox;
                if (text != null)
                    text.KeyPress += new KeyPressEventHandler(NotDigit_KeyPress);
            }
        }

          

        private void NotDigit_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar)) 
            {
                e.Handled = true;
                MessageBox.Show("Ввод только чисел");
            }
                
        }

        private void NotLetter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ввод только текста");
            }
        }
    }
} 
