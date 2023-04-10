using System.Collections.ObjectModel;
using System.Windows.Input;
using TreeView.Maui.Core;

namespace PhotoAuto
{
    public class MainPageViewModel : BindableObject
    {
        public ObservableCollection<TreeViewNode> Nodes { get; set; } = new();
        public ICommand RandomizeNamesCommand { get; set; }

        public ICommand SwitchIsLeafCommand { get; set; }
        public MainPageViewModel()
        {           
            var folders = Directory.GetDirectories("/storage/emulated/0/DCIM/Rony");
            foreach (var folder in folders)
            {
                var RootNode = new TreeViewNode(folder.Substring(30), "folder.png");
                foreach (var item in Helper.files_help(folder))
                {
                    var SupNode = new TreeViewNode(item.Substring(30), item);
                    RootNode.Children.Add(SupNode);
                }
                Nodes.Add(RootNode);
            }


            RandomizeNamesCommand = new Command(() =>
            {
                foreach (var node in Nodes)
                {
                    node.Name = node.Name + " " + new Random().Next(0, 100);
                }
            });

            SwitchIsLeafCommand = new Command(() =>
            {
                foreach (var node in Nodes)
                {
                    node.IsLeaf = !node.IsLeaf;
                }
            });
                     
        }
    }
}