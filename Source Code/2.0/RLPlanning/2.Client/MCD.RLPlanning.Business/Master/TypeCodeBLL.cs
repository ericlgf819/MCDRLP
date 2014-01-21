using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MCD.Common;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.IServices.Master;

namespace MCD.RLPlanning.BLL.Master
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeCodeBLL : BaseBLL<ITypeCodeService>
    {
        /// <summary>
        /// 查找所有typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet SelectAllTypeCode(string typecodeName, string rentTypeName, string entityTypeName, string status)
        {
            TypeCodeEntity entity = new TypeCodeEntity() {
                TypeCodeName = typecodeName, 
                RentTypeName = rentTypeName, 
                EntityTypeName = entityTypeName, 
                Status = status
            };
            //
            return base.DeSerilize(base.WCFService.SelectAllTypeCode(entity));
        }
        /// <summary>
        /// 查找單個typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TypeCodeEntity SelectSingleTypeCode(string typeCodeSnapshotID)
        {
            TypeCodeEntity entity = new TypeCodeEntity() { 
                TypeCodeSnapshotID = typeCodeSnapshotID
            };
            //
            return base.WCFService.SelectSingleTypeCode(entity);
        }
        /// <summary>
        /// 通过租金类型名称获取TypeCode
        /// </summary>
        /// <param name="rentTypeName"></param>
        /// <returns></returns>
        public TypeCodeEntity SelectTypeCodeByRentTypeName(string rentTypeName, string entityTypeName)
        {
            TypeCodeEntity entity = null;
            //
            DataSet ds = this.SelectAllTypeCode(string.Empty, rentTypeName, entityTypeName, "已生效");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                entity = new TypeCodeEntity();
                //
                DataTable dt = ds.Tables[0];
                DataRow row = dt.Rows[0];
                ReflectHelper.SetPropertiesByDataRow<TypeCodeEntity>(ref entity, row);
            }
            return entity;
        }

        /// <summary>
        /// 获取有效的TypeCode，状态为已生效，且未创建快照
        /// </summary>
        /// <returns></returns>
        public DataSet SelectActiveTypeCode()
        {
            return base.DeSerilize(base.WCFService.SelectActiveTypeCode());
        }
        /// <summary>
        /// 检查typecode录入规则
        /// </summary>
        /// <param name="typeCode">typecode</param>
        /// <returns>1:合法；0:非法</returns>
        public bool CheckInput(string typeCode)
        {
            return base.WCFService.CheckInput(typeCode) > 0;
        }
        /// <summary>
        /// 通过快照ID，找到TYPECODE名称
        /// </summary>
        /// <param name="id">快照ID</param>
        /// <returns>TYPECODE名称</returns>
        public string GetTypeCodeByID(string id)
        {
            return base.WCFService.GetTypeCodeByID(id);
        }
        /// <summary>
        /// 判断typecode是否处于修改状态
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public bool IsWorkFlowMod(string typeCodeName)
        {
            return base.WCFService.IsWorkFlowMod(typeCodeName) > 1;
        }

        /// <summary>
        /// 判断是否存在租金类型和实体类型的组合
        /// </summary>
        /// <param name="rentType">租金类型</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public bool IsExistsRentTypeAndEntityType(string typeCodeSnapshotID, string rentType, string entityType)
        {
            return base.WCFService.IsExistsRentTypeAndEntityType(typeCodeSnapshotID, rentType, entityType);
        }
        /// <summary>
        /// 是否TYPECODE已经存在
        /// </summary>
        /// <param name="typeCodeSnapshotID"></param>
        /// <param name="typeCodeName"></param>
        /// <returns></returns>
        public bool IsExistsTypeCode(string typeCodeSnapshotID, string typeCodeName)
        {
            return base.WCFService.IsExistsTypeCode(typeCodeSnapshotID, typeCodeName);
        }
    }
}