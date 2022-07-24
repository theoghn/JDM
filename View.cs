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
using System.IO;

namespace JDM
{
    public partial class View : Form
    {
        string titlu;
        public View(string nume)
        {
            titlu = nume;
            InitializeComponent();
            richTextBox2.Text = nume;
            richTextBox2.BackColor = ColorTranslator.FromHtml("#1C1C25");
            panel2.BackColor = ColorTranslator.FromHtml("#1C1C25");
            panel3.BackColor = ColorTranslator.FromHtml("#1F2029");

            panel1.BackColor = ColorTranslator.FromHtml("#1C1C25");
            pictureBox1.BackColor = ColorTranslator.FromHtml("#1F2029");
            richTextBox1.BackColor = ColorTranslator.FromHtml("#1F2029");

        }
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\baza.mdf;Integrated Security=True;Connect Timeout=30";
        string[] categorii= {"","", "", "", "" };
        string[] s = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        private Button button;
        private void View_Load(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand check = new SqlCommand("select * from quizz where titlu =@nume", con);
            check.Parameters.AddWithValue("nume", titlu);
            SqlDataReader reader2 = check.ExecuteReader();
            while (reader2.Read())
            {
                button12.Visible = true;
            }
            reader2.Close();
            SqlCommand command = new SqlCommand("select * from cate where nume = @a",con);
            command.Parameters.AddWithValue("a", titlu);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0]);

                categorii[id] = Convert.ToString(reader[1]);
                

                for (int i = 0 + id* 5; i < (id + 1) * 5; i++)
                {
                    if (s[i].Length == 0)
                    {
                        s[i] = Convert.ToString(reader[2]);
                        break;
                    }
                }
            }
            if (categorii[0].Length >0)
            {
                button1.Text = categorii[0];
                button1.Visible = true;
            }
            if (categorii[1].Length > 0)
            {
                button2.Text = categorii[1];
                button2.Visible = true;
            }
            if (categorii[2].Length > 0)
            {
                button3.Text = categorii[2];
                button3.Visible = true;
            }
            if (categorii[3].Length > 0)
            {
                button4.Text = categorii[3];
                button4.Visible = true;
            }
            if (categorii[4].Length > 0)
            {
                button5.Text = categorii[4];
                button5.Visible = true;
            }
            ActivateButton(button1);
            enable(0);
            ActivateButton2(button6);
            show();
            
        }
        private void enable(int a)
        {
            if (s[a * 5].Length > 0)
            {
                button6.Text = s[a * 5];
                button6.Visible = true;
            }
            else button6.Visible = false;

            if (s[a * 5 + 1].Length > 0)
            {
                button7.Text = s[a * 5 + 1];
                button7.Visible = true;
            }
            else button7.Visible = false;

            if (s[a * 5 +2].Length > 0)
            {
                button8.Text = s[a * 5 + 2];
                button8.Visible = true;
            }
            else button8.Visible = false;

            if (s[a * 5 + 3].Length > 0)
            {
                button9.Text = s[a * 5 + 3];
                button9.Visible = true;
            }
            else button9.Visible = false;

            if (s[a * 5 + 4].Length > 0)
            {
                button10.Text = s[a * 5 + 4];
                button10.Visible = true;
            }
            else button10.Visible = false;


        }
        private void ActivateButton(object Btnsender)
        {
            if (Btnsender != null)
            {
                if (button != (Button)Btnsender)
                {

                    DisableButton();
                    Color color = Color.LightGreen;
                    button = (Button)Btnsender;
                    button.BackColor = ColorTranslator.FromHtml("#6978F0");
                    button.ForeColor = ColorTranslator.FromHtml("#FDFFFF");
                }
            }
        }
        private void ActivateButton2(object Btnsender)
        {
            if (Btnsender != null)
            {
                if (button != (Button)Btnsender)
                {

                    DisableButton2();
                    Color color = Color.LightGreen;
                    button = (Button)Btnsender;
                    button.BackColor = ColorTranslator.FromHtml("#6978F0"); 
                    button.ForeColor = ColorTranslator.FromHtml("#FDFFFF");
                }
            }
        }
        private void DisableButton2()
        {
            foreach (Control btn in panel1.Controls)
            {
                if (btn.GetType() == typeof(Button))
                {
                    btn.BackColor = ColorTranslator.FromHtml("#1F2029"); 
                    btn.ForeColor = ColorTranslator.FromHtml("#FDFFFF");
                }
            }


        }
        private void DisableButton()
        {
            foreach (Control btn in panel2.Controls)
            {
                if (btn.GetType() == typeof(Button))
                {
                    btn.BackColor = ColorTranslator.FromHtml("#1F2029"); 
                    btn.ForeColor = ColorTranslator.FromHtml("#FDFFFF");
                }
            }
            

        }
        int x = 0;
        int y = 0;
        private void show()
        {
            if (File.Exists(Path.Combine(Application.StartupPath + @"\"+ titlu + @"\" + categorii[x], s[x * 5 + y] + ".png")))
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\"+ titlu + @"\" + categorii[x] + @"\" + s[x * 5 + y] + ".png");

            }
            if(File.Exists(Path.Combine(Application.StartupPath + @"\"+titlu + @"\" + categorii[x], s[x * 5 + y] + ".txt")))
            {
                richTextBox1.Text = System.IO.File.ReadAllText(Application.StartupPath + @"\" + titlu + @"\" + categorii[x] + @"\" + s[x * 5 + y] + ".txt");

            }
            GC.Collect();

            
        }
        private void load(int a)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivateButton(button1);
            enable(0);
            x = 0;
            y = 0;
            show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActivateButton(button2);
            enable(1);
            x = 1;
            y = 0;
            show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActivateButton(button3);
            enable(2);
            x = 2;
            y = 0;
            show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActivateButton(button4);
            enable(3);
            x = 3;
            y = 0;
            show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ActivateButton(button5);
            enable(4);
            x = 4;
            y = 0;
            show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActivateButton2(button6);
            y = 0;
            show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ActivateButton2(button7);
            y = 1;
            show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ActivateButton2(button8);
            y = 2;
            show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ActivateButton2(button9);
            y = 3;
            show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ActivateButton2(button10);
            y = 4;
            show();
        }
        int edit = 0;
        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edit == 0)
            {
                string message = "Doresti sa parasesti aceasta prezentare?";
                string title = "Leave?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                if (MessageBox.Show(message, title, buttons) == DialogResult.Yes)
                {
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;

                }
            }
            
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Bitmap bitmap =new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); ;
            pictureBox1.Image= bitmap;
            GC.Collect();
            edit = 1;
            Create create = new Create(titlu, true);
            create.Show();
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Quizz quizz = new Quizz(titlu,this);
            quizz.Show();
            this.Hide();
        }
    }
}
