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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Kbase.LibraryWrap
{
    /// <summary>
    /// This class is a dummy because we don't have the external logging libraries
    /// for this release.
    /// </summary>
    class Logger
    {
        internal static void Init() {
        }

        internal static void ShutDown()
        {
        }

        internal static void Log(string text)
        {
            Debug.WriteLine(text);
        }

        internal static void Log(Exception e)
        {
            Log(e.Message);
        }


        internal static void LogTimer(DateTime marker, string text)
        {
            Log(text);
        }

        internal static bool IsInitialized
        {
            get
            {
                return true;
            }

        }
    }
}
