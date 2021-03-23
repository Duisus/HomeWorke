using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.InheritAppAndWindow
{
    class MyApplication : Application
    {
        protected override void OnStartup(StartupEventArgs args) //Запускает код при запуске приложения
        {
            base.OnStartup(args); 
            MyWindow win = new MyWindow();
            win.Show(); //Открывает окно
        }
    }
}