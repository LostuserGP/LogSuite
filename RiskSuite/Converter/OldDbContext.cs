using Microsoft.EntityFrameworkCore;
using RiskSuite.Converter.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.Converter
{
    public class OldDbContext : DbContext
    {
        public OldDbContext(
            DbContextOptions<OldDbContext> options) : base(options)
        { }

        public DbSet<CtlCounterparty> Counterparties { get; set; }
        public DbSet<CtlSector> FinancailSectors { get; set; }
        public DbSet<CtlCountry> Countries { get; set; }

    }
}
