namespace logic.objects
{
    public class Pet
    {
        public string Name { get; set; }
        public AnimalType Type { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }

        public Pet(string name, AnimalType type, int age, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (!Enum.IsDefined(typeof(AnimalType), type))
            {
                throw new ArgumentException("Unknown animal type.", nameof(type));
            }
            if (age < 0)
            {
                throw new ArgumentException("Age cannot be negative.", nameof(age));
            }
            Name = name;
            Type = type;
            Age = age;
            Description = description;
        }
    }
}