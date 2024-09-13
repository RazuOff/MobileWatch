

public class WorldTimeAPI : APIController
{
    private const string URL = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";
    private void Awake()
    {
        _url = URL;
    }
}


