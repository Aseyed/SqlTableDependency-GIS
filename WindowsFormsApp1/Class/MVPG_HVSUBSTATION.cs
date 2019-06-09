using System;
public class MVPG_HVSUBSTATION
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public string ICityNID { get; set; }
    public int? HyperNID { get; set; }
    public string DispaCode { get; set; }
    public int? Owner { get; set; }
    public string HVSUBSTATION_Name { get; set; }
    public string Address { get; set; }
    public int? Using_Year { get; set; }
    public int? Voltage1 { get; set; }
    public int? Voltage2 { get; set; }
    public int? Deployment_Kind { get; set; }
    public int? Current_Trans_Max { get; set; }
    public int? Expansion_Trans { get; set; }
    public int? Max_Feeder_Cnt { get; set; }
    public int? Feeder_Cnt { get; set; }
    public int? Capacity_Extend_Max { get; set; }
    public int? Private_Feeder_Cnt { get; set; }
    public int? Reserve_Feeder_Cnt { get; set; }
    public int? Expansion_Possibility { get; set; }
    public int? Feeder_Out_Possibility { get; set; }
    public double? Busbar_System { get; set; }
    public decimal? x { get; set; }
    public decimal? y { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public int? U_ID { get; set; }
    public string VerID { get; set; }
    public string GUID { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
