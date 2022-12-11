using BMSWPF.Model;
using BMSWPF.View;
using BMSWPF.ViewModel.Commands;
using BMSWPF.ViewModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSWPF.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();
        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");

                ClearErrors(nameof(UserName));

                var list = new[] { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"" };
                bool res = list.Any(value.Contains);
                if (res)
                {
                    AddError(nameof(userName), "Invalid User Name. It must not contain any Special character except underscore(_)");
                }
            }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set
            {
                passWord = value;
                OnPropertyChanged("PassWord");
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set
            {
                warning = value;
                OnPropertyChanged("Warning");
            }
        }


        public LoginCommand loginCommand { get; set; }
       // public SignupCommand SignupCommand { get; set; }



        public LoginViewModel()
        {
            loginCommand = new LoginCommand(this);
          //  SignupCommand = new SignupCommand(this);
        }

        public async void MakeQuery()
        {
            //validation
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
                return;
            try
            {
                string agent = await LoginHelper.LoginAgent(new LoginData { UserName = UserName, Password = PassWord });

                if (agent == "User")
                {
                    //For User Login
                    GlobalVariables.USERNAME = UserName;
                    UserDashboard dashboard = new UserDashboard();
                    dashboard.Show(); 

                }
                else if (agent == "Admin")
                {
                    //AdminDashboardWindow obj = new AdminDashboardWindow();
                    //obj.Show();
                    //LoginWindow obj1 = new LoginWindow();
                    //obj1.Close();
                }
                else
                {
                    Warning = "Incorrect Username/Password";
                }
            }
            catch (Exception)
            {
                Warning = "Incorrect Data";
            }

        }

        //public void OpenSignupWindow()
        //{
        //    SignupWindow signupWindow = new SignupWindow();
        //    signupWindow.ShowDialog();
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        //Error Handling
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool HasErrors => propertyErrors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyErrors.GetValueOrDefault(propertyName, new List<string>());
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, new List<string>());
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertName));
        }

        private void ClearErrors(string propertyName)
        {
            //           propertyErrors.Clear();
            OnErrorsChanged(propertyName);
        }

        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
