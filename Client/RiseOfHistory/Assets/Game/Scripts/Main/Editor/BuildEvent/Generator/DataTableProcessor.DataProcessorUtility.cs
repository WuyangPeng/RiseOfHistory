//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework;

namespace Game.Scripts.Main.Editor.BuildEvent.Generator
{
    public sealed partial class DataTableProcessor
    {
        private static class DataProcessorUtility
        {
            private static readonly IDictionary<string, DataTableProcessor.DataProcessor> s_DataProcessors = new SortedDictionary<string, DataTableProcessor.DataProcessor>(StringComparer.Ordinal);

            static DataProcessorUtility()
            {
                System.Type dataProcessorBaseType = typeof(DataTableProcessor.DataProcessor);
                Assembly assembly = Assembly.GetExecutingAssembly();
                System.Type[] types = assembly.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    if (!types[i].IsClass || types[i].IsAbstract)
                    {
                        continue;
                    }

                    if (dataProcessorBaseType.IsAssignableFrom(types[i]))
                    {
                        DataTableProcessor.DataProcessor dataProcessor = (DataTableProcessor.DataProcessor)Activator.CreateInstance(types[i]);
                        foreach (string typeString in dataProcessor.GetTypeStrings())
                        {
                            s_DataProcessors.Add(typeString.ToLowerInvariant(), dataProcessor);
                        }
                    }
                }
            }

            public static DataTableProcessor.DataProcessor GetDataProcessor(string type)
            {
                if (type == null)
                {
                    type = string.Empty;
                }

                DataTableProcessor.DataProcessor dataProcessor = null;
                if (s_DataProcessors.TryGetValue(type.ToLowerInvariant(), out dataProcessor))
                {
                    return dataProcessor;
                }

                throw new GameFrameworkException(Utility.Text.Format("Not supported data processor type '{0}'.", type));
            }
        }
    }
}
