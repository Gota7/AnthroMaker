using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {

    /// <summary>
    /// Operator.
    /// </summary>
    public enum Operator { 
        And, Or, Not, AndNot,
    }
    
    /// <summary>
    /// Part library.
    /// </summary>
    public static class PartLibrary {

        /// <summary>
        /// Search.
        /// </summary>
        public class Search {

            /// <summary>
            /// Author.
            /// </summary>
            public string Author;

            /// <summary>
            /// Name.
            /// </summary>
            public string Name;

            /// <summary>
            /// Tags.
            /// </summary>
            public List<Tuple<Operator, PartCategory>> Tags = new List<Tuple<Operator, PartCategory>>();
        
        }
    
    }

}
