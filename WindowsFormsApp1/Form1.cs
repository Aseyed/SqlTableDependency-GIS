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
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Anacav.Esb.Messaging;
using Anacav.Esb.Connection;

namespace GIS
{

    public partial class Form1 : Form
    {
        private string _connectionString;
        public static System.Timers.Timer aTimer;
        public static Dictionary<string, List<Dictionary<string, object>>> Main_List;

        Thread userThread = new Thread(new ThreadStart(Counter));

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
           // userThread.Join();
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


        private void Connect_Click(object sender, EventArgs e)
        {
            _connectionString = String.Format(
                Properties.Settings.Default.ConnectionString, Host.Text, Database.Text, Username.Text, Password.Text);
            LoadCollectionData(65, "MVPT_DSWITCH");

            Stopwatch sw = Stopwatch.StartNew();

            CONECTION_dependency = new SqlTableDependency<CONECTION>(_connectionString, "CONECTION");
            CONECTION_dependency.OnChanged += CONECTION_OnChanged;
            CONECTION_dependency.Start();

            DESCRIPTION_dependency = new SqlTableDependency<DESCRIPTION>(_connectionString, "DESCRIPTION");
            DESCRIPTION_dependency.OnChanged += DESCRIPTION_OnChanged;
            DESCRIPTION_dependency.Start();

            LVPG_LVTABLET_dependency = new SqlTableDependency<LVPG_LVTABLET>(_connectionString, "LVPG_LVTABLET");
            LVPG_LVTABLET_dependency.OnChanged += LVPG_LVTABLET_OnChanged;
            LVPG_LVTABLET_dependency.Start();

            LVPL_BUSBAR_dependency = new SqlTableDependency<LVPL_BUSBAR>(_connectionString, "LVPL_BUSBAR");
            LVPL_BUSBAR_dependency.OnChanged += LVPL_BUSBAR_OnChanged;
            LVPL_BUSBAR_dependency.Start();

            LVPL_OVHLINE_dependency = new SqlTableDependency<LVPL_OVHLINE>(_connectionString, "LVPL_OVHLINE");
            LVPL_OVHLINE_dependency.OnChanged += LVPL_OVHLINE_OnChanged;
            LVPL_OVHLINE_dependency.Start();

            LVPL_SELFOVHLINE_dependency = new SqlTableDependency<LVPL_SELFOVHLINE>(_connectionString, "LVPL_SELFOVHLINE");
            LVPL_SELFOVHLINE_dependency.OnChanged += LVPL_SELFOVHLINE_OnChanged;
            LVPL_SELFOVHLINE_dependency.Start();

            LVPL_SERVICECABLE_dependency = new SqlTableDependency<LVPL_SERVICECABLE>(_connectionString, "LVPL_SERVICECABLE");
            LVPL_SERVICECABLE_dependency.OnChanged += LVPL_SERVICECABLE_OnChanged;
            LVPL_SERVICECABLE_dependency.Start();

            LVPL_UGLINE_dependency = new SqlTableDependency<LVPL_UGLINE>(_connectionString, "LVPL_UGLINE");
            LVPL_UGLINE_dependency.OnChanged += LVPL_UGLINE_OnChanged;
            LVPL_UGLINE_dependency.Start();

            LVPT_AUTOKEY_dependency = new SqlTableDependency<LVPT_AUTOKEY>(_connectionString, "LVPT_AUTOKEY");
            LVPT_AUTOKEY_dependency.OnChanged += LVPT_AUTOKEY_OnChanged;
            LVPT_AUTOKEY_dependency.Start();

            LVPT_AVENUELUSTR_dependency = new SqlTableDependency<LVPT_AVENUELUSTR>(_connectionString, "LVPT_AVENUELUSTR");
            LVPT_AVENUELUSTR_dependency.OnChanged += LVPT_AVENUELUSTR_OnChanged;
            LVPT_AVENUELUSTR_dependency.Start();

            LVPT_AVENUELUSTRPOLE_dependency = new SqlTableDependency<LVPT_AVENUELUSTRPOLE>(_connectionString, "LVPT_AVENUELUSTRPOLE");
            LVPT_AVENUELUSTRPOLE_dependency.OnChanged += LVPT_AVENUELUSTRPOLE_OnChanged;
            LVPT_AVENUELUSTRPOLE_dependency.Start();

            LVPT_CAPACITOR_dependency = new SqlTableDependency<LVPT_CAPACITOR>(_connectionString, "LVPT_CAPACITOR");
            LVPT_CAPACITOR_dependency.OnChanged += LVPT_CAPACITOR_OnChanged;
            LVPT_CAPACITOR_dependency.Start();

            LVPT_CONTACTOR_dependency = new SqlTableDependency<LVPT_CONTACTOR>(_connectionString, "LVPT_CONTACTOR");
            LVPT_CONTACTOR_dependency.OnChanged += LVPT_CONTACTOR_OnChanged;
            LVPT_CONTACTOR_dependency.Start();

            LVPT_EARTH_dependency = new SqlTableDependency<LVPT_EARTH>(_connectionString, "LVPT_EARTH");
            LVPT_EARTH_dependency.OnChanged += LVPT_EARTH_OnChanged;
            LVPT_EARTH_dependency.Start();

            LVPT_FORKBOX_dependency = new SqlTableDependency<LVPT_FORKBOX>(_connectionString, "LVPT_FORKBOX");
            LVPT_FORKBOX_dependency.OnChanged += LVPT_FORKBOX_OnChanged;
            LVPT_FORKBOX_dependency.Start();

            LVPT_FUSEKEY_dependency = new SqlTableDependency<LVPT_FUSEKEY>(_connectionString, "LVPT_FUSEKEY");
            LVPT_FUSEKEY_dependency.OnChanged += LVPT_FUSEKEY_OnChanged;
            LVPT_FUSEKEY_dependency.Start();

            LVPT_HEADER_dependency = new SqlTableDependency<LVPT_HEADER>(_connectionString, "LVPT_HEADER");
            LVPT_HEADER_dependency.OnChanged += LVPT_HEADER_OnChanged;
            LVPT_HEADER_dependency.Start();

            LVPT_INSULATOR_dependency = new SqlTableDependency<LVPT_INSULATOR>(_connectionString, "LVPT_INSULATOR");
            LVPT_INSULATOR_dependency.OnChanged += LVPT_INSULATOR_OnChanged;
            LVPT_INSULATOR_dependency.Start();

            LVPT_JOINT_dependency = new SqlTableDependency<LVPT_JOINT>(_connectionString, "LVPT_JOINT");
            LVPT_JOINT_dependency.OnChanged += LVPT_JOINT_OnChanged;
            LVPT_JOINT_dependency.Start();

            LVPT_JUMPER_dependency = new SqlTableDependency<LVPT_JUMPER>(_connectionString, "LVPT_JUMPER");
            LVPT_JUMPER_dependency.OnChanged += LVPT_JUMPER_OnChanged;
            LVPT_JUMPER_dependency.Start();

            LVPT_PARTNERPLAQUE_dependency = new SqlTableDependency<LVPT_PARTNERPLAQUE>(_connectionString, "LVPT_PARTNERPLAQUE");
            LVPT_PARTNERPLAQUE_dependency.OnChanged += LVPT_PARTNERPLAQUE_OnChanged;
            LVPT_PARTNERPLAQUE_dependency.Start();

            LVPT_POLE_dependency = new SqlTableDependency<LVPT_POLE>(_connectionString, "LVPT_POLE");
            LVPT_POLE_dependency.OnChanged += LVPT_POLE_OnChanged;
            LVPT_POLE_dependency.Start();

            LVPT_SELFKEY_dependency = new SqlTableDependency<LVPT_SELFKEY>(_connectionString, "LVPT_SELFKEY");
            LVPT_SELFKEY_dependency.OnChanged += LVPT_SELFKEY_OnChanged;
            LVPT_SELFKEY_dependency.Start();

            MVPG_HVSUBSTATION_dependency = new SqlTableDependency<MVPG_HVSUBSTATION>(_connectionString, "MVPG_HVSUBSTATION");
            MVPG_HVSUBSTATION_dependency.OnChanged += MVPG_HVSUBSTATION_OnChanged;
            MVPG_HVSUBSTATION_dependency.Start();

            MVPG_LAND_dependency = new SqlTableDependency<MVPG_LAND>(_connectionString, "MVPG_LAND");
            MVPG_LAND_dependency.OnChanged += MVPG_LAND_OnChanged;
            MVPG_LAND_dependency.Start();

            MVPG_PADSUBSTATION_dependency = new SqlTableDependency<MVPG_PADSUBSTATION>(_connectionString, "MVPG_PADSUBSTATION");
            MVPG_PADSUBSTATION_dependency.OnChanged += MVPG_PADSUBSTATION_OnChanged;
            MVPG_PADSUBSTATION_dependency.Start();

            MVPG_PADSUBSTATIONQUBIC_dependency = new SqlTableDependency<MVPG_PADSUBSTATIONQUBIC>(_connectionString, "MVPG_PADSUBSTATIONQUBIC");
            MVPG_PADSUBSTATIONQUBIC_dependency.OnChanged += MVPG_PADSUBSTATIONQUBIC_OnChanged;
            MVPG_PADSUBSTATIONQUBIC_dependency.Start();

            MVPL_BUSBAR_HVSUB_dependency = new SqlTableDependency<MVPL_BUSBAR_HVSUB>(_connectionString, "MVPL_BUSBAR_HVSUB");
            MVPL_BUSBAR_HVSUB_dependency.OnChanged += MVPL_BUSBAR_HVSUB_OnChanged;
            MVPL_BUSBAR_HVSUB_dependency.Start();

            MVPL_BUSBAR_PADSUB_dependency = new SqlTableDependency<MVPL_BUSBAR_PADSUB>(_connectionString, "MVPL_BUSBAR_PADSUB");
            MVPL_BUSBAR_PADSUB_dependency.OnChanged += MVPL_BUSBAR_PADSUB_OnChanged;
            MVPL_BUSBAR_PADSUB_dependency.Start();

            MVPL_OVHLINE_dependency = new SqlTableDependency<MVPL_OVHLINE>(_connectionString, "MVPL_OVHLINE");
            MVPL_OVHLINE_dependency.OnChanged += MVPL_OVHLINE_OnChanged;
            MVPL_OVHLINE_dependency.Start();

            MVPL_SELFOVHLINE_dependency = new SqlTableDependency<MVPL_SELFOVHLINE>(_connectionString, "MVPL_SELFOVHLINE");
            MVPL_SELFOVHLINE_dependency.OnChanged += MVPL_SELFOVHLINE_OnChanged;
            MVPL_SELFOVHLINE_dependency.Start();

            MVPL_UGLINE_dependency = new SqlTableDependency<MVPL_UGLINE>(_connectionString, "MVPL_UGLINE");
            MVPL_UGLINE_dependency.OnChanged += MVPL_UGLINE_OnChanged;
            MVPL_UGLINE_dependency.Start();

            MVPT_AUTOBOOSTER_dependency = new SqlTableDependency<MVPT_AUTOBOOSTER>(_connectionString, "MVPT_AUTOBOOSTER");
            MVPT_AUTOBOOSTER_dependency.OnChanged += MVPT_AUTOBOOSTER_OnChanged;
            MVPT_AUTOBOOSTER_dependency.Start();

            MVPT_AUTORECLOSER_dependency = new SqlTableDependency<MVPT_AUTORECLOSER>(_connectionString, "MVPT_AUTORECLOSER");
            MVPT_AUTORECLOSER_dependency.OnChanged += MVPT_AUTORECLOSER_OnChanged;
            MVPT_AUTORECLOSER_dependency.Start();

            MVPT_CABLEHEAD_dependency = new SqlTableDependency<MVPT_CABLEHEAD>(_connectionString, "MVPT_CABLEHEAD");
            MVPT_CABLEHEAD_dependency.OnChanged += MVPT_CABLEHEAD_OnChanged;
            MVPT_CABLEHEAD_dependency.Start();

            MVPT_CABLEJOINT_dependency = new SqlTableDependency<MVPT_CABLEJOINT>(_connectionString, "MVPT_CABLEJOINT");
            MVPT_CABLEJOINT_dependency.OnChanged += MVPT_CABLEJOINT_OnChanged;
            MVPT_CABLEJOINT_dependency.Start();

            MVPT_CAPACITY_dependency = new SqlTableDependency<MVPT_CAPACITY>(_connectionString, "MVPT_CAPACITY");
            MVPT_CAPACITY_dependency.OnChanged += MVPT_CAPACITY_OnChanged;
            MVPT_CAPACITY_dependency.Start();

            MVPT_COLONY_dependency = new SqlTableDependency<MVPT_COLONY>(_connectionString, "MVPT_COLONY");
            MVPT_COLONY_dependency.OnChanged += MVPT_COLONY_OnChanged;
            MVPT_COLONY_dependency.Start();

            MVPT_DSWITCH_dependency = new SqlTableDependency<MVPT_DSWITCH>(_connectionString, "MVPT_DSWITCH");
            MVPT_DSWITCH_dependency.OnChanged += MVPT_DSWITCH_OnChanged;
            MVPT_DSWITCH_dependency.Start();

            MVPT_DSWITCH_PADSUB_dependency = new SqlTableDependency<MVPT_DSWITCH_PADSUB>(_connectionString, "MVPT_DSWITCH_PADSUB");
            MVPT_DSWITCH_PADSUB_dependency.OnChanged += MVPT_DSWITCH_PADSUB_OnChanged;
            MVPT_DSWITCH_PADSUB_dependency.Start();

            MVPT_EARTH_dependency = new SqlTableDependency<MVPT_EARTH>(_connectionString, "MVPT_EARTH");
            MVPT_EARTH_dependency.OnChanged += MVPT_EARTH_OnChanged;
            MVPT_EARTH_dependency.Start();

            MVPT_FAULTDETECTOR_dependency = new SqlTableDependency<MVPT_FAULTDETECTOR>(_connectionString, "MVPT_FAULTDETECTOR");
            MVPT_FAULTDETECTOR_dependency.OnChanged += MVPT_FAULTDETECTOR_OnChanged;
            MVPT_FAULTDETECTOR_dependency.Start();

            MVPT_FRONTAGE_dependency = new SqlTableDependency<MVPT_FRONTAGE>(_connectionString, "MVPT_FRONTAGE");
            MVPT_FRONTAGE_dependency.OnChanged += MVPT_FRONTAGE_OnChanged;
            MVPT_FRONTAGE_dependency.Start();

            MVPT_FUSECUTOUT_dependency = new SqlTableDependency<MVPT_FUSECUTOUT>(_connectionString, "MVPT_FUSECUTOUT");
            MVPT_FUSECUTOUT_dependency.OnChanged += MVPT_FUSECUTOUT_OnChanged;
            MVPT_FUSECUTOUT_dependency.Start();

            MVPT_HEADER_dependency = new SqlTableDependency<MVPT_HEADER>(_connectionString, "MVPT_HEADER");
            MVPT_HEADER_dependency.OnChanged += MVPT_HEADER_OnChanged;
            MVPT_HEADER_dependency.Start();

            MVPT_HVSUBSTATIONTRANS_dependency = new SqlTableDependency<MVPT_HVSUBSTATIONTRANS>(_connectionString, "MVPT_HVSUBSTATIONTRANS");
            MVPT_HVSUBSTATIONTRANS_dependency.OnChanged += MVPT_HVSUBSTATIONTRAN_OnChanged;
            MVPT_HVSUBSTATIONTRANS_dependency.Start();

            MVPT_JUMPER_dependency = new SqlTableDependency<MVPT_JUMPER>(_connectionString, "MVPT_JUMPER");
            MVPT_JUMPER_dependency.OnChanged += MVPT_JUMPER_OnChanged;
            MVPT_JUMPER_dependency.Start();

            MVPT_MOF_dependency = new SqlTableDependency<MVPT_MOF>(_connectionString, "MVPT_MOF");
            MVPT_MOF_dependency.OnChanged += MVPT_MOF_OnChanged;
            MVPT_MOF_dependency.Start();

            MVPT_OUTGOING_dependency = new SqlTableDependency<MVPT_OUTGOING>(_connectionString, "MVPT_OUTGOING");
            MVPT_OUTGOING_dependency.OnChanged += MVPT_OUTGOING_OnChanged;
            MVPT_OUTGOING_dependency.Start();

            MVPT_PADSUBSTATIONTRANS_dependency = new SqlTableDependency<MVPT_PADSUBSTATIONTRANS>(_connectionString, "MVPT_PADSUBSTATIONTRANS");
            MVPT_PADSUBSTATIONTRANS_dependency.OnChanged += MVPT_PADSUBSTATIONTRAN_OnChanged;
            MVPT_PADSUBSTATIONTRANS_dependency.Start();

            MVPT_PLANTSCATTER_dependency = new SqlTableDependency<MVPT_PLANTSCATTER>(_connectionString, "MVPT_PLANTSCATTER");
            MVPT_PLANTSCATTER_dependency.OnChanged += MVPT_PLANTSCATTER_OnChanged;
            MVPT_PLANTSCATTER_dependency.Start();

            MVPT_POLE_dependency = new SqlTableDependency<MVPT_POLE>(_connectionString, "MVPT_POLE");
            MVPT_POLE_dependency.OnChanged += MVPT_POLE_OnChanged;
            MVPT_POLE_dependency.Start();

            MVPT_POLESUBSTATION_dependency = new SqlTableDependency<MVPT_POLESUBSTATION>(_connectionString, "MVPT_POLESUBSTATION");
            MVPT_POLESUBSTATION_dependency.OnChanged += MVPT_POLESUBSTATION_OnChanged;
            MVPT_POLESUBSTATION_dependency.Start();

            MVPT_SECTIONALIZER_dependency = new SqlTableDependency<MVPT_SECTIONALIZER>(_connectionString, "MVPT_SECTIONALIZER");
            MVPT_SECTIONALIZER_dependency.OnChanged += MVPT_SECTIONALIZER_OnChanged;
            MVPT_SECTIONALIZER_dependency.Start();

            MVPT_TOMB_dependency = new SqlTableDependency<MVPT_TOMB>(_connectionString, "MVPT_TOMB");
            MVPT_TOMB_dependency.OnChanged += MVPT_TOMB_OnChanged;
            MVPT_TOMB_dependency.Start();

            NEWCUSTOMERREQUESTS_dependency = new SqlTableDependency<NEWCUSTOMERREQUESTS>(_connectionString, "NEWCUSTOMERREQUESTS");
            NEWCUSTOMERREQUESTS_dependency.OnChanged += NEWCUSTOMERREQUEST_OnChanged;
            NEWCUSTOMERREQUESTS_dependency.Start();

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            userThread.Start();


        }

        private void CONECTION_OnChanged(object sender, RecordChangedEventArgs<CONECTION> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "CONECTION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "CONECTION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "CONECTION", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void DESCRIPTION_OnChanged(object sender, RecordChangedEventArgs<DESCRIPTION> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "DESCRIPTION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "DESCRIPTION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "DESCRIPTION", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPG_LVTABLET_OnChanged(object sender, RecordChangedEventArgs<LVPG_LVTABLET> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPG_LVTABLET", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPG_LVTABLET", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPG_LVTABLET", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPL_BUSBAR_OnChanged(object sender, RecordChangedEventArgs<LVPL_BUSBAR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_BUSBAR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_BUSBAR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_BUSBAR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPL_OVHLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_OVHLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPL_SELFOVHLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_SELFOVHLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPL_SERVICECABLE_OnChanged(object sender, RecordChangedEventArgs<LVPL_SERVICECABLE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SERVICECABLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SERVICECABLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_SERVICECABLE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPL_UGLINE_OnChanged(object sender, RecordChangedEventArgs<LVPL_UGLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_UGLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_UGLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPL_UGLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_AUTOKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_AUTOKEY> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AUTOKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AUTOKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AUTOKEY", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_AVENUELUSTR_OnChanged(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_AVENUELUSTRPOLE_OnChanged(object sender, RecordChangedEventArgs<LVPT_AVENUELUSTRPOLE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTRPOLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTRPOLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_AVENUELUSTRPOLE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_CAPACITOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_CAPACITOR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CAPACITOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CAPACITOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CAPACITOR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_CONTACTOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_CONTACTOR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CONTACTOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CONTACTOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_CONTACTOR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_EARTH_OnChanged(object sender, RecordChangedEventArgs<LVPT_EARTH> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_EARTH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_EARTH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_EARTH", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_FORKBOX_OnChanged(object sender, RecordChangedEventArgs<LVPT_FORKBOX> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FORKBOX", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FORKBOX", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FORKBOX", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_FUSEKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_FUSEKEY> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FUSEKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FUSEKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_FUSEKEY", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_HEADER_OnChanged(object sender, RecordChangedEventArgs<LVPT_HEADER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_HEADER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_HEADER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_HEADER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_INSULATOR_OnChanged(object sender, RecordChangedEventArgs<LVPT_INSULATOR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_INSULATOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_INSULATOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_INSULATOR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_JOINT_OnChanged(object sender, RecordChangedEventArgs<LVPT_JOINT> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JOINT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JOINT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JOINT", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_JUMPER_OnChanged(object sender, RecordChangedEventArgs<LVPT_JUMPER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JUMPER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JUMPER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_JUMPER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_PARTNERPLAQUE_OnChanged(object sender, RecordChangedEventArgs<LVPT_PARTNERPLAQUE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_PARTNERPLAQUE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_PARTNERPLAQUE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_PARTNERPLAQUE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_POLE_OnChanged(object sender, RecordChangedEventArgs<LVPT_POLE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_POLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_POLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_POLE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void LVPT_SELFKEY_OnChanged(object sender, RecordChangedEventArgs<LVPT_SELFKEY> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_SELFKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_SELFKEY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "LVPT_SELFKEY", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPG_HVSUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPG_HVSUBSTATION> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_HVSUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_HVSUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_HVSUBSTATION", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPG_LAND_OnChanged(object sender, RecordChangedEventArgs<MVPG_LAND> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_LAND", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_LAND", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_LAND", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPG_PADSUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATION> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATION", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPG_PADSUBSTATIONQUBIC_OnChanged(object sender, RecordChangedEventArgs<MVPG_PADSUBSTATIONQUBIC> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATIONQUBIC", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATIONQUBIC", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPG_PADSUBSTATIONQUBIC", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPL_BUSBAR_HVSUB_OnChanged(object sender, RecordChangedEventArgs<MVPL_BUSBAR_HVSUB> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_HVSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_HVSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_HVSUB", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPL_BUSBAR_PADSUB_OnChanged(object sender, RecordChangedEventArgs<MVPL_BUSBAR_PADSUB> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_PADSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_PADSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_BUSBAR_PADSUB", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPL_OVHLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_OVHLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_OVHLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPL_SELFOVHLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_SELFOVHLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_SELFOVHLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPL_UGLINE_OnChanged(object sender, RecordChangedEventArgs<MVPL_UGLINE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_UGLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_UGLINE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPL_UGLINE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_AUTOBOOSTER_OnChanged(object sender, RecordChangedEventArgs<MVPT_AUTOBOOSTER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTOBOOSTER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTOBOOSTER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTOBOOSTER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_AUTORECLOSER_OnChanged(object sender, RecordChangedEventArgs<MVPT_AUTORECLOSER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTORECLOSER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTORECLOSER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_AUTORECLOSER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_CABLEHEAD_OnChanged(object sender, RecordChangedEventArgs<MVPT_CABLEHEAD> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEHEAD", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEHEAD", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEHEAD", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_CABLEJOINT_OnChanged(object sender, RecordChangedEventArgs<MVPT_CABLEJOINT> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEJOINT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEJOINT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CABLEJOINT", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_CAPACITY_OnChanged(object sender, RecordChangedEventArgs<MVPT_CAPACITY> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CAPACITY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CAPACITY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_CAPACITY", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_COLONY_OnChanged(object sender, RecordChangedEventArgs<MVPT_COLONY> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_COLONY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_COLONY", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_COLONY", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_DSWITCH_OnChanged(object sender, RecordChangedEventArgs<MVPT_DSWITCH> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_DSWITCH_PADSUB_OnChanged(object sender, RecordChangedEventArgs<MVPT_DSWITCH_PADSUB> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH_PADSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH_PADSUB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_DSWITCH_PADSUB", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_EARTH_OnChanged(object sender, RecordChangedEventArgs<MVPT_EARTH> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_EARTH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_EARTH", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_EARTH", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_FAULTDETECTOR_OnChanged(object sender, RecordChangedEventArgs<MVPT_FAULTDETECTOR> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FAULTDETECTOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FAULTDETECTOR", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FAULTDETECTOR", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_FRONTAGE_OnChanged(object sender, RecordChangedEventArgs<MVPT_FRONTAGE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FRONTAGE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FRONTAGE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FRONTAGE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_FUSECUTOUT_OnChanged(object sender, RecordChangedEventArgs<MVPT_FUSECUTOUT> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FUSECUTOUT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FUSECUTOUT", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_FUSECUTOUT", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_HEADER_OnChanged(object sender, RecordChangedEventArgs<MVPT_HEADER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HEADER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HEADER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HEADER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_HVSUBSTATIONTRAN_OnChanged(object sender, RecordChangedEventArgs<MVPT_HVSUBSTATIONTRANS> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HVSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HVSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_HVSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_JUMPER_OnChanged(object sender, RecordChangedEventArgs<MVPT_JUMPER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_JUMPER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_JUMPER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_JUMPER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_MOF_OnChanged(object sender, RecordChangedEventArgs<MVPT_MOF> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_MOF", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_MOF", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_MOF", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_OUTGOING_OnChanged(object sender, RecordChangedEventArgs<MVPT_OUTGOING> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_OUTGOING", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_OUTGOING", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_OUTGOING", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_PADSUBSTATIONTRAN_OnChanged(object sender, RecordChangedEventArgs<MVPT_PADSUBSTATIONTRANS> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PADSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PADSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PADSUBSTATIONTRANS", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_PLANTSCATTER_OnChanged(object sender, RecordChangedEventArgs<MVPT_PLANTSCATTER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PLANTSCATTER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PLANTSCATTER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_PLANTSCATTER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_POLE_OnChanged(object sender, RecordChangedEventArgs<MVPT_POLE> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLE", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLE", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_POLESUBSTATION_OnChanged(object sender, RecordChangedEventArgs<MVPT_POLESUBSTATION> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLESUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLESUBSTATION", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_POLESUBSTATION", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_SECTIONALIZER_OnChanged(object sender, RecordChangedEventArgs<MVPT_SECTIONALIZER> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_SECTIONALIZER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_SECTIONALIZER", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_SECTIONALIZER", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void MVPT_TOMB_OnChanged(object sender, RecordChangedEventArgs<MVPT_TOMB> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_TOMB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_TOMB", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "MVPT_TOMB", e.ChangeType);
                        }
                        break;
                }
            }
        }

        private void NEWCUSTOMERREQUEST_OnChanged(object sender, RecordChangedEventArgs<NEWCUSTOMERREQUESTS> e)
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
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "NEWCUSTOMERREQUESTS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Insert:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Insert"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "NEWCUSTOMERREQUESTS", e.ChangeType);
                        }
                        break;
                    case ChangeType.Update:
                        StatusBox.Invoke(new Action(() => StatusBox.Text = "Update"));
                        try
                        {
                            Console.WriteLine("OBJECTID: " + changedEntity.OBJECTID);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Excption: ", error);
                        }
                        finally
                        {
                            LoadCollectionData(changedEntity.OBJECTID, "NEWCUSTOMERREQUESTS", e.ChangeType);
                        }
                        break;
                }
            }
        }


        private static void Counter()
        {
            aTimer = new System.Timers.Timer();

            aTimer.Interval = 3000;
            aTimer.Elapsed += PutOnBus;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void PutOnBus(object sender = null, ElapsedEventArgs e = null)
        {
            Console.WriteLine("Hello :)");
            // Configure Esb Connection
            Sender senderr = new Sender("172.30.10.130:9092", "anacav", "@n@cav#001", "anacav-gis-broker-event-group");
            senderr.Timeout = 5000;

            // Create Event
            EventMessage evnt = new EventMessage(new HeaderPart(Verbs.created, "test", "anacav"));

            var Result_Json = JsonConvert.SerializeObject(Main_List, Formatting.Indented);
            evnt.PayLoad = new PayloadPart();
            evnt.PayLoad.Data = Result_Json;

            // Send event and wait 5 seconds for delivery report
            Boolean result = senderr.SendEvent(evnt, "Gis-Test");

            Console.WriteLine("Bus Result:", result);
        }


        private void LoadCollectionData(int ID, string table_name, ChangeType ctype = ChangeType.None)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT * FROM [{0}] WHERE OBJECTID = {1}", table_name, ID);
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var Table_List = new List<Dictionary<string, object>>();
                        var Changed_Data = new Dictionary<string, object>();
                        var Table_Attributes = new Dictionary<string, object>();

                        Table_Attributes.Add("Change_type", ctype.ToString());

                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        Table_Attributes.Add("TimeStamp", unixTimestamp);

                        while (sqlDataReader.Read())
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                string key = sqlDataReader.GetName(i).ToString();
                                var value = sqlDataReader.GetValue(i);
                                Changed_Data.Add(key, value);

                            }
                            Table_Attributes.Add("Data", Changed_Data);
                            Table_List.Add(Table_Attributes);

                        }

                        Main_List.Add(table_name, Table_List);
                        var Result_Json = JsonConvert.SerializeObject(Main_List, Formatting.Indented);
                        System.IO.File.WriteAllText("./data.txt", Result_Json.ToString());
                        if (Main_List.Count() > 200)
                        {
                            aTimer.Stop();
                            PutOnBus();
                            Main_List.Clear();
                            aTimer.Start();
                        }
                    }
                }
            }
        }



    }

}
