using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using CryptChat.Core.Models;
using CryptChat.Core.Security;

namespace CryptChat.Client.Win.Models
{
    class Message : Core.Models.Message, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
