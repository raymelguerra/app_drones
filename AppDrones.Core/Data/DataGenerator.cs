using AppDrones.Core.Models;
using Bogus;

namespace AppDrones.Core.Data
{
    public static class DataGenerator
    {
        public static IEnumerable<Drone> GenerateDrones(int count)
        {
            var faker = new Faker<Drone>()
                // .RuleFor(d => d.DroneId, f => f.UniqueIndex)
                .RuleFor(d => d.SerialNumber, f => f.Random.AlphaNumeric(10))
                .RuleFor(d => d.Model, f => f.PickRandom<Model>())
                .RuleFor(d => d.WeightLimit, f => f.Random.Double(1, 500))
                .RuleFor(d => d.BatteryCapacity, f => f.Random.Int(0, 100))
                .RuleFor(d => d.State, f => f.PickRandom<State>());

            return faker.Generate(count);
        }

        public static IEnumerable<Medication> GenerateMedications(int count, IEnumerable<Drone> drones)
        {
            var faker = new Faker<Medication>()
                .RuleFor(m => m.MedicationId, f => f.UniqueIndex)
                .RuleFor(m => m.Name, f => f.Random.Words(2))
                .RuleFor(m => m.Weight, f => f.Random.Int(1, 5))
                .RuleFor(m => m.Code, f => f.Random.AlphaNumeric(6).ToUpper())
                .RuleFor(m => m.Image, f => f.Image.ToString())
                .RuleFor(m => m.DroneId, f => 0);

            return faker.Generate(count);
        }
    }
}
