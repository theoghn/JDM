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
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace JDM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\baza.mdf;Integrated Security=True;Connect Timeout=30";

        private void button1_Click(object sender, EventArgs e)
        {
            
            string input = Interaction.InputBox("Introdu nume", "Creeare Prezentare", "");
            if (input.Length > 0)
            {
                string root = Application.StartupPath;

                // Get all subdirectories

                string[] subdirectoryEntries = Directory.GetDirectories(root);

                // Loop through them to see if they have any other subdirectories
                int gasit = 0;

                foreach (string subdirectory in subdirectoryEntries)
                {
                    if (subdirectory.Equals(root + @"\" + input))
                    {
                        MessageBox.Show("Proiect cu acest nume este deja existent");
                        gasit = 1;
                    }
                }
                if(gasit == 0)
                {
                    Create create = new Create(input,false);
                    create.Show();
                    this.Hide();
                }
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox1.SelectedItem).Length > 0)
            {
                View view = new View(Convert.ToString(comboBox1.SelectedItem));
                view.Show();
                this.Hide();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox1.SelectedItem).Length > 0)
            {
                Create create = new Create(Convert.ToString(comboBox1.SelectedItem), true);
                create.Show();
                this.Hide();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#1C1C25");
            button1.BackColor = ColorTranslator.FromHtml("#6978F0");
            button2.BackColor = ColorTranslator.FromHtml("#6978F0");
            button3.BackColor = ColorTranslator.FromHtml("#6978F0");
            comboBox1.BackColor = ColorTranslator.FromHtml("#202531");

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("select nume from cate", con);
            SqlDataReader reader = command.ExecuteReader();
            string a="";
            while (reader.Read())
            {
                if (Convert.ToString(reader[0]).Equals(a)==false)
                {
                    a = Convert.ToString(reader[0]);
                    comboBox1.Items.Add(a);
                    comboBox1.SelectedItem = a;
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
