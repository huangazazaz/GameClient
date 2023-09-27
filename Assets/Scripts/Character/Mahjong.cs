
public class Mahjong
{
    private int type;
    private int weight;
    private int index;

    public Mahjong(int t, int w, int i)
    {
        type = t;
        weight = w;
        index = i;
    }

    public string getName()//与GameObject名字对应
    {
        string name = "";
        switch (type)
        {
            case 0:
                break;
            case 1:
                name += "tong";//9个，每个4张，共36张
                break;
            case 2:
                name += "wan";//9个，每个4张，共36张
                break;
            case 3:
                name += "tiao";//9个，每个4张，共36张
                break;
            case 4:
                name += "wind";//4个，每个4张，共16张
                break;
            case 5:
                name += "arrow";//3个，每个4张，共12张
                break;
            case 6:
                name += "flower";//8个，每个1张，共8张
                break;
            default:
                break;
        }
        name += weight;
        name += index;
        return name;

    }
}

