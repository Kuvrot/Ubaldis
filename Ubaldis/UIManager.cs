using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;
using Stride.UI.Panels;
using Stride.Core.IO;
using Stride.UI.Events;

namespace Ubaldis
{
    public class UIManager : SyncScript
    {
        public bool showStore = false;
        public override void Start ()
        {
            var openStore = GameManager.UI.RootElement.FindName("storeToggle") as Button;
            openStore.Click += ShowStore;
            var closeStore = GameManager.UI.RootElement.FindName("closeStore") as Button;
            closeStore.Click += CloseStore;

            var store = GameManager.UI.RootElement.FindName("store") as Canvas;
            store.Visibility = Stride.UI.Visibility.Hidden;
            showStore = false;
        }

        public override void Update()
        {
            
            
        }

        private void ShowStore (object sender, RoutedEventArgs e)
        {
            var store = GameManager.UI.RootElement.FindName("store") as Canvas;
            if (showStore)
            {
                store.Visibility = Stride.UI.Visibility.Hidden;
                showStore = false;
            }
            else if (!showStore)
            {
                store.Visibility = Stride.UI.Visibility.Visible;
                showStore = true;
            }
        }

        private void CloseStore (object sender, RoutedEventArgs e)
        {
            var store = GameManager.UI.RootElement.FindName("store") as Canvas;
            store.Visibility = Stride.UI.Visibility.Hidden;
            showStore = false;
        }
    }
}
