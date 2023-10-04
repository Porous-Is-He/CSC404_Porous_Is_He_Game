
public class LiquidInfo
{
    public string liquidType;

    public int liquidAmount;

    public LiquidInfo(string liquidType, int liquidAmount)
    {
        this.liquidType = liquidType;
        this.liquidAmount = liquidAmount;
    }



    //how to use:
    //LiquidInfo liquid = new LiquidInfo("Water", 1);
    //liquid.liquidAmount = 3;
}
