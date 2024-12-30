namespace API1;

public class StateModel
{
    public int? StateID { get; set; }
    public string StateName { get; set; }
    public string StateCode { get; set; }
    public int CountryID { get; set; }
    
}

public class CountryDropDownModel
{
    public int CountryID { get; set; }
    public string CountryName { get; set; }
}