using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class PersonData : IPersonData
    {
        private readonly ISqlDataAccess _sql;

        public PersonData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        // Insert person into database
        public async Task InsertPerson(PersonModel person)
        {
            await _sql.SaveData<PersonModel>("People_Insert", person);
        }


        public async Task InsertAds(AdsModel model)
        {
            await _sql.SaveData<AdsModel>("Advertisement_Insert", model);
        }

        public async Task<List<AdsModel>> GetAds()
        {
            return await _sql.LoadData<AdsModel, dynamic>("Advertisement_GetAll", new { });
        }


        // Get all persons from database as a list
        public async Task<List<PersonModel>> GetAllPeople()
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("People_GetAll", new { });

            return results;
        }


        // Get Person object with Id = id
        public async Task<PersonModel> GetPersonById(int id)
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("People_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Get Person object by firstName and lastName
        public async Task<PersonModel> GetPersonByFullName(string firstName, string lastName)
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("People_GetByFullName", new { FirstName = firstName, LastName = lastName });

            return results.FirstOrDefault();
        }


        // Update Person info in database
        public async Task UpdatePerson(PersonModel person)
        {
            await _sql.SaveData<PersonModel>("People_Update", person);
        }


        // Delete Person from database with Id = id
        public async Task DeletePerson(int id)
        {
            await _sql.DeleteData<dynamic>("People_Delete", new { id });
        }
    }
}
