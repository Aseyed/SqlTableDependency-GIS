
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using NLog;
using System.IO;
using System.Text;

namespace GIS
{

    public partial class Form1 : Form
    {
        private string _connectionString;
        static LoadData ld = new LoadData();


        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private SqlTableDependency<Domain> Domain_dependency;
        private SqlTableDependency<CONECTION> CONECTION_dependency;
        private SqlTableDependency<DESCRIPTION> DESCRIPTION_dependency;
        private SqlTableDependency<LVPG_LVTABLET> LVPG_LVTABLET_dependency;
        private SqlTableDependency<LVPL_BUSBAR> LVPL_BUSBAR_dependency;
        private SqlTableDependency<LVPL_OVHLINE> LVPL_OVHLINE_dependency;
        private SqlTableDependency<LVPL_SELFOVHLINE> LVPL_SELFOVHLINE_dependency;
        private SqlTableDependency<LVPL_SERVICECABLE> LVPL_SERVICECABLE_dependency;
        private SqlTableDependency<LVPL_UGLINE> LVPL_UGLINE_dependency;
        private SqlTableDependency<LVPT_AUTOKEY> LVPT_AUTOKEY_dependency;
        private SqlTableDependency<LVPT_AVENUELUSTR> LVPT_AVENUELUSTR_dependency;
        private SqlTableDependency<LVPT_AVENUELUSTRPOLE> LVPT_AVENUELUSTRPOLE_dependency;
        private SqlTableDependency<LVPT_CAPACITOR> LVPT_CAPACITOR_dependency;
        private SqlTableDependency<LVPT_CONTACTOR> LVPT_CONTACTOR_dependency;
        private SqlTableDependency<LVPT_EARTH> LVPT_EARTH_dependency;
        private SqlTableDependency<LVPT_FORKBOX> LVPT_FORKBOX_dependency;
        private SqlTableDependency<LVPT_FUSEKEY> LVPT_FUSEKEY_dependency;
        private SqlTableDependency<LVPT_HEADER> LVPT_HEADER_dependency;
        private SqlTableDependency<LVPT_INSULATOR> LVPT_INSULATOR_dependency;
        private SqlTableDependency<LVPT_JOINT> LVPT_JOINT_dependency;
        private SqlTableDependency<LVPT_JUMPER> LVPT_JUMPER_dependency;
        private SqlTableDependency<LVPT_PARTNERPLAQUE> LVPT_PARTNERPLAQUE_dependency;
        private SqlTableDependency<LVPT_POLE> LVPT_POLE_dependency;
        private SqlTableDependency<LVPT_SELFKEY> LVPT_SELFKEY_dependency;
        private SqlTableDependency<MVPG_HVSUBSTATION> MVPG_HVSUBSTATION_dependency;
        private SqlTableDependency<MVPG_LAND> MVPG_LAND_dependency;
        private SqlTableDependency<MVPG_PADSUBSTATION> MVPG_PADSUBSTATION_dependency;
        private SqlTableDependency<MVPG_PADSUBSTATIONQUBIC> MVPG_PADSUBSTATIONQUBIC_dependency;
        private SqlTableDependency<MVPL_BUSBAR_HVSUB> MVPL_BUSBAR_HVSUB_dependency;
        private SqlTableDependency<MVPL_BUSBAR_PADSUB> MVPL_BUSBAR_PADSUB_dependency;
        private SqlTableDependency<MVPL_OVHLINE> MVPL_OVHLINE_dependency;
        private SqlTableDependency<MVPL_SELFOVHLINE> MVPL_SELFOVHLINE_dependency;
        private SqlTableDependency<MVPL_UGLINE> MVPL_UGLINE_dependency;
        private SqlTableDependency<MVPT_AUTOBOOSTER> MVPT_AUTOBOOSTER_dependency;
        private SqlTableDependency<MVPT_AUTORECLOSER> MVPT_AUTORECLOSER_dependency;
        private SqlTableDependency<MVPT_CABLEHEAD> MVPT_CABLEHEAD_dependency;
        private SqlTableDependency<MVPT_CABLEJOINT> MVPT_CABLEJOINT_dependency;
        private SqlTableDependency<MVPT_CAPACITY> MVPT_CAPACITY_dependency;
        private SqlTableDependency<MVPT_COLONY> MVPT_COLONY_dependency;
        private SqlTableDependency<MVPT_DSWITCH> MVPT_DSWITCH_dependency;
        private SqlTableDependency<MVPT_DSWITCH_PADSUB> MVPT_DSWITCH_PADSUB_dependency;
        private SqlTableDependency<MVPT_EARTH> MVPT_EARTH_dependency;
        private SqlTableDependency<MVPT_FAULTDETECTOR> MVPT_FAULTDETECTOR_dependency;
        private SqlTableDependency<MVPT_FRONTAGE> MVPT_FRONTAGE_dependency;
        private SqlTableDependency<MVPT_FUSECUTOUT> MVPT_FUSECUTOUT_dependency;
        private SqlTableDependency<MVPT_HEADER> MVPT_HEADER_dependency;
        private SqlTableDependency<MVPT_HVSUBSTATIONTRANS> MVPT_HVSUBSTATIONTRANS_dependency;
        private SqlTableDependency<MVPT_JUMPER> MVPT_JUMPER_dependency;
        private SqlTableDependency<MVPT_MOF> MVPT_MOF_dependency;
        private SqlTableDependency<MVPT_OUTGOING> MVPT_OUTGOING_dependency;
        private SqlTableDependency<MVPT_PADSUBSTATIONTRANS> MVPT_PADSUBSTATIONTRANS_dependency;
        private SqlTableDependency<MVPT_PLANTSCATTER> MVPT_PLANTSCATTER_dependency;
        private SqlTableDependency<MVPT_POLE> MVPT_POLE_dependency;
        private SqlTableDependency<MVPT_POLESUBSTATION> MVPT_POLESUBSTATION_dependency;
        private SqlTableDependency<MVPT_SECTIONALIZER> MVPT_SECTIONALIZER_dependency;
        private SqlTableDependency<MVPT_TOMB> MVPT_TOMB_dependency;
        private SqlTableDependency<NEWCUSTOMERREQUESTS> NEWCUSTOMERREQUESTS_dependency;

        public Form1()
        {
            this.InitializeComponent();
            this.Closing += Form1_Closing;
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //ld.userThread1.Join();

            Logger.Info("Form Closing!");
            Logger.Info("Writing Data in File!");

            var list_string = LoadData.getJson();
            foreach (var item in list_string)
            {
                using (FileStream fs = new FileStream(String.Format("./Data/Data-Closed {0}.txt", list_string.IndexOf(item)), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(item.ToString());
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        private void GetData_Click(object sender, EventArgs e)
        {
            Logger.Info("Getting All Data");

            Stopwatch stopw = Stopwatch.StartNew();

            GetAllData();
            //ld.RecieveFromBus();

            stopw.Stop();
            Logger.Debug("All Data Collection--> Time: {0}m", stopw.Elapsed.TotalMinutes);

        }

        private void connection_dispose()
        {
            try
            {
                Domain_dependency.Stop();
                CONECTION_dependency.Stop();
                DESCRIPTION_dependency.Stop();
                LVPG_LVTABLET_dependency.Stop();
                LVPL_BUSBAR_dependency.Stop();
                LVPL_OVHLINE_dependency.Stop();
                LVPL_SELFOVHLINE_dependency.Stop();
                LVPL_SERVICECABLE_dependency.Stop();
                LVPL_UGLINE_dependency.Stop();
                LVPT_AUTOKEY_dependency.Stop();
                LVPT_AVENUELUSTR_dependency.Stop();
                LVPT_AVENUELUSTRPOLE_dependency.Stop();
                LVPT_CAPACITOR_dependency.Stop();
                LVPT_CONTACTOR_dependency.Stop();
                LVPT_EARTH_dependency.Stop();
                LVPT_FORKBOX_dependency.Stop();
                LVPT_FUSEKEY_dependency.Stop();
                LVPT_HEADER_dependency.Stop();
                LVPT_INSULATOR_dependency.Stop();
                LVPT_JOINT_dependency.Stop();
                LVPT_JUMPER_dependency.Stop();
                LVPT_PARTNERPLAQUE_dependency.Stop();
                LVPT_POLE_dependency.Stop();
                LVPT_SELFKEY_dependency.Stop();
                MVPG_HVSUBSTATION_dependency.Stop();
                MVPG_LAND_dependency.Stop();
                MVPG_PADSUBSTATION_dependency.Stop();
                MVPG_PADSUBSTATIONQUBIC_dependency.Stop();
                MVPL_BUSBAR_HVSUB_dependency.Stop();
                MVPL_BUSBAR_PADSUB_dependency.Stop();
                MVPL_OVHLINE_dependency.Stop();
                MVPL_SELFOVHLINE_dependency.Stop();
                MVPL_UGLINE_dependency.Stop();
                MVPT_AUTOBOOSTER_dependency.Stop();
                MVPT_AUTORECLOSER_dependency.Stop();
                MVPT_CABLEHEAD_dependency.Stop();
                MVPT_CABLEJOINT_dependency.Stop();
                MVPT_CAPACITY_dependency.Stop();
                MVPT_COLONY_dependency.Stop();
                MVPT_DSWITCH_dependency.Stop();
                MVPT_DSWITCH_PADSUB_dependency.Stop();
                MVPT_EARTH_dependency.Stop();
                MVPT_FAULTDETECTOR_dependency.Stop();
                MVPT_FRONTAGE_dependency.Stop();
                MVPT_FUSECUTOUT_dependency.Stop();
                MVPT_HEADER_dependency.Stop();
                MVPT_HVSUBSTATIONTRANS_dependency.Stop();
                MVPT_JUMPER_dependency.Stop();
                MVPT_MOF_dependency.Stop();
                MVPT_OUTGOING_dependency.Stop();
                MVPT_PADSUBSTATIONTRANS_dependency.Stop();
                MVPT_PLANTSCATTER_dependency.Stop();
                MVPT_POLE_dependency.Stop();
                MVPT_POLESUBSTATION_dependency.Stop();
                MVPT_SECTIONALIZER_dependency.Stop();
                MVPT_TOMB_dependency.Stop();
                NEWCUSTOMERREQUESTS_dependency.Stop();
            }
            catch (Exception dispose_exception)
            {
                Logger.Info("Disconnect Error" + dispose_exception);
            }
        }

        private void Make_Message(string table_name)
        {
            string message = "Can not Connect to Broker on Table " + "'" + table_name + "'"
                            + "Please Try again!";
            string title = "Connecion Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            _connectionString = String.Format(
                Properties.Settings.Default.ConnectionString, Host.Text, Database.Text, Username.Text, Password.Text);


            ld.setConn(int.Parse(CityCode.Text), _connectionString);

            Stopwatch sw = Stopwatch.StartNew();
            Logger.Info("Start Dependencies Connections");

            try
            {
                Domain_dependency = new SqlTableDependency<Domain>(_connectionString, "Domain");
                Domain_dependency.OnChanged += Domain_OnChanged;
                Domain_dependency.OnChanged += Domain_OnError;
                Domain_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("Domain");
                Logger.Error("Table CONECTION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                CONECTION_dependency = new SqlTableDependency<CONECTION>(_connectionString, "CONECTION");
                CONECTION_dependency.OnChanged += CONECTION_OnChanged;
                CONECTION_dependency.OnChanged += CONECTION_OnError;
                CONECTION_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("CONECTION");
                Logger.Error("Table CONECTION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                DESCRIPTION_dependency = new SqlTableDependency<DESCRIPTION>(_connectionString, "DESCRIPTION");
                DESCRIPTION_dependency.OnChanged += DESCRIPTION_OnChanged;
                DESCRIPTION_dependency.OnChanged += DESCRIPTION_OnError;
                DESCRIPTION_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("DESCRIPTION");
                Logger.Error("Table DESCRIPTION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPG_LVTABLET_dependency = new SqlTableDependency<LVPG_LVTABLET>(_connectionString, "LVPG_LVTABLET");
                LVPG_LVTABLET_dependency.OnChanged += LVPG_LVTABLET_OnChanged;
                LVPG_LVTABLET_dependency.OnChanged += LVPG_LVTABLET_OnError;
                LVPG_LVTABLET_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPG_LVTABLET");
                Logger.Error("Table LVPG_LVTABLET --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPL_BUSBAR_dependency = new SqlTableDependency<LVPL_BUSBAR>(_connectionString, "LVPL_BUSBAR");
                LVPL_BUSBAR_dependency.OnChanged += LVPL_BUSBAR_OnChanged;
                LVPL_BUSBAR_dependency.OnChanged += LVPL_BUSBAR_OnError;
                LVPL_BUSBAR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPL_BUSBAR");
                Logger.Error("Table LVPL_BUSBAR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPL_OVHLINE_dependency = new SqlTableDependency<LVPL_OVHLINE>(_connectionString, "LVPL_OVHLINE");
                LVPL_OVHLINE_dependency.OnChanged += LVPL_OVHLINE_OnChanged;
                LVPL_OVHLINE_dependency.OnChanged += LVPL_OVHLINE_OnError;
                LVPL_OVHLINE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPL_OVHLINE");
                Logger.Error("Table LVPL_OVHLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPL_SELFOVHLINE_dependency = new SqlTableDependency<LVPL_SELFOVHLINE>(_connectionString, "LVPL_SELFOVHLINE");
                LVPL_SELFOVHLINE_dependency.OnChanged += LVPL_SELFOVHLINE_OnChanged;
                LVPL_SELFOVHLINE_dependency.OnChanged += LVPL_SELFOVHLINE_OnError;
                LVPL_SELFOVHLINE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPL_SELFOVHLINE");
                Logger.Error("Table LVPL_SELFOVHLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPL_SERVICECABLE_dependency = new SqlTableDependency<LVPL_SERVICECABLE>(_connectionString, "LVPL_SERVICECABLE");
                LVPL_SERVICECABLE_dependency.OnChanged += LVPL_SERVICECABLE_OnChanged;
                LVPL_SERVICECABLE_dependency.OnChanged += LVPL_SERVICECABLE_OnError;
                LVPL_SERVICECABLE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPL_SERVICECABLE");
                Logger.Error("Table LVPL_SERVICECABLE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPL_UGLINE_dependency = new SqlTableDependency<LVPL_UGLINE>(_connectionString, "LVPL_UGLINE");
                LVPL_UGLINE_dependency.OnChanged += LVPL_UGLINE_OnChanged;
                LVPL_UGLINE_dependency.OnChanged += LVPL_UGLINE_OnError;
                LVPL_UGLINE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPL_UGLINE");
                Logger.Error("Table LVPL_UGLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_AUTOKEY_dependency = new SqlTableDependency<LVPT_AUTOKEY>(_connectionString, "LVPT_AUTOKEY");
                LVPT_AUTOKEY_dependency.OnChanged += LVPT_AUTOKEY_OnChanged;
                LVPT_AUTOKEY_dependency.OnChanged += LVPT_AUTOKEY_OnError;
                LVPT_AUTOKEY_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_AUTOKEY");
                Logger.Error("Table LVPT_AUTOKEY --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_AVENUELUSTR_dependency = new SqlTableDependency<LVPT_AVENUELUSTR>(_connectionString, "LVPT_AVENUELUSTR");
                LVPT_AVENUELUSTR_dependency.OnChanged += LVPT_AVENUELUSTR_OnChanged;
                LVPT_AVENUELUSTR_dependency.OnChanged += LVPT_AVENUELUSTR_OnError;
                LVPT_AVENUELUSTR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_AVENUELUSTR");
                Logger.Error("Table LVPT_AVENUELUSTR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_AVENUELUSTRPOLE_dependency = new SqlTableDependency<LVPT_AVENUELUSTRPOLE>(_connectionString, "LVPT_AVENUELUSTRPOLE");
                LVPT_AVENUELUSTRPOLE_dependency.OnChanged += LVPT_AVENUELUSTRPOLE_OnChanged;
                LVPT_AVENUELUSTRPOLE_dependency.OnChanged += LVPT_AVENUELUSTRPOLE_OnError;
                LVPT_AVENUELUSTRPOLE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_AVENUELUSTRPOLE");
                Logger.Error("Table LVPT_AVENUELUSTRPOLE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_CAPACITOR_dependency = new SqlTableDependency<LVPT_CAPACITOR>(_connectionString, "LVPT_CAPACITOR");
                LVPT_CAPACITOR_dependency.OnChanged += LVPT_CAPACITOR_OnChanged;
                LVPT_CAPACITOR_dependency.OnChanged += LVPT_CAPACITOR_OnError;
                LVPT_CAPACITOR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_CAPACITOR");
                Logger.Error("Table LVPT_CAPACITOR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_CONTACTOR_dependency = new SqlTableDependency<LVPT_CONTACTOR>(_connectionString, "LVPT_CONTACTOR");
                LVPT_CONTACTOR_dependency.OnChanged += LVPT_CONTACTOR_OnChanged;
                LVPT_CONTACTOR_dependency.OnChanged += LVPT_CONTACTOR_OnError;
                LVPT_CONTACTOR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_CONTACTOR");
                Logger.Error("Table LVPT_CONTACTOR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_EARTH_dependency = new SqlTableDependency<LVPT_EARTH>(_connectionString, "LVPT_EARTH");
                LVPT_EARTH_dependency.OnChanged += LVPT_EARTH_OnChanged;
                LVPT_EARTH_dependency.OnChanged += LVPT_EARTH_OnError;
                LVPT_EARTH_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_EARTH");
                Logger.Error("Table LVPT_EARTH --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_FORKBOX_dependency = new SqlTableDependency<LVPT_FORKBOX>(_connectionString, "LVPT_FORKBOX");
                LVPT_FORKBOX_dependency.OnChanged += LVPT_FORKBOX_OnChanged;
                LVPT_FORKBOX_dependency.OnChanged += LVPT_FORKBOX_OnError;
                LVPT_FORKBOX_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_FORKBOX");
                Logger.Error("Table LVPT_FORKBOX --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_FUSEKEY_dependency = new SqlTableDependency<LVPT_FUSEKEY>(_connectionString, "LVPT_FUSEKEY");
                LVPT_FUSEKEY_dependency.OnChanged += LVPT_FUSEKEY_OnChanged;
                LVPT_FUSEKEY_dependency.OnChanged += LVPT_FUSEKEY_OnError;
                LVPT_FUSEKEY_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_FUSEKEY");
                Logger.Error("Table LVPT_FUSEKEY --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_HEADER_dependency = new SqlTableDependency<LVPT_HEADER>(_connectionString, "LVPT_HEADER");
                LVPT_HEADER_dependency.OnChanged += LVPT_HEADER_OnChanged;
                LVPT_HEADER_dependency.OnChanged += LVPT_HEADER_OnError;
                LVPT_HEADER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_HEADER");
                Logger.Error("Table LVPT_HEADER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_INSULATOR_dependency = new SqlTableDependency<LVPT_INSULATOR>(_connectionString, "LVPT_INSULATOR");
                LVPT_INSULATOR_dependency.OnChanged += LVPT_INSULATOR_OnChanged;
                LVPT_INSULATOR_dependency.OnChanged += LVPT_INSULATOR_OnError;
                LVPT_INSULATOR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_INSULATOR");
                Logger.Error("Table LVPT_INSULATOR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_JOINT_dependency = new SqlTableDependency<LVPT_JOINT>(_connectionString, "LVPT_JOINT");
                LVPT_JOINT_dependency.OnChanged += LVPT_JOINT_OnChanged;
                LVPT_JOINT_dependency.OnChanged += LVPT_JOINT_OnError;
                LVPT_JOINT_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_JOINT");
                Logger.Error("Table LVPT_JOINT --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_JUMPER_dependency = new SqlTableDependency<LVPT_JUMPER>(_connectionString, "LVPT_JUMPER");
                LVPT_JUMPER_dependency.OnChanged += LVPT_JUMPER_OnChanged;
                LVPT_JUMPER_dependency.OnChanged += LVPT_JUMPER_OnError;
                LVPT_JUMPER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_JUMPER");
                Logger.Error("Table LVPT_JUMPER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_PARTNERPLAQUE_dependency = new SqlTableDependency<LVPT_PARTNERPLAQUE>(_connectionString, "LVPT_PARTNERPLAQUE");
                LVPT_PARTNERPLAQUE_dependency.OnChanged += LVPT_PARTNERPLAQUE_OnChanged;
                LVPT_PARTNERPLAQUE_dependency.OnChanged += LVPT_PARTNERPLAQUE_OnError;
                LVPT_PARTNERPLAQUE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_PARTNERPLAQUE");
                Logger.Error("Table LVPT_PARTNERPLAQUE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_POLE_dependency = new SqlTableDependency<LVPT_POLE>(_connectionString, "LVPT_POLE");
                LVPT_POLE_dependency.OnChanged += LVPT_POLE_OnChanged;
                LVPT_POLE_dependency.OnChanged += LVPT_POLE_OnError;
                LVPT_POLE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_POLE");
                Logger.Error("Table LVPT_POLE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                LVPT_SELFKEY_dependency = new SqlTableDependency<LVPT_SELFKEY>(_connectionString, "LVPT_SELFKEY");
                LVPT_SELFKEY_dependency.OnChanged += LVPT_SELFKEY_OnChanged;
                LVPT_SELFKEY_dependency.OnChanged += LVPT_SELFKEY_OnError;
                LVPT_SELFKEY_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("LVPT_SELFKEY");
                Logger.Error("Table LVPT_SELFKEY --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPG_HVSUBSTATION_dependency = new SqlTableDependency<MVPG_HVSUBSTATION>(_connectionString, "MVPG_HVSUBSTATION");
                MVPG_HVSUBSTATION_dependency.OnChanged += MVPG_HVSUBSTATION_OnChanged;
                MVPG_HVSUBSTATION_dependency.OnChanged += MVPG_HVSUBSTATION_OnError;
                MVPG_HVSUBSTATION_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPG_HVSUBSTATION");
                Logger.Error("Table MVPG_HVSUBSTATION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPG_LAND_dependency = new SqlTableDependency<MVPG_LAND>(_connectionString, "MVPG_LAND");
                MVPG_LAND_dependency.OnChanged += MVPG_LAND_OnChanged;
                MVPG_LAND_dependency.OnChanged += MVPG_LAND_OnError;
                MVPG_LAND_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPG_LAND");
                Logger.Error("Table MVPG_LAND --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPG_PADSUBSTATION_dependency = new SqlTableDependency<MVPG_PADSUBSTATION>(_connectionString, "MVPG_PADSUBSTATION");
                MVPG_PADSUBSTATION_dependency.OnChanged += MVPG_PADSUBSTATION_OnChanged;
                MVPG_PADSUBSTATION_dependency.OnChanged += MVPG_PADSUBSTATION_OnError;
                MVPG_PADSUBSTATION_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPG_PADSUBSTATION");
                Logger.Error("Table MVPG_PADSUBSTATION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPG_PADSUBSTATIONQUBIC_dependency = new SqlTableDependency<MVPG_PADSUBSTATIONQUBIC>(_connectionString, "MVPG_PADSUBSTATIONQUBIC");
                MVPG_PADSUBSTATIONQUBIC_dependency.OnChanged += MVPG_PADSUBSTATIONQUBIC_OnChanged;
                MVPG_PADSUBSTATIONQUBIC_dependency.OnChanged += MVPG_PADSUBSTATIONQUBIC_OnError;
                MVPG_PADSUBSTATIONQUBIC_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPG_PADSUBSTATIONQUBIC");
                Logger.Error("Table MVPG_PADSUBSTATIONQUBIC --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPL_BUSBAR_HVSUB_dependency = new SqlTableDependency<MVPL_BUSBAR_HVSUB>(_connectionString, "MVPL_BUSBAR_HVSUB");
                MVPL_BUSBAR_HVSUB_dependency.OnChanged += MVPL_BUSBAR_HVSUB_OnChanged;
                MVPL_BUSBAR_HVSUB_dependency.OnChanged += MVPL_BUSBAR_HVSUB_OnError;
                MVPL_BUSBAR_HVSUB_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPL_BUSBAR_HVSUB");
                Logger.Error("Table MVPL_BUSBAR_HVSUB --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPL_BUSBAR_PADSUB_dependency = new SqlTableDependency<MVPL_BUSBAR_PADSUB>(_connectionString, "MVPL_BUSBAR_PADSUB");
                MVPL_BUSBAR_PADSUB_dependency.OnChanged += MVPL_BUSBAR_PADSUB_OnChanged;
                MVPL_BUSBAR_PADSUB_dependency.OnChanged += MVPL_BUSBAR_PADSUB_OnError;

                MVPL_BUSBAR_PADSUB_dependency.Start();

            }
            catch (Exception exc)
            {
                Make_Message("MVPL_BUSBAR_PADSUB");
                Logger.Error("Table MVPL_BUSBAR_PADSUB --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPL_OVHLINE_dependency = new SqlTableDependency<MVPL_OVHLINE>(_connectionString, "MVPL_OVHLINE");
                MVPL_OVHLINE_dependency.OnChanged += MVPL_OVHLINE_OnChanged;
                MVPL_OVHLINE_dependency.OnChanged += MVPL_OVHLINE_OnError;

                MVPL_OVHLINE_dependency.Start();

            }
            catch (Exception exc)
            {
                Make_Message("MVPL_OVHLINE");
                Logger.Error("Table MVPL_OVHLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPL_SELFOVHLINE_dependency = new SqlTableDependency<MVPL_SELFOVHLINE>(_connectionString, "MVPL_SELFOVHLINE");
                MVPL_SELFOVHLINE_dependency.OnChanged += MVPL_SELFOVHLINE_OnChanged;
                MVPL_SELFOVHLINE_dependency.OnChanged += MVPL_SELFOVHLINE_OnError;
                MVPL_SELFOVHLINE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPL_SELFOVHLINE");
                Logger.Error("Table MVPL_SELFOVHLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPL_UGLINE_dependency = new SqlTableDependency<MVPL_UGLINE>(_connectionString, "MVPL_UGLINE");
                MVPL_UGLINE_dependency.OnChanged += MVPL_UGLINE_OnChanged;
                MVPL_UGLINE_dependency.OnChanged += MVPL_UGLINE_OnError;

                MVPL_UGLINE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPL_UGLINE");
                Logger.Error("Table MVPL_UGLINE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_AUTOBOOSTER_dependency = new SqlTableDependency<MVPT_AUTOBOOSTER>(_connectionString, "MVPT_AUTOBOOSTER");
                MVPT_AUTOBOOSTER_dependency.OnChanged += MVPT_AUTOBOOSTER_OnChanged;
                MVPT_AUTOBOOSTER_dependency.OnChanged += MVPT_AUTOBOOSTER_OnError;
                MVPT_AUTOBOOSTER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_AUTOBOOSTER");
                Logger.Error("Table MVPT_AUTOBOOSTER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_AUTORECLOSER_dependency = new SqlTableDependency<MVPT_AUTORECLOSER>(_connectionString, "MVPT_AUTORECLOSER");
                MVPT_AUTORECLOSER_dependency.OnChanged += MVPT_AUTORECLOSER_OnChanged;
                MVPT_AUTORECLOSER_dependency.OnChanged += MVPT_AUTORECLOSER_OnError;
                MVPT_AUTORECLOSER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_AUTORECLOSER");
                Logger.Error("Table MVPT_AUTORECLOSER --> " + exc);
                return;
            }
            try
            {
                MVPT_CABLEHEAD_dependency = new SqlTableDependency<MVPT_CABLEHEAD>(_connectionString, "MVPT_CABLEHEAD");
                MVPT_CABLEHEAD_dependency.OnChanged += MVPT_CABLEHEAD_OnChanged;
                MVPT_CABLEHEAD_dependency.OnChanged += MVPT_CABLEHEAD_OnError;

                MVPT_CABLEHEAD_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_CABLEHEAD");
                Logger.Error("Table MVPT_CABLEHEAD --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_CABLEJOINT_dependency = new SqlTableDependency<MVPT_CABLEJOINT>(_connectionString, "MVPT_CABLEJOINT");
                MVPT_CABLEJOINT_dependency.OnChanged += MVPT_CABLEJOINT_OnChanged;
                MVPT_CABLEJOINT_dependency.OnChanged += MVPT_CABLEJOINT_OnError;

                MVPT_CABLEJOINT_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_CABLEJOINT");
                Logger.Error("Table MVPT_CABLEJOINT --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_CAPACITY_dependency = new SqlTableDependency<MVPT_CAPACITY>(_connectionString, "MVPT_CAPACITY");
                MVPT_CAPACITY_dependency.OnChanged += MVPT_CAPACITY_OnChanged;
                MVPT_CAPACITY_dependency.OnChanged += MVPT_CAPACITY_OnError;

                MVPT_CAPACITY_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_CAPACITY");
                Logger.Error("Table MVPT_CAPACITY --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_COLONY_dependency = new SqlTableDependency<MVPT_COLONY>(_connectionString, "MVPT_COLONY");
                MVPT_COLONY_dependency.OnChanged += MVPT_COLONY_OnChanged;
                MVPT_COLONY_dependency.OnChanged += MVPT_COLONY_OnError;

                MVPT_COLONY_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_COLONY");
                Logger.Error("Table MVPT_COLONY --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_DSWITCH_dependency = new SqlTableDependency<MVPT_DSWITCH>(_connectionString, "MVPT_DSWITCH");
                MVPT_DSWITCH_dependency.OnChanged += MVPT_DSWITCH_OnChanged;
                MVPT_DSWITCH_dependency.OnChanged += MVPT_DSWITCH_OnError;

                MVPT_DSWITCH_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_DSWITCH");
                Logger.Error("Table MVPT_DSWITCH --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_DSWITCH_PADSUB_dependency = new SqlTableDependency<MVPT_DSWITCH_PADSUB>(_connectionString, "MVPT_DSWITCH_PADSUB");
                MVPT_DSWITCH_PADSUB_dependency.OnChanged += MVPT_DSWITCH_PADSUB_OnChanged;
                MVPT_DSWITCH_PADSUB_dependency.OnChanged += MVPT_DSWITCH_PADSUB_OnError;

                MVPT_DSWITCH_PADSUB_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_DSWITCH_PADSUB");
                Logger.Error("Table MVPT_DSWITCH_PADSUB --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_EARTH_dependency = new SqlTableDependency<MVPT_EARTH>(_connectionString, "MVPT_EARTH");
                MVPT_EARTH_dependency.OnChanged += MVPT_EARTH_OnChanged;
                MVPT_EARTH_dependency.OnChanged += MVPT_EARTH_OnError;
                MVPT_EARTH_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_EARTH");
                Logger.Error("Table MVPT_EARTH --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_FAULTDETECTOR_dependency = new SqlTableDependency<MVPT_FAULTDETECTOR>(_connectionString, "MVPT_FAULTDETECTOR");
                MVPT_FAULTDETECTOR_dependency.OnChanged += MVPT_FAULTDETECTOR_OnChanged;
                MVPT_FAULTDETECTOR_dependency.OnChanged += MVPT_FAULTDETECTOR_OnError;
                MVPT_FAULTDETECTOR_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_FAULTDETECTOR");
                Logger.Error("Table MVPT_FAULTDETECTOR --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_FRONTAGE_dependency = new SqlTableDependency<MVPT_FRONTAGE>(_connectionString, "MVPT_FRONTAGE");
                MVPT_FRONTAGE_dependency.OnChanged += MVPT_FRONTAGE_OnChanged;
                MVPT_FRONTAGE_dependency.OnChanged += MVPT_FRONTAGE_OnError;

                MVPT_FRONTAGE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_FRONTAGE");
                Logger.Error("Table MVPT_FRONTAGE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_FUSECUTOUT_dependency = new SqlTableDependency<MVPT_FUSECUTOUT>(_connectionString, "MVPT_FUSECUTOUT");
                MVPT_FUSECUTOUT_dependency.OnChanged += MVPT_FUSECUTOUT_OnChanged;
                MVPT_FUSECUTOUT_dependency.OnChanged += MVPT_FUSECUTOUT_OnError;
                MVPT_FUSECUTOUT_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_FUSECUTOUT");
                Logger.Error("Table MVPT_FUSECUTOUT --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_HEADER_dependency = new SqlTableDependency<MVPT_HEADER>(_connectionString, "MVPT_HEADER");
                MVPT_HEADER_dependency.OnChanged += MVPT_HEADER_OnChanged;
                MVPT_HEADER_dependency.OnChanged += MVPT_HEADER_OnError;
                MVPT_HEADER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_HEADER");
                Logger.Error("Table MVPT_HEADER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_HVSUBSTATIONTRANS_dependency = new SqlTableDependency<MVPT_HVSUBSTATIONTRANS>(_connectionString, "MVPT_HVSUBSTATIONTRANS");
                MVPT_HVSUBSTATIONTRANS_dependency.OnChanged += MVPT_HVSUBSTATIONTRAN_OnChanged;
                MVPT_HVSUBSTATIONTRANS_dependency.OnChanged += MVPT_HVSUBSTATIONTRAN_OnError;
                MVPT_HVSUBSTATIONTRANS_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_HVSUBSTATIONTRANS");
                Logger.Error("Table MVPT_HVSUBSTATIONTRANS --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_JUMPER_dependency = new SqlTableDependency<MVPT_JUMPER>(_connectionString, "MVPT_JUMPER");
                MVPT_JUMPER_dependency.OnChanged += MVPT_JUMPER_OnChanged;
                MVPT_JUMPER_dependency.OnChanged += MVPT_JUMPER_OnError;
                MVPT_JUMPER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_JUMPER");
                Logger.Error("Table MVPT_JUMPER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_MOF_dependency = new SqlTableDependency<MVPT_MOF>(_connectionString, "MVPT_MOF");
                MVPT_MOF_dependency.OnChanged += MVPT_MOF_OnChanged;
                MVPT_MOF_dependency.OnChanged += MVPT_MOF_OnError;
                MVPT_MOF_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_MOF");
                Logger.Error("Table MVPT_MOF --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_OUTGOING_dependency = new SqlTableDependency<MVPT_OUTGOING>(_connectionString, "MVPT_OUTGOING");
                MVPT_OUTGOING_dependency.OnChanged += MVPT_OUTGOING_OnChanged;
                MVPT_OUTGOING_dependency.OnChanged += MVPT_OUTGOING_OnError;
                MVPT_OUTGOING_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_OUTGOING");
                Logger.Error("Table MVPT_OUTGOING --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_PADSUBSTATIONTRANS_dependency = new SqlTableDependency<MVPT_PADSUBSTATIONTRANS>(_connectionString, "MVPT_PADSUBSTATIONTRANS");
                MVPT_PADSUBSTATIONTRANS_dependency.OnChanged += MVPT_PADSUBSTATIONTRAN_OnChanged;            
                MVPT_PADSUBSTATIONTRANS_dependency.OnChanged += MVPT_PADSUBSTATIONTRAN_OnError;
                MVPT_PADSUBSTATIONTRANS_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_PADSUBSTATIONTRANS");
                Logger.Error("Table MVPT_PADSUBSTATIONTRANS --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_PLANTSCATTER_dependency = new SqlTableDependency<MVPT_PLANTSCATTER>(_connectionString, "MVPT_PLANTSCATTER");
                MVPT_PLANTSCATTER_dependency.OnChanged += MVPT_PLANTSCATTER_OnChanged;
                MVPT_PLANTSCATTER_dependency.OnChanged += MVPT_PLANTSCATTER_OnError;
                MVPT_PLANTSCATTER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_PLANTSCATTER");
                Logger.Error("Table MVPT_PLANTSCATTER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_POLE_dependency = new SqlTableDependency<MVPT_POLE>(_connectionString, "MVPT_POLE");
                MVPT_POLE_dependency.OnChanged += MVPT_POLE_OnChanged;
                MVPT_POLE_dependency.OnChanged += MVPT_POLE_OnError;

                MVPT_POLE_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_POLE");
                Logger.Error("Table MVPT_POLE --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_POLESUBSTATION_dependency = new SqlTableDependency<MVPT_POLESUBSTATION>(_connectionString, "MVPT_POLESUBSTATION");
                MVPT_POLESUBSTATION_dependency.OnChanged += MVPT_POLESUBSTATION_OnChanged;
                MVPT_POLESUBSTATION_dependency.OnChanged += MVPT_POLESUBSTATION_OnError;
                MVPT_POLESUBSTATION_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_POLESUBSTATION");
                Logger.Error("Table MVPT_POLESUBSTATION --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_SECTIONALIZER_dependency = new SqlTableDependency<MVPT_SECTIONALIZER>(_connectionString, "MVPT_SECTIONALIZER");
                MVPT_SECTIONALIZER_dependency.OnChanged += MVPT_SECTIONALIZER_OnChanged;
                MVPT_SECTIONALIZER_dependency.OnChanged += MVPT_SECTIONALIZER_OnError;
                MVPT_SECTIONALIZER_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_SECTIONALIZER");
                Logger.Error("Table MVPT_SECTIONALIZER --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                MVPT_TOMB_dependency = new SqlTableDependency<MVPT_TOMB>(_connectionString, "MVPT_TOMB");
                MVPT_TOMB_dependency.OnChanged += MVPT_TOMB_OnChanged;
                MVPT_TOMB_dependency.OnChanged += MVPT_TOMB_OnError;
                MVPT_TOMB_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("MVPT_TOMB");
                Logger.Error("Table MVPT_TOMB --> " + exc);
                connection_dispose();
                return;
            }
            try
            {
                NEWCUSTOMERREQUESTS_dependency = new SqlTableDependency<NEWCUSTOMERREQUESTS>(_connectionString, "NEWCUSTOMERREQUESTS");
                NEWCUSTOMERREQUESTS_dependency.OnChanged += NEWCUSTOMERREQUEST_OnChanged;
                NEWCUSTOMERREQUESTS_dependency.OnChanged += NEWCUSTOMERREQUEST_OnError;
                NEWCUSTOMERREQUESTS_dependency.Start();
            }
            catch (Exception exc)
            {
                Make_Message("NEWCUSTOMERREQUESTS");
                Logger.Error("Table NEWCUSTOMERREQUESTS --> " + exc);
                connection_dispose();
                return;
            }
            sw.Stop();
            Logger.Debug("Dependencies Connections Creation Time: {0}ms", sw.Elapsed.TotalSeconds);

        }
   
        private void Discionnect_Click(object sender, EventArgs e)
        {
            connection_dispose();           
        }

        private void GetAllData()
        {
            List<string> Class_List = new List<string>()
            {
                "Domain", "CONECTION", "DESCRIPTION", "LVPG_LVTABLET", "LVPL_BUSBAR", "LVPL_OVHLINE", "LVPL_SELFOVHLINE",
                "LVPL_SERVICECABLE", "LVPL_UGLINE", "LVPT_AUTOKEY", "LVPT_AVENUELUSTR", "LVPT_AVENUELUSTRPOLE", "LVPT_CAPACITOR",
                "LVPT_CONTACTOR", "LVPT_EARTH", "LVPT_FORKBOX", "LVPT_FUSEKEY", "LVPT_HEADER", "LVPT_INSULATOR",
                "LVPT_JOINT", "LVPT_JUMPER", "LVPT_PARTNERPLAQUE", "LVPT_POLE", "LVPT_SELFKEY", "MVPG_HVSUBSTATION", "MVPG_LAND",
                "MVPG_PADSUBSTATION", "MVPG_PADSUBSTATIONQUBIC", "MVPL_BUSBAR_HVSUB", "MVPL_BUSBAR_PADSUB", "MVPL_OVHLINE",
                "MVPL_SELFOVHLINE", "MVPL_UGLINE", "MVPT_AUTOBOOSTER", "MVPT_AUTORECLOSER", "MVPT_CABLEHEAD",
                "MVPT_CABLEJOINT", "MVPT_CAPACITY", "MVPT_COLONY", "MVPT_DSWITCH", "MVPT_DSWITCH_PADSUB", "MVPT_EARTH",
                "MVPT_FAULTDETECTOR", "MVPT_FRONTAGE", "MVPT_FUSECUTOUT", "MVPT_HEADER", "MVPT_HVSUBSTATIONTRANS", "MVPT_JUMPER",
                "MVPT_MOF", "MVPT_OUTGOING", "MVPT_PADSUBSTATIONTRANS", "MVPT_PLANTSCATTER", "MVPT_POLE", "MVPT_POLESUBSTATION",
                "MVPT_SECTIONALIZER", "MVPT_TOMB", "NEWCUSTOMERREQUESTS"
            };

            //List<string> Class_L = new List<string>()
            //{
            //    "CONECTION", "DESCRIPTION", "LVPT_CAPACITOR", "LVPT_CONTACTOR", "LVPT_FORKBOX", "LVPT_JOINT", "LVPT_SELFKEY",
            //    "MVPG_HVSUBSTATION", "MVPG_LAND", "MVPG_PADSUBSTATION", "MVPG_PADSUBSTATIONQUBIC", "MVPL_BUSBAR_HVSUB",
            //    "MVPL_BUSBAR_PADSUB", "MVPL_SELFOVHLINE", "MVPL_UGLINE", "MVPT_AUTOBOOSTER", "MVPT_AUTORECLOSER", "MVPT_CABLEHEAD",
            //    "MVPT_CABLEJOINT", "MVPT_CAPACITY", "MVPT_DSWITCH", "MVPT_DSWITCH_PADSUB", "MVPT_EARTH", "MVPT_FAULTDETECTOR",
            //    "MVPT_HEADER", "MVPT_HVSUBSTATIONTRANS", "MVPT_JUMPER", "MVPT_MOF", "MVPT_OUTGOING", "MVPT_PADSUBSTATIONTRANS",
            //    "MVPT_PLANTSCATTER", "MVPT_POLESUBSTATION", "MVPT_SECTIONALIZER", "MVPT_TOMB", "NEWCUSTOMERREQUESTS"
            //};
            List<string> Domain_L = new List<string>() { "Domain" };

            foreach (var item in Domain_L)
            {
                _connectionString = String.Format(
                Properties.Settings.Default.ConnectionString, Host.Text, Database.Text, Username.Text, Password.Text);


                ld.setConn(int.Parse(CityCode.Text), _connectionString);

                Console.WriteLine(item);
                Stopwatch stopw = Stopwatch.StartNew();

                ld.LoadCollectionData(item);

                stopw.Stop();
                Logger.Debug("All Data Collection--> {0} Time: {1}s", item, stopw.Elapsed.TotalSeconds);


            }
        }

        private void Domain_OnChanged(object sender, RecordChangedEventArgs<Domain> e)
        {
            var Domain_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("Domain");
                ld.Domain_values.Clear();
                ld.Domain_values.Add(e.Entity.Layer);
                ld.Domain_values.Add(e.Entity.Field);
                ld.Domain_values.Add(e.Entity.Code);
                ld.Domain_values.Add(e.Entity.Value);
                Domain_LD.LoadCollectionData("Domain", 0, e.ChangeType);
            }
        }

        private void Domain_OnError(object sender, RecordChangedEventArgs<Domain> e)
        {
            try
            {
                if (Domain_dependency.Status.ToString() == "StopDueToCancellation" || Domain_dependency.Status.ToString() == "StopDueToError")
                {
                    Domain_dependency.Start();
                }

            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("Domain");
                connection_dispose();
                return;
            }
        }

        private void CONECTION_OnChanged(object sender, RecordChangedEventArgs<CONECTION> e)
        {
            var CONECTION_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("CONECTION {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                CONECTION_LD.LoadCollectionData("CONECTION", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void CONECTION_OnError(object sender, RecordChangedEventArgs<CONECTION> e)
        {
            try
            {
                if (CONECTION_dependency.Status.ToString() == "StopDueToCancellation" || CONECTION_dependency.Status.ToString() == "StopDueToError")
                {
                    CONECTION_dependency.Start();
                }
                
            }
            catch(Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("CONECTION");
                connection_dispose();
                return;
            }
        }
        private void DESCRIPTION_OnChanged(object sender, RecordChangedEventArgs<DESCRIPTION> e)
        {
            var DESCRIPTION_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("DESCRIPTION {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                DESCRIPTION_LD.LoadCollectionData("DESCRIPTION", e.Entity.OBJECTID, e.ChangeType);

            }
        }
        private void DESCRIPTION_OnError(object sender, RecordChangedEventArgs<DESCRIPTION> e)
        {
            try
            {
                if (DESCRIPTION_dependency.Status.ToString() == "StopDueToCancellation" || DESCRIPTION_dependency.Status.ToString() == "StopDueToError")
                {
                    DESCRIPTION_dependency.Start();
                }
                
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("DESCRIPTION");
                connection_dispose();
                return;
            }
        }

        private void LVPG_LVTABLET_OnChanged(object sender, RecordChangedEventArgs<LVPG_LVTABLET> e)
        {
            var LVPG_LVTABLET_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPG_LVTABLET {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPG_LVTABLET_LD.LoadCollectionData("LVPG_LVTABLET", e.Entity.OBJECTID, e.ChangeType);
            }
        }
        private void LVPG_LVTABLET_OnError(object sender, RecordChangedEventArgs<LVPG_LVTABLET> e)
        {
            try
            {
                if (LVPG_LVTABLET_dependency.Status.ToString() == "StopDueToCancellation" || LVPG_LVTABLET_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPG_LVTABLET_dependency.Start();
                }
                
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPG_LVTABLET");
                connection_dispose();
                return;
            }
        }

        private void LVPL_BUSBAR_OnChanged(object sender, RecordChangedEventArgs<LVPL_BUSBAR> e)
        {
            var LVPL_BUSBAR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPL_BUSBAR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPL_BUSBAR_LD.LoadCollectionData("LVPL_BUSBAR", e.Entity.OBJECTID, e.ChangeType);

            }
        }

        private void LVPL_BUSBAR_OnError(object sender, RecordChangedEventArgs<LVPL_BUSBAR> e)
        {
            try
            {
                if (LVPL_BUSBAR_dependency.Status.ToString() == "StopDueToCancellation" || LVPL_BUSBAR_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPL_BUSBAR_dependency.Start();
                }
                
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPL_BUSBAR");
                connection_dispose();
                return;
            }
        }

        private void LVPL_OVHLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_OVHLINE> e)
        {
            var LVPL_OVHLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPL_OVHLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPL_OVHLINE_LD.LoadCollectionData("LVPL_OVHLINE", e.Entity.OBJECTID, e.ChangeType);

            }
        }

        private void LVPL_OVHLINE_OnError(object sender, RecordChangedEventArgs<LVPL_OVHLINE> e)
        {
            try
            {
                if (LVPL_OVHLINE_dependency.Status.ToString() == "StopDueToCancellation" || LVPL_OVHLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPL_OVHLINE_dependency.Start();
                }
               
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPL_OVHLINE");
                connection_dispose();
                return;
            }
        }


        private void LVPL_SELFOVHLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_SELFOVHLINE> e)
        {
            var LVPL_SELFOVHLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPL_SELFOVHLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPL_SELFOVHLINE_LD.LoadCollectionData("LVPL_SELFOVHLINE", e.Entity.OBJECTID, e.ChangeType);
            }
        }


        private void LVPL_SELFOVHLINE_OnError(object sender, RecordChangedEventArgs<LVPL_SELFOVHLINE> e)
        {
            try
            {
                if (LVPL_SELFOVHLINE_dependency.Status.ToString() == "StopDueToCancellation" || LVPL_SELFOVHLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPL_SELFOVHLINE_dependency.Start();
                }
            
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPL_SELFOVHLINE");
                connection_dispose();
                return;
            }
        }

        private void LVPL_SERVICECABLE_OnChanged(object sender, RecordChangedEventArgs<LVPL_SERVICECABLE> e)
        {
            var LVPL_SERVICECABLE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPL_SERVICECABLE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPL_SERVICECABLE_LD.LoadCollectionData("LVPL_SERVICECABLE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPL_SERVICECABLE_OnError(object sender, RecordChangedEventArgs<LVPL_SERVICECABLE> e)
        {
            try
            {
                if (LVPL_SERVICECABLE_dependency.Status.ToString() == "StopDueToCancellation" || LVPL_SERVICECABLE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPL_SERVICECABLE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPL_SERVICECABLE");
                connection_dispose();
                return;
            }
        }

        private void LVPL_UGLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_UGLINE> e)
        {
            var LVPL_UGLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPL_UGLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPL_UGLINE_LD.LoadCollectionData("LVPL_UGLINE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPL_UGLINE_OnError(object sender, RecordChangedEventArgs<LVPL_UGLINE> e)
        {
            try
            {
                if (LVPL_UGLINE_dependency.Status.ToString() == "StopDueToCancellation" || LVPL_UGLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPL_UGLINE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPL_UGLINE");
                connection_dispose();
                return;
            }

        }

        private void LVPT_AUTOKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_AUTOKEY> e)
        {
            var LVPT_AUTOKEY_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_AUTOKEY {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_AUTOKEY_LD.LoadCollectionData("LVPT_AUTOKEY", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_AUTOKEY_OnError(object sender, RecordChangedEventArgs<LVPT_AUTOKEY> e)
        {
            try
            {
                if (LVPT_AUTOKEY_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_AUTOKEY_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_AUTOKEY_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_AUTOKEY");
                connection_dispose();
                return;
            }
        }


        private void LVPT_AVENUELUSTR_OnChanged(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTR> e)
        {
            var LVPT_AVENUELUSTR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_AVENUELUSTR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_AVENUELUSTR_LD.LoadCollectionData("LVPT_AVENUELUSTR", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_AVENUELUSTR_OnError(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTR> e)
        {
            try
            {
                if (LVPT_AVENUELUSTR_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_AVENUELUSTR_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_AVENUELUSTR_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_AVENUELUSTR");
                connection_dispose();
                return;
            }
        }

        private void LVPT_AVENUELUSTRPOLE_OnChanged(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTRPOLE> e)
        {
            var LVPT_AVENUELUSTRPOLE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_AVENUELUSTRPOLE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_AVENUELUSTRPOLE_LD.LoadCollectionData("LVPT_AVENUELUSTRPOLE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_AVENUELUSTRPOLE_OnError(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTRPOLE> e)
        {
            try
            {
                if (LVPT_AVENUELUSTRPOLE_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_AVENUELUSTRPOLE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_AVENUELUSTRPOLE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_AVENUELUSTRPOLE");
                connection_dispose();
                return;
            }
        }

        private void LVPT_CAPACITOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_CAPACITOR> e)
        {
            var LVPT_CAPACITOR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_CAPACITOR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_CAPACITOR_LD.LoadCollectionData("LVPT_CAPACITOR", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_CAPACITOR_OnError(object sender, RecordChangedEventArgs<LVPT_CAPACITOR> e)
        {
            try
            {
                if (LVPT_CAPACITOR_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_CAPACITOR_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_CAPACITOR_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_CAPACITOR");
                connection_dispose();
                return;
            }
        }

        private void LVPT_CONTACTOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_CONTACTOR> e)
        {
            var LVPT_CONTACTOR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_CONTACTOR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_CONTACTOR_LD.LoadCollectionData("LVPT_CONTACTOR", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_CONTACTOR_OnError(object sender, RecordChangedEventArgs<LVPT_CONTACTOR> e)
        {
            try
            {
                if (LVPT_CONTACTOR_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_CONTACTOR_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_CONTACTOR_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_CONTACTOR");
                connection_dispose();
                return;
            }
        }

        private void LVPT_EARTH_OnChanged(object sender, RecordChangedEventArgs<LVPT_EARTH> e)
        {
            var LVPT_EARTH_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_EARTH {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_EARTH_LD.LoadCollectionData("LVPT_EARTH", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_EARTH_OnError(object sender, RecordChangedEventArgs<LVPT_EARTH> e)
        {
            try
            {
                if (LVPT_EARTH_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_EARTH_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_EARTH_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_EARTH");
                connection_dispose();
                return;
            }
        }

        private void LVPT_FORKBOX_OnChanged(object sender, RecordChangedEventArgs<LVPT_FORKBOX> e)
        {
            var LVPT_FORKBOX_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_FORKBOX {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_FORKBOX_LD.LoadCollectionData("LVPT_FORKBOX", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_FORKBOX_OnError(object sender, RecordChangedEventArgs<LVPT_FORKBOX> e)
        {
            try
            {
                if (LVPT_FORKBOX_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_FORKBOX_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_FORKBOX_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_FORKBOX");
                connection_dispose();
                return;
            }
        }

        private void LVPT_FUSEKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_FUSEKEY> e)
        {
            var LVPT_FUSEKEY_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_FUSEKEY {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_FUSEKEY_LD.LoadCollectionData("LVPT_FUSEKEY", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_FUSEKEY_OnError(object sender, RecordChangedEventArgs<LVPT_FUSEKEY> e)
        {
            try
            {
                if (LVPT_FUSEKEY_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_FUSEKEY_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_FUSEKEY_dependency.Start();
                }
                
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_FUSEKEY");
                connection_dispose();
                return;
            }
        }

        private void LVPT_HEADER_OnChanged(object sender, RecordChangedEventArgs<LVPT_HEADER> e)
        {
            var LVPT_HEADER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_HEADER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_HEADER_LD.LoadCollectionData("LVPT_HEADER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_HEADER_OnError(object sender, RecordChangedEventArgs<LVPT_HEADER> e)
        {
            try
            {
                if (LVPT_HEADER_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_HEADER_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_HEADER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_HEADER");
                connection_dispose();
                return;
            }
        }

        private void LVPT_INSULATOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_INSULATOR> e)
        {
            var LVPT_INSULATOR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_INSULATOR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_INSULATOR_LD.LoadCollectionData("LVPT_INSULATOR", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_INSULATOR_OnError(object sender, RecordChangedEventArgs<LVPT_INSULATOR> e)
        {
            try
            {
                if (LVPT_INSULATOR_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_INSULATOR_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_INSULATOR_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_INSULATOR");
                connection_dispose();
                return;
            }
        }

        private void LVPT_JOINT_OnChanged(object sender, RecordChangedEventArgs<LVPT_JOINT> e)
        {
            var LVPT_JOINT_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_JOINT {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_JOINT_LD.LoadCollectionData("LVPT_JOINT", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_JOINT_OnError(object sender, RecordChangedEventArgs<LVPT_JOINT> e)
        {
            try
            {
                if (LVPT_JOINT_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_JOINT_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_JOINT_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_JOINT");
                connection_dispose();
                return;
            }
        }

        private void LVPT_JUMPER_OnChanged(object sender, RecordChangedEventArgs<LVPT_JUMPER> e)
        {
            var LVPT_JUMPER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_JUMPER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_JUMPER_LD.LoadCollectionData("LVPT_JUMPER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_JUMPER_OnError(object sender, RecordChangedEventArgs<LVPT_JUMPER> e)
        {
            try
            {
                if (LVPT_JUMPER_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_JUMPER_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_JUMPER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_JUMPER");
                connection_dispose();
                return;
            }
        }

        private void LVPT_PARTNERPLAQUE_OnChanged(object sender, RecordChangedEventArgs<LVPT_PARTNERPLAQUE> e)
        {
            var LVPT_PARTNERPLAQUE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_PARTNERPLAQUE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_PARTNERPLAQUE_LD.LoadCollectionData("LVPT_PARTNERPLAQUE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_PARTNERPLAQUE_OnError(object sender, RecordChangedEventArgs<LVPT_PARTNERPLAQUE> e)
        {
            try
            {
                if (LVPT_PARTNERPLAQUE_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_PARTNERPLAQUE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_PARTNERPLAQUE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_PARTNERPLAQUE");
                connection_dispose();
                return;
            }
        }

        private void LVPT_POLE_OnChanged(object sender, RecordChangedEventArgs<LVPT_POLE> e)
        {
            var LVPT_POLE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_POLE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_POLE_LD.LoadCollectionData("LVPT_POLE", e.Entity.OBJECTID, e.ChangeType);
            }
        }
        private void LVPT_POLE_OnError(object sender, RecordChangedEventArgs<LVPT_POLE> e)
        {
            try
            {
                if (LVPT_POLE_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_POLE_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_POLE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_POLE");
                connection_dispose();
                return;
            }
        }

        private void LVPT_SELFKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_SELFKEY> e)
        {
            var LVPT_SELFKEY_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("LVPT_SELFKEY {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                LVPT_SELFKEY_LD.LoadCollectionData("LVPT_SELFKEY", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void LVPT_SELFKEY_OnError(object sender, RecordChangedEventArgs<LVPT_SELFKEY> e)
        {
            try
            {
                if (LVPT_SELFKEY_dependency.Status.ToString() == "StopDueToCancellation" || LVPT_SELFKEY_dependency.Status.ToString() == "StopDueToError")
                {
                    LVPT_SELFKEY_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("LVPT_SELFKEY");
                connection_dispose();
                return;
            }
        }

        private void MVPG_HVSUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPG_HVSUBSTATION> e)
        {
            var MVPG_HVSUBSTATION_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPG_HVSUBSTATION {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPG_HVSUBSTATION_LD.LoadCollectionData("MVPG_HVSUBSTATION", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPG_HVSUBSTATION_OnError(object sender, RecordChangedEventArgs<MVPG_HVSUBSTATION> e)
        {
            try
            {
                if (MVPG_HVSUBSTATION_dependency.Status.ToString() == "StopDueToCancellation" || MVPG_HVSUBSTATION_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPG_HVSUBSTATION_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPG_HVSUBSTATION");
                connection_dispose();
                return;
            }
        }

        private void MVPG_LAND_OnChanged(object sender, RecordChangedEventArgs<MVPG_LAND> e)
        {
            var MVPG_LAND_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPG_LAND {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPG_LAND_LD.LoadCollectionData("MVPG_LAND", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPG_LAND_OnError(object sender, RecordChangedEventArgs<MVPG_LAND> e)
        {
            try
            {
                if (MVPG_LAND_dependency.Status.ToString() == "StopDueToCancellation" || MVPG_LAND_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPG_LAND_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPG_LAND");
                connection_dispose();
                return;
            }
        }

        private void MVPG_PADSUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATION> e)
        {
            var MVPG_PADSUBSTATION_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPG_PADSUBSTATION {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPG_PADSUBSTATION_LD.LoadCollectionData("MVPG_PADSUBSTATION", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPG_PADSUBSTATION_OnError(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATION> e)
        {
            try
            {
                if (MVPG_PADSUBSTATION_dependency.Status.ToString() == "StopDueToCancellation" || MVPG_PADSUBSTATION_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPG_PADSUBSTATION_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPG_PADSUBSTATION");
                connection_dispose();
                return;
            }
        }

        private void MVPG_PADSUBSTATIONQUBIC_OnChanged(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATIONQUBIC> e)
        {
            var MVPG_PADSUBSTATIONQUBIC_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPG_PADSUBSTATIONQUBIC {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPG_PADSUBSTATIONQUBIC_LD.LoadCollectionData("MVPG_PADSUBSTATIONQUBIC", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPG_PADSUBSTATIONQUBIC_OnError(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATIONQUBIC> e)
        {
            try
            {
                if (MVPG_PADSUBSTATIONQUBIC_dependency.Status.ToString() == "StopDueToCancellation" || MVPG_PADSUBSTATIONQUBIC_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPG_PADSUBSTATIONQUBIC_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPG_PADSUBSTATIONQUBIC");
                connection_dispose();
                return;
            }
        }

        private void MVPL_BUSBAR_HVSUB_OnChanged(object sender, RecordChangedEventArgs<MVPL_BUSBAR_HVSUB> e)
        {
            var MVPL_BUSBAR_HVSUB_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPL_BUSBAR_HVSUB {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPL_BUSBAR_HVSUB_LD.LoadCollectionData("MVPL_BUSBAR_HVSUB", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPL_BUSBAR_HVSUB_OnError(object sender, RecordChangedEventArgs<MVPL_BUSBAR_HVSUB> e)
        {
            try
            {
                if (MVPL_BUSBAR_HVSUB_dependency.Status.ToString() == "StopDueToCancellation" || MVPL_BUSBAR_HVSUB_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPL_BUSBAR_HVSUB_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPL_BUSBAR_HVSUB");
                connection_dispose();
                return;
            }
        }

        private void MVPL_BUSBAR_PADSUB_OnChanged(object sender, RecordChangedEventArgs<MVPL_BUSBAR_PADSUB> e)
        {
            var MVPL_BUSBAR_PADSUB_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPL_BUSBAR_PADSUB {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPL_BUSBAR_PADSUB_LD.LoadCollectionData("MVPL_BUSBAR_PADSUB", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPL_BUSBAR_PADSUB_OnError(object sender, RecordChangedEventArgs<MVPL_BUSBAR_PADSUB> e)
        {
            try
            {
                if (MVPL_BUSBAR_PADSUB_dependency.Status.ToString() == "StopDueToCancellation" || MVPL_BUSBAR_PADSUB_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPL_BUSBAR_PADSUB_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPL_BUSBAR_PADSUB");
                connection_dispose();
                return;
            }
        }

        private void MVPL_OVHLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_OVHLINE> e)
        {
            var MVPL_OVHLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPL_OVHLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPL_OVHLINE_LD.LoadCollectionData("MVPL_OVHLINE", e.Entity.OBJECTID, e.ChangeType);
            }
        }


        private void MVPL_OVHLINE_OnError(object sender, RecordChangedEventArgs<MVPL_OVHLINE> e)
        {
            try
            {
                if (MVPL_OVHLINE_dependency.Status.ToString() == "StopDueToCancellation" || MVPL_OVHLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPL_OVHLINE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPL_OVHLINE");
                connection_dispose();
                return;
            }
        }

        private void MVPL_SELFOVHLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_SELFOVHLINE> e)
        {
            var MVPL_SELFOVHLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPL_SELFOVHLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPL_SELFOVHLINE_LD.LoadCollectionData("MVPL_SELFOVHLINE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPL_SELFOVHLINE_OnError(object sender, RecordChangedEventArgs<MVPL_SELFOVHLINE> e)
        {
            
            try
            {
                if (MVPL_SELFOVHLINE_dependency.Status.ToString() == "StopDueToCancellation" || MVPL_SELFOVHLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPL_SELFOVHLINE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPL_SELFOVHLINE");
                connection_dispose();
                return;
            }
        }

        private void MVPL_UGLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_UGLINE> e)
        {
            var MVPL_UGLINE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPL_UGLINE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPL_UGLINE_LD.LoadCollectionData("MVPL_UGLINE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPL_UGLINE_OnError(object sender, RecordChangedEventArgs<MVPL_UGLINE> e)
        {
            try
            {
                if (MVPL_UGLINE_dependency.Status.ToString() == "StopDueToCancellation" || MVPL_UGLINE_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPL_UGLINE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPL_UGLINE");
                connection_dispose();
                return;
            }
        }

        private void MVPT_AUTOBOOSTER_OnChanged(object sender, RecordChangedEventArgs<MVPT_AUTOBOOSTER> e)
        {
            var MVPT_AUTOBOOSTER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_AUTOBOOSTER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_AUTOBOOSTER_LD.LoadCollectionData("MVPT_AUTOBOOSTER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_AUTOBOOSTER_OnError(object sender, RecordChangedEventArgs<MVPT_AUTOBOOSTER> e)
        {
            try
            {
                if (MVPT_AUTOBOOSTER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_AUTOBOOSTER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_AUTOBOOSTER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_AUTOBOOSTER");
                connection_dispose();
                return;
            }
        }

        private void MVPT_AUTORECLOSER_OnChanged(object sender, RecordChangedEventArgs<MVPT_AUTORECLOSER> e)
        {
            var MVPT_AUTORECLOSER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_AUTORECLOSER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_AUTORECLOSER_LD.LoadCollectionData("MVPT_AUTORECLOSER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_AUTORECLOSER_OnError(object sender, RecordChangedEventArgs<MVPT_AUTORECLOSER> e)
        {
            try
            {
                if (MVPT_AUTORECLOSER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_AUTORECLOSER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_AUTORECLOSER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_AUTORECLOSER");
                connection_dispose();
                return;
            }
        }
        private void MVPT_CABLEHEAD_OnChanged(object sender, RecordChangedEventArgs<MVPT_CABLEHEAD> e)
        {
            var MVPT_CABLEHEAD_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_CABLEHEAD {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_CABLEHEAD_LD.LoadCollectionData("MVPT_CABLEHEAD", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_CABLEHEAD_OnError(object sender, RecordChangedEventArgs<MVPT_CABLEHEAD> e)
        {
            try
            {
                if (MVPT_CABLEHEAD_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_CABLEHEAD_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_CABLEHEAD_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_CABLEHEAD");
                connection_dispose();
                return;
            }
        }

        private void MVPT_CABLEJOINT_OnChanged(object sender, RecordChangedEventArgs<MVPT_CABLEJOINT> e)
        {
            var MVPT_CABLEJOINT_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_CABLEJOINT {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_CABLEJOINT_LD.LoadCollectionData("MVPT_CABLEJOINT", e.Entity.OBJECTID, e.ChangeType);
            }

        }

        private void MVPT_CABLEJOINT_OnError(object sender, RecordChangedEventArgs<MVPT_CABLEJOINT> e)
        {
            try
            {
                if (MVPT_CABLEJOINT_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_CABLEJOINT_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_CABLEJOINT_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_CABLEJOINT");
                connection_dispose();
                return;
            }

        }

        private void MVPT_CAPACITY_OnChanged(object sender, RecordChangedEventArgs<MVPT_CAPACITY> e)
        {
            var MVPT_CAPACITY_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_CAPACITY {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_CAPACITY_LD.LoadCollectionData("MVPT_CAPACITY", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_CAPACITY_OnError(object sender, RecordChangedEventArgs<MVPT_CAPACITY> e)
        {
            try
            {
                if (MVPT_CAPACITY_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_CAPACITY_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_CAPACITY_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_CAPACITY");
                connection_dispose();
                return;
            }
        }

        private void MVPT_COLONY_OnChanged(object sender, RecordChangedEventArgs<MVPT_COLONY> e)
        {
            var MVPT_COLONY_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_COLONY {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_COLONY_LD.LoadCollectionData("MVPT_COLONY", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_COLONY_OnError(object sender, RecordChangedEventArgs<MVPT_COLONY> e)
        {
            try
            {
                if (MVPT_COLONY_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_COLONY_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_COLONY_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_COLONY");
                connection_dispose();
                return;
            }
        }

        private void MVPT_DSWITCH_OnChanged(object sender, RecordChangedEventArgs<MVPT_DSWITCH> e)
        {
            var MVPT_DSWITCH_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_DSWITCH {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_DSWITCH_LD.LoadCollectionData("MVPT_DSWITCH", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_DSWITCH_OnError(object sender, RecordChangedEventArgs<MVPT_DSWITCH> e)
        {
            try
            {
                if (MVPT_DSWITCH_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_DSWITCH_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_DSWITCH_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_DSWITCH");
                connection_dispose();
                return;
            }
        }

        private void MVPT_DSWITCH_PADSUB_OnChanged(object sender, RecordChangedEventArgs<MVPT_DSWITCH_PADSUB> e)
        {
            var MVPT_DSWITCH_PADSUB_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_DSWITCH_PADSUB {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_DSWITCH_PADSUB_LD.LoadCollectionData("MVPT_DSWITCH_PADSUB", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_DSWITCH_PADSUB_OnError(object sender, RecordChangedEventArgs<MVPT_DSWITCH_PADSUB> e)
        {
            try
            {
                if (MVPT_DSWITCH_PADSUB_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_DSWITCH_PADSUB_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_DSWITCH_PADSUB_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_DSWITCH_PADSUB");
                connection_dispose();
                return;
            }
        }

        private void MVPT_EARTH_OnChanged(object sender, RecordChangedEventArgs<MVPT_EARTH> e)
        {
            var MVPT_EARTH_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_EARTH {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_EARTH_LD.LoadCollectionData("MVPT_EARTH", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_EARTH_OnError(object sender, RecordChangedEventArgs<MVPT_EARTH> e)
        {
            try
            {
                if (MVPT_EARTH_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_EARTH_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_EARTH_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_EARTH");
                connection_dispose();
                return;
            }
        }

        private void MVPT_FAULTDETECTOR_OnChanged(object sender, RecordChangedEventArgs<MVPT_FAULTDETECTOR> e)
        {
            var MVPT_FAULTDETECTOR_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_FAULTDETECTOR {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_FAULTDETECTOR_LD.LoadCollectionData("MVPT_FAULTDETECTOR", e.Entity.OBJECTID, e.ChangeType);
            }
        }
        private void MVPT_FAULTDETECTOR_OnError(object sender, RecordChangedEventArgs<MVPT_FAULTDETECTOR> e)
        {
            try
            {
                if (MVPT_FAULTDETECTOR_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_FAULTDETECTOR_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_FAULTDETECTOR_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_FAULTDETECTOR");
                connection_dispose();
                return;
            }
        }

        private void MVPT_FRONTAGE_OnChanged(object sender, RecordChangedEventArgs<MVPT_FRONTAGE> e)
        {
            var MVPT_FRONTAGE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_FRONTAGE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_FRONTAGE_LD.LoadCollectionData("MVPT_FRONTAGE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_FRONTAGE_OnError(object sender, RecordChangedEventArgs<MVPT_FRONTAGE> e)
        {
            try
            {
                if (MVPT_FRONTAGE_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_FRONTAGE_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_FRONTAGE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_FRONTAGE");
                connection_dispose();
                return;
            }
        }

        private void MVPT_FUSECUTOUT_OnChanged(object sender, RecordChangedEventArgs<MVPT_FUSECUTOUT> e)
        {

            var MVPT_FUSECUTOUT_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_FUSECUTOUT {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_FUSECUTOUT_LD.LoadCollectionData("MVPT_FUSECUTOUT", e.Entity.OBJECTID, e.ChangeType);
            }           
        }

        private void MVPT_FUSECUTOUT_OnError(object sender, RecordChangedEventArgs<MVPT_FUSECUTOUT> e)
        {
            try
            {
                if(MVPT_FUSECUTOUT_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_FUSECUTOUT_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_FUSECUTOUT_dependency.Start();
                }
                
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_FUSECUTOUT");
                connection_dispose();
                return;
            }
        }

        private void MVPT_HEADER_OnChanged(object sender, RecordChangedEventArgs<MVPT_HEADER> e)
        {
            var MVPT_HEADER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_HEADER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_HEADER_LD.LoadCollectionData("MVPT_HEADER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_HEADER_OnError(object sender, RecordChangedEventArgs<MVPT_HEADER> e)
        {
            try
            {
                if (MVPT_HEADER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_HEADER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_HEADER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_HEADER");
                connection_dispose();
                return;
            }
        }

        private void MVPT_HVSUBSTATIONTRAN_OnChanged(object sender, RecordChangedEventArgs<MVPT_HVSUBSTATIONTRANS> e)
        {
            var MVPT_HVSUBSTATIONTRANS_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_HVSUBSTATIONTRANS {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_HVSUBSTATIONTRANS_LD.LoadCollectionData("MVPT_HVSUBSTATIONTRANS", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_HVSUBSTATIONTRAN_OnError(object sender, RecordChangedEventArgs<MVPT_HVSUBSTATIONTRANS> e)
        {
            try
            {
                if (MVPT_HVSUBSTATIONTRANS_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_HVSUBSTATIONTRANS_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_HVSUBSTATIONTRANS_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_HVSUBSTATIONTRANS");
                connection_dispose();
                return;
            }
        }

        private void MVPT_JUMPER_OnChanged(object sender, RecordChangedEventArgs<MVPT_JUMPER> e)
        {
            var MVPT_JUMPER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_JUMPER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_JUMPER_LD.LoadCollectionData("MVPT_JUMPER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_JUMPER_OnError(object sender, RecordChangedEventArgs<MVPT_JUMPER> e)
        {
            try
            {
                if (MVPT_JUMPER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_JUMPER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_JUMPER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_JUMPER");
                connection_dispose();
                return;
            }
        }

        private void MVPT_MOF_OnChanged(object sender, RecordChangedEventArgs<MVPT_MOF> e)
        {
            var MVPT_MOF_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_MOF {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_MOF_LD.LoadCollectionData("MVPT_MOF", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_MOF_OnError(object sender, RecordChangedEventArgs<MVPT_MOF> e)
        {
            try
            {
                if (MVPT_MOF_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_MOF_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_MOF_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_MOF");
                connection_dispose();
                return;
            }
        }

        private void MVPT_OUTGOING_OnChanged(object sender, RecordChangedEventArgs<MVPT_OUTGOING> e)
        {
            var MVPT_OUTGOING_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_OUTGOING {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_OUTGOING_LD.LoadCollectionData("MVPT_OUTGOING", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_OUTGOING_OnError(object sender, RecordChangedEventArgs<MVPT_OUTGOING> e)
        {
            try
            {
                if (MVPT_OUTGOING_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_OUTGOING_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_OUTGOING_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_OUTGOING");
                connection_dispose();
                return;
            }
        }
        private void MVPT_PADSUBSTATIONTRAN_OnChanged(object sender, RecordChangedEventArgs<MVPT_PADSUBSTATIONTRANS> e)
        {
            var MVPT_PADSUBSTATIONTRANS_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_PADSUBSTATIONTRANS {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_PADSUBSTATIONTRANS_LD.LoadCollectionData("MVPT_PADSUBSTATIONTRANS", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_PADSUBSTATIONTRAN_OnError(object sender, RecordChangedEventArgs<MVPT_PADSUBSTATIONTRANS> e)
        {
            try
            {
                if (MVPT_PADSUBSTATIONTRANS_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_PADSUBSTATIONTRANS_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_PADSUBSTATIONTRANS_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_PADSUBSTATIONTRANS");
                connection_dispose();
                return;
            }
        }

        private void MVPT_PLANTSCATTER_OnChanged(object sender, RecordChangedEventArgs<MVPT_PLANTSCATTER> e)
        {
            var MVPT_PLANTSCATTER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_PLANTSCATTER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_PLANTSCATTER_LD.LoadCollectionData("MVPT_PLANTSCATTER", e.Entity.OBJECTID, e.ChangeType);
            }
        }


        private void MVPT_PLANTSCATTER_OnError(object sender, RecordChangedEventArgs<MVPT_PLANTSCATTER> e)
        {
            try
            {
                if (MVPT_PLANTSCATTER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_PLANTSCATTER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_PLANTSCATTER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_PLANTSCATTER");
                connection_dispose();
                return;
            }
        }

        private void MVPT_POLE_OnChanged(object sender, RecordChangedEventArgs<MVPT_POLE> e)
        {
            var MVPT_POLE_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_POLE {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_POLE_LD.LoadCollectionData("MVPT_POLE", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_POLE_OnError(object sender, RecordChangedEventArgs<MVPT_POLE> e)
        {
            try
            {
                if (MVPT_POLE_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_POLE_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_POLE_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_POLE");
                connection_dispose();
                return;
            }
        }

        private void MVPT_POLESUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPT_POLESUBSTATION> e)
        {
            var MVPT_POLESUBSTATION_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_POLESUBSTATION {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_POLESUBSTATION_LD.LoadCollectionData("MVPT_POLESUBSTATION", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_POLESUBSTATION_OnError(object sender, RecordChangedEventArgs<MVPT_POLESUBSTATION> e)
        {
            try
            {
                if (MVPT_POLESUBSTATION_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_POLESUBSTATION_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_POLESUBSTATION_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_POLESUBSTATION");
                connection_dispose();
                return;
            }
        }

        private void MVPT_SECTIONALIZER_OnChanged(object sender, RecordChangedEventArgs<MVPT_SECTIONALIZER> e)
        {
            var MVPT_SECTIONALIZER_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_SECTIONALIZER {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_SECTIONALIZER_LD.LoadCollectionData("MVPT_SECTIONALIZER", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_SECTIONALIZER_OnError(object sender, RecordChangedEventArgs<MVPT_SECTIONALIZER> e)
        {
            try
            {
                if (MVPT_SECTIONALIZER_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_SECTIONALIZER_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_SECTIONALIZER_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_SECTIONALIZER");
                connection_dispose();
                return;
            }
        }

        private void MVPT_TOMB_OnChanged(object sender, RecordChangedEventArgs<MVPT_TOMB> e)
        {
            var MVPT_TOMB_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("MVPT_TOMB {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                MVPT_TOMB_LD.LoadCollectionData("MVPT_TOMB", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void MVPT_TOMB_OnError(object sender, RecordChangedEventArgs<MVPT_TOMB> e)
        {
            try
            {
                if (MVPT_TOMB_dependency.Status.ToString() == "StopDueToCancellation" || MVPT_TOMB_dependency.Status.ToString() == "StopDueToError")
                {
                    MVPT_TOMB_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("MVPT_TOMB");
                connection_dispose();
                return;
            }
        }

        private void NEWCUSTOMERREQUEST_OnChanged(object sender, RecordChangedEventArgs<NEWCUSTOMERREQUESTS> e)
        {
            var NEWCUSTOMERREQUESTS_LD = new LoadData(_connectionString);

            if (e.ChangeType != ChangeType.None)
            {
                Logger.Info("NEWCUSTOMERREQUESTS {0} + OBJECTID {1}", e.ChangeType, e.Entity.OBJECTID);
                NEWCUSTOMERREQUESTS_LD.LoadCollectionData("NEWCUSTOMERREQUESTS", e.Entity.OBJECTID, e.ChangeType);
            }
        }

        private void NEWCUSTOMERREQUEST_OnError(object sender, RecordChangedEventArgs<NEWCUSTOMERREQUESTS> e)
        {
            try
            {
                if (NEWCUSTOMERREQUESTS_dependency.Status.ToString() == "StopDueToCancellation" || NEWCUSTOMERREQUESTS_dependency.Status.ToString() == "StopDueToError")
                {
                    NEWCUSTOMERREQUESTS_dependency.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.Error("onError: " + ee);
                Make_Message("NEWCUSTOMERREQUESTS");
                connection_dispose();
                return;
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipText = "I'm Here";
                notifyIcon.BalloonTipTitle = "GIS";
                notifyIcon.ShowBalloonTip(3000);
                this.ShowInTaskbar = false;
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;

        }

    }
}
