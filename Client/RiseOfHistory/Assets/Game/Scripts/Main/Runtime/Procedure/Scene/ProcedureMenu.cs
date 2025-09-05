using Game.Scripts.Main.Runtime.Game;
using Game.Scripts.Main.Runtime.SaveData;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIForm;
using System.Collections.Generic;
using System.Text;
using GameFramework;
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
            var fileSystems = GameEntry.FileSystem.GetFileSystem("Local");

            if (fileSystems == null)
            {
                return;
            }

            for (var i = 0; i < SaveMaxCount; ++i)
            {
                var bytes = fileSystems.ReadFile("Save/" + i + "/SaveHead.dat");

                if (bytes == null)
                {
                    var data0 = new HeadData();
                    var json1 = Utility.Json.ToJson(data0);
                    fileSystems.WriteFile("Save/" + i + "/SaveHead.dat", json1);
                    return;
                }

                var json = Encoding.UTF8.GetString(bytes);
                var data = Utility.Json.ToObject<HeadData>(json);
                headData.Add(data);
            }
        }
    }
}
