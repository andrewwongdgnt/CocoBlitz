using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUtil  {

    public enum EntityEnum { Coco, Banana, Chomp, Pillow, Shoe };
    public enum ColorEnum { Brown, Yellow, Green, Red, Blue };

    public static Dictionary<EntityEnum, ColorEnum> EntityColorMap = new Dictionary<EntityEnum, ColorEnum>
        {
            { EntityEnum.Coco, ColorEnum.Brown },
            { EntityEnum.Banana, ColorEnum.Yellow },
            { EntityEnum.Chomp, ColorEnum.Green },
            { EntityEnum.Pillow, ColorEnum.Red },
            { EntityEnum.Shoe, ColorEnum.Blue },
        };
    public static Dictionary<ColorEnum, EntityEnum> ColorEntityMap = new Dictionary<ColorEnum, EntityEnum>
        {
            {  ColorEnum.Brown, EntityEnum.Coco },
            { ColorEnum.Yellow, EntityEnum.Banana  },
            { ColorEnum.Green, EntityEnum.Chomp  },
            { ColorEnum.Red, EntityEnum.Pillow  },
            { ColorEnum.Blue, EntityEnum.Shoe  },
        };

    public static Dictionary<ColorEnum, Color32> ColorColorMap = new Dictionary<ColorEnum, Color32>
        {
            { ColorEnum.Brown, new Color32( 160, 109, 41, 255)},
            { ColorEnum.Yellow, new Color32(255,250,40, 255) },
            { ColorEnum.Green, new Color32(85,184,83, 255) },
            { ColorEnum.Red, new Color32(222,0,0, 255) },
            { ColorEnum.Blue, new Color32(15,167,217, 255) },
        };

    public static HashSet<EntityEnum> Entities_2Card = new HashSet<EntityEnum>
        {
             EntityEnum.Coco, EntityEnum.Banana,  EntityEnum.Chomp, EntityEnum.Pillow, EntityEnum.Shoe 
        };
}
