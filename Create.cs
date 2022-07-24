using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using Microsoft.VisualBasic.FileIO;

namespace JDM
{
    public partial class Create : Form
    {
        string nume;
        bool edit_mode;
        public Create(string input, bool edit)
        {
            InitializeComponent();
            edit_mode = edit;
            textBox5.Text = input;
            richTextBox2.Text = input;
            richTextBox2.BackColor = ColorTranslator.FromHtml("#1C1C25");

            panel1.BackColor = ColorTranslator.FromHtml("#1C1C25");
            panel2.BackColor = ColorTranslator.FromHtml("#202531");
            pictureBox1.BackColor = ColorTranslator.FromHtml("#202531");
            richTextBox1.BackColor = ColorTranslator.FromHtml("#202531");
            textBox3.BackColor = ColorTranslator.FromHtml("#202531");
            comboBox1.BackColor = ColorTranslator.FromHtml("#202531");


            button1.BackColor = ColorTranslator.FromHtml("#6978F0");
            button2.BackColor = ColorTranslator.FromHtml("#6978F0");
            button3.BackColor = ColorTranslator.FromHtml("#6978F0");
            button4.BackColor = ColorTranslator.FromHtml("#6978F0");
            button5.BackColor = ColorTranslator.FromHtml("#6978F0");
            button7.BackColor = ColorTranslator.FromHtml("#6978F0");




        }


        private void button2_Click(object sender, EventArgs e)
        {
             
            OpenFileDialog open = new OpenFileDialog();
            
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg; *.jpeg; *.gif; *.bmp;*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                 
                pictureBox1.Image = new Bitmap(open.FileName);
                poza = 1;
                
               
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        int i = 0;
        string[] categorii= {"","", "", "", "" };
        string[] s = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        private void button4_Click(object sender, EventArgs e)
        {
            int b = 0;
            for (int i = 0; i < 5; i++)
            {
                if (textBox3.Text.Equals(categorii[i]) && textBox3.Text.Length > 0)
                {
                    MessageBox.Show("Subiect deja existent");
                    b = 1;
                }
            }
            if (b == 0)
            {
                int a = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (categorii[i].Length == 0)
                    {
                        a = 1;
                        categorii[i] = textBox3.Text;
                        update();
                        break;
                    }
                }
                if (a == 1)
                {
                    MessageBox.Show("Subiect Adaugat");
                }
                else
                {
                    MessageBox.Show("Nr max de subiecte atins");
                }
                
            }
            textBox3.Text = "";

        }


        private void update()
        {
            comboBox1.Items.Clear();
            int a = 1;
            for (int i = 0; i < 5; i++)
            {
                if (categorii[i].Length != 0)
                {
                    a = 0;
                    comboBox1.Items.Add(categorii[i]);
                    comboBox1.SelectedIndex = i;
                }
            }
            if (a == 1)
            {
                comboBox1.Text = "";
            }

        }
        

        

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            if (comboBox1.SelectedIndex != -1)
            {
                categorii[4] = "";
                for (int i = comboBox1.SelectedIndex; i < 4; i++)
                {
                    SqlCommand update;
                    categorii[i] = categorii[i + 1];
                    if(edit_mode==true) 
                        update = new SqlCommand("UPDATE copy SET id=@i WHERE nume=@nume and id=@ie",con);
                    else
                        update = new SqlCommand("UPDATE cate SET id=@i WHERE nume=@nume and id=@ie", con);
                    update.Parameters.AddWithValue("i", i - 1);
                    update.Parameters.AddWithValue("ie", i);
                    update.Parameters.AddWithValue("nume", textBox5.Text);
                    update.ExecuteNonQuery();


                }
                for (int i = 0 + comboBox1.SelectedIndex * 5; i < 20; i++)
                {
                    s[i] = s[i + 5];
                   
                }
                string dir;
                if (edit_mode == true)
                    dir = Application.StartupPath + @"\" + textBox5.Text + "_copy" + @"\" + comboBox1.Text;
                else
                    dir = Application.StartupPath + @"\" + textBox5.Text + @"\" + comboBox1.Text;
                if (Directory.Exists(dir))
                {
                    
                    Directory.Delete(dir, true);

                }

                s[20] = "";
                s[21] = "";
                s[22] = "";
                s[23] = "";
                s[24] = "";

                SqlCommand command;
                if (edit_mode == true)
                    command = new SqlCommand("delete from copy where categorie =@a ", con);
                else
                    command = new SqlCommand("delete from cate where categorie =@a ", con);

                command.Parameters.AddWithValue("a", comboBox1.Text);
                command.ExecuteNonQuery();

                update();
                MessageBox.Show("Subiect Sters");

            }
            else
            {
                MessageBox.Show("error");

            }



        }

        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\baza.mdf;Integrated Security=True;Connect Timeout=30";

        int poza=0, text=0;
        
        
        private void button3_Click(object sender, EventArgs e)
        {
            bool input_existent = false;
            string input="";
            int nr_pagini = 0;
            SqlCommand command;
            SqlConnection con = new SqlConnection(connection);
            if (text == 1 && poza == 1 && comboBox1.SelectedIndex!=-1)
            {
                input = Interaction.InputBox("Introdu un nume pentru pagina ", "Salveaza Pagina", "");
                con.Open();
                if (edit_mode == true)
                    command = new SqlCommand("select subcategorie from copy id where categorie=@categorie and nume =@nume", con);
                else
                    command = new SqlCommand("select subcategorie from cate id where categorie=@categorie and nume =@nume", con);

                command.Parameters.AddWithValue("nume", textBox5.Text);
                command.Parameters.AddWithValue("categorie", comboBox1.Text);

                SqlDataReader reader = command.ExecuteReader();

                
                while (reader.Read())
                {
                    if (Convert.ToString(reader[0]).Equals(input))
                    {
                        input_existent = true;
                        MessageBox.Show("Pagina cu nume deja existenta in categoria " + comboBox1.Text);
                    }
                    else if (Convert.ToString(reader[0]).Length > 20)
                    {
                        input_existent = true;
                        MessageBox.Show("Numele este prea lung " + comboBox1.Text);
                    }
                    else
                    {
                        nr_pagini++;
                    }

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Incarca imagine si scrie o descriere si alege un subiect");
                input_existent = true;

            }

            if (nr_pagini >= 5 && input_existent == false)
            {
                MessageBox.Show("Nr pagini max atins");
                input_existent = true;
            }
            
            
            

            if (input_existent == false)
            {
                string dir;
                if (edit_mode==true)
                    dir = Application.StartupPath + @"\" + textBox5.Text + "_copy";
                else
                    dir = Application.StartupPath + @"\" + textBox5.Text;


                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                dir = dir + @"\" + comboBox1.Text;
                
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }


                File.WriteAllText(dir + @"\" + input + ".txt", richTextBox1.Text);
                    pictureBox1.Image.Save(dir + @"\" + input + ".png", ImageFormat.Png);



                    if (comboBox1.Text.Length != 0 && input.Length != 0)
                    {

                        if (edit_mode == true)
                            command = new SqlCommand("insert into copy(id,categorie,subcategorie,nume) values(@a,@b,@c,@d)", con);
                        else
                            command = new SqlCommand("insert into cate(id,categorie,subcategorie,nume) values(@a,@b,@c,@d)", con);
                    
                        command.Parameters.AddWithValue("a", comboBox1.SelectedIndex);
                        command.Parameters.AddWithValue("b", comboBox1.Text);
                        command.Parameters.AddWithValue("c", input);
                        command.Parameters.AddWithValue("d", textBox5.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Pagina " + input + " incarcata in Prezentarea " + textBox5.Text + "");

                    }                             
            }

           
        }

        

        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                text = 1;
            }
            else
                text = 0;
        }

        private void Create_Load(object sender, EventArgs e)
        {
            GC.Collect();
            if(edit_mode == true)
            {
                button1.Text = "Editeaza chestionarul";
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand("select * from cate where nume = @a", con);
                command.Parameters.AddWithValue("a", textBox5.Text);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader[0]);

                    categorii[id] = Convert.ToString(reader[1]);


                    for (int i = 0 + id * 5; i < (id + 1) * 5; i++)
                    {
                        if (s[i].Length == 0)
                        {
                            s[i] = Convert.ToString(reader[2]);
                            break;
                        }
                    }
                }
                update();
                reader.Close();
                command = new SqlCommand("delete from copy", con);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into copy select * from cate ", con);
                command.ExecuteNonQuery();
                string sourcePath = Application.StartupPath+ @"\" + textBox5.Text;
                string destinationPath = Application.StartupPath + @"\" + textBox5.Text+"_copy";
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourcePath, destinationPath, UIOption.AllDialogs);
                }

                

            }
        }

        

        private void Create_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Doresti sa salvezi aceasta prezentare?";
            if (edit_mode==true)
                message = "Doresti sa salvezi schimbarile facute aceastei prezentari?";
            string title = "Save Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult dialog = MessageBox.Show(message, title, buttons);
            if (dialog == DialogResult.Yes)
            {
                if(edit_mode == true)
                {
                    string root = Application.StartupPath + @"\" + textBox5.Text; 
                    string root2 = Application.StartupPath + @"\" + textBox5.Text +"_copy";
                    if (Directory.Exists(root) && Directory.Exists(root2))
                    {
                        Directory.Delete(root, true);
                        Directory.CreateDirectory(root);
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(root2, root, UIOption.AllDialogs);
                        Directory.Delete(root2, true);

                    }                    
                    SqlConnection con = new SqlConnection(connection);
                    con.Open();
                    SqlCommand command = new SqlCommand("delete from cate",con);
                    command.ExecuteNonQuery();
                    command = new SqlCommand("insert into cate select * from copy ", con);
                    command.ExecuteNonQuery();
                    Application.ExitThread();
                }
                else
                    Application.ExitThread();
            }
            else if (dialog == DialogResult.No)
            {
                if (edit_mode == true)
                {
                    string root = Application.StartupPath + @"\" + textBox5.Text + "_copy";
                    if (Directory.Exists(root))
                    {                        
                        Directory.Delete(root, true);
                        Application.ExitThread();

                    }
                    Application.ExitThread();
                }
                else
                {
                    string root = Application.StartupPath + @"\" + textBox5.Text;
                    if (Directory.Exists(root))
                    {
                        SqlConnection con = new SqlConnection(connection);
                        con.Open();
                        SqlCommand command = new SqlCommand("delete from cate where nume=@nume", con);
                        command.Parameters.AddWithValue("nume", textBox5.Text);
                        command.ExecuteNonQuery();
                        command = new SqlCommand("delete from quizz where nume=@nume", con);
                        command.Parameters.AddWithValue("nume", textBox5.Text);
                        command.ExecuteNonQuery();
                        Directory.Delete(root, true);
                        Application.ExitThread();

                    }
                    Application.ExitThread();
                }
                    

            }
            else if (dialog == DialogResult.Cancel)
            {
                e.Cancel = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (edit_mode == true)
                MessageBox.Show("Toate schimbarile din cadrul chestionarului vor fi permanente");
            CreateQuizz create = new CreateQuizz(textBox5.Text, edit_mode,this);
            create.Show();
            this.Hide();
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
