using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChinesePoker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        internal void Update(List<Card> cards)
        {
            // JF - Update the images of cards hold in hand
            for (int i = 0; i < cards.Count; i++)
            {
                var x = GetType().GetProperty(String.Concat("card", i.ToString()));
            }
        }
    }
}