using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Builders;
using Mongo.Data;

namespace Mongo
{
    public partial class NewUser : Form
    {
        public MongoCollection<User> UsersCollection { get; set; }
        public MongoCursor<User> Users { get; set; }

        public User selectedUser { get; set; }

        public MongoDatabase AUBINDB { get; set; }

        public NewUser()
        {
            string connectionString = "mongodb://localhost";
            var server = MongoServer.Create(connectionString);
            AUBINDB = server.GetDatabase("AUBINDB");

            InitializeComponent();

            Configure();
           refreshUsersList();
        }


        public void Configure()
        {

            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (UsersCollection == null)
            {
                UsersCollection = AUBINDB.GetCollection<User>(new MongoCollectionSettings<User>(AUBINDB, "users"));
            }

            if (!UsersCollection.Validate().Ok)
            {
                MessageBox.Show(string.Format("Erreur validate de la collection."));
            }
        }

        public void refreshUsersList()
        {
            Users = UsersCollection.FindAllAs<User>();

            listBox1.ValueMember = "_id";
            listBox1.DisplayMember = "name";

            listBox1.Items.Clear();

            foreach (User currentUser in Users)
	        {
                    listBox1.Items.Add(currentUser);
	        }
        }

        public void addUserToMongoDB(User newUser)
        {
            //BsonDocument user = new BsonDocument {
            //     { "name", newUser.name },
            //     { "address", new BsonDocument {
            //         { "street", "123 Main St." },
            //         { "city", "Centerville" },
            //         { "state", "PA" },
            //         { "zip", 12345}
            //     }}
            // };
            
            //AUBINDB.GetCollection("users").Insert(user);


            UsersCollection.Insert(newUser);
            refreshUsersList();
            
        }

        public void deleteUserToMongoDB(User oldUser)
        {
            var query = Query.EQ("name", oldUser.name);
            UsersCollection.Remove(query);

            refreshUsersList();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                addUserToMongoDB(new User() { name = string.Format("{0} {1}", textBox1.Text, textBox2.Text) });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteUserToMongoDB(selectedUser);
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                var selectedUserl = ((ListBox)sender).SelectedItem as User;
                selectedUser = selectedUserl;
                button2.Enabled = true;

                if (selectedUser.addresse != null)
                {
                    button4.Enabled = true;
                }
                else
                {
                    button4.Enabled = false;
                }

                if (selectedUser.Properties != null)
                {
                    button5.Enabled = true;
                }
                else
                {
                    button5.Enabled = false;
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(string.Format("Erreur : {0}", exe.Message), "Attention !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshUsersList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var showAddress = new ShowAddress(selectedUser.addresse);
            showAddress.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ShowProperties = new ShowProperties(selectedUser.Properties);
            ShowProperties.ShowDialog();
        }


    }
}
