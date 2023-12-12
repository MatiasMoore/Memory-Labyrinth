namespace MemoryLabyrinth.Achievemnts
{
    public abstract class AchievementTracker
    {
        protected readonly Achievement _achievement;

        public AchievementTracker(Achievement achievement)
        {
            _achievement = achievement;
        }
    }
}
