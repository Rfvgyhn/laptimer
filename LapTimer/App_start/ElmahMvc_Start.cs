[assembly: WebActivator.PreApplicationStartMethod(typeof(LapTimer.App_Start.ElmahMvc), "Start")]
namespace LapTimer.App_Start
{
    public class ElmahMvc
    {
        public static void Start()
        {
            Elmah.Mvc.Bootstrap.Initialize();
        }
    }
}