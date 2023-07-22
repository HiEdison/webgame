using UnityEngine;
 
public class Frame : MonoBehaviour
{
    // 记录帧数
    private int _frame;
    // 上一次计算帧率的时间
    private float _lastTime;
    // 平均每一帧的时间
    public static float _frameDeltaTime;
    // 间隔多长时间(秒)计算一次帧率
    public static float _Fps;
    private const float _timeInterval =1f;
 
    void Start()
    {
        _lastTime = Time.realtimeSinceStartup;
    }
 
    void Update()
    {
        FrameCalculate();
    }
 
    private void FrameCalculate()
    {
        _frame++;
        if (Time.realtimeSinceStartup - _lastTime < _timeInterval)
        {
            return;
        }
 
        float time = Time.realtimeSinceStartup - _lastTime;
        _Fps = _frame / time;
        _frameDeltaTime = time / _frame;
 
        _lastTime = Time.realtimeSinceStartup;
        _frame = 0;
    }
}