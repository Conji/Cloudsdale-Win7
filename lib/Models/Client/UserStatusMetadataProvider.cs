using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Providers;

namespace CloudsdaleWin7.lib.Models.Client
{
    public class UserStatusMetadataProvider : IMetadataProvider
    {
        public IMetadataObject CreateNew(CloudsdaleModel model)
        {
            return new UserStatusMetadata(model);
        }

        public class UserStatusMetadata : IMetadataObject, INotifyPropertyChanged
        {
            private CloudController controller;
            public UserStatusMetadata(CloudsdaleModel model)
            {
                Model = model;
                CorrectController();

                if (Model is Session)
                {
                    Model.PropertyChanged += ControllerOnPropertyChanged;
                }
            }

            void CorrectController()
            {

                if (controller == MainWindow.MainApp.MessageController.CurrentCloud) return;
                if (controller != null)
                {
                    controller.PropertyChanged -= ControllerOnPropertyChanged;
                }
                controller = MainWindow.MainApp.MessageController.CurrentCloud;
                controller.PropertyChanged += ControllerOnPropertyChanged;
            }

            private void ControllerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
            {
                OnPropertyChanged("Value");
            }

            public object Value
            {
                get
                {
                    if (Model is Session)
                    {
                        return (Model as Session).PreferredStatus;
                    }
                    return MainWindow.MainApp.MessageController.CurrentCloud.StatusForUser(((User)Model).Id);
                }
                set { throw new NotSupportedException(); }
            }

            public CloudsdaleModel Model { get; private set; }
            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class UserOnlineMetadataProvider : IMetadataProvider
    {
        public IMetadataObject CreateNew(CloudsdaleModel model)
        {
            return new UserOnlineMetadata(model);
        }

        public class UserOnlineMetadata : IMetadataObject, INotifyPropertyChanged
        {
            private CloudController controller;
            public UserOnlineMetadata(CloudsdaleModel model)
            {
                Model = model;
                CorrectController();
            }

            void CorrectController()
            {
                if (controller == MainWindow.MainApp.MessageController.CurrentCloud) return;
                if (controller != null)
                {

                }
                controller = MainWindow.MainApp.MessageController.CurrentCloud;
                controller.PropertyChanged += ControllerOnPropertyChanged;
            }

            private void ControllerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
            {
                OnPropertyChanged("Value");
            }

            public object Value
            {
                get { return (Status)Model.UIMetadata["Status"].Value != Status.Offline; }
                set { throw new NotSupportedException(); }
            }

            public CloudsdaleModel Model { get; private set; }
            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
