using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models
{
    public partial class FinanceDBContext : DbContext
    {
        public FinanceDBContext()
        {
        }

        public FinanceDBContext(DbContextOptions<FinanceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; } = null!;
        public virtual DbSet<BudgetTransactionSummary> BudgetTransactionSummaries { get; set; } = null!;
        public virtual DbSet<BuisnessInfo> BuisnessInfos { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Expenditure> Expenditures { get; set; } = null!;
        public virtual DbSet<ExpenditureEmployee> ExpenditureEmployees { get; set; } = null!;
        public virtual DbSet<FinanceAccount> FinanceAccounts { get; set; } = null!;
        public virtual DbSet<Income> Incomes { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tax> Taxes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BKO6ROR\\SERVERBASE;Initial Catalog=FinanceDB;User ID=sa;Password=lox45lol1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasKey(e => e.IdBudget)
                    .HasName("PK_ID_Budget");

                entity.ToTable("Budget");

                entity.Property(e => e.IdBudget).HasColumnName("ID_Budget");

                entity.Property(e => e.ActualBudget)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Actual_Budget");

                entity.Property(e => e.ExpectedBudget)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Expected_Budget");

                entity.Property(e => e.IdBudgetUser).HasColumnName("ID_Budget_User");

                entity.Property(e => e.NameBudget)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Name_Budget");
            });

            modelBuilder.Entity<BudgetTransactionSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BudgetTransactionSummary");

                entity.Property(e => e.BudgetId).HasColumnName("Budget_ID");

                entity.Property(e => e.СуммаВсехОпераций)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Сумма всех операций");
            });

            modelBuilder.Entity<BuisnessInfo>(entity =>
            {
                entity.HasKey(e => e.IdBuisness)
                    .HasName("PK_ID_Buisness");

                entity.ToTable("Buisness_Info");

                entity.HasIndex(e => e.Msrnie, "UQ__Buisness__325B9F16E231F949")
                    .IsUnique();

                entity.Property(e => e.IdBuisness).HasColumnName("ID_Buisness");

                entity.Property(e => e.BuisnessName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Buisness_Name");

                entity.Property(e => e.IdUserBuisness).HasColumnName("ID_User_Buisness");

                entity.Property(e => e.Msrnie)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("MSRNIE");

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasColumnName("Open_Date");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK_ID_Category");

                entity.ToTable("Category");

                entity.HasIndex(e => e.NameCategory, "UQ_Name_Category")
                    .IsUnique();

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.NameCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Name_Category");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.IdCurrency)
                    .HasName("PK_ID_Currency");

                entity.ToTable("Currency");

                entity.HasIndex(e => e.NameCurrency, "UQ_Name_Currency")
                    .IsUnique();

                entity.Property(e => e.IdCurrency).HasColumnName("ID_Currency");

                entity.Property(e => e.NameCurrency)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Name_Currency");

                entity.Property(e => e.SymbolAbbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Symbol_Abbreviation");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK_ID_Employee");

                entity.ToTable("Employee");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IdBuisnessEmployee).HasColumnName("ID_Buisness_Employee");

                entity.Property(e => e.PostEmployee)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Post_Employee");
            });

            modelBuilder.Entity<Expenditure>(entity =>
            {
                entity.HasKey(e => e.IdExpenditure)
                    .HasName("PK_ID_Expendicture");

                entity.ToTable("Expenditure");

                entity.Property(e => e.IdExpenditure).HasColumnName("ID_Expenditure");

                entity.Property(e => e.ExpenditureDate)
                    .HasColumnType("date")
                    .HasColumnName("Expenditure_Date");

                entity.Property(e => e.ExpenditureName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Expenditure_Name");

                entity.Property(e => e.ExpenditureSum)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Expenditure_Sum");

                entity.Property(e => e.IdExpendCategory).HasColumnName("ID_Expend_Category");

                entity.Property(e => e.IdExpendCurrency).HasColumnName("ID_Expend_Currency");

                entity.Property(e => e.IdExpendFinAccount).HasColumnName("ID_Expend_FinAccount");
            });

            modelBuilder.Entity<ExpenditureEmployee>(entity =>
            {
                entity.HasKey(e => e.IdExpenditureEmployee)
                    .HasName("PK_ID_Expenditure_Employee");

                entity.ToTable("Expenditure_Employee");

                entity.Property(e => e.IdExpenditureEmployee).HasColumnName("ID_Expenditure_Employee");

                entity.Property(e => e.ExpendEmployeeId).HasColumnName("Expend_Employee_ID");

                entity.Property(e => e.ExpendExpenditureId).HasColumnName("Expend_Expenditure_ID");

                entity.Property(e => e.ExpendFinAccountId).HasColumnName("Expend_FinAccount_ID");
            });

            modelBuilder.Entity<FinanceAccount>(entity =>
            {
                entity.HasKey(e => e.IdFinanceAccount)
                    .HasName("PK_ID_Finance_Account");

                entity.ToTable("Finance_Account");

                entity.Property(e => e.IdFinanceAccount).HasColumnName("ID_Finance_Account");

                entity.Property(e => e.AccountBalance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Account_Balance");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Account_Name");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Account_Number");

                entity.Property(e => e.IdBuisnessFinAccount).HasColumnName("ID_Buisness_FinAccount");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(e => e.IdIncome)
                    .HasName("PK_ID_Income");

                entity.ToTable("Income");

                entity.Property(e => e.IdIncome).HasColumnName("ID_Income");

                entity.Property(e => e.IdIncomeCategory).HasColumnName("ID_Income_Category");

                entity.Property(e => e.IdIncomeCurrency).HasColumnName("ID_Income_Currency");

                entity.Property(e => e.IdIncomeFinAccount).HasColumnName("ID_Income_FinAccount");

                entity.Property(e => e.IncomeDate)
                    .HasColumnType("date")
                    .HasColumnName("Income_Date");

                entity.Property(e => e.IncomeName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Income_Name");

                entity.Property(e => e.IncomeSum)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Income_Sum");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK_ID_Log");

                entity.Property(e => e.IdLog).HasColumnName("ID_Log");

                entity.Property(e => e.IdUserLog).HasColumnName("ID_User_Log");

                entity.Property(e => e.LogDate)
                    .HasColumnType("date")
                    .HasColumnName("Log_Date");

                entity.Property(e => e.LogMessage)
                    .IsUnicode(false)
                    .HasColumnName("Log_Message");

                entity.Property(e => e.LogTime).HasColumnName("Log_Time");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.IdOperation)
                    .HasName("PK_ID_Operation");

                entity.ToTable("Operation");

                entity.HasIndex(e => e.NumOperation, "UQ__Operatio__B07E03D6034BD250")
                    .IsUnique();

                entity.Property(e => e.IdOperation).HasColumnName("ID_Operation");

                entity.Property(e => e.BudgetId).HasColumnName("Budget_ID");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CurrencyId).HasColumnName("Currency_ID");

                entity.Property(e => e.DateOperation)
                    .HasColumnType("date")
                    .HasColumnName("Date_Operation");

                entity.Property(e => e.NumOperation).HasColumnName("Num_Operation");

                entity.Property(e => e.Recipient)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sender)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SumOperation)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Sum_Operation");

                entity.Property(e => e.TypeOperation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Type_Operation");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_ID_Role");

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ__Role__32E244D4CBA98826")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.IdTax)
                    .HasName("PK_ID_Tax");

                entity.ToTable("Tax");

                entity.Property(e => e.IdTax).HasColumnName("ID_Tax");

                entity.Property(e => e.IdTaxCurrency).HasColumnName("ID_Tax_Currency");

                entity.Property(e => e.IdTaxFinAccount).HasColumnName("ID_Tax_FinAccount");

                entity.Property(e => e.TaxDate)
                    .HasColumnType("date")
                    .HasColumnName("Tax_Date");

                entity.Property(e => e.TaxName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Tax_Name");

                entity.Property(e => e.TaxSum)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Tax_Sum");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_ID_User");

                entity.ToTable("User");

                entity.HasIndex(e => e.Login, "UQ__User__5E55825B2EEF9434")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__A9D1053496FDBAD1")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Login)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
