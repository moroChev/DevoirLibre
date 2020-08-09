using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApp7LetsGo
{
    class Form2:Form
    {
        private Button button;
        public Form2()
        {
            this.Text = "Windows Interface";
            this.Size = new Size(300,100);
            button = new Button();
            button.Text = "Button 1";
            button.Size = new Size(80,30);
            button.Location = new Point(110,20);
            button.Click +=new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        public void button_Click(object sender,EventArgs evt)
        {
            MessageBox.Show("box mes","box mes",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information);
        }
    }
}
