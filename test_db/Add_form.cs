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
    public partial class Add_form : Form
    {
        Class1 database = new Class1();
        public Add_form()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var type = textBox_type.Text;
            var count = textBox_count2.Text;
            var postavka = textBox_postavka2.Text;
            int price;


            if (int.TryParse(textBox_price2.Text, out price))
            {
                var addQuery = $"insert into test_db (type_of,count_of, postavka, price) values ('{type}', '{count}', '{postavka}', '{price}')";

                var command = new SqlCommand(addQuery, database.getConnection());
                command.ExecuteNonQuery();
            
                MessageBox.Show("Запись успешно создано!","Успех!",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Цена должна иметь числовой формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }

        private void Add_form_Load(object sender, EventArgs e)
        {

        }
    }
}

