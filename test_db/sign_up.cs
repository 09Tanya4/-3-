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
    public partial class sign_up : Form
    {
        Class1 database = new Class1();
        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            

            textBox_password2.PasswordChar = '*';
            pictureBox4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            var login = textBox_login2.Text;
            var password = textBox_password2.Text;


            string querystring = $"Insert into register(login_user, password_user) values('{login}','{password}')";


            SqlCommand command=new SqlCommand(querystring, database.getConnection());

            database.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан", "Успех!");
                log_in frm_log = new log_in();
                this.Hide();
                frm_log.ShowDialog();

            }
            else 
            {
                MessageBox.Show("Аккаунт не создан");
            }
            database.closeConnection();
            if(checkUser())
            {
                return;
            }
        }

        private Boolean checkUser()
        {
            var loginUser = textBox_login2.Text;
            var passUser = textBox_password2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select* from register where login_user = '{loginUser}' password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_login2.Text = "";
            textBox_password2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox_password2.UseSystemPasswordChar = true;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox_password2.UseSystemPasswordChar = false;
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
        }
    }
    }

