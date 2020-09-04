using System;
using System.Collections.Generic;
using System.Text;

namespace Competitor.Common
{
    public static class GuardExtensions
    {
        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentIsNull(this object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }
    }
}
