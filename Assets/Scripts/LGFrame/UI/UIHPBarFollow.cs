using PathologicalGames;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class UIHPBarFollow : MonoBehaviour
{
    public enum type
    {
        friend,
        enemy
    }

    [SerializeField]
    private type _type;
    public type Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    public float xOffset;
    public float yOffset;

    private RectTransform recTransform;
    private const string friend = "UIHPBar_Friend";
    private const string enemy = "UIHPBar_Enemy";
    private const string pool = "HPBars";

    private Slider slider;
    private float value;

    [SerializeField]
    private float delay = 0.05f;

    [SerializeField]
    private float change = 0.05f;

    private bool timing = false;

    // Use this for initialization
    private void Start()
    {
        SetRectTransform();
    }

    private void Update()
    {
        UpdataPostion();
    }

    private void UpdataPostion()
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);
        recTransform.position = player2DPosition + new Vector2(xOffset, yOffset);

        //血条超出屏幕就不显示
        if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
            recTransform.gameObject.SetActive(false);
        else
            recTransform.gameObject.SetActive(true);
    }

    private void SetRectTransform()
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(transform.position);
        switch (this.Type)
        {
            case type.friend:
                this.recTransform = PoolManager.Pools[pool].Spawn(friend) as RectTransform;
                break;

            case type.enemy:
                this.recTransform = PoolManager.Pools[pool].Spawn(enemy) as RectTransform;
                break;
        }

        this.slider = this.recTransform.GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        this.value = Mathf.Clamp01(value);
        if (!timing)
            this.StartCoroutine(this.valueChange());
    }

    private IEnumerator valueChange()
    {
        this.timing = true;
        this.slider.value = Mathf.Clamp(this.slider.value - this.change, this.value, 1f);

        yield return new WaitForSeconds(this.delay);

        if (this.slider.value <= this.value)
            this.timing = false;
        else
            this.StartCoroutine(this.valueChange());
    }
}