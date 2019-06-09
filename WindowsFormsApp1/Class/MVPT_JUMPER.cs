using System;
public class MVPT_JUMPER
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public int? Status { get; set; }
    public string Address { get; set; }
    public int? Jumper_Connect_Kind { get; set; }
    public string Cut_Direction { get; set; }
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
