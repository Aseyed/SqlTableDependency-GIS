using System;

public class LVPL_OVHLINE
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string HPLID { get; set; }
    public string HDLID { get; set; }
    public int? Decor_Kind { get; set; }
    public int? Conductor_Kind { get; set; }
    public int? Conductor_Year { get; set; }
    public int? Install_Year { get; set; }
    public int? FiberCnt { get; set; }
    public int? RArea { get; set; }
    public int? SArea { get; set; }
    public int? TArea { get; set; }
    public int? NArea { get; set; }
    public int? LustrArea { get; set; }
    public decimal? OnLineLoad { get; set; }
    public decimal? MaxLoad { get; set; }
    public decimal? Length { get; set; }
    public decimal? px { get; set; }
    public decimal? py { get; set; }
    public decimal? cx { get; set; }
    public decimal? cy { get; set; }
    public int? U_ID { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public string VerID { get; set; }
    public string PName { get; set; }
    public string CName { get; set; }
    public string Section_Name { get; set; }
    public string IPxyz { get; set; }
    public string ICxyz { get; set; }
    public string GUID { get; set; }
    public string FeederName { get; set; }
    public int? bd1 { get; set; }
    public int? bd2 { get; set; }
    public int? bd3 { get; set; }
    public int? CS { get; set; }
    public int? OHL_Composite { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
