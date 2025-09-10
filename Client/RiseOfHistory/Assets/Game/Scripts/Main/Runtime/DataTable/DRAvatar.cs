//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2025-09-10 10:04:01.354
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Scripts.Main.Runtime.DataTable
{
    /// <summary>
    /// 头像表。
    /// </summary>
    public class DRAvatar : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取头像编号。
        /// </summary>
        public override int Id => m_Id;

        /// <summary>
        /// 获取名字。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取路径。
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取姓别。
        /// </summary>
        public int Sex
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Name = columnStrings[index++];
            Path = columnStrings[index++];
            Sex = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    Path = binaryReader.ReadString();
                    Sex = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
