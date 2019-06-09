using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Anacav.Esb.Messaging;
using Anacav.Esb.Connection;

class Variables
{
    public System.Timers.Timer aTimer;
    public Dictionary<string, List<Dictionary<string, object>>> Main_List;

    public object Result_Json;

    public Variables()
    {


    }
    private void Counter()
    {
        aTimer = new System.Timers.Timer();

        aTimer.Interval = 3000;
        aTimer.Elapsed += PutOnBus;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    public void PutOnBus(object sender = null, ElapsedEventArgs e = null)
    {
        Console.WriteLine("Hello :)");
        // Configure Esb Connection
        Sender senderr = new Sender("172.30.10.130:9092", "anacav", "@n@cav#001", "anacav-gis-broker-event-group");
        senderr.Timeout = 5000;

        // Create Event
        EventMessage evnt = new EventMessage(new HeaderPart(Verbs.created, "test", "anacav"));


        evnt.PayLoad = new PayloadPart();
        evnt.PayLoad.Data = Result_Json;

        // Send event and wait 5 seconds for delivery report
        Boolean result = senderr.SendEvent(evnt, "Gis-Test");

        Console.WriteLine("Bus Result:", result);
    }
}

