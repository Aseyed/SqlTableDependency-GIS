using System;

public class DESCRIPTION
{
    public int OBJECTID { get; set; }
    public string comment { get; set; }
    public string VerId { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public decimal? x { get; set; }
    public decimal? y { get; set; }
    public string GUID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
    public int? Request_Type { get; set; }
    public int? Object_Type { get; set; }
    public int? Organizational_unit { get; set; }
    public string Request_Date { get; set; }
    public string Request_Code { get; set; }
    public string Controller_User { get; set; }
    public int? Control_Result { get; set; }
    public string Control_Date { get; set; }
}
