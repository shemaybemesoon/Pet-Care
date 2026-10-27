namespace logic.objects
{
    public class Owner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string? MailAddress { get; set; }
        public int Age { get; set; }

        public Owner(string firstName, string lastName, string phone, string? mailAddress, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Phone cannot be null or empty.", nameof(phone));
            }
            if (age < 0)
            {
                throw new ArgumentException("Age cannot be negative.", nameof(age));
            }

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            MailAddress = mailAddress ?? "";
            Age = age;
        }
    }
}