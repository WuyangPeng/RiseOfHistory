//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using Game.Scripts.Main.Runtime.GameModule.Base;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.Base
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            GameEntry.InitBuiltinComponents();
            GameEntry.InitCustomComponents();
            GameEntry.InitModuleComponent();
        }
    }
}
