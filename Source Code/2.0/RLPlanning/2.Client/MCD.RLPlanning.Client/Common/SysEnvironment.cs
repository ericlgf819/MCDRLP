using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

using MCD.Common;
using MCD.Framework.AppCode;
using MCD.RLPlanning.Entity;
using MCD.RLPlanning.Entity.Master;
using MCD.RLPlanning.BLL.ContractMg;
using MCD.RLPlanning.Client.Common;
using MCD.RLPlanning.BLL.Master;

namespace MCD.RLPlanning.Client.AppCode
{
    /// <summary>
    /// 系统环境变量
    /// </summary>
    public class SysEnvironment
    {
        //Fields
        private static Guid m_SystemID = Guid.Empty;
        private static string m_SystemCode = string.Empty;
        private static string m_SynchronizateTime = string.Empty;
        /// <summary>
        /// 本地化缓存的 DataTable 数据
        /// </summary>
        public static Dictionary<string, DataTable> LocalTables = new Dictionary<string, DataTable>();

        //Properties
        /// <summary>
        /// 系统ID
        /// </summary>
        public static Guid SystemID
        {
            get
            {
                if (SysEnvironment.m_SystemID.Equals(Guid.Empty))
                {
                    SysEnvironment.m_SystemID = new Guid(ConfigurationManager.AppSettings["SystemID"] + string.Empty);
                }
                return SysEnvironment.m_SystemID;
            }
        }
        /// <summary>
        /// 系统编码
        /// </summary>
        public static string SystemCode
        {
            get
            {
                if (SysEnvironment.m_SystemCode.Equals(string.Empty))
                {
                    SysEnvironment.m_SystemCode = ConfigurationManager.AppSettings["SystemCode"] + string.Empty;
                }
                return SysEnvironment.m_SystemCode;
            }
        }
        /// <summary>
        /// 系统同步时间
        /// </summary>
        public static string SynchronizateTime
        {
            get
            {
                if (SysEnvironment.m_SynchronizateTime.Equals(string.Empty))
                {
                    SysEnvironment.m_SynchronizateTime = ConfigurationManager.AppSettings["SynchronizateTime"] + string.Empty;
                }
                return SysEnvironment.m_SynchronizateTime;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static UserEntity CurrentUser { get; set; }
        /// <summary>
        /// 获取或设置当前语言版本
        /// </summary>
        public static LANGUAGES CurrentLanguage { get; set; }
        /// <summary>
        /// 获取或设置系统配置信息。
        /// </summary>
        public static SystemSettings SystemSettings { get; set; }
        /// <summary>
        /// 获取系统配置的允许上传的附件类型。
        /// </summary>
        public static string AttachFileTypeList
        {
            get { return SysEnvironment.SystemSettings["AttachFileTypeList"]; }
        }
        /// <summary>
        /// 获取单次批量处理允许的最大数量，上限285。
        /// </summary>
        public static int BatchSubmitCount
        {
            get
            {
                int number = 0;
                if (int.TryParse(SysEnvironment.SystemSettings["TaskSumbitCount"], out number) && number > 0 && number <= 285)
                {
                    return number;
                }
                return 285;
            }
        }
        /// <summary>
        /// 获取系统配置的分页页大小，默认60
        /// </summary>
        public static int DefaultPageSize
        {
            get
            {
                int pageSize = 0;
                if (int.TryParse(SysEnvironment.SystemSettings["DefaultPageSize"], out pageSize) && pageSize > 0)
                {
                    return pageSize;
                }
                return 60;
            }
        }

        public static DataTable DtArea
        {
            get
            {
                DataTable DtArea = new DataTable("DtArea");
                DtArea.Columns.Add("ID", typeof(Guid));
                DtArea.Columns.Add("AreaName");

                foreach (var v in CurrentAreaEntitys)
                {
                    DtArea.Rows.Add(v.ID, v.AreaName);
                    //DtArea.Rows.Add(v.AreaName);

                }

                return DtArea.Copy();

            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static List<AreaEntity> CurrentAreaEntitys
        {
            get
            {
                List<AreaEntity> _ListAreaEntitys = new List<AreaEntity>();


                //UserCompanyEntity _UserAreaEntity1 = new UserCompanyEntity();
                //_UserAreaEntity1.UserID = CurrentUser.ID;
                //UserCompanyBLL userAreaBLL = new UserCompanyBLL();
                //AreaBLL _AreaBLL = new AreaBLL();

                //DataSet _dsUseArea = userAreaBLL.SelectUserArea(_UserAreaEntity1);




                //DataTable tableAreas = _AreaBLL.SelectAreas().Tables[0];

                //for (int i = 0; i < _dsUseArea.Tables[0].Rows.Count; i++)
                //{
                //    AreaEntity _AreaEntity = new AreaEntity();

                //    string _AreaID = _dsUseArea.Tables[0].Rows[i]["AreaID"].ToString();

                //    string strFilter = "[ID]='" + _AreaID + "'";
                //    DataRow[] rows = tableAreas.Select(strFilter);

                //    _AreaEntity.ID = new Guid(_AreaID);
                //    _AreaEntity.AreaName = rows[0]["AreaName"].ToString();
                //    _AreaEntity.Remark = rows[0]["Remark"].ToString();
                //    string _UpdateTime = rows[0]["UpdateTime"].ToString();
                //    _AreaEntity.UpdateTime = DateTime.Parse(_UpdateTime);
                //    //string _OperationID = rows[i]["OperationID"].ToString();
                //    //_AreaEntity.OperationID = new Guid(_OperationID);

                //    _ListAreaEntitys.Add(_AreaEntity);
                //}
                return _ListAreaEntitys;
            }
        }

        #region 用于合同变更时判断状态的字段

        /// <summary>
        /// 合同复制类型（新建、变更、续租）
        /// </summary>
        public static ContractCopyType ContractCopyType { get; set; }

        /// <summary>
        /// 当前合同状态
        /// </summary>
        public static string CurrentContractStatus { get; set; }
        /// <summary>
        /// 当前合同快照ID
        /// </summary>
        public static string CurrentContractSnapshotID { get; set; }
        /// <summary>
        /// 当前合同快照 创建时间
        /// </summary>
        public static DateTime? CurrentContractSnapshotCreateTime { get; set; }

        /// <summary>
        /// 新增中的合同快照ID
        /// </summary>
        public static string EditingContractSnapshotID { get; set; }

        /// <summary>
        /// 存储实体变更后，租赁起止时间是否与上次变更的时间段有交集
        /// </summary>
        public static Dictionary<string, bool> s_mapIsLastEntityRentDateOverlap = new Dictionary<string,bool>();

        /// <summary>
        /// false表面用户改了租赁起止时间，但没有点tab页面
        /// </summary>
        public static bool s_bugFixed;
        #endregion
    }
}