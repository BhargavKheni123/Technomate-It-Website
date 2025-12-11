using Technomate.Models;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddMessage(ContactMessage message)
    {
        // Agar CreatedDate default value hai to set karo
        if (message.CreatedDate == default(DateTime))
        {
            message.CreatedDate = DateTime.Now;
        }

        _context.ContactMessages.Add(message);
        _context.SaveChanges();
    }

}
