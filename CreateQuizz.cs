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

namespace JDM
{
    public partial class CreateQuizz : Form
    {
        string titlu;
        bool edit;
        Create create;
        int curent = 1;
        int corect = 1;
        public CreateQuizz(string titlu1,bool edit1,Create create1)
        {            
            
            InitializeComponent();
            titlu = titlu1;
            edit = edit1;
            create = create1;
            if (edit == false)
                radioButton1.Checked = true;
            button2.Visible = false;
            if (edit == true)
            {
                update();
            }

        }

        
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\baza.mdf;Integrated Security=True;Connect Timeout=30";

        private void button1_Click(object sender, EventArgs e)
        {
            save();
            
            if (curent == 5)
                button1.Visible = false;

            if (edit == true)
            {
                update();
            }

        }
        private void save()
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand delete = new SqlCommand("delete from quizz where id=@id and titlu=@titlu", con);
                delete.Parameters.AddWithValue("id", curent);
                delete.Parameters.AddWithValue("titlu", titlu);
                delete.ExecuteNonQuery();

                SqlCommand command = new SqlCommand("insert into quizz(intrebare,r1,r2,r3,r4,titlu,corect,id) values(@a,@b,@c,@d,@e,@f,@g,@h)", con);
                command.Parameters.AddWithValue("a", textBox1.Text);
                command.Parameters.AddWithValue("b", textBox2.Text);
                command.Parameters.AddWithValue("c", textBox3.Text);
                command.Parameters.AddWithValue("d", textBox4.Text);
                command.Parameters.AddWithValue("e", textBox5.Text);
                command.Parameters.AddWithValue("f", titlu);
                command.Parameters.AddWithValue("g", corect);
                command.Parameters.AddWithValue("h", curent);
                command.ExecuteNonQuery();
                curent++;
                button2.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            curent--;
            if (curent == 1)
                button2.Visible = false;
            update();
        }
        private void update()
        {
            radioButton1.Checked = true;

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("select intrebare,r1,r2,r3,r4,corect from quizz where id=@id and titlu=@titlu", con);
            command.Parameters.AddWithValue("id", curent);
            command.Parameters.AddWithValue("titlu", titlu);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = Convert.ToString(reader[0]);
                textBox2.Text = Convert.ToString(reader[1]);
                textBox3.Text = Convert.ToString(reader[2]);
                textBox4.Text = Convert.ToString(reader[3]);
                textBox5.Text = Convert.ToString(reader[4]);
                corect = Convert.ToInt32(reader[5]);
                if (corect == 1)
                    radioButton1.Checked = true;
                if (corect == 2)
                    radioButton2.Checked = true;
                if (corect == 3)
                    radioButton3.Checked = true;
                if (corect == 4)
                    radioButton4.Checked = true;
            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            corect = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            corect = 2;

        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            corect = 4;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            corect = 3;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            save();
            create.Show();
            this.Close();
        }

        private void CreateQuizz_Load(object sender, EventArgs e)
        {
            button1.BackColor = ColorTranslator.FromHtml("#6978F0");
            button2.BackColor = ColorTranslator.FromHtml("#6978F0");
            button3.BackColor = ColorTranslator.FromHtml("#6978F0");
            textBox1.BackColor = ColorTranslator.FromHtml("#202531");
            textBox2.BackColor = ColorTranslator.FromHtml("#202531");
            textBox3.BackColor = ColorTranslator.FromHtml("#202531");
            textBox4.BackColor = ColorTranslator.FromHtml("#202531");
            textBox5.BackColor = ColorTranslator.FromHtml("#202531");
            this.BackColor = ColorTranslator.FromHtml("#1C1C25");
        }

        private void CreateQuizz_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
        }
    }
}
