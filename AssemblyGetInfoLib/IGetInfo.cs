using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyGetInfoLib
{
    public interface IGetInfo
    {
        Node GetInfoFromFile(string fileName);
    }
}
