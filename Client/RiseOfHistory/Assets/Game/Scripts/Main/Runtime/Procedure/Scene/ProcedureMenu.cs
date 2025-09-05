using Game.Scripts.Main.Runtime.Game;
using Game.Scripts.Main.Runtime.SaveData;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIForm;
using GameFramework;
using GameFramework.FileSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        private bool m_LoadGame = false;
        private readonly FormComponent formComponent = new FormComponent();
        private readonly List<HeadData> headData = new List<HeadData>();

        public override bool UseNativeDialog => false;

        public readonly int SaveMaxCount = 2;

        public void LoadGame()
        {
            m_LoadGame = true;
        }

        public void StartGame()
        {
            m_StartGame = true;
        }

        public void OpenUIForm(UIFormId form)
        {
            formComponent.OpenUIForm(form);
        }

        public void RemoveUIForm(UIFormId form)
        {
            formComponent.RemoveUIForm(form);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_StartGame = false;
            m_LoadGame = false;

            formComponent.AddForm(UIFormId.MenuForm);
            formComponent.OnEnter(procedureOwner);

            GameEntry.ModuleComponent.ResetModule();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);



            formComponent.OnLeave(procedureOwner, isShutdown);
        }

        private int GetNextSceneId()
        {
            if (m_LoadGame)
            {
                return GameEntry.Config.GetInt("Scene.Home");
            }

            return m_StartGame ? GameEntry.Config.GetInt("Scene.Create") : 0;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_StartGame && !m_LoadGame) return;
            procedureOwner.SetData<VarInt32>("NextSceneId", GetNextSceneId());
            procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        public void LoadHeadData()
        {
            // 1. 真实可写目录
            string rootPath = Path.Combine(Application.persistentDataPath, "GameSaves.idx");
           

            // 2. 创建（返回 IFileSystem 实例，同时内部以 rootPath 当 key 注册）
            IFileSystem fileSystems = GameEntry.FileSystem.CreateFileSystem(
                rootPath,
                FileSystemAccess.ReadWrite,
                1024, 1024);

            // 3. 以后读取：必须再传同一条完整路径
            fileSystems = GameEntry.FileSystem.GetFileSystem(rootPath);   // 注意：不是 "GameSaves"



            for (var i = 0; i < SaveMaxCount; ++i)
            {
                var bytes = fileSystems.ReadFile("GameSaves/" + i + "/SaveHead.dat");

                if (bytes == null)
                {
                    var data0 = new HeadData();
                    var json1 = Utility.Json.ToJson(data0);
                    byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(json1);
                    fileSystems.WriteFile("GameSaves/" + i, bytes1, 0);
                    fileSystems.SaveAsFile("GameSaves/" + i, "GameSaves/" + i + "/SaveHead.dat");

                    return;
                }

                var json = Encoding.UTF8.GetString(bytes);
                var data = Utility.Json.ToObject<HeadData>(json);
                headData.Add(data);
            }
        }
    }
}
