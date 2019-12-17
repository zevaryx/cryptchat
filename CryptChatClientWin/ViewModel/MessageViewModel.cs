using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using CryptChat.Client.Win.Models;

namespace CryptChat.Client.Win.ViewModel
{
    class MessageViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }

        public void LoadMessages()
        {
            ObservableCollection<Message> messages = new ObservableCollection<Message>();
        }
    }
}
