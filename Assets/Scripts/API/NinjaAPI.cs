

public class NinjaAPI : APIController
{
    private const string URL = "https://api.api-ninjas.com/v1/worldtime?lat=55.751244&lon=37.618423";

    private void Awake()
    {
        _keyName="X-Api-Key";
        _useKey = true;
        _key = "PfJeVrWvpHOIgCGOnPzQdw==AJ3G6SWv94nCEoXy";
        _url = URL;
    }
}

