using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using FinanceAPI.Controllers;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinanceDBContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(FinanceDBContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogCritical("You are doing well!");
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            
            if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'CercovContext.Users'  is null.");
            }

            user.IdUser = null;
            user.Salt = SaltGenerator.GenerateSalt();
            user.Password = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password + user.Salt)));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        [HttpPost("Authorize")]
        public async Task<ActionResult<UserToken>> Authorize([FromBody] User obj)
        {
            _logger.LogCritical("You have been authorized");
            if (_context.Users == null || obj == null)
            {
                return NotFound();
            }

            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Login == obj.Login);

            if (user == null) return NotFound();

            string hashedPassword = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(obj.Password + user.Salt)));
            if (hashedPassword != user.Password) return NotFound();
            //return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
            User testUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == obj.Login && x.Password == hashedPassword); 
            return new UserToken(testUser, AccountController.GetToken());

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'FinanceDBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteUsers([FromQuery] List<int> ids)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var user = await _context.Users.FindAsync(item);

                if (user == null) continue;
                List<Budget> budgets = _context.Budgets.Where(x => x.IdBudgetUser == user.IdUser).ToList();
                foreach(var budget in budgets)
                {
                    List<Operation> operations = _context.Operations.Where(x => x.BudgetId == budget.IdBudget).ToList();
                    foreach(var operation in operations)
                    {
                        _context.Operations.Remove(operation);
                    }
                    _context.Budgets.Remove(budget);
                }
                List<BuisnessInfo> buisnessInfos = _context.BuisnessInfos.Where(x => x.IdUserBuisness == user.IdUser).ToList();
                foreach(var buisnessInfo in buisnessInfos)
                {
                    List<Employee> employees = _context.Employees.Where(x => x.IdBuisnessEmployee == buisnessInfo.IdBuisness).ToList();
                    foreach(var employee in employees)
                    {
                        List<ExpenditureEmployee> expenditureEmployees = _context.ExpenditureEmployees.Where(x => x.ExpendEmployeeId == employee.IdEmployee).ToList();
                        foreach(var  expense in expenditureEmployees)
                        {
                            _context.ExpenditureEmployees.Remove(expense);
                        }
                        _context.Employees.Remove(employee);
                    }
                    List<FinanceAccount> financeAccounts = _context.FinanceAccounts.Where(x => x.IdBuisnessFinAccount == buisnessInfo.IdBuisness).ToList();
                    foreach(var financeAccount in financeAccounts)
                    {
                        List<Expenditure> expenditures = _context.Expenditures.Where(x => x.IdExpendFinAccount == financeAccount.IdFinanceAccount).ToList();
                        foreach(var expenditure in expenditures)
                        {
                            _context.Expenditures.Remove(expenditure);
                        }

                        List<Income> incomes = _context.Incomes.Where(x => x.IdIncomeFinAccount == financeAccount.IdFinanceAccount).ToList();
                        foreach (var income in incomes)
                        {
                            _context.Incomes.Remove(income);
                        }

                        List<Tax> taxes = _context.Taxes.Where(x => x.IdTaxFinAccount == financeAccount.IdFinanceAccount).ToList();
                        foreach (var tax in taxes)
                        {
                            _context.Taxes.Remove(tax);
                        }
                        _context.FinanceAccounts.Remove(financeAccount);
                    }

                }
                List<Log> logs = _context.Logs.Where(x => x.IdUserLog == user.IdUser).ToList();
                foreach(var log in logs)
                {
                    _context.Logs.Remove(log);
                }
               
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
