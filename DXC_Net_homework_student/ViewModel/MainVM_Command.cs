using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXC_Net_homework_student
{
    internal partial class MainViewModel
    {
        public ICommand ShowTeacherWindowCommand
        {
            get { return _showTeacherInfoCommand; }
            set { _showTeacherInfoCommand = value; }
        }


        public ICommand GoToPhotoWindowsCommand { 
        
            get { return _gotoPhotoCommand; }
            set { _gotoPhotoCommand = value; }
        
        }
        public ICommand ClassScoreDetailCommand
        {
            get { return _classScoreDetailCommand; }
            set { _classScoreDetailCommand = value; }
        }
        public ICommand LoadToExcelCommand {
            get { return _loadToExcelCommand; }
            set { _loadToExcelCommand = value; }
        }
        public ICommand SearchStuCommand
        {
            get { return _searchStuCommand; }
            set { _searchStuCommand = value; }
        }
        public ICommand SelectSuggestedIdCommand
        {
            get { return _selectSuggestedIdCommand; }
            set { _selectSuggestedIdCommand = value; }
        }

        public ICommand SelectSuggestedNameCommand
        {
            get { return _selectSuggestedNameCommand; }
            set { _selectSuggestedNameCommand = value; }
        }
        public ICommand SelectCommand
        {
            get { return _selectCommand; }
            set { _selectCommand = value; }
        }

        public ICommand SelectAllCommand
        {
            get { return _selectAllCommand; }

            set
            {
                _selectAllCommand = value;
            }
        }


        public ICommand AddStuCommand
        {
            get { return _addStuCommand; }
            set
            {
                _addStuCommand = value;
            }
        }
        public ICommand DeleteStuCommand
        {
            get { return _deleteStuCommand; }
            set
            {
                _deleteStuCommand = value;
            }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshStuCommand; }
            set
            {
                _refreshStuCommand = value;
            }
        }



        public ICommand UpdateStuCommand
        {
            get { return _updateStuCommand; }
            set
            {
                _updateStuCommand = value;
            }
        }
    }
}


