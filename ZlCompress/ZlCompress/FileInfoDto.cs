using System.IO;

namespace ZlCompress
{
    /// <summary>
    /// 文件信息对象
    /// </summary>
    public class FileInfoDto
    {
        public FileInfoDto(FileInfo info)
        {
            Info = info;
        }

        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo Info { get; private set; }

        /// <summary>
        /// 文件字节
        /// </summary>
        private byte[] _fileContent;

        /// <summary>
        /// 得到文件内容
        /// </summary>
        public byte[] FileContent
        {
            get
            {
                lock (this)
                {
                    if (_fileContent == null)
                    {
                        var content = new byte[Info.Length];
                        using (var read=Info.OpenRead())
                        {
                            read.Read(content, 0, content.Length);
                        }

                        _fileContent = content;
                    }
                    return _fileContent;
                }
            }
        }
    }
}