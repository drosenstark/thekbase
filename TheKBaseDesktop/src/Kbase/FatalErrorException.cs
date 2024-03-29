/*
This file is part of TheKBase Desktop
A Multi-Hierarchical  Information Manager
Copyright (C) 2004-2006 Daniel Rosenstark

TheKBase Desktop is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
("GPL") version 2 as published by the Free Software Foundation.
See the file LICENSE.TXT for the full text of the GNU GPL, or see
http://www.gnu.org/licenses/gpl.txt

For using TheKBase Desktop with software that can not be combined with 
the GNU GPL or any other queries, please contact Daniel Rosenstark 
(license@thekbase.com).
*/
using System;
using System.Threading;

namespace Kbase
{
    /// <summary>
    /// .NET has no checked exceptions, which is cool, but in a UI based app
    /// you have to know if you want to bomb or not.
    /// </summary>
	public class FatalErrorException : Exception
	{
		public FatalErrorException(string msg, Exception wrapped) : base(msg, wrapped) {
		}

        public FatalErrorException(string msg) : base(msg)
        {
        }

	}
}
