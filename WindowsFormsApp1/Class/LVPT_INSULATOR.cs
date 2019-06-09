using System;
public class LVPT_INSULATOR
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string HPLID { get; set; }
    public string HDLID { get; set; }
    public int? Insulator_Composite { get; set; }
    public int? Constructor_Year { get; set; }
    public int? Install_Year { get; set; }
    public string Constructor_Name { get; set; }
    public int? Lustr_phase { get; set; }
    public int? Decor { get; set; }
    public int? PartnerR { get; set; }
    public int? PartnerS { get; set; }
    public int? PartnerT { get; set; }
    public int? Earth { get; set; }
    public string Earth_Meter_Date { get; set; }
    public decimal? Earth_Resistance { get; set; }
    public decimal? VoltageR { get; set; }
    public decimal? VoltageS { get; set; }
    public decimal? VoltageT { get; set; }
    public decimal? VoltageN { get; set; }
    public string Voltage_Meter_Date { get; set; }
    public decimal? x { get; set; }
    public decimal? y { get; set; }
    public int? U_ID { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public string VerID { get; set; }
    public decimal? Angel { get; set; }
    public string GUID { get; set; }
    public string FeederName { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
