using MenuEditor.Services;
using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MenuEditor.Models;

namespace MenuEditor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            this.workPath = Configuration.GetValue<string>("finallyopen");
            this.dataService = new JsonDataService();

            this.TopMenu.SaveData += onSaveData;
            this.TopMenu.NewProject += onNewProj;

            if (System.IO.File.Exists(this.workPath))
            {
                this.rawMenu = dataService.LoadData<RawMenu>(workPath + "/src.menu");
            }
            else
            {
                this.rawMenu = new RawMenu();
            }

            load();
        }

        private IDataService dataService;

        private string workPath;

        private RawMenu rawMenu;

        private void onSaveData()
        {
            if (!System.IO.Directory.Exists(workPath))
            {
                var Dialog = new System.Windows.Forms.FolderBrowserDialog();
                Dialog.Description = "文件夹必须为空";
                var result = Dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dialog.SelectedPath);
                    if (di.GetFiles().Length + di.GetDirectories().Length == 0)
                    {
                        //目录为空
                        this.workPath = Dialog.SelectedPath;
                        dataService.SaveData(workPath + "/src.menu", rawMenu);
                    }
                }
            }
            else
            {
                dataService.SaveData(workPath + "/src.menu", rawMenu);
            }
        }

        private void onNewProj()
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            Dialog.Description = "文件夹必须为空";
            var result = Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dialog.SelectedPath);
                if (di.GetFiles().Length + di.GetDirectories().Length == 0)
                {
                    //目录为空
                    this.workPath = Dialog.SelectedPath;
                    this.rawMenu = new RawMenu();
                    this.load();
                }
            }
        }

        private void load()
        {
            var pages = new List<PageViewModel>();
            foreach (var item in rawMenu.Pages)
            {
                pages.Add(new PageViewModel(item, new Views.EditMenu()));
            }
            this.MenuCollection = new ObservableCollection<PageViewModel>();
        }

        public TopMenuViewModel TopMenu = new TopMenuViewModel();

        private ObservableCollection<PageViewModel> _MenuCollection = new() { };
        public ObservableCollection<PageViewModel> MenuCollection
        {
            get => _MenuCollection;
            set
            {
                _ = SetProperty(ref _MenuCollection, value);
            }
        }


    }
}
