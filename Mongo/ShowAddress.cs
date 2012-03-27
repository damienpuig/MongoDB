using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mongo.Data;

namespace Mongo
{
    public partial class ShowAddress : Form
    {
        public ShowAddress(Address addresse)
        {
            InitializeComponent();
            label1.Text = addresse.street;
            label2.Text = addresse.city;
            label3.Text = addresse.zip.ToString();
            label4.Text = addresse.state;
        }
    }
}
