using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using MCD.RLPlanning.Entity.Master;

namespace MCD.RLPlanning.IServices.Master
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ITypeCodeService : IBaseService
    {
        /// <summary>
        /// 查找所有typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectAllTypeCode(TypeCodeEntity entity);

        /// <summary>
        /// 查找單個typecode信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        TypeCodeEntity SelectSingleTypeCode(TypeCodeEntity entity);

        /// <summary>
        /// 通过租金类型名称获取TypeCode
        /// </summary>
        /// <param name="rentTypeName"></param>
        /// <returns></returns>
        [OperationContract]
        TypeCodeEntity SelectTypeCodeByRentTypeName(string rentTypeName);

        /// <summary>
        /// 获取有效的TypeCode，状态为已生效，且未创建快照
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] SelectActiveTypeCode();



        /// <summary>
        /// 检查typecode录入规则
        /// </summary>
        /// <param name="typeCode">typecode</param>
        /// <returns></returns>
        [OperationContract]
        int CheckInput(string typeCode);
        /// <summary>
        /// 通过快照ID，找到TYPECODE名称
        /// </summary>
        /// <param name="id">快照ID</param>
        /// <returns>TYPECODE名称</returns>
        [OperationContract]
        string GetTypeCodeByID(string id);
        /// <summary>
        /// 判断typecode是否处于修改状态
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        [OperationContract]
        int IsWorkFlowMod(string typeCodeName);

        /// <summary>
        /// 判断是否存在租金类型和实体类型的组合
        /// </summary>
        /// <param name="rentType">租金类型</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsRentTypeAndEntityType(string typeCodeSnapshotID,string rentType, string entityType);
        /// <summary>
        /// 是否TYPECODE已经存在
        /// </summary>
        /// <param name="typeCodeSnapshotID"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsTypeCode(string typeCodeSnapshotID, string typeCodeName);
    }
}