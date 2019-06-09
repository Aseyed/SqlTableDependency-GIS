using System;
public class MVPT_AUTOBOOSTER
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string Address { get; set; }
    public string Constructor_Name { get; set; }
    public string SerialNo { get; set; }
    public string Model { get; set; }
    public int? Deploy_Kind { get; set; }
    public int? Config_Type { get; set; }
    public int? Config_Point_Cnt { get; set; }
    public decimal? Up_Current_Config { get; set; }
    public decimal? Down_Current_Config { get; set; }
    public int? Connect_Status { get; set; }
    public decimal? Max_Voltage_Up { get; set; }
    public decimal? Max_Voltage_Down { get; set; }
    public decimal? Flow_Range { get; set; }
    public decimal? Max_Flow_A { get; set; }
    public decimal? Config_Range_A { get; set; }
    public decimal? Max_Flow_B { get; set; }
    public decimal? Config_Range_B { get; set; }
    public decimal? Max_Flow_C { get; set; }
    public decimal? Config_Range_C { get; set; }
    public decimal? Max_Flow_D { get; set; }
    public decimal? Config_Range_D { get; set; }
    public int? Connect_Kind { get; set; }
    public int? Install_Year { get; set; }
    public int? Constructor_Year { get; set; }
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
