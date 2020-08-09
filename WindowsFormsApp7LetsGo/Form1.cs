using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7LetsGo
{
    public partial class Form1 : Form
    {
        string strCnn = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\naouf\Documents\Courses\GI2\S4\dotNet\Database41.mdb";
        
        public Form1()
        {
            InitializeComponent();
            this.button3.Enabled = false;
            this.button5.Enabled = false;
            this.groupBox2.Enabled = false;
            OleDbConnection cnn = new OleDbConnection(strCnn);
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM INFORMATIONS;", cnn);
            int i = (int)cmd.ExecuteScalar();
            if (i == 0)
                this.groupBox3.Enabled = false;
            else
                this.groupBox3.Enabled = true;
            cnn.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Text!="") {
                this.button5.Enabled = true;
                this.button3.Enabled = true;
            }
            else
            {
                this.button3.Enabled = false;
                this.button5.Enabled = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
                this.listBox1.Items.Add(this.comboBox1.SelectedItem);
            else
            {
                for (int i = 0; i < this.listBox1.Items.Count; i++)
                {
                    if (!this.listBox1.Items[i].ToString().Equals(this.comboBox1.SelectedItem.ToString()))
                        this.listBox1.Items.Add(this.comboBox1.SelectedItem);
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = true;
            this.groupBox2.Enabled = true;
            this.button3.Enabled = true;
            this.textBox6.Clear();
            this.textBox5.Clear();
            this.textBox4.Clear();
            this.textBox3.Clear();
            this.textBox2.Clear();
            this.textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection = new OleDbConnection(strCnn) ;
            oleDbConnection.Open();
            OleDbCommand oleDbCommand = new OleDbCommand("INSERT INTO INFORMATIONS (NOM,PRENOM,ADRESSE,EMAIL,TEL_FIXE,TEL_MOBILE) VALUES('"+ this.textBox6.Text + "','"+ this.textBox5.Text + "','"+this.textBox4.Text + "','"+ this.textBox1.Text + "','"+this.textBox2.Text + "','"+this.textBox3.Text + "');" , oleDbConnection);
            oleDbCommand.ExecuteNonQuery();
            oleDbConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection = new OleDbConnection(strCnn);
            oleDbConnection.Open();
            OleDbCommand cmd;
            OleDbDataReader oleDbDataReader;
            if (textBox7.Text == "")
            {
                this.listBox1.Items.Clear();
                cmd = new OleDbCommand("SELECT ID,Nom,Prenom FROM INFORMATIONS;", oleDbConnection);
                oleDbDataReader = cmd.ExecuteReader();
                while (oleDbDataReader.Read())
                {

                    this.listBox1.Items.Add(oleDbDataReader["ID"] +"-"+ oleDbDataReader["Nom"] + " " + oleDbDataReader["Prenom"]);
                }
            }
            else
            {
                cmd =new OleDbCommand("SELECT ID,NOM,PRENOM FROM INFORMATIONS WHERE UCASE(NOM) = UCASE('"+this.textBox7.Text+ "') OR UCASE(PRENOM) = UCASE('" + this.textBox7.Text+"');",oleDbConnection);
                oleDbDataReader = cmd.ExecuteReader();
                while (oleDbDataReader.Read())
                {
                         this.comboBox1.Items.Add(oleDbDataReader["ID"]+"-"+oleDbDataReader["NOM"] + " " + oleDbDataReader["PRENOM"]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection = new OleDbConnection(strCnn);
            oleDbConnection.Open();
            OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM INFORMATIONS WHERE ID="+ this.listBox1.SelectedItem.ToString().Split('-')[0]+";",oleDbConnection);
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
            while (oleDbDataReader.Read())
            {
                this.textBox6.Text = (string)oleDbDataReader["Nom"];
                this.textBox5.Text = (string)oleDbDataReader["Prenom"];
                this.textBox4.Text = (string)oleDbDataReader["Adresse"];
                this.textBox1.Text = (string)oleDbDataReader["Email"];
                this.textBox2.Text = (string)oleDbDataReader["Tel_fixe"];
                this.textBox3.Text = (string)oleDbDataReader["Tel_mobile"];
                this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                if (!oleDbDataReader["Image"].ToString().Equals(""))
                    this.pictureBox1.Image = new Bitmap(oleDbDataReader["Image"].ToString());
                this.groupBox1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox6.Clear();
            this.textBox5.Clear();
            this.textBox4.Clear();
            this.textBox3.Clear();
            this.textBox2.Clear();
            this.textBox1.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection = new OleDbConnection(strCnn);
            oleDbConnection.Open();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            openFile.Multiselect = false;
            openFile.Filter = "Image files (*.jpg, *.png, *.jpeg) | *.jpg; *.png; *.jpeg";
            openFile.ShowDialog();
            String []s = this.listBox1.SelectedItem.ToString().Split('-');
            OleDbCommand oleDbCommand = new OleDbCommand("UPDATE INFORMATIONS SET Image='"+ openFile.FileName + "' WHERE ID="+s[0]+";", oleDbConnection);
            oleDbCommand.ExecuteNonQuery();
            oleDbConnection.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
