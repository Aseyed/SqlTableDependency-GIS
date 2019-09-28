using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;   
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using System.IO;

namespace GIS
{

    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        List<List<Dictionary<string, string>>> data_list = new List<List<Dictionary<string, string>>>();

        private string _connectionString;
        private SqlTableDependency<LVPL_BUSBAR> _LVPL_BUSBAR_dependency;

        public Form1()
        {
            this.InitializeComponent();
            this.Closing += Form1_Closing;


        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _LVPL_BUSBAR_dependency.Stop();

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            _connectionString = String.Format(
                Properties.Settings.Default.ConnectionString, Host.Text, Database.Text, Username.Text, Password.Text);
            LoadCollectionData(16982);

            _LVPL_BUSBAR_dependency = new SqlTableDependency<LVPL_BUSBAR>(_connectionString, "LVPL_BUSBAR");
            _LVPL_BUSBAR_dependency.OnChanged += _OnChanged;
            _LVPL_BUSBAR_dependency.Start();

        }
        private void _OnChanged(object sender, RecordChangedEventArgs<LVPL_BUSBAR> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                var changedEntity = e.Entity;

                Console.WriteLine("DML operation: " + e.ChangeType);


                switch (e.ChangeType)
                {
                    case ChangeType.Delete:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Delete"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                            Console.WriteLine("SHAPE: " + changedEntity.SHAPE);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);

                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, e.ChangeType);

                        }


                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                            Console.WriteLine("SHAPE: " + changedEntity.SHAPE);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);

                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                            Console.WriteLine("SHAPE: " + changedEntity.SHAPE);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);

                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, e.ChangeType);

                        }

                        break;
                }

            }
        }

        private void LoadCollectionData(int ID, ChangeType ctype = ChangeType.None)
        {
            //var conn = new SqlConnection(
            //  "Data Source =127.0.0.1; Initial Catalog = Test; User ID = sa; Password =3113; ; MultipleActiveResultSets = true;");
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    // WHERE OBJECTID = {0}", ID
                    sqlCommand.CommandText = string.Format("SELECT * FROM [LVPL_BUSBAR] WHERE OBJECTID = {0}", ID);
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var tmp_json = new Dictionary<string, string> { };
                        var t_dict = new Dictionary<string, string>();
                        var tmp_dict = new Dictionary<string, string>();

                        List<Dictionary<string, string>> foo = new List<Dictionary<string, string>>();

                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        tmp_dict.Add(ctype.ToString(), unixTimestamp.ToString());

                        foo.Add(tmp_dict);

                        while (sqlDataReader.Read())
                        {
                            t_dict.Clear();

                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                string key = sqlDataReader.GetName(i).ToString();
                                string value = sqlDataReader.GetValue(i).ToString();
                                //Console.Write(key);
                                //Console.Write(string.Format("[{0}]: ", i));
                                //Console.WriteLine(value);
                                t_dict.Add(key, value);

                            }
                            foo.Add(t_dict);
                            data_list.Add(foo);

                        }

                        var Result = JsonConvert.SerializeObject(data_list, Formatting.Indented);
                        System.IO.File.WriteAllText("./data.txt", Result);
                    }
                }
            }
        }


        private void Host_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
