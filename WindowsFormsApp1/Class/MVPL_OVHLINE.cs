using System;
public class MVPL_OVHLINE
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string Conductor_Constructor { get; set; }
    public int? Conductor_Composite { get; set; }
    public int? Circut_Max { get; set; }
    public int? Circut_Current_Cnt { get; set; }
    public int? Spacer { get; set; }
    public int? Install_Year { get; set; }
    public decimal? Length { get; set; }
    public decimal? Loadmax { get; set; }
    public decimal? LoadOnLine { get; set; }
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
    public int? CS { get; set; }
    public int? bd1 { get; set; }
    public int? bd2 { get; set; }
    public int? bd3 { get; set; }
    public string GUID { get; set; }
    public string FeederName { get; set; }
    public Guid? GlobalID2 { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
