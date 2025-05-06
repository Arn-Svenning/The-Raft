
namespace NPC
{
    /// <summary>
    /// This enum stores all the states that the NPCs can be in
    /// </summary>
    public enum NPCStates
    {
        Idle,

        Fishing,
        Paddle,
        CollectRain,
        RepairRaft,
        CasualChatter
    };

    public class NPCStateNames
    {
        public static readonly string IDLE_STATE = NPCStates.Idle.ToString();
        public static readonly string FISHING_STATE = NPCStates.Fishing.ToString();
        public static readonly string PADDLING_STATE = NPCStates.Paddle.ToString();
        public static readonly string COLLECT_RAIN_STATE = NPCStates.CollectRain.ToString();
        public static readonly string REPAIR_RAFT_STATE = NPCStates.RepairRaft.ToString();
        public static readonly string CASUAL_CHATTER_STATE = NPCStates.CasualChatter.ToString();
    }
}

