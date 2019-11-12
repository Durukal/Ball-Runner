

namespace Assets.Scripts
{
    public static class BallTimer
    {
        public static float InitialDeathTime = 50;
        public static float DeathTime = InitialDeathTime;
        public static float CurrentTime;

        public static float RoadPieceTimeBonus { get; private set; }
        public static float CoinTimeBonus { get; private set; }

        public static void SetTimeBonuses(float roadPieceBonus, float coinBonus)
        {
            RoadPieceTimeBonus = roadPieceBonus;
            CoinTimeBonus = coinBonus;
        }

        public static void IncrementTimer(float value)
        {
            DeathTime += value;
        }
    }
}
