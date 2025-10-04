// See https://aka.ms/new-console-template for more information
class Command
{
    public const string Get = "GET";
    public const string Post = "POST";
    public const string Put = "PUT";
    public const string Delete = "DELETE";

    public string? Text { get; set; }
    public Car? car { get; set; }
    public int CarId { get; set; }

}
