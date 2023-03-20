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

namespace test_db
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form1 : Form
    {
        Class1 database = new Class1();
        int selectedRow;
        public Form1()
        {
            InitializeComponent();
            
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("name_of", "Наименование товара");
            dataGridView1.Columns.Add("type_of", "Тип оборудования");
            dataGridView1.Columns.Add("registr_n", "Регистрация номера");
            dataGridView1.Columns.Add("isNew", String.Empty);
        }
       private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
         dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string querryString = $"Select * from test_equip";

            SqlCommand command = new SqlCommand(querryString, database.getConnection());
            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
           
        }

        private void button_new_Click(object sender, EventArgs e)
        {
            Add_form addfrms = new Add_form();
            addfrms.Show();
        }


        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from test_equip where concat(id, name_of, type_of, registr_n) like '%"+textBox_search.Text+"%'";

            SqlCommand com = new SqlCommand(searchString, database.getConnection());

            database.openConnection();
            SqlDataReader read = com.ExecuteReader();
            while(read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

           dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == String.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
        }

        private void Update()
        {
            database.openConnection();
            for(int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;

                if (rowState == RowState.Existed)
                    continue;
                if(rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery=$"delete from test_equip where id = {id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());

                    command.ExecuteNonQuery();
                }
                if(rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var type = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var registr = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var price = dataGridView1.Rows[index].Cells[4].Value.ToString();

                    var ChangeQuery = $"update test_equip set name_of = '{name}', type_of = '{type}', registr_n = '{registr}' where id = '{id}'";

                    var command = new SqlCommand(ChangeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }
          database.closeConnection();
        }
        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);

        }

      
     
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox_search_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            log_in frm_sign = new log_in();
            frm_sign.Show();
            this.Hide();
        }
    }
}
