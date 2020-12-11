using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IPersonData
    {
        Task DeletePerson(int id);
        Task<List<PersonModel>> GetAllPeople();
        Task<PersonModel> GetPersonById(int id);
        Task<PersonModel> GetPersonByFullName(string firstName, string lastName);
        Task InsertPerson(PersonModel person);
        Task UpdatePerson(PersonModel person);
        Task InsertAds(AdsModel model);
        Task<List<AdsModel>> GetAds();
    }
}