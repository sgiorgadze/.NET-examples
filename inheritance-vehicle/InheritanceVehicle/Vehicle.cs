namespace InheritanceVehicle
{
    public class Vehicle
    {
        public Vehicle(string name, int maxSpeed)
        {
            this.Name = name;
            this.MaxSpeed = maxSpeed;
        }

        public int MaxSpeed { get; }

        protected string Name { get; set; }
    }
}
