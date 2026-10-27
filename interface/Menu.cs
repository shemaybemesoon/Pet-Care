using System.Globalization;
using logic.objects;

namespace interfacer
{
    public class Menu
    {
        public bool Running = true;
        public List<Visit> Visits = new List<Visit>();
        public List<Pet> Pets = new List<Pet>();
        public List<Worker> Workers = new List<Worker>();
        public List<Owner> Owners = new List<Owner>();
        private readonly logic.Service.VisitService _visitService = new();

        public string UI()
        {
            return "\n-= Menu =-\n" +
                    "- [C]/Create\n" +
                    "  ^ - [V]/Visit\n" +
                    "  ^ - [P]/Pet\n" +
                    "  ^ - [W]/Worker\n" +
                    "  ^ - [O]/Owner\n" +
                    "\n" +
                    "- [R]/Read\n" +
                    "  ^ - [V]/Visits\n" +
                    "  ^ - [P]/Pets\n" +
                    "  ^ - [W]/Workers\n" +
                    "  ^ - [O]/Owners\n" +
                    "\n" +
                    "- [U]/Update\n" +
                    "  ^ - [V]/Visits\n" +
                    "  ^ - [P]/Pets\n" +
                    "  ^ - [W]/Workers\n" +
                    "  ^ - [O]/Owners\n" +
                    "\n" +
                    "- [D]/Delete\n" +
                    "  ^ - [V]/Visits\n" +
                    "  ^ - [P]/Pets\n" +
                    "  ^ - [W]/Workers\n" +
                    "  ^ - [O]/Owners\n" +
                    "\n" +
                    "- [M]/Menu\n" +
                    "- [Q]/Quit\n" +
                    ". . .";
        }
        public void Redirector(string selector)
        {
            if (selector == null)
            {
                return;
            }
            switch (selector.ToUpper())
            {
                case "Q":
                {
                    Console.WriteLine("\nLeaving . . . ");
                    Running = false;
                    break;
                }
                case "M":
                {
                    Console.WriteLine(UI());
                    break;
                }
                case "C":
                {
                    Console.WriteLine(" > ");
                    switch (Console.ReadLine()?.ToUpper())
                    {
                        case "V":
                        {
                            CreateVisit();
                            break;
                        }
                        case "P":
                        {
                            CreatePet();
                            break;
                        }
                        case "W":
                        {
                            CreateWorker();
                            break;
                        }
                        case "O":
                        {
                            CreateOwner();
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unknown input. Returning back");
                            break;
                        }
                    }
                    break;
                }
                case "R":
                {
                    Console.WriteLine(" > ");
                    switch (Console.ReadLine()?.ToUpper())
                    {
                        case "V":
                        {
                            ReadVisit();
                            break;
                        }
                        case "P":
                        {
                            ReadPet();
                            break;
                        }
                        case "W":
                        {
                            ReadWorker();
                            break;
                        }
                        case "O":
                        {
                            ReadOwner();
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unknown input. Returning back");
                            break;
                        }
                    }
                    break;
                }
                case "U":
                {
                    Console.WriteLine(" > ");
                    switch (Console.ReadLine()?.ToUpper())
                    {
                        case "V":
                        {
                            UpdateVisit();
                            break;
                        }
                        case "P":
                        {
                            UpdatePet();
                            break;
                        }
                        case "W":
                        {
                            UpdateWorker();
                            break;
                        }
                        case "O":
                        {
                            UpdateOwner();
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unknown input. Returning back");
                            break;
                        }
                    }
                    break;
                }
                case "D":
                {
                    Console.WriteLine(" > ");
                    switch (Console.ReadLine()?.ToUpper())
                    {
                        case "V":
                        {
                            DeleteVisit();
                            break;
                        }
                        case "P":
                        {
                            DeletePet();
                            break;
                        }
                        case "W":
                        {
                            DeleteWorker();
                            break;
                        }
                        case "O":
                        {
                            DeleteOwner();
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unknown input. Returning back");
                            break;
                        }
                    }
                    break;
                }
                default:
                {
                    Console.WriteLine("Unknown input. Try again");
                    break;
                }

            }   
        }

        private void CreateVisit()
        {
            if (Pets.Count == 0 || Owners.Count == 0 || Workers.Count == 0)
            {
                Console.WriteLine("Create at least one pet, owner, and worker before creating a visit.");
                return;
            }

            var petIndex = ReadSelection(Pets, "Select a pet", pet => $"{pet.ID} | {pet.Name} ({pet.Type})");
            if (petIndex is null) return;

            var ownerIndex = ReadSelection(Owners, "Select an owner", owner => $"{owner.ID} | {owner.FirstName} {owner.LastName}");
            if (ownerIndex is null) return;

            var workerIndex = ReadSelection(Workers, "Select a worker", worker => $"{worker.ID} | {worker.FirstName} {worker.LastName}");
            if (workerIndex is null) return;

            var procedureType = ReadEnumChoice<ProcedureType>("Select a procedure");
            if (procedureType is null) return;

            var description = ReadRequiredValue("Procedure description");
            if (description is null) return;

            Console.Write("Procedure cost: ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.CurrentCulture, out var cost) || cost < 0)
            {
                Console.WriteLine("Invalid cost. Visit was not created.");
                return;
            }

            var startVisitTime = ReadVisitTime("Start time (yyyy-MM-dd HH:mm)");
            if (startVisitTime is null) return;

            var endVisitTime = ReadVisitTime("End time (yyyy-MM-dd HH:mm)");
            if (endVisitTime is null) return;

            try
            {
                var visit = _visitService.CreateVisit(
                    Pets[petIndex.Value],
                    Owners[ownerIndex.Value],
                    Workers[workerIndex.Value],
                    procedureType.Value,
                    description,
                    cost,
                    startVisitTime.Value,
                    endVisitTime.Value);
                Visits.Add(visit);
                Console.WriteLine($"Visit created. ID: {visit.ID}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Visit was not created: {exception.Message}");
            }
        }
        private void CreatePet()
        {
            var name = ReadRequiredValue("Pet name");
            if (name is null) return;

            var type = ReadEnumChoice<AnimalType>("Select an animal type");
            if (type is null) return;

            var age = ReadNonNegativeInt("Pet age");
            if (age is null) return;

            Console.Write("Description (optional): ");
            var description = Console.ReadLine();

            try
            {
                var pet = new Pet(name, type.Value, age.Value, description);
                Pets.Add(pet);
                Console.WriteLine($"Pet created. ID: {pet.ID}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Pet was not created: {exception.Message}");
            }
        }
        private void CreateWorker()
        {
            var firstName = ReadRequiredValue("First name");
            if (firstName is null) return;
            var middleName = ReadRequiredValue("Middle name");
            if (middleName is null) return;
            var lastName = ReadRequiredValue("Last name");
            if (lastName is null) return;
            var phone = ReadRequiredValue("Phone");
            if (phone is null) return;
            var mailAddress = ReadRequiredValue("Mail address");
            if (mailAddress is null) return;
            var age = ReadNonNegativeInt("Worker age");
            if (age is null) return;
            var experience = ReadNonNegativeInt("Experience in years");
            if (experience is null) return;

            try
            {
                var worker = new Worker(firstName, middleName, lastName, phone, mailAddress, age.Value, experience.Value);
                Workers.Add(worker);
                Console.WriteLine($"Worker created. ID: {worker.ID}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Worker was not created: {exception.Message}");
            }
        }
        private void CreateOwner()
        {
            var firstName = ReadRequiredValue("First name");
            if (firstName is null) return;
            var lastName = ReadRequiredValue("Last name");
            if (lastName is null) return;
            var phone = ReadRequiredValue("Phone");
            if (phone is null) return;
            Console.Write("Mail address (optional): ");
            var mailAddress = Console.ReadLine();
            var age = ReadNonNegativeInt("Owner age");
            if (age is null) return;

            try
            {
                var owner = new Owner(firstName, lastName, phone, mailAddress, age.Value);
                Owners.Add(owner);
                Console.WriteLine($"Owner created. ID: {owner.ID}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Owner was not created: {exception.Message}");
            }
        }

        private static int? ReadSelection<T>(IReadOnlyList<T> items, string prompt, Func<T, string> format)
        {
            Console.WriteLine(prompt + ":");
            for (var index = 0; index < items.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {format(items[index])}");
            }

            Console.Write("Enter number: ");
            if (int.TryParse(Console.ReadLine(), out var selection) && selection >= 1 && selection <= items.Count)
            {
                return selection - 1;
            }

            Console.WriteLine("Invalid selection. No changes were made.");
            return null;
        }

        private static TEnum? ReadEnumChoice<TEnum>(string prompt) where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            Console.WriteLine(prompt + ":");
            for (var index = 0; index < values.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {values[index]}");
            }

            Console.Write("Enter number: ");
            if (int.TryParse(Console.ReadLine(), out var selection) && selection >= 1 && selection <= values.Length)
            {
                return values[selection - 1];
            }

            Console.WriteLine("Invalid selection.");
            return null;
        }

        private static string? ReadRequiredValue(string prompt)
        {
            Console.Write($"{prompt}: ");
            var value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("A value is required.");
            return null;
        }

        private static int? ReadNonNegativeInt(string prompt)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out var value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Enter a non-negative whole number.");
            return null;
        }

        private static DateTime? ReadVisitTime(string prompt)
        {
            Console.Write($"{prompt}: ");
            if (DateTime.TryParseExact(
                Console.ReadLine(),
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var value))
            {
                return value;
            }

            Console.WriteLine("Invalid date and time. Visit was not created.");
            return null;
        }

        private void ReadVisit()
        {
            if (Visits.Count == 0)
            {
                Console.WriteLine("No visits have been created.");
                return;
            }

            foreach (var visit in Visits)
            {
                Console.WriteLine(
                    $"ID: {visit.ID} | Status: {visit.Status} | " +
                    $"Start: {visit.StartVisitTime:yyyy-MM-dd HH:mm} | End: {visit.EndVisitTime:yyyy-MM-dd HH:mm} | " +
                    $"Pet: {visit.Pet.ID} ({visit.Pet.Name}) | " +
                    $"Owner: {visit.Owner.ID} ({visit.Owner.FirstName} {visit.Owner.LastName}) | " +
                    $"Worker: {visit.Worker.ID} ({visit.Worker.FirstName} {visit.Worker.LastName}) | " +
                    $"Procedure: {visit.Procedure.Type} - {visit.Procedure.Description} | Cost: {visit.Procedure.Cost}");
            }
        }
        private void ReadPet()
        {
            if (Pets.Count == 0)
            {
                Console.WriteLine("No pets have been created.");
                return;
            }

            foreach (var pet in Pets)
            {
                Console.WriteLine($"ID: {pet.ID} | Name: {pet.Name} | Type: {pet.Type} | Age: {pet.Age} | Description: {pet.Description ?? "-"}");
            }
        }
        private void ReadWorker()
        {
            if (Workers.Count == 0)
            {
                Console.WriteLine("No workers have been created.");
                return;
            }

            foreach (var worker in Workers)
            {
                Console.WriteLine($"ID: {worker.ID} | Name: {worker.FirstName} {worker.MiddleName} {worker.LastName} | Phone: {worker.Phone} | Email: {worker.MailAddress} | Age: {worker.Age} | Experience: {worker.Experience} years");
            }
        }
        private void ReadOwner()
        {
            if (Owners.Count == 0)
            {
                Console.WriteLine("No owners have been created.");
                return;
            }

            foreach (var owner in Owners)
            {
                Console.WriteLine($"ID: {owner.ID} | Name: {owner.FirstName} {owner.LastName} | Phone: {owner.Phone} | Email: {owner.MailAddress ?? "-"} | Age: {owner.Age}");
            }
        }

        private void UpdateVisit()
        {
            var index = ReadSelection(Visits, "Select a visit to update", visit => $"{visit.ID} | {visit.StartVisitTime:yyyy-MM-dd HH:mm} | {visit.Pet.Name} | {visit.Status}");
            if (index is null) return;

            var visit = Visits[index.Value];
            var pet = ReadUpdatedSelection(Pets, "Pet", current => $"{current.ID} | {current.Name} ({current.Type})", visit.Pet);
            if (pet is null) return;
            var owner = ReadUpdatedSelection(Owners, "Owner", current => $"{current.ID} | {current.FirstName} {current.LastName}", visit.Owner);
            if (owner is null) return;
            var worker = ReadUpdatedSelection(Workers, "Worker", current => $"{current.ID} | {current.FirstName} {current.LastName}", visit.Worker);
            if (worker is null) return;
            var procedureType = ReadUpdatedEnumChoice("Procedure type", visit.Procedure.Type);
            if (procedureType is null) return;
            var description = ReadUpdatedRequiredValue("Procedure description", visit.Procedure.Description);
            if (description is null) return;
            var cost = ReadUpdatedNonNegativeDecimal("Procedure cost", visit.Procedure.Cost);
            if (cost is null) return;
            var status = ReadUpdatedEnumChoice("Visit status", visit.Status);
            if (status is null) return;
            var startTime = ReadUpdatedVisitTime("Start time", visit.StartVisitTime);
            if (startTime is null) return;
            var endTime = ReadUpdatedVisitTime("End time", visit.EndVisitTime);
            if (endTime is null) return;

            try
            {
                var procedure = new Procedure(procedureType.Value, description, cost.Value);
                if (startTime.Value >= endTime.Value)
                {
                    Console.WriteLine("Start time must be earlier than end time. No changes were made.");
                    return;
                }

                visit.Pet = pet;
                visit.Owner = owner;
                visit.Worker = worker;
                visit.Procedure = procedure;
                visit.Status = status.Value;
                visit.StartVisitTime = startTime.Value;
                visit.EndVisitTime = endTime.Value;
                Console.WriteLine($"Visit updated. ID: {visit.ID}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Visit was not updated: {exception.Message}");
            }
        }
        private void UpdatePet()
        {
            var index = ReadSelection(Pets, "Select a pet to update", pet => $"{pet.ID} | {pet.Name} ({pet.Type})");
            if (index is null) return;

            var pet = Pets[index.Value];
            var name = ReadUpdatedRequiredValue("Name", pet.Name);
            if (name is null) return;
            var type = ReadUpdatedEnumChoice("Animal type", pet.Type);
            if (type is null) return;
            var age = ReadUpdatedNonNegativeInt("Age", pet.Age);
            if (age is null) return;
            var description = ReadUpdatedOptionalValue("Description", pet.Description);
            if (description is null) return;

            pet.Name = name;
            pet.Type = type.Value;
            pet.Age = age.Value;
            pet.Description = description;
            Console.WriteLine($"Pet updated. ID: {pet.ID}");
        }
        private void UpdateWorker()
        {
            var index = ReadSelection(Workers, "Select a worker to update", worker => $"{worker.ID} | {worker.FirstName} {worker.LastName}");
            if (index is null) return;

            var worker = Workers[index.Value];
            var firstName = ReadUpdatedRequiredValue("First name", worker.FirstName);
            if (firstName is null) return;
            var middleName = ReadUpdatedRequiredValue("Middle name", worker.MiddleName);
            if (middleName is null) return;
            var lastName = ReadUpdatedRequiredValue("Last name", worker.LastName);
            if (lastName is null) return;
            var phone = ReadUpdatedRequiredValue("Phone", worker.Phone);
            if (phone is null) return;
            var mailAddress = ReadUpdatedRequiredValue("Mail address", worker.MailAddress);
            if (mailAddress is null) return;
            var age = ReadUpdatedNonNegativeInt("Age", worker.Age);
            if (age is null) return;
            var experience = ReadUpdatedNonNegativeInt("Experience in years", worker.Experience);
            if (experience is null) return;

            worker.FirstName = firstName;
            worker.MiddleName = middleName;
            worker.LastName = lastName;
            worker.Phone = phone;
            worker.MailAddress = mailAddress;
            worker.Age = age.Value;
            worker.Experience = experience.Value;
            Console.WriteLine($"Worker updated. ID: {worker.ID}");
        }
        private void UpdateOwner()
        {
            var index = ReadSelection(Owners, "Select an owner to update", owner => $"{owner.ID} | {owner.FirstName} {owner.LastName}");
            if (index is null) return;

            var owner = Owners[index.Value];
            var firstName = ReadUpdatedRequiredValue("First name", owner.FirstName);
            if (firstName is null) return;
            var lastName = ReadUpdatedRequiredValue("Last name", owner.LastName);
            if (lastName is null) return;
            var phone = ReadUpdatedRequiredValue("Phone", owner.Phone);
            if (phone is null) return;
            var mailAddress = ReadUpdatedOptionalValue("Mail address", owner.MailAddress);
            if (mailAddress is null) return;
            var age = ReadUpdatedNonNegativeInt("Age", owner.Age);
            if (age is null) return;

            owner.FirstName = firstName;
            owner.LastName = lastName;
            owner.Phone = phone;
            owner.MailAddress = mailAddress;
            owner.Age = age.Value;
            Console.WriteLine($"Owner updated. ID: {owner.ID}");
        }

        private void DeleteVisit()
        {
            var index = ReadSelection(Visits, "Select a visit to delete", visit => $"{visit.ID} | {visit.StartVisitTime:yyyy-MM-dd HH:mm} | {visit.Pet.Name} | {visit.Status}");
            if (index is null) return;

            var visit = Visits[index.Value];
            if (!ConfirmDeletion($"visit for {visit.Pet.Name}", visit.ID)) return;
            Visits.RemoveAt(index.Value);
            Console.WriteLine($"Visit deleted. ID: {visit.ID}");
        }
        private void DeletePet()
        {
            var index = ReadSelection(Pets, "Select a pet to delete", pet => $"{pet.ID} | {pet.Name} ({pet.Type})");
            if (index is null) return;

            var pet = Pets[index.Value];
            if (!ConfirmDeletion($"pet {pet.Name}", pet.ID)) return;
            Pets.RemoveAt(index.Value);
            Console.WriteLine($"Pet deleted. ID: {pet.ID}");
        }
        private void DeleteWorker()
        {
            var index = ReadSelection(Workers, "Select a worker to delete", worker => $"{worker.ID} | {worker.FirstName} {worker.LastName}");
            if (index is null) return;

            var worker = Workers[index.Value];
            if (!ConfirmDeletion($"worker {worker.FirstName} {worker.LastName}", worker.ID)) return;
            Workers.RemoveAt(index.Value);
            Console.WriteLine($"Worker deleted. ID: {worker.ID}");
        }
        private void DeleteOwner()
        {
            var index = ReadSelection(Owners, "Select an owner to delete", owner => $"{owner.ID} | {owner.FirstName} {owner.LastName}");
            if (index is null) return;

            var owner = Owners[index.Value];
            if (!ConfirmDeletion($"owner {owner.FirstName} {owner.LastName}", owner.ID)) return;
            Owners.RemoveAt(index.Value);
            Console.WriteLine($"Owner deleted. ID: {owner.ID}");
        }

        private static string? ReadUpdatedRequiredValue(string prompt, string currentValue)
        {
            Console.Write($"{prompt} (current: {currentValue}; Enter to keep): ");
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return currentValue;
            }

            return value.Trim();
        }

        private static string? ReadUpdatedOptionalValue(string prompt, string? currentValue)
        {
            Console.Write($"{prompt} (current: {currentValue ?? "-"}; Enter to keep, - to clear): ");
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return currentValue ?? string.Empty;
            }

            return value.Trim() == "-" ? string.Empty : value.Trim();
        }

        private static TEnum? ReadUpdatedEnumChoice<TEnum>(string prompt, TEnum currentValue) where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            Console.WriteLine($"{prompt} (current: {currentValue}; Enter to keep):");
            for (var index = 0; index < values.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {values[index]}");
            }

            Console.Write("Enter number: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return input is null ? null : currentValue;
            }

            if (int.TryParse(input, out var selection) && selection >= 1 && selection <= values.Length)
            {
                return values[selection - 1];
            }

            Console.WriteLine("Invalid selection. No changes were made.");
            return null;
        }

        private static int? ReadUpdatedNonNegativeInt(string prompt, int currentValue)
        {
            Console.Write($"{prompt} (current: {currentValue}; Enter to keep): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return input is null ? null : currentValue;
            }

            if (int.TryParse(input, out var value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Enter a non-negative whole number. No changes were made.");
            return null;
        }

        private static decimal? ReadUpdatedNonNegativeDecimal(string prompt, decimal currentValue)
        {
            Console.Write($"{prompt} (current: {currentValue}; Enter to keep): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return input is null ? null : currentValue;
            }

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out var value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Enter a non-negative number. No changes were made.");
            return null;
        }

        private static DateTime? ReadUpdatedVisitTime(string prompt, DateTime currentValue)
        {
            Console.Write($"{prompt} (current: {currentValue:yyyy-MM-dd HH:mm}; Enter to keep): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return input is null ? null : currentValue;
            }

            if (DateTime.TryParseExact(
                input,
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var value))
            {
                return value;
            }

            Console.WriteLine("Invalid date and time. No changes were made.");
            return null;
        }

        private static T? ReadUpdatedSelection<T>(
            IReadOnlyList<T> items,
            string prompt,
            Func<T, string> format,
            T currentValue) where T : class
        {
            Console.WriteLine($"Select {prompt} (current: {format(currentValue)}; Enter to keep):");
            for (var index = 0; index < items.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {format(items[index])}");
            }

            Console.Write("Enter number: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return input is null ? null : currentValue;
            }

            if (int.TryParse(input, out var selection) && selection >= 1 && selection <= items.Count)
            {
                return items[selection - 1];
            }

            Console.WriteLine("Invalid selection. No changes were made.");
            return null;
        }

        private static bool ConfirmDeletion(string description, Guid id)
        {
            Console.Write($"Delete {description} (ID: {id})? Enter Y to confirm: ");
            return string.Equals(Console.ReadLine(), "Y", StringComparison.OrdinalIgnoreCase);
        }
    }
}