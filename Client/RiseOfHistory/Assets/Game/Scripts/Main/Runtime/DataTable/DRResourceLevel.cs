//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
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
    /// 资源等级表。
    /// </summary>
    public class DRResourceLevel : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取资源等级编号。
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
        /// 获取描述。
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别0。
        /// </summary>
        public int Level0
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别1。
        /// </summary>
        public int Level1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别2。
        /// </summary>
        public int Level2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别3。
        /// </summary>
        public int Level3
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别4。
        /// </summary>
        public int Level4
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别5。
        /// </summary>
        public int Level5
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别6。
        /// </summary>
        public int Level6
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别7。
        /// </summary>
        public int Level7
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别8。
        /// </summary>
        public int Level8
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取级别9。
        /// </summary>
        public int Level9
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            var columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (var i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            var index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Name = columnStrings[index++];
            Description = columnStrings[index++];
            Level0 = int.Parse(columnStrings[index++]);
            Level1 = int.Parse(columnStrings[index++]);
            Level2 = int.Parse(columnStrings[index++]);
            Level3 = int.Parse(columnStrings[index++]);
            Level4 = int.Parse(columnStrings[index++]);
            Level5 = int.Parse(columnStrings[index++]);
            Level6 = int.Parse(columnStrings[index++]);
            Level7 = int.Parse(columnStrings[index++]);
            Level8 = int.Parse(columnStrings[index++]);
            Level9 = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (var memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (var binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    Description = binaryReader.ReadString();
                    Level0 = binaryReader.Read7BitEncodedInt32();
                    Level1 = binaryReader.Read7BitEncodedInt32();
                    Level2 = binaryReader.Read7BitEncodedInt32();
                    Level3 = binaryReader.Read7BitEncodedInt32();
                    Level4 = binaryReader.Read7BitEncodedInt32();
                    Level5 = binaryReader.Read7BitEncodedInt32();
                    Level6 = binaryReader.Read7BitEncodedInt32();
                    Level7 = binaryReader.Read7BitEncodedInt32();
                    Level8 = binaryReader.Read7BitEncodedInt32();
                    Level9 = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, int>[] level;

        public int LevelCount => level.Length;

        public int GetLevel(int id)
        {
            foreach (var i in level)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetLevel with invalid id '{0}'.", id));
        }

        public int GetLevelAt(int index)
        {
            if (index < 0 || index >= level.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetLevelAt with invalid index '{0}'.", index));
            }

            return level[index].Value;
        }

        private void GeneratePropertyArray()
        {
            level = new KeyValuePair<int, int>[]
            {
                new (0, Level0),
                new (1, Level1),
                new (2, Level2),
                new (3, Level3),
                new (4, Level4),
                new (5, Level5),
                new (6, Level6),
                new (7, Level7),
                new (8, Level8),
                new (9, Level9),
            };
        }
    }
}
