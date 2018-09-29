using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HouseofCat.Library.File
{
    class DirectoryEnumerators
    {
        #region Standard Enumeration

        public static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern,
                                                               SearchOption searchOpt)
        {
            try
            {
                var directories = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories)
                {
                    directories = Directory.EnumerateDirectories(parentDirectory)
                                            .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOpt));
                }
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }

        #endregion

        #region Standard Enumeration w/ Parallel

        public static IEnumerable<string> EnumerateDirectoriesInParallel(string parentDirectory, string searchPattern,
                                                                   SearchOption searchOpt)
        {
            try
            {
                var directories = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories)
                {
                    directories = Directory.EnumerateDirectories(parentDirectory)
                                           .AsParallel()
                                           .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOpt));
                }
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern).AsParallel());
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }

        #endregion
    }
}
