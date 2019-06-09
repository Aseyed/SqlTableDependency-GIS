using System;
public class MVPT_HEADER
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string DispaCode { get; set; }
    public string Trans_ID { get; set; }
    public int? TransCode { get; set; }
    public int? FeederKind { get; set; }
    public int? FeederStatus { get; set; }
    public int? UsingYear { get; set; }
    public int? OwnerCity1 { get; set; }
    public decimal? LoadPercentCity1 { get; set; }
    public int? OwnerCity2 { get; set; }
    public decimal? LoadPercentCity2 { get; set; }
    public int? OwnerCity3 { get; set; }
    public decimal? LoadPercentCity3 { get; set; }
    public int? OwnerCity4 { get; set; }
    public decimal? LoadPercentCity4 { get; set; }
    public int? Header_Kind { get; set; }
    public int? Install_Year { get; set; }
    public int? Install_Kind { get; set; }
    public int? Header_Size { get; set; }
    public int? Header_Earth { get; set; }
    public string Constructor_Name { get; set; }
    public int? Famous_Voltage { get; set; }
    public decimal? x { get; set; }
    public decimal? y { get; set; }
    public int? U_ID { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public string VerID { get; set; }
    public decimal? Angel { get; set; }
    public string GUID { get; set; }
    public string HVSUBUID { get; set; }
    public string FeederName { get; set; }
    public string HVSUBSTATION_Name { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
