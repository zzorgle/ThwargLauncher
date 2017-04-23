﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThwargLauncher
{
    class UserAcctViewModel : PropertyChangedBase
    {
        private UserAccount _account;
        public UserAcctViewModel(UserAccount account)
        {
            _account = account;
            _account.PropertyChanged += AccountPropertyChanged;
            PersistSettings(Persistence.Load);
        }
        private void AccountPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
        public bool AccountLaunchable { get { return _account.AccountLaunchable; } set { _account.AccountLaunchable = value; } }
        public string AccountName { get { return _account.Name; } }
        public ObservableCollection<Server> Servers { get { return _account.Servers; } }
        public ObservableCollection<Server> VisibleServers { get { return _account.VisibleServers; } }
        public ObservableCollection<Server> SelectedServers { get { return _account.SelectedServers; } }
        public string Password { get { return _account.Password; } }
        public string AccountSummary { get { return _account.AccountSummary; } }
        public UserAccount Account { get { return _account; } }
        public bool IsExpanded { get; set; }
        private enum Persistence { Load, Save };
        private void PersistSettings(Persistence direction)
        {
            var settings = PersistenceHelper.SettingsFactory.Get();
            string key = AccountName + ":IsExpanded";
            if (direction == Persistence.Load)
            {
                IsExpanded = settings.GetBool(key, false);
            }
            else
            {
                settings.SetBool(key, IsExpanded);
            }
        }
        public void SaveSettings()
        {
            PersistSettings(Persistence.Save);
        }
    }
}
