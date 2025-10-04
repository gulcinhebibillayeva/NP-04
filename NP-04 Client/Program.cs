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

string str = null!;
string response = null!;
Command command = null!;
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
    switch (input[0])
    {
        case Command.Get:
            command = new Command { Text = input[0] };
            bw.Write(JsonSerializer.Serialize(command));
            response = br.ReadString();
            var carList = JsonSerializer.Deserialize<List<Car>>(response);
            carList!.ForEach(car =>
            {
                Console.WriteLine($"Id: {car.Id}");
                Console.WriteLine($"Make: {car.Make}");
                Console.WriteLine($"Model: {car.Model}");
                Console.WriteLine($"Year: {car.Year.Year}");
                Console.WriteLine($"HorsePower: {car.HorsePower}");
                Console.WriteLine("------------------------");
            });

            break;
        case Command.Post:
            Console.WriteLine("Enter Car Make:");
            string make = Console.ReadLine()!;

            Console.WriteLine("Enter Model:");
            string model = Console.ReadLine()!;

            Console.WriteLine("Enter Year (YYYY):");
            int yearInput = int.Parse(Console.ReadLine()!);
            DateTime year = new DateTime(yearInput, 1, 1);

            Console.WriteLine("Enter HorsePower:");
            int horsepower = int.Parse(Console.ReadLine()!);

            Car carObject = new Car
            {
                Make = make,
                Model = model,
                Year = year,
                HorsePower = horsepower
            };

            command = new Command
            {
                Text = input[0],
                car = carObject
            };

            bw.Write(JsonSerializer.Serialize(command));
            response = br.ReadString();
            Console.WriteLine(response);
            break;
        case Command.Put:
            Console.WriteLine("Enter Car Id:");
            int id = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Car Name:");
            string carMake = Console.ReadLine()!;

            Console.WriteLine("Enter Producer:");
            string carmodel = Console.ReadLine()!;

            Console.WriteLine("Enter Year (YYYY):");
            int caryearInput = int.Parse(Console.ReadLine()!);
            DateTime caryear = new DateTime(caryearInput, 1, 1);

            Console.WriteLine("Enter Horsepower:");
            int carhorsepower = int.Parse(Console.ReadLine()!);


            Car UpdatedCar = new Car
            { 
                Id=id,
                Make=carMake,
                Model=carmodel,
                Year= caryear,
                HorsePower=carhorsepower

            };

            command = new Command
            {
                Text = input[0],
                car = UpdatedCar
            };
            bw.Write(JsonSerializer.Serialize(command));
            response = br.ReadString();
            Console.WriteLine(response);


            break;
        case Command.Delete:
            Console.WriteLine("Enter Car Id:");
            int iddel = int.Parse(Console.ReadLine()!);


            command = new Command
            {
                Text = input[0],
                CarId = iddel
            };
            bw.Write(JsonSerializer.Serialize(command));
            response = br.ReadString();
            Console.WriteLine(response);

            break;
    }
}