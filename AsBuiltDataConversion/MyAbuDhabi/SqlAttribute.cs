using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public class SqlAttribute : System.Attribute
    {
        /// <summary>
        /// Whether the property is record primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Whether the property should be an auto-increment number
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        /// Whether the property is record foreign key
        /// </summary>
        public bool ForeignKey { get; set; }

        /// <summary>
        /// Whether the property should be indexed
        /// </summary>
        public bool Index { get; set; }

        /// <summary>
        /// Whether the text field is fieldAttributes WKT geometry field
        /// </summary>
        public bool IsWkt { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SqlAttribute()
        {
            IsPrimaryKey = false;
            AutoIncrement = false;
            ForeignKey = false;
            Index = true;
            IsWkt = false;
        }

    }
}
