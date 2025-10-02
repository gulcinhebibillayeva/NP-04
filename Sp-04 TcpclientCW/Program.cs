using Sp_04_TcpclientCW;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;
var client = new TcpClient();
client.Connect(ip, port);
var stream = client.GetStream();
var bw = new BinaryWriter(stream);
var br = new BinaryReader(stream);

static void adding() {
    using (var db = new TpContext())
    {
        Console.WriteLine("Enter Car Name:");
        string carName = Console.ReadLine()!;

        Console.WriteLine("Enter Producer:");
        string producer = Console.ReadLine()!;

        Console.WriteLine("Enter Price:");
        int price = int.Parse(Console.ReadLine()!);

        Car car = new Car
        {

            CarName = carName,
            Producer = producer,
            Price = price
        };

        db.Cars.Add(car);
        db.SaveChanges();

        Console.WriteLine("Masin elave edildi");
    }
}

static void GetAllCars(List<Car> carsFromServer) {
    using (var db = new TpContext())
    {
        var cars = db.Cars.ToList();
        if (cars.Any())
        {
            foreach (var car in cars)
            {
                Console.WriteLine($"Id:{car.Id}\nName:{car.CarName}\nProducer{car.Producer}\nPrice:{car.Price}");
            }
        }
        else
        {
            Console.WriteLine("Masin tapilmasi");
        }

    }
}


static void DeleteCar() { 
    using var dbdel = new TpContext();
Console.WriteLine("Enter Car Id:");
    int iddel = int.Parse(Console.ReadLine()!);
var deleteCar = dbdel.Cars.FirstOrDefault(c => c.Id == iddel);
if (deleteCar != null)
{
    dbdel.Cars.Remove(deleteCar);
    dbdel.SaveChanges();
    Console.WriteLine("Masin siyahidan silindi");
}
else
{
    Console.WriteLine("Masin tapilmadi");
}}





    //update
    static void PostCars()
{
    using var dbupdate = new TpContext();
    Console.WriteLine("Enter Car Id:");
    int idup = int.Parse(Console.ReadLine()!);
    var carup = dbupdate.Cars.FirstOrDefault(c => c.Id == idup);
    if (carup is not null)
    {
        Console.WriteLine("Enter Car Name:");
        string carName = Console.ReadLine()!;
        carup.CarName = carName;

        Console.WriteLine("Enter Producer:");
        string producer = Console.ReadLine()!;
        carup.Producer = producer;

        Console.WriteLine("Enter Price:");
        int price = int.Parse(Console.ReadLine()!);
        carup.Price = price;
    }

}





Command command = null!;
string response = null!;
string str = null!;
while (true)
{
    Console.WriteLine("Write command name or help");
    str = Console.ReadLine()!.ToUpper();
    if (str == "HELP")
    {
        Console.WriteLine();
        Console.WriteLine("Command List:");
        Console.WriteLine(Command.Get);
        Console.WriteLine($"{Command.Post} ");
        Console.WriteLine($"{Command.Put} ");
        Console.WriteLine($"{Command.Delete} ");
        Console.ReadLine();
        Console.Clear();
    }
    var input = str.Split(' ');
    switch(input[0])
    {
        case Command.Get:
                command = new Command { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            response = br.ReadString();
            var carsFromServer = JsonSerializer.Deserialize<List<Car>>(response);
            if (carsFromServer != null && carsFromServer.Any())
            {
                carsFromServer.ForEach(car =>
                {
                    Console.WriteLine($"Id: {car.Id}");
                    Console.WriteLine($"Name: {car.CarName}");
                    Console.WriteLine($"Producer: {car.Producer}");
                    Console.WriteLine($"Price: {car.Price}");
                    
                });
            }
            else
            {
                Console.WriteLine("Masin tapilmadi");
            }
            break;

         

        case Command.Post:
           
            break;
        case Command.Put:
                break;

        case Command.Delete:
            break;

                }
    
}



//command = new Command { Text = input[0] };
//bw.Write(JsonSerializer.Serialize(command));
//response = br.ReadString();
//var carsFromServer = JsonSerializer.Deserialize<List<Car>>(response);