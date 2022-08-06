using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingFrontEnd.Models;

namespace TrackingFrontEnd.Data
{
    public class TrackingFrontEndContext : DbContext
    {
        public TrackingFrontEndContext (DbContextOptions<TrackingFrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<IssueModel> IssueModel { get; set; } = default!;
    }
}
