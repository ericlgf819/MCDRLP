using System;

namespace LiveUpdate.App
{
    /// <summary>
    /// 表示待更新的文件信息。
    /// </summary>
    [Serializable]
    public class UpdateFile
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 获取或设置文件下载后的保存位置
        /// </summary>
        public string SavePath { get; set; }
    }
}