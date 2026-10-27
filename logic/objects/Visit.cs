namespace logic.objects
{
    public class Visit
    {
        public DateTime StartVisitTime { get; set; }
        public DateTime EndVisitTime { get; set; }
        public VisitStatus Status { get; set; }

        public Pet Pet { get; set; }
        public Owner Owner { get; set; }
        public Worker Worker { get; set; }
        public Procedure Procedure { get; set; }
        public Guid ID { get; } = Guid.NewGuid();

        public Visit(DateTime startVisitTime, DateTime endVisitTime, VisitStatus status, Pet pet, Owner owner, Worker worker, Procedure procedure)
        {
            if (startVisitTime >= endVisitTime)
            {
                throw new ArgumentException("Start visit time must be earlier than end visit time.");
            }
            if (!Enum.IsDefined(typeof(VisitStatus), status))
            {
                throw new ArgumentException("Unknown visit status.", nameof(status));
            }
            StartVisitTime = startVisitTime;
            EndVisitTime = endVisitTime;
            Status = status;
            Pet = pet ?? throw new ArgumentNullException(nameof(pet));
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Worker = worker ?? throw new ArgumentNullException(nameof(worker));
            Procedure = procedure ?? throw new ArgumentNullException(nameof(procedure));
        }
    }
}