using System;
public class MVPG_PADSUBSTATION
{
    public int OBJECTID { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string Post_Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string FeederName { get; set; }
    public int? Constructor_Year { get; set; }
    public int? Post_Structure_Kind { get; set; }
    public int? Post_FaceStatus_Kind { get; set; }
    public int? Post_Owner_Kind { get; set; }
    public int? Post_Kind { get; set; }
    public int? Post_possession_Kind { get; set; }
    public int? Trans_Cnt { get; set; }
    public int? AirCondition_Kind { get; set; }
    public decimal? Dim_Post_Length { get; set; }
    public decimal? Dim_Post_Width { get; set; }
    public decimal? Dim_Post_height { get; set; }
    public int? Capacity_Extended { get; set; }
    public int? Trans_Extended { get; set; }
    public int? layer_Cnt { get; set; }
    public int? Qubic_Cnt { get; set; }
    public int? Qubic_Extended_Cnt { get; set; }
    public decimal? x { get; set; }
    public decimal? y { get; set; }
    public int? U_ID { get; set; }
    public string U_Date { get; set; }
    public string U_Time { get; set; }
    public string VerID { get; set; }
    public string GUID { get; set; }
    public Guid? GlobalID2 { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public int? Design { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
