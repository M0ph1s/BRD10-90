namespace Morpheus.Bard;

public class BRDBattleData
{
    public static BRDBattleData Instance = new();// 静态的 BRDBattleData 实例，用于存储战斗数据

    public void Reset()// 重置战斗数据的方法
    {
        Instance = new BRDBattleData();// 将 Instance 设置为新的 BRDBattleData 实例
    }

}