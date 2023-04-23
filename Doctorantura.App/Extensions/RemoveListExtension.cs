using Doctorantura.App.Context;
using System.Collections.Generic;

namespace Doctorantura.App.Extensions
{
    public static class RemoveListExtension
    {
        private static readonly AppDbContext _dbContext;

        public static void RemoveFile<T>(List<T> files)
        {
            foreach (var file in files)
            {
                _dbContext.Remove(file);
            }
        }
    }
}
