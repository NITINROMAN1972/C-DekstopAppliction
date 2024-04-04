using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudOpration
{
    public partial class Form1 : Form
    {

        SqlConnection connection;

        private int tempID;
        public Form1()
        {
            InitializeComponent();

             connection=new SqlConnection(@"Data Source=LAPTOP-GIF306L0; Initial Catalog=School;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        public void display()
        {
            SqlDataAdapter sqlDataAdapter= new SqlDataAdapter("Select * from Student" ,connection);  
            
            DataTable dt= new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource= dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            if (textBox1.Text=="" && textBox2.Text==""&& comboBox1.Text=="")
            {
                MessageBox.Show("FILL THE  ALL INFORAMATION " , "Error" ,MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }
            else{
            SqlCommand cmd;
            connection.Open();
            string sqlQuery = "INSERT INTO Student (Name,Mobile,Gender) VALUES(@p1,@p2,@p3)";

            cmd=new SqlCommand(sqlQuery,connection);
            cmd.Parameters.AddWithValue("@p1",textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", comboBox1.Text);

            cmd.CommandType= CommandType.Text;  
            cmd.ExecuteNonQuery();
            connection.Close();
            

            display();

            MessageBox.Show("DATA ADD", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearFields();
           } 

        }

        public void clearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();

            for(int i = 0;i< dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow delRow= dataGridView1.Rows[i];
                if (delRow.Selected == true)
                {
                    string sqlQuery = "DELETE FROM Student Where name='" + dataGridView1.Rows[i].Cells[0].Value + "'";

                    SqlCommand sqlCommand= new SqlCommand(sqlQuery,connection);
                    sqlCommand.ExecuteNonQuery();
                    dataGridView1.Rows.RemoveAt(i);

                    clearFields();

                    MessageBox.Show("DATA DELETE", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
               tempID=0;
                  connection.Close();
        }

        private void mouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                tempID = int.Parse( dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
           if(tempID<=0){
                MessageBox.Show("SELECT THE RECORD  " , "Error" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                   return;  
            }      
            else{
            SqlCommand cmd;
            connection.Open();

           string sqlQuery = "UPDATE Student set Name=@p1,Mobile=@p2,Gender=@p3  where id='"+tempID+"' ";
       
          
            cmd = new SqlCommand(sqlQuery, connection);
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", comboBox1.Text);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            connection.Close();


            display();

            MessageBox.Show("RECORD UPDATE ", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            clearFields();
             tempID=0;
            }
        }
    }
}
