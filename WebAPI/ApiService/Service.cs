using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.ApiService;

public class Service
{
    private ApplicationContext _dbcontext;

    public Service(ApplicationContext context)
    {
        _dbcontext = context;
    }

    public List<Person> GetAllPersons()
    {
        var persons = _dbcontext.persons.Include(p => p.skills).ToList();
        return persons;
    }

    public Person GetPerson(long idPerson)
    {
        var person = _dbcontext.persons.Include(p => p.skills).FirstOrDefault(p => p.id == idPerson);

        if (person == null)
        {
            throw new InvalidOperationException("The person was not found");
        }

        return person;
    }

    public void PostNewPerson(Person personFromReq)
    {
        try
        {
            _dbcontext.persons.Add(personFromReq);
            _dbcontext.SaveChanges();
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error filling in parameters");
        }
    }

    public void PutPerson(long id, PersonForResponse personFromReq)
    {
        var findPerson = _dbcontext.persons.FirstOrDefault(p => p.id == id);

        if (findPerson == null)
        {
            throw new InvalidOperationException("The person was not found");
        }

        findPerson.name = personFromReq.name;
        findPerson.name = personFromReq.name;

        findPerson.skills.Clear();
        findPerson.skills.AddRange(personFromReq.skills);

        _dbcontext.SaveChanges();
    }

    public void DeletePerson(long id)
    {
        var findPerson = GetPerson(id);

        if (findPerson != null)
        {
            _dbcontext.persons.Remove(findPerson);
            _dbcontext.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("The person was not deleted!");
        }
    }

}