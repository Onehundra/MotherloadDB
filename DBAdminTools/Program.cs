using MotherloadDB.Data;
using MotherloadDB.Models;
using System.ComponentModel.DataAnnotations;
using ClosedXML.Excel;


namespace DBAdminTools
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new AppDbContext();
            var dbService = new DbService(context);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Companies Database Admin Tools ===");
                Console.WriteLine("1. Upload CSV");
                Console.WriteLine("2. Create Company");
                Console.WriteLine("3. View All Companies");
                Console.WriteLine("4. Delete Company");
                Console.WriteLine("5. Export To CSV");
                Console.WriteLine("6. Database Statistics");
                Console.WriteLine("7. Search Company");
                Console.WriteLine("0. EXIT");
                Console.Write("Select Option:  ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await UploadCSV(dbService);
                        break;

                        case "2":
                        await CreateCompany(dbService);
                        break;

                        case "3":
                        await ViewAllCompanies(dbService);
                        break;

                        case "4": 
                        await DeleteCompany(dbService);
                        break;

                        case "5": Console.WriteLine("");
                        break;

                        case "6": 
                        await ShowStatistics(dbService);
                        break;

                        case "7":
                        await SearchCompany(dbService);
                        break;
                    case "0": Console.WriteLine("Invalid Option");
                        break;
                        
                }
                Console.WriteLine("Press Any Key To Continue");
                Console.ReadKey();

            }

        }

        static async Task UploadCSV(DbService dbService)
        {
            Console.Write("Enter File Path");
            var filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found");
                return;
            }


            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
            int count = 0;

            foreach ( var row in rows)
            {
                var orgNumber = row.Cell(5).GetString();

                var company = new Company
                {
                    Name = row.Cell(1).GetString(),
                    City = row.Cell(2).GetString(),
                    Industry = row.Cell(3).GetString(),
                    OrgNumber = orgNumber,
                    WebSite = row.Cell(8).GetString(),
                };

                await dbService.CreateCompany(company);
                count++;
                if (count % 100 == 0) Console.WriteLine($"Processed {count} companies");
            }

            
        }
        
        static async Task CreateCompany(DbService dbService)
        {
            Console.WriteLine("Create a company by filling out below");
            Console.Write("Name of the company: ");
            var name = Console.ReadLine();

            Console.Write("Enter org number: ");
            var number = Console.ReadLine();

            var company = new Company
            {
                OrgNumber = number,
                Name = name
            };

            await dbService.CreateCompany(company);
        }


        static async Task ViewAllCompanies(DbService dbService)
        {
            var companies = await dbService.GetCompanies();
            foreach(var c in companies)
            {
                Console.WriteLine($"\n{c.Id} | {c.Name} | {c.OrgNumber}");
            }
        }

        static async Task SearchCompany(DbService dbservice)
        {
            Console.Write("Enter company id");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var company = await dbservice.GetCompany(id);

                if (company != null)
                {
                    Console.WriteLine(company.Name);
                    Console.WriteLine(company.OrgNumber);
                }
                else
                {
                    Console.WriteLine("Company was not found");
                }
            }
        }
        

        static async Task DeleteCompany(DbService dbservice)
        {
            Console.WriteLine("Enter id of company to remove");

            if(int.TryParse(Console.ReadLine(),out int id))
            {
                var success = await dbservice.DeleteCompany(id);

                Console.WriteLine(success ? "Deleted"  : "Not found!");
            }
        }
        
        static async Task ShowStatistics(DbService dbservice)
        {
            var companies = await dbservice.GetCompanies();
            Console.WriteLine($"Total companies: {companies.Count}");
            Console.WriteLine($"Cites: {companies.Select(c=> c.City).Distinct().Count()}");
        }
    }
}
