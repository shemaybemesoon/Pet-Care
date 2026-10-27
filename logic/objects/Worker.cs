namespace logic.objects
{
    public class Worker
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MailAddress { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();

        public Worker(string firstName, string middleName, string lastName, string phone, string mailAddress, int age, int experience)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(middleName))
            {
                throw new ArgumentException("Middle name cannot be null or empty.", nameof(middleName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Phone cannot be null or empty.", nameof(phone));
            }
            if (string.IsNullOrWhiteSpace(mailAddress))
            {
                throw new ArgumentException("Mail address cannot be null or empty.", nameof(mailAddress));
            }
            if (age < 0)
            {
                throw new ArgumentException("Age cannot be negative.", nameof(age));
            }
            if (experience < 0)
            {
                throw new ArgumentException("Experience cannot be negative.", nameof(experience));
            }
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Phone = phone;
            MailAddress = mailAddress;
            Age = age;
            Experience = experience;
        }
    }
}