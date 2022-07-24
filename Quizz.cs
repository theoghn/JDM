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
    public partial class Quizz : Form
    {
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\baza.mdf;Integrated Security=True;Connect Timeout=30";
        int intrebare = 1;
        int[] corect = new int[6];
        int[] raspuns = new int[] {0, -1, -3, -5, -7, -9 };
        int nr=0;
        string titlu;
        View view;
        public Quizz(string titlu1,View v)
        {
            view = v;
            titlu = titlu1;
            InitializeComponent();
            update();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            for (int i = 1; i < 6; i++)
            {
                SqlCommand command = new SqlCommand("select corect from quizz where id=@id and titlu=@titlu", con);
                command.Parameters.AddWithValue("id", i);
                command.Parameters.AddWithValue("titlu", titlu);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nr++;
                    corect[i] = Convert.ToInt32(reader[0]);
                }
                reader.Close();
            }
            update();
            show();
            richTextBox1.BackColor = ColorTranslator.FromHtml("#1d1e26");
        }
        private void update()
        {

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("select intrebare,r1,r2,r3,r4 from quizz where id=@id and titlu=@titlu", con);
            command.Parameters.AddWithValue("id", intrebare);
            command.Parameters.AddWithValue("titlu", titlu);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                richTextBox1.Text = Convert.ToString(reader[0]);
                button1.Text = Convert.ToString(reader[1]);
                button2.Text = Convert.ToString(reader[2]);
                button3.Text = Convert.ToString(reader[3]);
                button4.Text = Convert.ToString(reader[4]);

            }
        }
        private void show()
        {
            if (intrebare == 1)
            {
                button5.Visible = false;
            }
            if(intrebare == nr)
            {
                button6.Text = "Finalizare";
            }
        }
        private void Quizz_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#1d1e26");

            button7.BackColor = ColorTranslator.FromHtml("#23252d");
            button5.BackColor = ColorTranslator.FromHtml("#23252d");
            button6.BackColor = ColorTranslator.FromHtml("#23252d");
            button1.BackColor = ColorTranslator.FromHtml("#5c61ed");
            button2.BackColor = ColorTranslator.FromHtml("#dba550");
            button3.BackColor = ColorTranslator.FromHtml("#ec857d");
            button4.BackColor = ColorTranslator.FromHtml("#6cb8a8");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            raspuns[intrebare] = 1;
            if (button6.Text.Equals("Finalizare"))
            {
                check();
            }
            else
            {
                intrebare++;
                update();
                show();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            raspuns[intrebare] = 2;
            if (button6.Text.Equals("Finalizare"))
            {
                check();
            }
            else
            {
                intrebare++;
                update();
                show();
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            raspuns[intrebare] = 3;
            if (button6.Text.Equals("Finalizare"))
            {
                check();
            }
            else
            {
                intrebare++;
                update();
                show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            raspuns[intrebare] = 4;
            if (button6.Text.Equals("Finalizare"))
            {
                check();
            }
            else
            {
                intrebare++;
                update();
                show();
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            intrebare--;
            update();
            show();

        }
       

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text.Equals("Finalizare"))
            {
                check();
            }
            else
            {
                intrebare++;
                update();
                show();

            }

        }

        private void check()
        {
            int nice = 0 ;
            for (int i = 1; i <=nr; i++)
            {
                if (raspuns[i] == corect[i])
                    nice++;

            }
            richTextBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button6.Visible = false;
            label2.Visible = true;
            label2.Text = "Scorul tau a fost " + nice + "/" + nr;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            view.Show();
            this.Close();
        }
    }
}
