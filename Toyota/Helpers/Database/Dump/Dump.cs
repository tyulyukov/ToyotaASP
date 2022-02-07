using System;
using Toyota.Data;

namespace Toyota.Helpers.Database.Dump
{
    public abstract class Dump
    {
        public readonly String Path = Media.WebRootPath + "\\storage\\dumps\\";
        protected ApplicationDbContext context;

        public Dump(ApplicationDbContext _context)
        {
            context = _context;
        }

        public abstract String Create();
        public abstract object Restore(String filePath);
    }
}
