using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplayUI : MonoBehaviour, IStats
{
    public Image[] hpImage;
    public GameObject list;
    public float hp { get; set; }
    public float maxHp { get; set; }
    public const int _maxHp = 5;
    public Sprite Front;

    private void Awake()
    {
        hp = _maxHp;
        maxHp = _maxHp;
        for (int i = 0; i < maxHp; i++)
        {
            hpImage[i] = list.transform.GetChild(i).GetComponent<Image>();
            hpImage[i].sprite = Front;
        }

        SetHp(3.5f);
    }

    public void SetHp(float val)
    {

        hp = val;

        //Hp_Max값을 넘기지 못하도록 처리함
        hp = Mathf.Clamp(hp, 0, maxHp);

        //전체적으로 안보이게 한다.
        for (int i = 0; i < maxHp; i++)
        {
            //전부 비워둔 상태에서,
            hpImage[i].fillAmount = 0;

            //Pull로 채워줘야 되는것은 100% 채우고
            if ((int)hp > i)
            {
                hpImage[i].fillAmount = 1;
            }

            //세밀하게 이미지를 그릴 수 있도록 함
            if ((int)hp == i)
                hpImage[i].fillAmount = hp - (int)hp;  //ex -> Hp = 3.4의 경우 3.4에 3을 빼면 0.4만 남는다.
        }
    }
}
