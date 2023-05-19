using System.Text.Json.Serialization;

namespace ImgWreck.Doc;
public class DriverLicense
{
    [JsonPropertyName("FIRST_NAME")]
    public string? FirstName { get; set; }

    [JsonPropertyName("LAST_NAME")]
    public string? LastName { get; set; }

    [JsonPropertyName("MIDDLE_NAME")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("DATE_OF_BIRTH")]
    public DateTime Dob { get; set; }

    [JsonPropertyName("ADDRESS")]
    public string? Address { get; set; }

    [JsonPropertyName("CITY_IN_ADDRESS")]
    public string? City { get; set; }

    [JsonPropertyName("STATE_IN_ADDRESS")]
    public string? State { get; set; }

    [JsonPropertyName("STATE_NAME")]
    public string? StateName { get; set; }

    [JsonPropertyName("ZIP_CODE_IN_ADDRESS")]
    public string? ZipCode { get; set; }

    [JsonPropertyName("DOCUMENT_NUMBER")]
    public string? DocumentNumber { get; set; }

    [JsonPropertyName("DATE_OF_ISSUE")]
    public DateTime IssuedDate { get; set; }

    [JsonPropertyName("EXPIRATION_DATE")]
    public DateTime ExpirationDate { get; set; }

    [JsonPropertyName("CLASS")]
    public string? Class { get; set; }
}
