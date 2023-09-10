using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Items : MonoBehaviour
{

    public IdItem IdItem;
    public int IdPopUp = -1;
    public EnumGetGift EnumGetGift;
    public IdCardItem IdCard;
    public Material[] mats;
    public MeshRenderer meshCard;
    [Header("Map3")]
    public Number Number;
    public ManhRua ManhRua;
    public TronVuong TronVuong;
    private void Start()
    {
        if (meshCard != null)
        {
            Material[] mat = new Material[meshCard.materials.Length];

            mat[0] = mats[(int)IdCard];
            meshCard.materials = mat;

        }
    }
}

public enum IdItem
{
    Card = 0,
    Controller = 1,
    pin = 2,
    NutBamDrone = 3,
    Cong8 = 4,
    Sung = 5,
    Hoso = 6,
    Huyhieu = 7,
    Dan = 8,
    NutBam = 9,
    CardTu = 10,
    Sach = 11,
    DungSach = 12,
    XepSach = 13,
    XepViTri = 14,
    TruMove = 15,
    Number = 16,
    ControllerMap3 = 17,
    MiniGame4 = 18,
    NutBamDrone2 = 19,
    ManhRua = 20,
    MayDap = 21,
    CanGat = 22,
    TronVuong =23,
    DatVong = 24,
}
public enum EnumGetGift
{
    X2Speed = 0,
    GetAllPin = 1,
    GetX1BulletNow = 4,
    GetHoSoNow = 5,
    GetHanCuffX1 = 6,
}
public enum IdCardItem
{
    Card1 = 0,
    Card2 = 1,
    Card3 = 2,
    Card4 = 3,
    Card5 = 4
}
public enum Number
{
    Number0 = 0,
    Number1 = 1,
    Number2 = 2,
    Number3 = 3,
    Number4 = 4,
    Number5 = 5,
}
public enum ManhRua
{
    Manh0 = 0,
    Manh1 = 1,
    Manh2 = 2,
    Manh3 = 3,
    Manh4 = 4,
    Manh5 = 5,

}
public enum TronVuong
{
    tron = 0,
    vuong = 1,
    tamgiac = 2,
    ngu = 3,
    sao = 4,

}