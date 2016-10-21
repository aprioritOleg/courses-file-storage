namespace CloudStorage.Domain.FileAggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class FileInfoValidation
    {
        /// <summary>
        /// Validates the name of file.
        /// </summary>
        /// <param name="name">The name of file for validation.</param>
        /// <returns>Validity of the name of file.</returns>
        public static bool IsValidName(string name)
        {
            return !(string.IsNullOrEmpty(name));
        }
    }
}