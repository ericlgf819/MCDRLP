using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;


   public static class ExtCmd
    {
       public static string GetCmdString(this DbCommand _DbCommand)
       {
           string strResult = "";
           strResult = _DbCommand.CommandText;

//           declare @p2 nvarchar(20)
//set @p2=NULL
//exec t2_insert @timeticks=634783993232004455,@outString=@p2 output
//select @p2

           string paras="";
           for (int i = 0; i < _DbCommand.Parameters.Count; i++)
           { 
               paras =paras+","+ _DbCommand.Parameters[i].ParameterName+"='"+_DbCommand.Parameters[i].Value+"'";

           }
           if ("" != paras)
               paras = paras.Substring(1);

           strResult = strResult + "  " + paras;
           if (_DbCommand.CommandType == CommandType.StoredProcedure)
           {
               strResult = "exec " + strResult;
           }


           strResult = "    " + strResult + "   ";
           return strResult;
       }
    }
