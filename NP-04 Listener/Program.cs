
using Azure;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;
var listener = new TcpListener(ip, port);
listener.Start();
static void GetAllCars(BinaryWriter bw)
{
    using (var db = new CarContext())
    {
        var cars = db.Cars.ToList();
        if (cars.Any())
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car);
                var json = JsonSerializer.Serialize(cars);
                bw.Write(json);
            }
        }
        else
        {
            Console.WriteLine("Masin tapilmasi");
        }

    }
}

static void Post(Car car ,BinaryWriter bw)
{
    using (var db = new CarContext())
    {
        

        db.Cars.Add(car);
        db.SaveChanges();

        var json = JsonSerializer.Serialize(car);
        bw.Write(json);

        Console.WriteLine("Masin elave edildi");
    }
}



static void Put(Car UpdatedCar, BinaryWriter bw)
{
    using var db = new CarContext();

    var car = db.Cars.FirstOrDefault(c => c.Id == UpdatedCar.Id);
    if (car is not null)
    {
        car.Make = UpdatedCar.Make;
        car.Model = UpdatedCar.Model;
        car.Year = UpdatedCar.Year;
        car.HorsePower = UpdatedCar.HorsePower;
        db.SaveChanges();
        Console.WriteLine("Car updated");
    }
    else
    {
        Console.WriteLine("Car not found");
    }

}





static void DeleteCar(int carId, BinaryWriter bw)
{
    using var db = new CarContext();
   
    var deleteCar = db.Cars.FirstOrDefault(c => c.Id == carId);
    if (deleteCar != null)
    {
        db.Cars.Remove(deleteCar);
        db.SaveChanges();
        Console.WriteLine(" Car Removed");
    }
    else
    {
        Console.WriteLine("Car not found");
    }
}




while (true)
        {
            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();
            var bw = new BinaryWriter(stream);
            var br = new BinaryReader(stream);

            while (true)
            {
                var input = br.ReadString();
                Console.WriteLine(input);
                var command = JsonSerializer.Deserialize<Command>(input);
                switch (command.Text)
                {
                    case Command.Get:
                        GetAllCars(bw);

                        break;
                    case Command.Post:
                        Post(command.car, bw);

                        break;
                    case Command.Put:
                Put(command.car, bw);
                        break;
                    case Command.Delete:
                DeleteCar(command.CarId, bw);
                        break;
                }
            }
        }