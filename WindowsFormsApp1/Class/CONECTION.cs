using System;

public class CONECTION
{
    public int OBJECTID { get; set; }
    public Microsoft.SqlServer.Types.SqlGeometry SHAPE { get; set; }
    public string GUID { get; set; }
    public string VerId { get; set; }
    public decimal? cx { get; set; }
    public decimal? cy { get; set; }
    public decimal? px { get; set; }
    public decimal? py { get; set; }
    public int? CityNID { get; set; }
    public int? HyperNID { get; set; }
    public int? HeaderNID { get; set; }
    public string created_user { get; set; }
    public DateTime? created_date { get; set; }
    public string last_edited_user { get; set; }
    public DateTime? last_edited_date { get; set; }
}
