using System;
using System.Data;
using System.Data.Common;

namespace RAST.DAL
{
    interface IDALObject
    {
        DataSet ReadDataset(int intUserId,int intCurrentPage, int intPageSize);
        IDataReader ReadDataReader(int intCurrentPage, int intPageSize);
        DataSet ReadElementsForUI(int intId);

        DataSet ReadUIData(int intId);
        
        int Add();
        int Update();
        int Delete();
        int Enable();
        int Disable();
    }
}
