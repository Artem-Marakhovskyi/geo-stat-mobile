using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using GeoStat.Common.Services;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;


namespace GeoStat.Common.ViewModels
{
    public class GroupViewModel : MvxViewModel<GroupModel>
    {
        private readonly GroupService _groupService;
        private readonly IMvxNavigationService _navigationService;

        public GroupModel CurrentGroup { get; set; }
        public IEnumerable<UserModel> GroupMembers { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public GroupViewModel(GroupService groupService,
                              IMvxNavigationService navigationService)
        {
            _groupService = groupService;
            _navigationService = navigationService;
        }

        public async override void Prepare(GroupModel group)
        {
            CurrentGroup = group;
            Title = CurrentGroup.Label;

            //GroupMembers = await _groupService.GetUsersOfGroupAsync(CurrentGroup.Id);
            GroupMembers = new List<UserModel>
            {
                new UserModel{Email = "user1@mail.com"},
                new UserModel{Email = "user1@mail.com"},
                new UserModel{Email = "user1@mail.com"},
                new UserModel{Email = "user1@mail.com"}
            };
        }
 
        private void ShowGroupMap()
        {
            _navigationService.Navigate<GroupMapViewModel, GroupModel>(CurrentGroup);
        }
        
        public void ShowGroupUserMap(UserModel user)
        {
            //_navigationService.Navigate<UserMapViewModel>();
        }

        public IMvxCommand ShowGroupUserMapCommand => new MvxCommand<UserModel>((user) => ShowGroupUserMap(user));
        public IMvxCommand ShowGroupMapCommand => new MvxCommand(ShowGroupMap);
    }
}
