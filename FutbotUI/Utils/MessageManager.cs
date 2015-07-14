using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FutbotUI.Utils
{
    public class MessageManager
    {
        public static MessageBoxResult ShowDeleteConfirmMessage()
        {
            return MessageBox.Show("Are You sure to delete this item?", "Delete item", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }
    }
}
