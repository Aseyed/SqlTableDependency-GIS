using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient.Base.Enums;
using Anacav.Esb.Connection;
using Anacav.Esb.Messaging;
using NLog;
using System.Timers;
using System.Threading;
using System.IO;
using Newtonsoft.Json.Linq;

class LoadData
{
    public static Dictionary<string, List<object>> Main_List = new Dictionary<string, List<object>>();
    public static string city_code = "";
    private string _connectionString;
    private static int List_counter = 0;
    public static System.Timers.Timer aTimer;
    private const int STEP = 2000;
    private static bool isUsing;
    private static Logger Logger = LogManager.GetCurrentClassLogger();
    public static Thread userThread1 = new Thread(new ThreadStart(Counter));
    public static Thread userThread2 = new Thread(new ThreadStart(bus_counter));
    private bool flag;
    public List<object> Domain_values;


    public LoadData(string conn = " ")
    {
        _connectionString = conn;
        try
        {
            userThread1.Start();
            userThread2.Start();
        }
        catch(Exception ee)
        {
            Logger.Info("timer thread");
        }
    }
    public void setConn(int code = 0, string conn = "")
    {
        _connectionString = conn;
        city_code = code.ToString();
    }
    public Dictionary<string, List<object>> getList()
    {
        return Main_List;
    }
    public static void list_clear()
    {
        Main_List.Clear();
    }
    public static List<object> getJson()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.AddSqlConverters();
        List<object> tmpL = new List<object>();
        foreach (var item in Main_List)
        {
            var val = item.Value;
            if (val.Count > 0)
            {
                for (int i = 0; i < val.Count; i += STEP)
                {
                    Dictionary<string, Dictionary<string, List<object>>> _List = new Dictionary<string, Dictionary<string, List<object>>>();

                    var end = STEP;
                    if (val.Count <= i + STEP)
                    {
                        end = val.Count - i;
                    }
                    var tmpDict = new Dictionary<string, List<object>>();
                    tmpDict.Add(item.Key, item.Value.GetRange(i, end));
                    //Console.WriteLine("tmpDict Length");
                    //foreach (var term in tmpDict.Keys)
                    //{

                    //    Console.WriteLine(tmpDict[term].Count);
                    //}

                    _List.Add(city_code, tmpDict);

                    var json_item = JsonConvert.SerializeObject(_List, settings);

                    tmpL.Add(json_item);
                    
                }
                Console.WriteLine("Item Length");
                Console.WriteLine(val.Count);

                Console.WriteLine("\n");

                //using (FileStream f = new FileStream(String.Format("./Data/Length.txt"), FileMode.Append, FileAccess.Write, FileShare.None))
                //{
                //    using (StreamWriter sw = new StreamWriter(f))
                //    {
                //        sw.Write(item.Key.ToString() + ": ");
                //        sw.WriteLine(val.Count);
                //    }
                //}
            }
            else
            {
                list_clear();
                break;
            }
        }
        return tmpL;
    }

    public void LoadCollectionData(string table_name, int ID = 0, ChangeType ctype = ChangeType.None)
    {
      //  bool flag;
        string queryString = "";

        if (ID == 0 || ctype == ChangeType.None)
        {
            flag = true;
            queryString = string.Format("SELECT * FROM [{0}]", table_name);

        }
        else
        {
            flag = false;
            queryString = string.Format("SELECT * FROM [{0}] WHERE OBJECTID = {1}", table_name, ID);
            Console.WriteLine(queryString);
        }

        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {

            try
            {

                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);

                sqlConnection.Open();

                using (SqlDataReader sql_data_reader = sqlCommand.ExecuteReader())
                {
                    var Table_List = new List<object>();                   
                    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    if (ctype == ChangeType.Delete)
                    {
                        var Changed_Data = new Dictionary<string, object>();
                        var Table_Attributes = new Dictionary<string, object>();

                        Table_Attributes.Add("Change_type", ctype.ToString());
                        Table_Attributes.Add("TimeStamp", unixTimestamp);
                        if (table_name != "Domain") { 
                            Changed_Data.Add("OBJECTID", ID);
                        }
                        else
                        {
                            Changed_Data.Add("Layer", Domain_values[0]);
                            Changed_Data.Add("Field", Domain_values[1]);
                            Changed_Data.Add("Code", Domain_values[2]);
                            Changed_Data.Add("Value", Domain_values[3]);

                        }
                        Table_Attributes.Add("Data", Changed_Data);
                        Table_List.Add(Table_Attributes);
                    }
                    else
                    {


                        while (sql_data_reader.Read())
                        {
                            var Changed_Data = new Dictionary<string, object>();
                            var Table_Attributes = new Dictionary<string, object>();

                            for (int i = 0; i < sql_data_reader.FieldCount; i++)
                            {
                                string key = sql_data_reader.GetName(i).ToString();

                                if (key != "created_user" &&
                                    key != "created_date" &&
                                    key != "last_edited_user" &&
                                    key != "last_edited_date")
                                {
                                    var value = sql_data_reader.GetValue(i);
                                    Changed_Data.Add(key, value);
                                }
                            }

                            Table_Attributes.Add("Change_type", ctype.ToString());
                            Table_Attributes.Add("TimeStamp", unixTimestamp);
                            if (Changed_Data.ContainsKey("SHAPE"))
                            {
                                Changed_Data["SHAPE"] = Changed_Data["SHAPE"].ToString();
                            }

                            Table_Attributes.Add("Data", Changed_Data);
                            Table_List.Add(Table_Attributes);
                        }
                    }

                    if (Main_List.ContainsKey(table_name))
                    {
                        if (!flag)
                        {
                            foreach(var item in Table_List)
                            {
                                Main_List[table_name].Add(item);
                            }
                            
                            List_counter++;
                        }
                        else
                        {
                            Main_List[table_name].Add(Table_List);
                        }
                        
                    }
                    else
                    {
                        if (!flag)
                        {
                            List_counter++;
                        }
                        Main_List.Add(table_name, Table_List);
                    }
                    // PushOnBus();
                    if (!flag)
                    {
                        if (List_counter > 500)
                        {
                            List_counter = 0;
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        PushOnBus();
                    }



                }
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger.Error("Command Execution" + e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }


    public static void PushOnBus()
    {
        isUsing = true;
        try
        {
            Logger.Debug("Pushting on Bus");
            // Configure Esb Connection
            Sender senderr = new Sender("172.30.10.131:9092", "epedc-gis", "epedc#G!$@001", "gis-data-event-group");
            senderr.Timeout = 5000;

            // Create Event
            EventMessage evnt = new EventMessage(new HeaderPart(Verbs.created, "Gis-Tables", "Gis"));

            evnt.PayLoad = new PayloadPart();

            string[] files_path = Directory.GetFiles(@".\Data\");
            foreach (var item in files_path)
            {
                using (StreamReader sr = new StreamReader(item))
                {

                    String lines = sr.ReadToEnd();
                    if (lines != "")
                    {
                        var l_json = JObject.Parse(lines);

                        lines = l_json.ToString();

                        evnt.PayLoad.Data = lines;
                        Boolean result = senderr.SendEvent(evnt, "Gis-Data");
                        Logger.Info("Bus Result: <Pushing File>" + result);
                        senderr.Dispose();
                        if (!result)
                        {
                            throw new ArgumentException("Cann't push on the Bus.");
                        }
                    }
                }
            }

            if (Main_List.Count != 0)
            {
                var list_string = getJson();
                foreach (var item in list_string)
                {
                    evnt.PayLoad.Data = item;
                    Boolean res = senderr.SendEvent(evnt, "Gis-Data");

                    Logger.Info("Bus Result: <Pushing List> " + res);
                    if (!res)
                    {
                        senderr.Dispose();
                        throw new ArgumentException("Cann't push on the Bus.");
                    }
                }
                senderr.Dispose();
                list_clear();

            }

            foreach(var item in files_path)
            {
                File.Delete(item);
            }
        }
        catch (Exception error)
        {
            Logger.Error(error);
            // Logger.Error("Bus connection error!!!");

            if (Main_List.Count != 0)
            {

                var list_string = getJson();
                var counter = 0;
                foreach (var item in list_string)
                {

                    var Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    using (FileStream fs = new FileStream(String.Format("./Data/Data_{0}+{1}.txt", counter, Timestamp.ToString()), FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(item.ToString());
                        fs.Write(info, 0, info.Length);

                    }
                    counter++;
                }
                list_clear();
            }
        }

        isUsing = false;
    }


    private static void bus_counter()
    {
        aTimer = new System.Timers.Timer();

        aTimer.Interval = 180000;
        aTimer.Elapsed += bus_counter_elapsed;
        Logger.Debug("bus_counter Time Elapsed");

        aTimer.Enabled = true;
    }

    private static void bus_counter_elapsed(object sender, ElapsedEventArgs e)
    {
        bool isEmpty = !Directory.EnumerateFiles(@".\Data\").Any();
        if (!isEmpty || Main_List.Count > 0 && !isUsing)
        {
            PushOnBus();
        }
    }

    private static void Counter()
    {
        aTimer = new System.Timers.Timer();

        aTimer.Interval = 3600000;
        aTimer.Elapsed += ImAlive;
        Logger.Debug("Time Elapsed");

        aTimer.Enabled = true;

    }

    private static void ImAlive(object sender, ElapsedEventArgs e)
    {
        // Configure Esb Connection
        Sender sndr = new Sender("172.30.10.130:9092", "epedc-gis", "epedc#G!$@001", "anacav-gis-data-event-group");
        sndr.Timeout = 5000;

        // Create Event
        EventMessage evnt = new EventMessage(new HeaderPart(Verbs.created, "Heart-Bit", "anacav"));

        evnt.PayLoad = new PayloadPart();
        var message = new Dictionary<string, string>();
        message.Add(city_code, "I'm Alive! ");
        evnt.PayLoad.Data = message.ToString();

        // Send event and wait 5 seconds for delivery report
        Boolean result = sndr.SendEvent(evnt, "Gis-Health-Check");
        Logger.Info("Bus Result: <Alive Message> " + result);
        sndr.Dispose();
    }
}

