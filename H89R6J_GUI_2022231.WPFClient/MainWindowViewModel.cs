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
        #region Characthers
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
        #endregion

        #region Weapons
        public RestCollection<Weapon> Weapons { get; set; }


        public ICommand CreateWeaponCommand { get; set; }
        public ICommand DeleteWeaponCommand { get; set; }
        public ICommand UpdateWeaponCommand { get; set; }

        private Weapon selectedWeapon;

        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set 
            {
                if (value != null)
                {
                    selectedWeapon = new Weapon()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        PeakDmg = value.PeakDmg,
                        CharacterId = value.CharacterId,
                        Character = value.Character

                    };
                    OnPropertyChanged();
                    (DeleteWeaponCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Artifacts

        public RestCollection<Artifact> Artifacts { get; set; }


        public ICommand CreateArtifactCommand { get; set; }
        public ICommand DeleteArtifactCommand { get; set; }
        public ICommand UpdateArtifactCommand { get; set; }

        private Artifact selectedArtifact;

        public Artifact SelectedArtifact
        {
            get { return selectedArtifact; }
            set 
            {
                if (value != null)
                {
                    selectedArtifact = new Artifact()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Cost = value.Cost,
                        CharacterId = value.CharacterId,
                        Character = value.Character
                    };
                    OnPropertyChanged();
                    (DeleteArtifactCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        #endregion

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
                Characters = new RestCollection<Character>("http://localhost:8160/", "character", "hub");
                Weapons = new RestCollection<Weapon>("http://localhost:8160/", "weapon", "hub");
                Artifacts = new RestCollection<Artifact>("http://localhost:8160/", "artifact", "hub");


                #region Character Commands
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
                #endregion

                /////////////////////////////////////////////////////
                
                #region Weapon Commands
                CreateWeaponCommand = new RelayCommand(
                    () => {
                        Weapons.Add(new Weapon()
                        {
                            Name = SelectedWeapon.Name,
                            PeakDmg = SelectedWeapon.PeakDmg,
                            CharacterId = SelectedWeapon.CharacterId,
                            Character = SelectedWeapon.Character
                        });
                    });

                DeleteWeaponCommand = new RelayCommand(
                    () => { Weapons.Delete(SelectedWeapon.Id); },
                    () => { return SelectedWeapon != null; });

                UpdateWeaponCommand = new RelayCommand(
                    () => { Weapons.Update(SelectedWeapon); });

                #endregion

                /////////////////////////////////////////////////////

                #region Artifact Commands
                CreateArtifactCommand = new RelayCommand(
                    () => {
                        Artifacts.Add(new Artifact()
                        {
                            Name = SelectedArtifact.Name,
                            Cost = SelectedArtifact.Cost,
                            CharacterId = SelectedArtifact.CharacterId,
                            Character = SelectedArtifact.Character
                        });
                    });

                DeleteArtifactCommand = new RelayCommand(
                    () => { Artifacts.Delete(SelectedArtifact.Id); },
                    () => { return SelectedArtifact != null; });

                UpdateArtifactCommand = new RelayCommand(
                    () => { Artifacts.Update(SelectedArtifact); });
                #endregion

                SelectedCharacter = new Character();
                SelectedWeapon = new Weapon();
                SelectedArtifact = new Artifact();
            }
        }
    }
}
