using JEH01V_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace H89R6J_GUI_2022231.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Character> Characters { get; set; }

        public ICommand CreateCharacterCommand { get; set; }
        public ICommand DeleteCharacterCommand { get; set; }
        public ICommand UpdateCharacterCommand { get; set; }


        private Character selectedCharacter;

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set 
            {
                if (value  != null)
                {
                    selectedCharacter = new Character()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Element = value.Element,
                        Weapons = value.Weapons,
                        Artifacts = value.Artifacts
                    };
                    OnPropertyChanged();
                    (DeleteCharacterCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Characters = new RestCollection<Character>("http://localhost:8160/", "character");


                CreateCharacterCommand = new RelayCommand(
                    () => {
                        Characters.Add(new Character()
                        {
                            Name = SelectedCharacter.Name,
                            Element = SelectedCharacter.Element,
                            Weapons = SelectedCharacter.Weapons,
                            Artifacts = SelectedCharacter.Artifacts
                        });
                    });

                DeleteCharacterCommand = new RelayCommand(
                    () => { Characters.Delete(SelectedCharacter.Id); },
                    () => { return SelectedCharacter != null; });

                UpdateCharacterCommand = new RelayCommand(
                    () => { Characters.Update(SelectedCharacter); });

                SelectedCharacter = new Character();
            }
        }
    }
}
