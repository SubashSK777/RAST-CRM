using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAST.Utilities;

namespace RAST.DAL
{
    public class DALFactory
    {
        IDALObject objDAL;

        /*public IDALObject CreateDALObject(DALObjectType objObjectType)
        {
            switch (objObjectType)
            {
                case DALObjectType.Users:
                    objDAL = new Users();
                    break;
                
            }
            return objDAL;
        }
         * */
    }
}
