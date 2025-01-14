using Python.Runtime;

namespace TestApp;

public class Service
{
    // get Audio of youtube by save on path

    private static bool _isPythonInitialized = false;
    public static void InitializePython()
    {
        if (!_isPythonInitialized) // Ensure Python is initialized only once
        {
            Runtime.PythonDLL = @"C:\Program Files\Python312\python312.dll";
            PythonEngine.Initialize();
            _isPythonInitialized = true;
        }
    }

    //get video audio as mp3 and save it on python files | scriptName is python file name
    public string getAudioVidoeAsMp3(string scriptName, string video_Url)
    {
        InitializePython();
        //Runtime.PythonDLL = @"C:\Program Files\Python312\python312.dll";
        //PythonEngine.Initialize();

        using (Py.GIL())
        {
            dynamic sys = Py.Import("sys");
            sys.path.append(@"D:\Final_Project_Daneshgah\Youtube_Project\YoutubeProject\TestApp");

            PyObject pythonScript = Py.Import(scriptName);


            PyString videoUrl = new PyString(video_Url);



            PyObject pyObjResult = pythonScript.InvokeMethod("download_audio_as_mp3", new PyObject[] { videoUrl });

            return pyObjResult.ToString();
        }
    }
}
