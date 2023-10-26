using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum AnimationLayerType
{
    Default = 0,
    Snow = 1,
    UpperBody = 2,
}

public enum AnimationUpperBody
{
    HandWave,
    Drink,

}

public enum AnimationDialogue
{
    None,
    Stand,
    Talk01,
    Talk02,

}

public class AnimationController
{
    public Animator anim;

    // Move, Interact ���� ��ġ�� �ִµ�
    // Move������ float ��ġ�� ���⿡ �Ѱ��ָ鼭 �ִϸ��̼��� ������
    // Interact ������ Ʈ���� ���ַ� �Լ��� �����Ű�鼭 �ִϸ��̼��� ������
    // �׷��ٸ� Move�� Interact�� ���� Animation���� �̾����ֵ��� �ؾ��ϴµ�...

    // �ִϸ��̼� ��� �ӵ� ���� ��ġ
    // public float _animModifier = -0.3f;

    private Coroutine lastAnim;

    string currentAnim;
    int currentAnimLayer = 0;
    float weightToChange;
    float speedToChange;

    public void ChangePos(AnimationDialogue animationType)
    {
        switch (animationType)
        {
            case AnimationDialogue.None:
                break;
            case AnimationDialogue.Stand:
                break;
            case AnimationDialogue.Talk01:
                break;
            case AnimationDialogue.Talk02:
                break;
        }
    }

    public void UpdateAnimation()
    {
        float weight = LerpFloat(anim.GetLayerWeight(currentAnimLayer), weightToChange);
        float speed = LerpFloat(anim.speed, speedToChange);

        anim.SetLayerWeight(currentAnimLayer, weight);
        anim.speed = speed;
    }

    private float LerpFloat(float startValue, float endValue)
    {
        float difference = endValue - startValue;

        if (Mathf.Approximately(difference, 0f))
        {
            return endValue;
        }

        startValue += (difference > 0) ? Time.deltaTime : -Time.deltaTime;

        return startValue;
    }

    public void ChangeAnimationTo(string key, AnimationLayerType layerType, float weight, float speed = 1f)
    {
        if (currentAnim != key)
        {
            currentAnim = key;
            ChangeAnimationLayer(layerType, weight, speed);
            anim.Play(key);
        }
    }

    public void ChangeAnimationLayer(AnimationLayerType layerType, float weight, float speed = 1f, bool isGlobal = true)
    {
        if (isGlobal)
        {
            var layers = Enum.GetValues(typeof(AnimationLayerType));
            foreach (AnimationLayerType layer in layers)
            {
                if (currentAnimLayer == (int)layer)
                    continue;

                anim.SetLayerWeight((int)layer, 0f);
            }

        }
        currentAnimLayer = (int)layerType;
        weightToChange = weight;
        speedToChange = speed;
    }

    public void Play(AnimationUpperBody animation, float startLerpTime = 0.5f, float endLerpTime = 0.8f)
    {
        if (lastAnim != null)
            CoroutineManager.StopCoroutine(lastAnim);

        lastAnim = CoroutineManager.StartCoroutine(PlayCO(animation, startLerpTime, endLerpTime));
    }

    IEnumerator PlayCO(AnimationUpperBody animation, float startLerpTime = 0.5f, float endLerpTime = 0.8f)
    {
        var clips = anim.GetCurrentAnimatorClipInfo((int)AnimationLayerType.UpperBody);
        anim.Play(Enum.GetName(typeof(AnimationUpperBody), animation), (int)AnimationLayerType.UpperBody, 0f);
        float lerpTime = 0f;

        DOTween.To(() => lerpTime, x => lerpTime = x, 1f, startLerpTime).onUpdate = () =>
        {
            anim.SetLayerWeight((int)AnimationLayerType.UpperBody, lerpTime);
        };

        yield return new WaitForSeconds(clips[0].clip.length - endLerpTime);

        DOTween.To(() => lerpTime, x => lerpTime = x, 0f, endLerpTime).onUpdate = () =>
        {
            anim.SetLayerWeight((int)AnimationLayerType.UpperBody, lerpTime);
        };
    }
}
