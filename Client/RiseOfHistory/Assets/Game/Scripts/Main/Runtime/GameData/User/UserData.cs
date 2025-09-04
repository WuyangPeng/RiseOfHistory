namespace Game.Scripts.Main.Runtime.GameData.User
{
    public class UserData
    {
        private SexType sexType;

        private int age;

        public UserData()
        {
            sexType = SexType.Male;
        }

        public UserData(SexType sexType)
        {
            this.sexType = sexType;
        }
    }
}