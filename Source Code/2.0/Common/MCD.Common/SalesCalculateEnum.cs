using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCD.Common
{
    public enum SalesCalRetCode
    {
        EN_CAL_SUCCESS = 0,
        EN_CAL_HASENTITYINCAL,
        EN_CAL_TIMEOUT,
        EN_CAL_SERVERBUSY,
        EN_CAL_CHECKENTITYINCALERR,
        EN_CAL_UNKNOWNERR
    }
}
