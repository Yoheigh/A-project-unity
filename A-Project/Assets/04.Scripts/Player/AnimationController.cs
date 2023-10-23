using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationLayerType
{
    Default = 0,
    UpperBody = 1,
    Snow = 2,
}

public enum AnimationUpperBody
{
    HandWave,
}

public enum AnimationDialogue
{
    Talk01,
    Talk02,

}

public class AnimationController
{
    public Animator anim;

    // 애니메이션 재생 속도 조정 수치
    // public float _animModifier = -0.3f;

    private Coroutine lastAnim;

    string currentAnim;
    int currentAnimLayer = 0;
    float weightToChange;
    float speedToChange;
    float currentTime = 0;

    public void UpdateAnimation()
    {
        float weight = LerpFloat(anim.GetLayerWeight(currentAnimLayer), weightToChange);
        float speed = LerpFloat(anim.speed, speedToChange);

        anim.SetLayerWeight(currentAnimLayer, weight);
        anim.speed = speed;
    }

    private float LerpFloat(float startValue, float endValue, float lerpTime = 1f)
    {
        float difference = endValue - startValue;

        if (Mathf.Approximately(difference, 0f))
        {
            return endValue;
        }

        startValue += (difference > 0) ? Time.deltaTime : -Time.deltaTime;

        if(currentTime < lerpTime)
            currentTime += Time.deltaTime;

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

    public void Play(AnimationUpperBody animation)
    {
        if (lastAnim != null)
            CoroutineManager.StopCoroutine(lastAnim);

        lastAnim = CoroutineManager.StartCoroutine(PlayCO(animation));
    }

    IEnumerator PlayCO(AnimationUpperBody animation)
    {
        anim.SetLayerWeight((int)AnimationLayerType.UpperBody, 1f);
        anim.Play(Enum.GetName(typeof(AnimationUpperBody), animation), (int)AnimationLayerType.UpperBody, 0f);
        var clip = anim.GetCurrentAnimatorClipInfo((int)AnimationLayerType.UpperBody);

        yield return new WaitForSeconds(clip[0].clip.length);

        anim.SetLayerWeight((int)AnimationLayerType.UpperBody, 0f);
    }
}