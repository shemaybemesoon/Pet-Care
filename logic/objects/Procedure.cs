namespace logic.objects
{
    public class Procedure
    {
        public ProcedureType Type { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public Procedure(ProcedureType type, string description, decimal cost)
        {
            if (!Enum.IsDefined(typeof(ProcedureType), type))
            {
                throw new ArgumentException("Unknown procedure type.", nameof(type));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }
            if (cost < 0)
            {
                throw new ArgumentException("Cost cannot be negative.", nameof(cost));
            }
            Type = type;
            Description = description;
            Cost = cost;
        }
    }
}