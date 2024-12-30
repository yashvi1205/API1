namespace API;
public class CityModel
{
    public int CityID { get; set; }
    public string CityName { get; set; }
    public string PinCode { get; set; }
    public int CountryID { get; set; }
    public int StateID { get; set; }
    public string CountryName { get; set; }
    public string StateName { get; set; }
}
public class CountryDropDown
{
    public int CountryID { get; set; }
    public string CountryName { get; set; }
}

public class StateDropDownModel
{
    public int StateID { get; set; }
    public string StateName { get; set; }
}