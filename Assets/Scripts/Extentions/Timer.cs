using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;


namespace Extentions
{
    public class Timer
    {
        public async void WaitForSeconds(int _time)
        { 
            Task.Delay(_time*1000).Wait();
        }
    }
}