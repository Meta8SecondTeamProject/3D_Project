using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }
    public IEnumerator FadeIn()
    {
        //시작전 혹시 모르니 알파값 초기화
        fadeCanvasGroup.alpha = 0;

        float timer = 0f;

        //페이드 지속시간동안 반복
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            //fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            //화면이 너무 확확 돌아가는 느낌을 받아서 Lerp보다 부드러운 SmoothStep 사용, 인자로 들어가는 값은 Lerp랑 같음
            fadeCanvasGroup.alpha = Mathf.SmoothStep(0, 1, timer / fadeDuration);

            yield return null;
        }
        //마무리 하고도 초기화 진행
        fadeCanvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut()
    {
        fadeCanvasGroup.alpha = 1;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            //fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, timer / (fadeDuration * 2));
            fadeCanvasGroup.alpha = Mathf.SmoothStep(1, 0, timer / fadeDuration);


            //fadeCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;
    }

    public IEnumerator ShowImage(Image image, float displayDuration = 2f)
    {
        //뭐든 인자로 들어오는거 있으면 항상 null체크하기
        if (image == null) yield break;

        //Color color = image.color; //이미지의 컬러를 받아옴
        //color.a = 0f; //알파값을 0으로 맞춤
        //image.color = color; //알파값 0을 이미지에 적용해서 완전히 불투명한 상태로 변경?
        //그러면 이건 FadeIn이 아니라 FadeOut 않인가
        //TODO : (완료됨) 메서드 이름 변경
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f); //보기 불편해서 변경함

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            //color.a = Mathf.SmoothStep(0, 1, timer / fadeDuration);
            //image.color = color;
            float alpha = Mathf.SmoothStep(0,1, timer / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f); //최종 알파값 1

        //이미지 표시 시간 동안 대기
        yield return new WaitForSeconds(displayDuration);
    }

    public IEnumerator HideImage(Image image)//이미지가 사라지기만 하면 되니 인자로 float값 필요없음
    {
        if (image == null) yield break;

        //image.color.a = 0f; //이게 됐다면 매번 new 선언할 필요도 없을텐데
        image.color = new Color(image.color.r,image.color.g,image.color.b, 1f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.SmoothStep(1, 0, timer / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
}

