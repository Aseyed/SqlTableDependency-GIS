using System;
public class LVPT_EARTH
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string HPLID { get; set; }
    public string HDLID { get; set; }
    public int? Kind { get; set; }
    public int? ElectrodKind { get; set; }
    public int? ExecYear { get; set; }
    public int? ExecWay { get; set; }
    public decimal? Deep { get; set; }
    public decimal? Resistance { get; set; }
    public string MeasureDate { get; set; }
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
