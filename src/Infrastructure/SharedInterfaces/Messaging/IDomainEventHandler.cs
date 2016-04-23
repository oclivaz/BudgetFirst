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
namespace BudgetFirst.SharedInterfaces.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a domain event handler, intended for read side.
    /// </summary>
    /// <typeparam name="TDomainEvent">Type of domain event to handle</typeparam>
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Handle the domain event
        /// </summary>
        /// <param name="event">Event to handle</param>
        void Handle(TDomainEvent @event);
    }
}
