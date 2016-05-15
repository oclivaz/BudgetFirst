﻿// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using ReadSide.ReadModel;
    using Repository;

    /// <summary>
    /// Account list view model
    /// </summary>
    [ComVisible(false)]
    public class AccountListViewModel : ListViewModel<AccountList, AccountListItem, AccountListItemViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AccountListViewModel"/> class.
        /// </summary>
        /// <param name="listReadModel">List read model</param>
        /// <param name="viewModelRepository">List item view model repository</param>
        public AccountListViewModel(AccountList listReadModel, IViewModelRepository<AccountListItem, AccountListItemViewModel> viewModelRepository) : base(listReadModel, viewModelRepository)
        {
        }
    }
}
