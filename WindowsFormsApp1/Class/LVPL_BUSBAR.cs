using System;
public class LVPL_BUSBAR
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string HPLID { get; set; }
    public string HDLID { get; set; }
    public decimal? px { get; set; }
    public decimal? py { get; set; }
    public decimal? cx { get; set; }
    public decimal? cy { get; set; }
    public decimal? Length { get; set; }
    public int? Genus { get; set; }
    public int? Voltage_Level { get; set; }
    public int? Area_Dimension { get; set; }
    public decimal? Angel { get; set; }
    public int? U_ID { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public string VerID { get; set; }
    public string GUID { get; set; }
    public string HV_PostUID { get; set; }
    public string Tablet_LocationID { get; set; }
    public Guid? GlobalID2 { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}


