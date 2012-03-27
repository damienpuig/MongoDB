using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mongo
{
    public partial class ShowProperties : Form
    {
        public ShowProperties(Dictionary<string, object> dico)
        {

            InitializeComponent();

            dico.ToList<KeyValuePair<string, object>>().ForEach(
                (kv) => 
                    {
                        listBox1.Items.Add(string.Format("{0} : {1}", kv.Key, kv.Value));
                    });
        }
    }
}
