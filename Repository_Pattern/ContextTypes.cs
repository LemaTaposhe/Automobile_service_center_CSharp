using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern
/*
The ContextTypes enum is likely used to indicate the type of data source the repository should use. 
In this case, it mentions XMLSource, suggesting that there might be an XML-based 
implementation of the repository. This allows for a modular and configurable 
approach to selecting the data source at runtime.
*/
{
    public enum ContextTypes { XMLSource }
} 

