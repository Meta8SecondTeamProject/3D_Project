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
        //������ Ȥ�� �𸣴� ���İ� �ʱ�ȭ
        fadeCanvasGroup.alpha = 0;

        float timer = 0f;

        //���̵� ���ӽð����� �ݺ�
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            //fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            //ȭ���� �ʹ� ȮȮ ���ư��� ������ �޾Ƽ� Lerp���� �ε巯�� SmoothStep ���, ���ڷ� ���� ���� Lerp�� ����
            fadeCanvasGroup.alpha = Mathf.SmoothStep(0, 1, timer / fadeDuration);

            yield return null;
        }
        //������ �ϰ� �ʱ�ȭ ����
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
        //���� ���ڷ� �����°� ������ �׻� nullüũ�ϱ�
        if (image == null) yield break;

        //Color color = image.color; //�̹����� �÷��� �޾ƿ�
        //color.a = 0f; //���İ��� 0���� ����
        //image.color = color; //���İ� 0�� �̹����� �����ؼ� ������ �������� ���·� ����?
        //�׷��� �̰� FadeIn�� �ƴ϶� FadeOut ���ΰ�
        //TODO : (�Ϸ��) �޼��� �̸� ����
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f); //���� �����ؼ� ������

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

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f); //���� ���İ� 1

        //�̹��� ǥ�� �ð� ���� ���
        yield return new WaitForSeconds(displayDuration);
    }

    public IEnumerator HideImage(Image image)//�̹����� ������⸸ �ϸ� �Ǵ� ���ڷ� float�� �ʿ����
    {
        if (image == null) yield break;

        //image.color.a = 0f; //�̰� �ƴٸ� �Ź� new ������ �ʿ䵵 �����ٵ�
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

