﻿namespace Snake_box
{
    public interface ITimeService
    {
        float DeltaTime();
        float UnscaledDeltaTime();
        float FixedDeltaTime();
        float RealtimeSinceStartup();
        float GameTime();
        long Timestamp();
        void SetTimeScale(float timeScale);        
        void ResetDeltaTime();
        float TimeSinceLevelStart();
    }
}
