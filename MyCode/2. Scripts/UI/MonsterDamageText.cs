using DG.Tweening;
using TMPro;
using UnityEngine;
//UI 애니메이션 구현하기
public class UIDamageText : MonoBehaviour
{
    private int rand;
    private float randDir;
    TMP_Text textPro;
    Sequence sequence;
    private void Awake()
    {
        textPro = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        rand = Random.Range(100, 200);
        //randDir = Random.Range(-30, 30);
        //transform.rotation = Quaternion.Euler(0, 0, randDir);

        textPro.text = rand.ToString();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        sequence = DOTween.Sequence()//시퀀스 생성
        .Join(transform.DOLocalMove(new Vector3(0, 1, 0), 1))//UI좌표
        .Join(transform.DOScale(new Vector3(1, 1, 1), 0.2f))
        .Insert(0.2f, transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.8f))//0.3f
        .Insert(0.2f, transform.GetComponent<TextMeshProUGUI>().DOFade(0, 0.4f));
    }
}