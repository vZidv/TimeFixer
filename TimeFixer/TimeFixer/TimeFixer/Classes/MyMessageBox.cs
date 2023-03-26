using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeFixer.Classes
{
    public enum MyMessageBoxOptions
    {
        YesNo,
        Ok
    }

    public static class MyMessageBox
    {
        static View.Windows.Messagebox_win CreateMessageBox(string title, string message, MyMessageBoxOptions messageBoxButtons)
        {
            View.Windows.Messagebox_win messagebox = new View.Windows.Messagebox_win(messageBoxButtons);
            messagebox.title = title;
            messagebox.message = message;
            return messagebox;
        }
        public static void Show(string title, string message)
        {
            View.Windows.Messagebox_win messagebox = CreateMessageBox(title, message, MyMessageBoxOptions.Ok);
            SystemSounds.Exclamation.Play(); 
            messagebox.ShowDialog();
        }
        public static bool Show(string title, string message, MyMessageBoxOptions messageBoxButtons)
        {
            View.Windows.Messagebox_win messagebox = CreateMessageBox(title, message,messageBoxButtons);
            SystemSounds.Exclamation.Play();
            messagebox.ShowDialog();
            return messagebox.result;
        }
        public static bool Show(string title, string message,bool IsError)
        {
            View.Windows.Messagebox_win messagebox = CreateMessageBox(title, message, MyMessageBoxOptions.Ok);
            SystemSounds.Exclamation.Play();
            messagebox.error = IsError;
            messagebox.ShowDialog();
            return messagebox.result;
        }
    }
}
