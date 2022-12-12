using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit1.LESSON7
{
    class ContactsManager
    {
        /*
           UNIT 1 LESSON 7 EX 1.
        */
        static void Main(string[] args)
        {
            new ContactsManager().Run();
        }

        private void Run()
        {
            while (true)
            {
                Console.WriteLine("Please, press:");
                Console.WriteLine("C - Create");
                Console.WriteLine("R - Retrieve");
                Console.WriteLine("U - Update");
                Console.WriteLine("D - Delete");
                Console.WriteLine("Q - Query all contacts (limit to 128 items)");

                var input = Console.ReadKey();

                Console.WriteLine("\n------------");

                switch (input.Key)
                {
                    case ConsoleKey.C:
                        CreateContact();
                        break;
                    case ConsoleKey.R:
                        RetrieveContact();
                        break;
                    case ConsoleKey.U:
                        UpdateContact();
                        break;
                    case ConsoleKey.D:
                        DeleteContact();
                        break;
                    case ConsoleKey.Q:
                        QueryAllContacts();
                        break;
                    default:
                        return;
                }

                Console.WriteLine("------------");
            }
        }

        private void CreateContact()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Console.WriteLine("Name: ");
                var name = Console.ReadLine();

                Console.WriteLine("Email: ");
                var email = Console.ReadLine();

                var contact = new Contact
                {
                    Name = name,
                    Email = email
                };

                // The Store method is responsible to register the "storing" intention in the session.
                // You can access the document right after the Store call was made,
                // even though the document was not saved to the database yet.
                session.Store(contact);

                Console.WriteLine($"New Contact ID {contact.Id}");

                // The SaveChanges method applies the registered actions in the session to the database.
                // when you call SaveChanges, all changes to those entities are sent to the database in a single remote call.
                session.SaveChanges();
            }
        }

        private void RetrieveContact()
        {
            Console.WriteLine("Enter the contact id: ");
            var id = Console.ReadLine();

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<Contact>(id);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Email: {contact.Email}");
            }
        }

        private void UpdateContact()
        {
            Console.WriteLine("Enter the contact id: ");
            var id = Console.ReadLine();

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<Contact>(id);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                Console.WriteLine($"Actual name: {contact.Name}");
                Console.WriteLine("New name: ");
                contact.Name = Console.ReadLine();

                Console.WriteLine($"Actual email: {contact.Email}");
                Console.WriteLine("New email address: ");
                contact.Email = Console.ReadLine();

                session.SaveChanges();
            }
        }

        private void DeleteContact()
        {
            Console.WriteLine("Enter the contact id: ");
            var id = Console.ReadLine();

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contact = session.Load<Contact>(id);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.");
                    return;
                }

                // The Delete method will delete the matching document on the server side.
                // You can provide the document ID or an entity instance.
                session.Delete(contact);
                session.SaveChanges();
            }
        }

        private void QueryAllContacts()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var contacts = session.Query<Contact>().ToList();

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"{contact.Id} - {contact.Name} - {contact.Email}");
                }

                Console.WriteLine($"{contacts.Count} contacts found.");
            }
        }
    }
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
