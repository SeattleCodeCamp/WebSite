#region License, Terms and Author(s)
//
// Orlando Code Camp for Windows Phone 7
// Copyright (c) 2012 Orlando .Net User Group. All rights reserved.
//
//  Author(s):
//
//      Brian Mishler, http://www.qualitydata.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.Windows.Navigation;
using OCC.WindowsPhone.ViewModels;
using OCC.WindowsPhone.Extensions;
namespace OCC.WindowsPhone.Views
{
    public partial class AnnouncementPage
    {
        bool isNewInstance;
        AnnouncementViewModel viewModel;

        public AnnouncementPage()
        {
            InitializeComponent();
            isNewInstance = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (isNewInstance)
            {
                if (viewModel == null)
                {
                    if (State.Count > 0)
                    {
                        viewModel = (AnnouncementViewModel)State["viewModel"];
                    }
                    else
                    {
                        viewModel = new AnnouncementViewModel();
                        viewModel.Load(this.QueryStringInt("announcementid"));
                    }
                    DataContext = viewModel;
                }
            }
            isNewInstance = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back)
            {
                //State["viewModel"] = viewModel;

            }
        }


    }
}