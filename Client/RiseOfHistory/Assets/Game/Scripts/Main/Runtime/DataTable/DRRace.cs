//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2025-09-09 15:39:04.510
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
    /// 种族表。
    /// </summary>
    public class DRRace : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取种族编号。
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
        /// 获取属性id-0。
        /// </summary>
        public int PropertyId0
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取属性改变-0。
        /// </summary>
        public int PropertyChange0
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
            Description = columnStrings[index++];
            PropertyId0 = int.Parse(columnStrings[index++]);
            PropertyChange0 = int.Parse(columnStrings[index++]);

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
                    Description = binaryReader.ReadString();
                    PropertyId0 = binaryReader.Read7BitEncodedInt32();
                    PropertyChange0 = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, int>[] m_PropertyId = null;

        public int PropertyIdCount
        {
            get
            {
                return m_PropertyId.Length;
            }
        }

        public int GetPropertyId(int id)
        {
            foreach (KeyValuePair<int, int> i in m_PropertyId)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetPropertyId with invalid id '{0}'.", id));
        }

        public int GetPropertyIdAt(int index)
        {
            if (index < 0 || index >= m_PropertyId.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetPropertyIdAt with invalid index '{0}'.", index));
            }

            return m_PropertyId[index].Value;
        }

        private KeyValuePair<int, int>[] m_PropertyChange = null;

        public int PropertyChangeCount
        {
            get
            {
                return m_PropertyChange.Length;
            }
        }

        public int GetPropertyChange(int id)
        {
            foreach (KeyValuePair<int, int> i in m_PropertyChange)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetPropertyChange with invalid id '{0}'.", id));
        }

        public int GetPropertyChangeAt(int index)
        {
            if (index < 0 || index >= m_PropertyChange.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetPropertyChangeAt with invalid index '{0}'.", index));
            }

            return m_PropertyChange[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_PropertyId = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(0, PropertyId0),
            };

            m_PropertyChange = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(0, PropertyChange0),
            };
        }
    }
}
