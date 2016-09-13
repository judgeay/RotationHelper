﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace RotationHelper
{
    [Serializable]
    public class RotationHelperFile
    {
        #region Constructors

        public RotationHelperFile()
        {
            Rotations = new ObservableCollection<Rotation>();
        }

        #endregion

        #region Properties

        public ObservableCollection<Rotation> Rotations { get; }

        public string Title { get; set; }

        #endregion

        #region Methods

        public static RotationHelperFile Deserialize(string path)
        {
            try
            {
                RotationHelperFile wagFile;

                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var bin = new XmlSerializer(typeof(RotationHelperFile));

                    wagFile = (RotationHelperFile)bin.Deserialize(stream);
                }

                return wagFile;
            }
            catch
            {
                MessageBox.Show("Unable to load the selected file");
                return null;
            }
        }

        public void Serialize(string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Create))
                {
                    Serialize(stream);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Unable to save rotation file");
            }
        }

        private void Serialize(Stream stream)
        {
            var bin = new XmlSerializer(typeof(RotationHelperFile));
            bin.Serialize(stream, this);
        }

        #endregion
    }

    [Serializable]
    public class Rotation : INotifyPropertyChanged
    {
        #region Fields

        private string _title;

        #endregion

        #region Constructors

        public Rotation()
        {
            KeyCommands = new ObservableCollection<KeyCommand>();
        }

        #endregion

        #region Properties

        public ObservableCollection<KeyCommand> KeyCommands { get; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    [Serializable]
    public class KeyCommand : INotifyPropertyChanged
    {
        #region Fields

        [XmlIgnore]
        public static readonly ObservableCollection<VirtualKeyCode> PossibleKeys = new ObservableCollection<VirtualKeyCode>
        {
            VirtualKeyCode.VK_A,
            VirtualKeyCode.VK_B,
            VirtualKeyCode.VK_C,
            VirtualKeyCode.VK_D,
            VirtualKeyCode.VK_E,
            VirtualKeyCode.VK_F,
            VirtualKeyCode.VK_G,
            VirtualKeyCode.VK_H,
            VirtualKeyCode.VK_I,
            VirtualKeyCode.VK_J,
            VirtualKeyCode.VK_K,
            VirtualKeyCode.VK_L,
            VirtualKeyCode.VK_M,
            VirtualKeyCode.VK_N,
            VirtualKeyCode.VK_O,
            VirtualKeyCode.VK_P,
            VirtualKeyCode.VK_Q,
            VirtualKeyCode.VK_R,
            VirtualKeyCode.VK_S,
            VirtualKeyCode.VK_T,
            VirtualKeyCode.VK_U,
            VirtualKeyCode.VK_V,
            VirtualKeyCode.VK_W,
            VirtualKeyCode.VK_X,
            VirtualKeyCode.VK_Y,
            VirtualKeyCode.VK_Z,
            VirtualKeyCode.VK_0,
            VirtualKeyCode.VK_1,
            VirtualKeyCode.VK_2,
            VirtualKeyCode.VK_3,
            VirtualKeyCode.VK_4,
            VirtualKeyCode.VK_5,
            VirtualKeyCode.VK_6,
            VirtualKeyCode.VK_7,
            VirtualKeyCode.VK_8,
            VirtualKeyCode.VK_9
        };

        private byte _blue;

        [XmlIgnore]
        private Color _color;

        private byte _green;
        private byte _red;

        #endregion

        #region Properties

        public byte Blue
        {
            get { return _blue; }
            set
            {
                if (Blue == value) return;
                _blue = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        [XmlIgnore]
        public Color Color
        {
            get { return Color.FromArgb(255, Red, Green, Blue); }
        }

        public byte Green
        {
            get { return _green; }
            set
            {
                if (Green == value) return;
                _green = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public VirtualKeyCode Key { get; set; }

        public byte Red
        {
            get { return _red; }
            set
            {
                if (Red == value) return;
                _red = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}