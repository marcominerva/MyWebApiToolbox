using MyToolbox.Shared.Models;

namespace MyToolbox.BusinessLayer.Services.Interfaces;

public interface IPeopleService
{
    Task<IEnumerable<Person>> GetListAsync(string name);
}