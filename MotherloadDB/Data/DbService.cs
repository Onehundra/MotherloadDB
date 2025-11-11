using Microsoft.EntityFrameworkCore;
using MotherloadDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotherloadDB.Data
{
    public class DbService
    {
        private readonly AppDbContext _context;

        public DbService(AppDbContext context)
        {
            _context = new AppDbContext();
            _context = context;
        }




        //CREATE
        public async Task<Company> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }


        //READ 1

        public async Task<Company> GetCompany(int id)
        {
            return await _context.Companies.FindAsync(id);
        }


        //READ 2 ReadAll

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }


        //UPDATE 

        public async Task<Company> UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }


        //DELETE

        public async Task<bool> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if(company == null)
            {
                return false;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
