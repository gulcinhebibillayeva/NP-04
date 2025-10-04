// See https://aka.ms/new-console-template for more information
class Car
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Make { get; set; }
    public  int HorsePower { get; set; }



    public override string ToString()
    {
        return ($"Id:{Id}\nMaker:{Make}\nModel{Model}\nYear:{Year}\nHorsepower{HorsePower}");
    }
}