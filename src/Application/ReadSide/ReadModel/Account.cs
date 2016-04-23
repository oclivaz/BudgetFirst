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
// along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.ReadSide.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces.ReadModel;

    /// <summary>
    /// Account read model
    /// </summary>
    public class Account : ReadModel
    {
        /// <summary>
        /// Account Id
        /// </summary>
        private Guid id;

        /// <summary>
        /// Account name
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets account Id
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }
        
        /// <summary>
        /// Gets or sets the account name
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }
    }
}
