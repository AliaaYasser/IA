namespace IAIA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class testEntities : DbContext
    {
        public testEntities()
            : base("name=testEntities")
        {
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProject>().HasKey(sc => new { sc.ProjectId, sc.userId });
        }


        public DbSet<User> users { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<UserProject> userProjects { get; set; }
        public DbSet<Requst>requsts { set; get; }
        public DbSet<Comment> comments { set; get; }
        public DbSet<Feedback>feedbacks { set; get; }
    }

}

